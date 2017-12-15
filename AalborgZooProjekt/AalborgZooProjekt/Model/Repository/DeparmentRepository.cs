using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model.Database;

namespace AalborgZooProjekt.Model
{
    public class DeparmentRepository : IDepartmentRepository
    {
        public List<Department> GetDepartments()
        {
            List<Department> _depList = new List<Department>();
            using (var db = new AalborgZooContainer1())
            {
                foreach (Department dep in db.DepartmentSet.Include("ZooKeepers"))
                {
                    _depList.Add(dep);
                }
            }
            return _depList;
        }

    }
}
