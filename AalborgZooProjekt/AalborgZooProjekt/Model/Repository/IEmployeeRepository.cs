using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int employeeID);
        Employee AddEmployee(Employee employee);
        List<Zookeeper> GetAllZookeepers();
    }
}
