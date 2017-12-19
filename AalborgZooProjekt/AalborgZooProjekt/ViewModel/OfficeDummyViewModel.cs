using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.View;
using AalborgZooProjekt.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp;
using AalborgZooProjekt.Model.Repository;
using System.Windows.Forms;

namespace AalborgZooProjekt.ViewModel
{
    public class OfficeDummyViewModel
    {
        //TODO Vareliste som popup
        //TODO Når der trykkes bestilt collapser menuen

        public List<Model.OrderLine> OrderList { get; set; } = new List<Model.OrderLine>();
        //public List<DummyOrderDepartment> DepartmentListApples { get; set; } = new List<DummyOrderDepartment>();

        public OfficeDummyViewModel()
        {
            //string fileAndPath = "../../DummyOrders.txt";
            //string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            //foreach (string product in lines)
            //{
            using (var db = new Model.AalborgZooContainer1())
            {
                //foreach (OrderLine ol in db.OrderLineSet)
                //{
                ////ol.Quantity
                //OrderList.Add(new OrderLine()
                //{
                //    Order = ol.Order,                        
                //    OrderId = ol.OrderId,
                //    ProductVersion = ol.ProductVersion,
                //    ProductVersionId = ol.ProductVersionId,
                //    Quantity = ol.Quantity,
                //    UnitID = ol.UnitID                                                   
                //    });
                OrderList = db.OrderLineSet.Include("ProductVersion.Product").ToList();

            }
            //  string[] words = product.Split(' ');
            //Queue<string> wordQueue = new Queue<string>(words);

            //string word1 = wordQueue.Dequeue();

            //OrderList.Add(new OrderLine() { Order = new Order() { Note =  } });

            //while (wordQueue.Count > 1)
            //{
            //word1 = wordQueue.Dequeue();
            //string word2 = wordQueue.Dequeue();

            // OrderList.Last().OrderLines.Add(new OrderLine() {ProductVersion = new ProductVersion() { Product = new Product() { Name = word2 } } word2, Quantity = word1});
            //}            

            //fileAndPath = "../../DummyOrdersDepartment.txt";
            //lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            //foreach (string order in lines)
            //{
            //string[] words = order.Split(' ');
            //Queue<string> wordQueue = new Queue<string>(words);
            //string department = wordQueue.Dequeue();
            //string good = wordQueue.Dequeue();

            //DummyOrderDepartment DepartmentOrder = new DummyOrderDepartment(department, good);

            //while(wordQueue.Count > 1)
            //{
            //string word1 = wordQueue.Dequeue();
            //string word2 = wordQueue.Dequeue();
            //  DepartmentOrder.Orders.Add(new OrderLine(word2, Double.Parse(word1)));
            //}
            //DepartmentListApples.Add(DepartmentOrder);

            EditOrder = new RelayCommand<object>(EditOrderWindow);
            //}

    }

        private RelayCommand _newCommand;

        public RelayCommand NewCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new RelayCommand(
                    async () => Bar()
                    ));
            }
        }

        private void Bar()
        {
            OrderRepository orderRepository = new OrderRepository();
            List<Order> AllOrders = orderRepository.GetOrdersWithNoShoppinglist();
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
                ProductVersion key = uniter.QuantityPerProduct.FirstOrDefault(x => x.Key.Id == orderLine.ProductVersion.Id).Key;
                QuantityAndUnit value = uniter.QuantityPerProduct.FirstOrDefault(x => x.Key.Id == orderLine.ProductVersion.Id).Value;

                if (key != null && value.OrderedUnit == orderLine.Unit)
                {
                    uniter.QuantityPerProduct[orderLine.ProductVersion].Quantity += orderLine.Quantity;
                } else
                {
                    uniter.QuantityPerProduct.Add(orderLine.ProductVersion, new QuantityAndUnit(orderLine.Quantity, orderLine.Unit));
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
                    orders.QuantityPerProduct.ElementAt(i).Key.Product.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(nameX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).Value.Quantity.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(quantityX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                graph.DrawString(
                    orders.QuantityPerProduct.ElementAt(i).Value.OrderedUnit.Name,
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
            saveFileDialog.FileName = orders.QuantityPerProduct.ElementAt(0).Key.Supplier;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pdf.Save(saveFileDialog.FileName);
            }

            pdf.Save($"{path}{orders.QuantityPerProduct.ElementAt(0).Key.Supplier}.pdf");
            Process.Start($"{path}{orders.QuantityPerProduct.ElementAt(0).Key.Supplier}.pdf");
        }

        private RelayCommand<object> _editOrder;
        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand<object> EditOrder
        {
            get
            {
                return _editOrder;
                    //?? (_editOrder = new RelayCommand<object>
                    //(
                    //    () =>
                    //    {
                    //        EditOrderWindow();
                    //    }
                    //)
                    //);
            }
            set
            {
                _editOrder = value;
            }
        }
        public bool yas(object wee)
        {
            return 5 == 5;
        }
            
        public void Yas()
        { }

        public void EditOrderWindow(object context)
        {
            OrderLine ol = context as OrderLine;            
            OfficeFeedTypeOrders orders = new OfficeFeedTypeOrders();
            orders.dgFoodList.DataContext = ol;          
            orders.ShowDialog();
        }

    }
}
