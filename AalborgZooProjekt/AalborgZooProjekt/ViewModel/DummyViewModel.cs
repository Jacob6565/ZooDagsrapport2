using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using System.IO;

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

        public DummyViewModel()
        {
            string[] lines = File.ReadAllLines("../../DummyProducts.txt");
            foreach (string product in lines)
            {
                _dummyFood.Add(new DummyProduct() { Name = product });
            }
        }
    }
}