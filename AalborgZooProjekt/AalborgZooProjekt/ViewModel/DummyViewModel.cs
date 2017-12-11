using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Database;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Linq;

namespace AalborgZooProjekt
{
    public class DummyViewModel : ViewModelBase
    {
        string connectionString1 = "name=AalborgZooMockContainer";
        string connectionString = "name=AalborgZooContainer1";
        public List<DummyProduct> DummyFoodList { get; set; } = new List<DummyProduct>();

        public List<DummyHistoryEntry> DummyHistoryList { get; set; } = new List<DummyHistoryEntry>();

        public List<Employee> MockTestEmployee
        {
            get
            {
                using (var db = new AalborgZooContainer1(connectionString))
                {
                    return db.EmployeeSet.Select(x => x).ToList();                    
                }
            }
            set
            {
                using (var db = new AalborgZooContainer1(connectionString))
                {
                    Employee emp = db.EmployeeSet.Where(x => x.Name == "Hans").First();
                    emp.Name = value.ToString();
                    db.SaveChanges();
                }
            }
        }

        public DummyViewModel()
        {
            //using (var db = new AalborgZooContainer1(connectionString))
            //{
            //    Employee emp = new Employee() { Name = "HANS", DateHired = "000", DateStopped = "9/11" };
            //    db.EmployeeSet.Add(emp);
            //    db.SaveChanges();
            //}
            string fileAndPath = "../../DummyProducts.txt";
            string[] lines = File.ReadAllLines(fileAndPath, Encoding.UTF7);
            foreach (string product in lines)
            {
                DummyFoodList.Add(new DummyProduct(product));
            }
            fileAndPath = "../../DummyHistoryEntries.txt";
            lines = File.ReadAllLines(fileAndPath);
            foreach (string orders in lines)
            {
                DummyHistoryList.Add(new DummyHistoryEntry(orders));
            }          
        }
    }
}
