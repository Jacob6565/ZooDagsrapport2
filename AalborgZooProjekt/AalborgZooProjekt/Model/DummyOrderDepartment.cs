using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AalborgZooProjekt.Database;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.DummyModel
{
    public class DummyOrderDepartment
    {
        public DummyOrderDepartment(string department, string good)
        {
            Department = department;
            Good = good;
        }
        public string Department { get; set; }
        public string Good { get; set; }
        public List<OrderLine> Orders { get; set; } = new List<OrderLine>();
    }
}
