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

        public OfficeViewModel()
        {
            using (var db = new Model.AalborgZooContainer())
            {
                OrderList = db.OrderLineSet.Include("ProductVersion.Product").ToList();
            }
        }

        private RelayCommand _makePdfCommand;
        public RelayCommand MakePdfCommand
        {
            get
            {
                return _makePdfCommand ?? (_makePdfCommand = new RelayCommand(
                    async () => PDFGenerator() 
                    ));
            }
        }        

        private void PDFGenerator()
        {
            OrderRepository orderRepository = new OrderRepository();
            List<Order> AllOrders = orderRepository.GetOrdersWithNoShoppinglist();

            if (AllOrders.Count == 0)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("There is no new orders?", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {                    
                    return;
                }
            }

            ShoppingList list = orderRepository.AddToShoppingList(AllOrders, 1);
            List<OrderLine> AllOrderLines = new List<OrderLine>();
            
            foreach (Order order in AllOrders)
            {
                AllOrderLines.AddRange(order.OrderLines);
            }

            List<List<OrderLine>> OrderLinesBySupplier = AllOrderLines
                .GroupBy(x => x.ProductVersion.Supplier)
                .Select(grp => grp.ToList())
                .ToList();

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
                

                uniter.Sort();

                if (key != null && key.OrderedUnit == orderLine.Unit)
                {
                    key.Quantity += orderLine.Quantity;
                } else
                {
                    uniter.QuantityPerProduct.Add(new OrderWrapper(orderLine.Quantity, orderLine.Unit, orderLine.ProductVersion));
                }
            }

            return uniter;
        }

        private void CreatePDF(OrderlineUniter orders)
        {
            string danishAlphabet = "abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ";
            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont fontHeader = new XFont("Verdana", 13, XFontStyle.Bold);
            XFont fontParagraph = new XFont("Verdana", 13, XFontStyle.Regular);
            XSize size = PageSizeConverter.ToSize(PdfSharp.PageSize.A4);
            int marginTop = 20;
            int marginLeft = 50;
            int marginRight = 50;

            double lineHeight = graph.MeasureString(danishAlphabet, fontParagraph).Height + 5;

            //Draw the headlines.
            string nameString = "Produkt";
            string quantityString = "Antal";
            string unitString = "Enhed";

            double nameLength = graph.MeasureString(nameString, fontHeader).Width;
            double quantityLength = graph.MeasureString(quantityString, fontHeader).Width;
            double unitLength = graph.MeasureString(unitString, fontHeader).Width;

            double nameX = marginLeft;
            double quantityX = size.Width - nameLength - unitLength - marginRight;
            double unitX = size.Width - unitLength - marginRight;

            graph.DrawString(
                nameString,
                fontHeader,
                XBrushes.Black,
                new XRect(nameX, marginTop, pdfPage.Width.Point, pdfPage.Height.Point),
                XStringFormats.TopLeft);

            graph.DrawString(
                quantityString,
                fontHeader,
                XBrushes.Black,
                new XRect(quantityX, marginTop, pdfPage.Width.Point, pdfPage.Height.Point),
                XStringFormats.TopLeft);

            graph.DrawString(
                unitString,
                fontHeader,
                XBrushes.Black,
                new XRect(unitX, marginTop, pdfPage.Width.Point, pdfPage.Height.Point),
                XStringFormats.TopLeft);

            //Draw entries
            for (int i = 0; i < orders.QuantityPerProduct.Count; i++)
            {
                double lineY = lineHeight * (i + 1);
                if (i % 2 == 1)
                {
                    XSolidBrush brush = new XSolidBrush(XColors.LightGray);

                    graph.DrawRectangle(brush, marginLeft, lineY + marginTop, size.Width - marginLeft - marginRight, lineHeight - 2);

                }

                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).ProdV.Product.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(nameX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).Quantity.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(quantityX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).OrderedUnit.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(unitX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Title = "Save PDF";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = orders.QuantityPerProduct.ElementAt(0).ProdV.Supplier;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pdf.Save(saveFileDialog.FileName);
            }
            Process.Start(saveFileDialog.FileName);
        }

        private RelayCommand<object> _editOrder;    
        public RelayCommand<object> EditOrder
        { 
            get
            {
                return _editOrder ?? (_editOrder= new RelayCommand<object>(EditOrderWindow));
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
