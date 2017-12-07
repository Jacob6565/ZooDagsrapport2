using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyOrder
    {
        public DummyOrder(string order)
        {
            Name = order;
        }

        public string Name;
        public int Amount = 0;
    }
}
