using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using AalborgZooProjekt.Database;
using System.Linq;

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

        public List<DummyOrder> DummyOrderList { get; set; } = new List<DummyOrder>();

        public DummyViewModel()
        {
            using (var db = new AalborgZooContainer1())
            {
                string name;
                //The following presents two ways to read the name property of the employee with id 1.
                //First
                Employee em = db.EmployeeSet.Where(x => x.Id == 1).First();
                name = em.Name;

                //Second - oneliner
                name = db.EmployeeSet.Where(x => x.Id == 1).Select(x => x.Name).First();
            }

            string[] lines = File.ReadAllLines("../../DummyProducts.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                DummyFoodList.Add(new DummyProduct(product));
            }
            lines = File.ReadAllLines("../../DummyOrders.txt");
            foreach (string orders in lines)
            {
                DummyOrderList.Add(new DummyOrder(orders));
            }


        }
    }
}