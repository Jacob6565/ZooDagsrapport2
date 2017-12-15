using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;
using AalborgZooProjekt.ViewModel;

namespace AalborgZooProjekt
{
    public class Owner
    {
        public int ID;
        public string Name;

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class House
    {
        public int ID;
        public Owner HouseOwner;
    }

    public class DummyViewModel : ViewModelBase
    {
        public List<DummyProduct> DummyFoodList { get; set; } = new List<DummyProduct>();
        private List<Model.DummyProduct> _dummyFruit = new List<Model.DummyProduct>();
        public List<Model.DummyProduct> DummyFruitList
        {
            get { return _dummyFruit; }
            set { _dummyFruit = value; }
        }

        private List<Model.DummyProduct> _dummyOtherFood = new List<Model.DummyProduct>();
        public List<Model.DummyProduct> DummyOtherFoodList
        {
            get { return _dummyOtherFood; }
            set { _dummyOtherFood = value; }
        }

        public List<Model.DummyOrder> DummyHistoryList { get; set; } = new List<Model.DummyOrder>();

        ObservableCollection<Unit> DummyUnitList = new ObservableCollection<Unit>();


        public List<Employee> MockTestEmployee
        {
            get
            {
                using (var db = new AalborgZooContainer1())
                {
                    return db.EmployeeSet.Select(x => x).ToList();                    
                }
            }
            set
            {
                using (var db = new AalborgZooContainer1())
                {
                    Employee emp = db.EmployeeSet.Where(x => x.Name == "Hans").First();
                    emp.Name = value.ToString();
                    db.SaveChanges();
                }
            }
        }

        public DummyViewModel()
        {
            DummyUnitList.Add(new Unit() { Name = "kg" });
            DummyUnitList.Add(new Unit() { Name = "styks" });
            DummyUnitList.Add(new Unit() { Name = "kasse(r)" });

            //if (!new AalborgZooContainer1().OrderLineSet.Any(x => true))
            //    Populater.PopulateDatabase();

            string[] lines = File.ReadAllLines("../../Model/DummyStuff/DummyFruit.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                Model.DummyProduct dummyProduct = new Model.DummyProduct(product);
                dummyProduct.Units = DummyUnitList;
                DummyFruitList.Add(dummyProduct);
            }

            lines = File.ReadAllLines("../../Model/DummyStuff/DummyOtherFood.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                Model.DummyProduct dummyProduct = new Model.DummyProduct(product);
                dummyProduct.Units = DummyUnitList;
                DummyOtherFoodList.Add(dummyProduct);
            }

            lines = File.ReadAllLines("../../Model/DummyStuff/DummyHistoryEntries.txt");
            foreach (string orders in lines)
            {
                DummyHistoryList.Add(new DummyOrder(orders));
            }          
        }
    }
}
