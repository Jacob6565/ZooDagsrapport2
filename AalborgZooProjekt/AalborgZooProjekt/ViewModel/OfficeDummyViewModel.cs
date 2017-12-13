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

namespace AalborgZooProjekt.ViewModel
{
    public class OfficeDummyViewModel
    {
        //TODO Vareliste som popup
        //TODO Når der trykkes bestilt collapser menuen

        string connectionString = "name=AalborgZooContainer1";

        public List<OrderLine> OrderList { get; set; } = new List<OrderLine>();
        //public List<DummyOrderDepartment> DepartmentListApples { get; set; } = new List<DummyOrderDepartment>();

        public OfficeDummyViewModel()
        {
            //string fileAndPath = "../../DummyOrders.txt";
            //string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            //foreach (string product in lines)
            //{
            using (var db = new AalborgZooContainer1(connectionString))
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
                    async () => Foo()
                    ));
            }
        }

        private void Foo()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ";
            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont fontHeader = new XFont("Verdana", 13, XFontStyle.Bold);
            XFont fontParagraph = new XFont("Verdana", 13, XFontStyle.Regular);
            XSize size = PageSizeConverter.ToSize(PdfSharp.PageSize.A4);
            int marginTop = 20;
            int marginLeft = 50;
            int marginRight = 50;

            double lineHeight = graph.MeasureString(alphabet, fontParagraph).Height + 5;

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
            for (int i = 0; i < OrderList.Count; i++)
            {
                double lineY = lineHeight * (i + 1);
                if (i % 2 == 1)
                {
                    XSolidBrush brush = new XSolidBrush(XColors.LightGray);
                    
                    graph.DrawRectangle(brush, marginLeft, lineY + marginTop, size.Width - marginLeft - marginRight, lineHeight - 2);

                }
                
                graph.DrawString(
                    OrderList[i].ProductVersion.Product.Name,
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(nameX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);
                
                graph.DrawString(
                    OrderList[i].Quantity.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(quantityX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);
                
                graph.DrawString(
                    "kg", //Selected unit value
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(unitX, marginTop + lineY, pdfPage.Width, pdfPage.Height),
                    XStringFormats.TopLeft);
            }
            
            pdf.Save("C:\\Users\\Tobias\\Desktop\\firstpage.pdf");
            Process.Start("C:\\Users\\Tobias\\Desktop\\firstpage.pdf");
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
            //orders.dgFoodList. = ol;          
            orders.ShowDialog();
        }

    }
}
