using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyOrderLine
    {
        public int Quantity { get; set; }
        public string UnitID { get; set; }
        public int ProductVersionID {get;set;}
        public int OrderID { get; set; }
    }
}
