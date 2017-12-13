using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public partial class Order
    {
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public Product Product { get; set; }
    }

}
