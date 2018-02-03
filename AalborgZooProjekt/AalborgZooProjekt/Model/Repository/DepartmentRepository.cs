using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DeparmentRepository : IDepartmentRepository
    {
        public List<Department> GetDepartments()
        {
            List<Department> _depList = new List<Department>();
            using (var db = new AalborgZooContainer())
            {
                foreach (Department dep in db.DepartmentSet.Include("ZooKeepers"))
                {
                    _depList.Add(dep);
                }
            }
            return _depList;
        }

        public Department AddDepartment(Department department)
        {
            using (var db = new AalborgZooContainer())
            {
                Department departmentWithID = db.DepartmentSet.Add(department);
                db.SaveChanges();

                return departmentWithID;
            }
        }
    }
}
