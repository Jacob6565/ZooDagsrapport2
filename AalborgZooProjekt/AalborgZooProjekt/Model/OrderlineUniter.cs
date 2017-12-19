using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class OrderlineUniter
    {
        public SortedDictionary<ProductVersion, QuantityAndUnit> QuantityPerProduct = new SortedDictionary<ProductVersion, QuantityAndUnit>();

        public void Sort()
        {
            QuantityPerProduct = (SortedDictionary<ProductVersion, QuantityAndUnit>) QuantityPerProduct.OrderBy(x => x.Key);
        }
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
