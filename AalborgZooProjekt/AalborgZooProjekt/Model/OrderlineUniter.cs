using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class OrderlineUniter
    {
        public Dictionary<ProductVersion, QuantityAndUnit> QuantityPerProduct = new Dictionary<ProductVersion, QuantityAndUnit>();
    }

    class QuantityAndUnit
    {
        public QuantityAndUnit(int quantity, Unit unit)
        {
            Quantity = quantity;
            OrderedUnit = unit;
        }

        public int Quantity;
        public Unit OrderedUnit;
    }
}
