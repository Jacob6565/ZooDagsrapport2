using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    interface IRepository
    {
        void AddOrder(Order order);
        Order GetOrder(int orderID);
        void UpdateOrder(Order order);
    }
}
