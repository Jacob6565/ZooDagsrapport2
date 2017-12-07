using AalborgZooProjekt.Interfaces;
using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public partial class Shopper : IShopper
    {
        public void CheckIfOrderExist(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public int GetID()
        {
            return this.Id;
        }

        public void MakeProduct()
        {
            throw new NotImplementedException();
        }
    }
}
