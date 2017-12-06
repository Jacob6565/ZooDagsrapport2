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

        public List<DummyProduct> DummyFoodList { get; set; } = new List<DummyProduct>();

        public List<DummyHistoryEntry> DummyHistoryList { get; set; } = new List<DummyHistoryEntry>();

        public List<Employee> MockTestEmployee
        {
            get
            {
                using (var db = new AalborgZooMock())
                {
                    return db.EmployeeSet.Select(x => x).ToList();
                    
                }
            }
        }

        public DummyViewModel()
        {
            using (var db = new AalborgZooMock())
            {
                Employee emp = new Employee() { Name = "HANS", DateHired = "000", DateStopped = "9/11" };
                db.EmployeeSet.Add(emp);
                db.SaveChanges();
            }
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
