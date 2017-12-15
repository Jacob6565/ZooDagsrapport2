using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    public interface IShoppingList
    {
        //AddOrder is needed in Order
        void AddOrder(Order order);
    }
}
