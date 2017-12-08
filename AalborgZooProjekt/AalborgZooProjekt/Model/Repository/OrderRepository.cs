using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            using (var db = new AalborgZooContainer1())
            {
                db.OrderSet.Add(order);
                db.SaveChanges();
            }
        }

        public Order GetOrder(int orderID)
        {
            using (var db = new AalborgZooContainer1())
            {
                var result = db.OrderSet.SingleOrDefault(b => b.Id == orderID);

                return result;
            }
        }


        public void UpdateOrder(Order order)
        {
            using (var db = new AalborgZooContainer1())
            {
                var result = db.OrderSet.SingleOrDefault(b => b.Id == order.Id);
                if (result != null)
                {
                    result = order;
                    db.SaveChanges();
                }
            }
        }

    }
}
