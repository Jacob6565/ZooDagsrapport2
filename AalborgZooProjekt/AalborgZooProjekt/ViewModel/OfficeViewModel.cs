using AalborgZooProjekt.Model;
using System.Collections.Generic;
using System.Linq;
using AalborgZooProjekt.View;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp;
using System.Windows.Forms;

namespace AalborgZooProjekt.ViewModel
{
    public class OfficeViewModel
    {
        public List<Model.OrderLine> OrderList { get; set; } = new List<Model.OrderLine>();

        private IProductRepository productRepository;
        ReadFromExcelFile readFromExcelFile;
        public OfficeViewModel()
        {
            //So it can load in all the products from the database.
            productRepository = new ProductRepository();

            using (var db = new Model.AalborgZooContainer())
            {
                OrderList = db.OrderLineSet.Include("ProductVersion.Product").ToList();
            }
            readFromExcelFile = new ReadFromExcelFile();

            AllProducts = productRepository.GetAllProducts();
        }

        private RelayCommand _makePdfCommand;
        public RelayCommand MakePdfCommand
        {
            get
            {
                return _makePdfCommand ?? (_makePdfCommand = new RelayCommand(
                    () => PDFGenerator()
                    ));
            }
        }

        private RelayCommand _readProductsCommand;
        public RelayCommand ReadProductsCommand
        {
            get
            {
                return _readProductsCommand ?? (_readProductsCommand = new RelayCommand(
                    () => ReadProductsFromExcelFile()
                    ));
            }
        }

        private RelayCommand _editProductsCommand;
        public RelayCommand EditProductsCommand
        {
            get
            {
                return _editProductsCommand ?? (_editProductsCommand = new RelayCommand(
                    () => EditProduct()
                    ));
            }
        }

        public void EditProduct()
        {
            throw new System.NotImplementedException();
        }
       

        public List<Product> AllProducts = new List<Product>();

        /// <summary>
        /// Loads in an excelfile and reads the products contained in it.
        /// </summary>
        private void ReadProductsFromExcelFile()
        {
            //Opens the dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            string path = "";

            //Gets the path from where the file was located
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               path = openFileDialog.FileName;
            }
            else
            {
                return;
            }


            //Calls the read function
            var products = readFromExcelFile.GetProductsFromExcelIntoDatabase(path);

            //Assigns the result to a list, so we can acces the instances of them.
            AllProducts = products;
        }


        private void PDFGenerator()
        {
            //Gets all relevant orders, i.e. orders without a linked shoppinglist.
            OrderRepository orderRepository = new OrderRepository();
            List<Order> AllOrders = orderRepository.GetOrdersWithNoShoppinglist();

            //In case of no orders we notify the user and return.
            if (AllOrders.Count == 0)
            {
                System.Windows.MessageBox.Show("Der er ingen nye ordrer.", "Bekræftelse", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            //Adds all orders to a new shoppinglist.
            ShoppingList list = orderRepository.AddToShoppingList(AllOrders, 1);
            List<OrderLine> AllOrderLines = new List<OrderLine>();

            //Adds all orderlines from all orders to one list of orderlines - this makes it easier to sum up the orderlines and sort the list.
            foreach (Order order in AllOrders)
            {
                AllOrderLines.AddRange(order.OrderLines);
            }

            //Groups the orderlines by supplier, so every supplier gets their own PDF of orders.
            List<List<OrderLine>> OrderLinesBySupplier = AllOrderLines
                .GroupBy(x => x.ProductVersion.Supplier)
                .Select(grp => grp.ToList())
                .ToList();

            //Makes a PDF for every supplier.
            foreach (List<OrderLine> orderlinesForOneSupplier in OrderLinesBySupplier)
            {
                CreatePDF(UniteOrderlines(orderlinesForOneSupplier));
            }
        }

        private OrderlineUniter UniteOrderlines(List<OrderLine> unSortedOrderlines)
        {
            OrderlineUniter uniter = new OrderlineUniter();

            foreach (OrderLine orderLine in unSortedOrderlines)
            {
                OrderWrapper key = uniter.QuantityPerProduct.FirstOrDefault(x => x.ProdV.Id == orderLine.ProductVersion.Id);


                if (key != null && key.OrderedUnit == orderLine.Unit)
                {
                    //The orderline already exists in the list, so we simply add the quantity of the current orderline.
                    key.Quantity += orderLine.Quantity;
                }
                else
                {
                    //The orderlines does not exsist, so we add a new one to the list.
                    uniter.QuantityPerProduct.Add(new OrderWrapper(orderLine.Quantity, orderLine.Unit, orderLine.ProductVersion));
                }
            }
            //Sorts the orderlines by name. Orderlines with the same name but different units will be two different entries, but grouped together.
            uniter.Sort();

            return uniter;
        }

        private void CreatePDF(OrderlineUniter orders)
        {
            string danishAlphabet = "abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ"; //Used to get the height ratio.
            //PDF Sharp initializations.
            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont fontHeader = new XFont("Verdana", 13, XFontStyle.Bold);
            XFont fontParagraph = new XFont("Verdana", 13, XFontStyle.Regular);
            XSize size = PageSizeConverter.ToSize(PdfSharp.PageSize.A4);

            //Margins
            int marginTop = 20;
            int marginLeft = 50;
            int marginRight = 50;

            //Line height
            double lineHeight = graph.MeasureString(danishAlphabet, fontParagraph).Height + 5;

            //Calculates the headline positions.
            string nameString = "Produkt";
            double nameLength = graph.MeasureString(nameString, fontHeader).Width;
            double nameX = marginLeft;

            string unitString = "Enhed";
            double unitLength = graph.MeasureString(unitString, fontHeader).Width;
            double unitX = size.Width - unitLength - marginRight;

            string quantityString = "Antal";
            double quantityLength = graph.MeasureString(quantityString, fontHeader).Width;
            double quantityX = size.Width - nameLength - unitLength - marginRight;

            //Draws the product name headline.
            graph.DrawString(
                nameString,
                fontHeader,
                XBrushes.Black,
                new XRect(nameX, marginTop, pdfPage.Width.Point, pdfPage.Height.Point),
                XStringFormats.TopLeft);

            //Draws the quantity headline.
            graph.DrawString(
                quantityString,
                fontHeader,
                XBrushes.Black,
                new XRect(quantityX, marginTop, pdfPage.Width.Point, pdfPage.Height.Point),
                XStringFormats.TopLeft);

            //Draws the unit headline.
            graph.DrawString(
                unitString,
                fontHeader,
                XBrushes.Black,
                new XRect(unitX, marginTop, pdfPage.Width.Point, pdfPage.Height.Point),
                XStringFormats.TopLeft);

            //Draw entries
            for (int i = 0; i < orders.QuantityPerProduct.Count; i++)
            {
                //Calculates the position of the current line.
                double lineY = lineHeight * (i + 1);

                //Gives every second line a light gray background color.
                if (i % 2 == 1)
                {
                    XSolidBrush brush = new XSolidBrush(XColors.LightGray);

                    graph.DrawRectangle(brush, marginLeft, lineY + marginTop, size.Width - marginLeft - marginRight, lineHeight - 2);

                }

                //Draws the product name.
                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).ProdV.Product.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(nameX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                //Draws the quantity.
                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).Quantity.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(quantityX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                //Draws the unit.
                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).OrderedUnit.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(unitX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);
            }

            //Code used to setup the save file dialog.
            SaveFileDialog saveFileDialog = new SaveFileDialog(); //Asks the user for the position of the code.
            saveFileDialog.Title = "Save PDF";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = orders.QuantityPerProduct.ElementAt(0).ProdV.Supplier; //File name corresponds to name of the supplier by default.

            //Shows the save file dialog.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pdf.Save(saveFileDialog.FileName);
            }

            //Opens the newly generated pdf.
            Process.Start(saveFileDialog.FileName);
        }

        private RelayCommand<object> _editOrder;
        public RelayCommand<object> EditOrder
        {
            get
            {
                return _editOrder ?? (_editOrder = new RelayCommand<object>(EditOrderWindow));
            }
            set
            {
                _editOrder = value;
            }
        }

        public void EditOrderWindow(object context)
        {
            OrderLine ol = context as OrderLine;
            OfficeFeedTypeOrders orders = new OfficeFeedTypeOrders();
            orders.dgFoodList.DataContext = ol;
            orders.ShowDialog();
        }
    }
}
