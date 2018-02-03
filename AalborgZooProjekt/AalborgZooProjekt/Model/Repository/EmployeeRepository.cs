using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.Repository
{
    public partial class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Adding an employee to the database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Employee AddEmployee(Employee employee)
        {
            using (var db = new AalborgZooContainer())
            {
                Employee employeeWithID = db.EmployeeSet.Add(employee);
                db.SaveChanges();

                return employeeWithID;
            }
        }

        /// <summary>
        /// Gets all the Zookeepers in the database.
        /// </summary>
        /// <returns></returns>
        public List<Zookeeper> GetAllZookeepers()
        {
            List<Zookeeper> AllZookeepers = new List<Zookeeper>();
            using (var db = new AalborgZooContainer())
            {
                var temp = new List<Employee>(db.EmployeeSet);
                
                foreach(Employee e in temp)
                {
                    Zookeeper tempZookeeper = e as Zookeeper;
                    if(tempZookeeper != null)
                    {
                        AllZookeepers.Add(tempZookeeper);
                    }
                }

                return AllZookeepers;
            }
        }

        public Employee GetEmployee(int employeeID)
        {
            using (var db = new AalborgZooContainer())
            {
                return db.EmployeeSet.FirstOrDefault(x => x.Id == employeeID);
            }
        }

        
    }
}
