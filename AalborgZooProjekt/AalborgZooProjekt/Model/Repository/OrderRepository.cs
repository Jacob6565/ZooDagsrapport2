using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class OrderRepository : IOrderRepository
    {
        private AalborgZooContainer1 _context;

        public OrderRepository(AalborgZooContainer1 context)
        {
            _context = context;
        }
        public OrderRepository() : this(new AalborgZooContainer1())
        {
            
        }
        /// <summary>
        /// Adds a not yet excisting order to the database
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            using (_context)
            {
                _context.OrderSet.Add(order);
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Gets an order from orderID in the database
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order GetOrder(int orderID)
        {
            using (_context)
            {
                var result = _context.OrderSet.Find(orderID);

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
            using (_context)
            {
                var result = _context.OrderSet.SingleOrDefault(b => b.Id == order.Id);
                if (result != null)
                {
                    //Updating the order
                    result = order;

                    //Calls the database to save the changes
                    _context.SaveChanges();
                }
                else if (result == null)
                    throw new OrderDoesNotExistInDatabaseException();
            }
        }
    }
}
