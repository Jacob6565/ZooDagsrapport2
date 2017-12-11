using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.DummyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.View;
using AalborgZooProjekt.Database;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;

namespace AalborgZooProjekt.ViewModel
{
    public class OfficeDummyViewModel
    {
        //TODO Vareliste som popup
        //TODO Når der trykkes bestilt collapser menuen

        string connectionString = "name=AalborgZooContainer1";

        public List<OrderLine> OrderList { get; set; } = new List<OrderLine>();
        public List<DummyOrderDepartment> DepartmentListApples { get; set; } = new List<DummyOrderDepartment>();

        public OfficeDummyViewModel()
        {
            //string fileAndPath = "../../DummyOrders.txt";
            //string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            //foreach (string product in lines)
            //{
                 using(var db = new AalborgZooContainer1(connectionString))
                {
                    foreach (OrderLine ol in db.OrderLineSet)
                    {
                    //ol.Quantity
                        OrderList.Add(ol);
                    }
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
            orders.SpecifikProductOrders.Text = ol.Order.Note;
            orders.dgFoodList.DataContext = ol;
            orders.ShowDialog();
        }

    }
}
