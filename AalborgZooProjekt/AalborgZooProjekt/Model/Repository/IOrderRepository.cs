using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public interface IOrderRepository
    {
        void AddOrder(Database.Order order);
        Database.Order GetOrder(int orderID);
        void UpdateOrder(Order order);
    }
}
