using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Database;

namespace AalborgZooProjekt.ViewModel
{

    public class DBAalborgZoo
    {
        public DBAalborgZoo()
        {
            using (AalborgZooContainer context = new AalborgZooContainer())
            {
                context.EmployeeSet.Add(new Employee() { FirstName = "Tobias", LastName = "Pallumate" });
                context.SaveChanges();
            }
        }
    }
}
