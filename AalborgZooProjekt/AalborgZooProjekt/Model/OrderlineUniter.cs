using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class OrderlineUniter
    {
        public List<OrderWrapper> QuantityPerProduct = new List<OrderWrapper>();

        public void Sort()
        {
            QuantityPerProduct = QuantityPerProduct.OrderBy(x => x.ProdV.Name).ToList();
        }
    }

    class OrderWrapper
    {
        public OrderWrapper(int quantity, Unit unit, ProductVersion prodV)
        {
            Quantity = quantity;
            OrderedUnit = unit;
            ProdV = prodV;
        }

        public ProductVersion ProdV;
        public int Quantity;
        public Unit OrderedUnit;
    }
}
