using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.Repository
{
    public partial class EmployeeRepository : IEmployeeRepository
    {
        public Employee GetEmployee(int employeeID)
        {
            using (var db = new AalborgZooContainer())
            {
                return db.EmployeeSet.FirstOrDefault(x => x.Id == employeeID);
            }
        }
    }
}
