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
        /// <summary>
        /// Adds a not yet excisting order to the database
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            using (var _context = new AalborgZooContainer1())
            {
                foreach (OrderLine orderLine in order.OrderLines)
                {
                    _context.OrderLineSet.Add(orderLine);
                }

                _context.OrderSet.Add(order);
                
                _context.SaveChanges();
            }
        }

        //public void AddOrderLine(OrderLine orderLine, Order order)
        //{
        //    using (var _context = new AalborgZooContainer1())
        //    {
        //        //Adds the orderline to orderlineset in database and updates the local version, so it also contains
        //        //the orderline key, that the database created
        //        OrderLine orderLineWithKey = _context.OrderLineSet.Add(orderLine);

        //        //Updates the Order to contain the orderLine
        //        _context.OrderSet.Find(order.Id).OrderLines.Add(orderLineWithKey);


        //        _context.SaveChanges();
        //    }
        //}


        /// <summary>
        /// Gets an order from orderID in the database
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order GetOrder(int orderID)
        {
            using (var _context = new AalborgZooContainer1())
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
            using (var _context = new AalborgZooContainer1())
            {
                var result = _context.OrderSet.SingleOrDefault(b => b.Id == order.Id);
                if (result != null)
                {
                    result.OrderLines = order.OrderLines;
                    result.DateOrdered = order.DateOrdered;
                    result.Note = order.Note;
                    result.OrderedByID = order.OrderedByID;
                    result.Status = order.Status;
                    result.ShoppingListId = order.ShoppingListId;

                    

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
