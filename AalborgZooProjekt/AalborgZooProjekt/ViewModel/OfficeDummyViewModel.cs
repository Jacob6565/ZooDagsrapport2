using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.DummyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.ViewModel
{
    class OfficeDummyViewModel
    {

        public List<DummyOrder> OrderList { get; set; } = new List<DummyOrder>();

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

                    OrderList.Last().Orders.Add(new Tuple<string, double>(word1, Double.Parse(word2)));
                }



            }
        }
    }
}
