using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.DummyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;

namespace AalborgZooProjekt.ViewModel
{
    public class OfficeDummyViewModel
    {
        public List<DummyOrder> OrderList { get; set; } = new List<DummyOrder>();
        public List<DummyOrderDepartment> DepartmentListApples { get; set; } = new List<DummyOrderDepartment>();

        public OfficeDummyViewModel()
        {
            string fileAndPath = "../../DummyOrders.txt";
            string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            foreach (string product in lines)
            {
                string[] words = product.Split(' ');
                Queue<string> wordQueue = new Queue<string>(words);

                string word1 = wordQueue.Dequeue();

                OrderList.Add(new DummyOrder(word1));
                
                while (wordQueue.Count > 1)
                {
                    word1 = wordQueue.Dequeue();
                    string word2 = wordQueue.Dequeue();

                    OrderList.Last().Orders.Add(new OrderLine(word2, Double.Parse(word1)));
                }
            }

            fileAndPath = "../../DummyOrdersDepartment.txt";
            lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            foreach (string order in lines)
            {
                string[] words = order.Split(' ');
                Queue<string> wordQueue = new Queue<string>(words);
                string department = wordQueue.Dequeue();
                string good = wordQueue.Dequeue();

                DummyOrderDepartment DepartmentOrder = new DummyOrderDepartment(department, good);

                while(wordQueue.Count > 1)
                {
                    string word1 = wordQueue.Dequeue();
                    string word2 = wordQueue.Dequeue();
                    DepartmentOrder.Orders.Add(new OrderLine(word2, Double.Parse(word1)));
                }
                DepartmentListApples.Add(DepartmentOrder);
            }
        }

    }
}
