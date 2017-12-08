using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class OrderRepository : IRepository
    {
        /// <summary>
        /// Adds a not yet excisting order to the database
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            using (var db = new AalborgZooContainer1())
            {
                db.OrderSet.Add(order);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Gets an order from orderID in the database
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order GetOrder(int orderID)
        {
            using (var db = new AalborgZooContainer1())
            {
                var result = db.OrderSet.SingleOrDefault(b => b.Id == orderID);

                if (result == null)
                    throw new OrderDoesNotExistInDatabaseException();

                return result;
            }
        }

        /// <summary>
        /// Updates an excisting order in the database
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order)
        {
            using (var db = new AalborgZooContainer1())
            {
                var result = db.OrderSet.SingleOrDefault(b => b.Id == order.Id);
                if (result != null)
                {
                    //Updating the order
                    result = order;

                    //Calls the database to save the changes
                    db.SaveChanges();
                }
                else if (result == null)
                    throw new OrderDoesNotExistInDatabaseException();
            }
        }

    }
}
