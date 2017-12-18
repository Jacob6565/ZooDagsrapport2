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

namespace AalborgZooProjekt.ViewModel
{
    public class OfficeDummyViewModel
    {
        //TODO Vareliste som popup
        //TODO Når der trykkes bestilt collapser menuen

        string connectionString = "name=AalborgZooContainer1";

        public List<Model.OrderLine> OrderList { get; set; } = new List<Model.OrderLine>();
        //public List<DummyOrderDepartment> DepartmentListApples { get; set; } = new List<DummyOrderDepartment>();

        public OfficeDummyViewModel()
        {
            //string fileAndPath = "../../DummyOrders.txt";
            //string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            //foreach (string product in lines)
            //{
            using (var db = new Model.Database.AalborgZooContainer1())
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
                CreatePDF(orderlinesForOneSupplier);
            }
        }

        private void CreatePDF(List<OrderLine> orders)
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
            for (int i = 0; i < orders.Count; i++)
            {
                double lineY = lineHeight * (i + 1);
                if (i % 2 == 1)
                {
                    XSolidBrush brush = new XSolidBrush(XColors.LightGray);

                    graph.DrawRectangle(brush, marginLeft, lineY + marginTop, size.Width - marginLeft - marginRight, lineHeight - 2);

                }

                graph.DrawString(
                    orders[i].ProductVersion.Product.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(nameX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                graph.DrawString(
                    orders[i].Quantity.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(quantityX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);

                UnitRepository unitRepository = new UnitRepository();
                Unit unit = unitRepository.GetUnitById(orders[i].UnitID);
                graph.DrawString(
                    unitRepository.GetUnitById(orders[i].UnitID).Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(unitX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);
            }

            Directory.CreateDirectory("C:\\Users\\Tobias\\Desktop\\Bestillinger");
            pdf.Save($"C:\\Users\\Tobias\\Desktop\\Bestillinger\\{orders.First().ProductVersion.Supplier}.pdf");
            Process.Start($"C:\\Users\\Tobias\\Desktop\\Bestillinger\\{orders.First().ProductVersion.Supplier}.pdf");
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
