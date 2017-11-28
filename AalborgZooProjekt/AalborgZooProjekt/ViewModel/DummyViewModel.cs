using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using System.IO;
using System.Text;

namespace AalborgZooProjekt
{
    public class DummyViewModel : ViewModelBase
    {
        private List<DummyProduct> _dummyFood = new List<DummyProduct>();

        public List<DummyProduct> DummyFoodList
        {
            get { return _dummyFood; }
            set { _dummyFood = value; }
        }

        public List<DummyHistoryEntry> DummyHistoryList { get; set; } = new List<DummyHistoryEntry>();

        public DummyViewModel()
        {
            string fileAndPath = "../../DummyHistoryEntries.txt";
            string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            foreach (string product in lines)
            {
                DummyFoodList.Add(new DummyProduct(product));
            }
            lines = File.ReadAllLines(fileAndPath);
            foreach (string orders in lines)
            {
                DummyHistoryList.Add(new DummyHistoryEntry(orders));
            }


        }
    }
}