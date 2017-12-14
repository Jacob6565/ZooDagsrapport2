using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;
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
                Order order = _context.OrderSet.Find(orderID);

                if (order == null)
                    throw new OrderDoesNotExistInDatabaseException();

                return order;
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


        /// <summary>
        /// Finds an department order that is under construction and thereby not yet sent. If no such order exist it
        /// will return null. 
        /// The function is linear and will look through all orders in database until a satisfying order is found.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Order GetUnfinishedOrder(Department department)
        {
            using (var _context = new AalborgZooContainer1())
            {
                foreach (Order order in _context.OrderSet)
                {
                    if (order.Status == order.UnderConstruction && order.DepartmentID == department.Id)
                        return order;
                }
                return null;
            }
        }
    }
}
