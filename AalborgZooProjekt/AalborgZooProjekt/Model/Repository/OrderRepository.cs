using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Order AddOrder(Order order)
        {
            using (var _context = new AalborgZooContainer())
            {
                var temp1 = order.OrderLines.First().ProductVersion.Id;
                Order tempOrder = new Order()
                {
                    OrderedByID = order.OrderedByID,
                    DateCreated = order.DateCreated,
                    DepartmentID = order.DepartmentID,
                    Status = order.Status,
                    Note = order.Note,
                    DateOrdered = order.DateOrdered,
                    Id = order.Id,
                    DateCancelled = order.DateCancelled,
                    DeletedByID = order.DeletedByID,
                    OrderLines = new Collection<OrderLine>(),
                    ShoppingList = null,
                    ShoppingListId = null
                };

                List<OrderLine> tempOrderLines = new List<OrderLine>();
                foreach (OrderLine item in order.OrderLines)
                {
                    _context.ProductVersionSet.Attach(item.ProductVersion);
                    _context.OrderLineSet.Add(item);
                    tempOrder.OrderLines.Add(item);
                }

                _context.OrderSet.Add(tempOrder);

                _context.SaveChanges();
                return tempOrder;
            }
        }


        /// <summary>
        /// Gets an order from orderID in the database
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order GetOrder(int orderID)
        {
            using (var _context = new AalborgZooContainer())
            {
                Order order = _context.OrderSet.Find(orderID);

                if (order == null)
                {
                    throw new OrderDoesNotExistInDatabaseException();
                }
                return order;
            }
        }

        /// <summary>
        /// Updates an excisting order in the database
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order)
        {
            using (var _context = new AalborgZooContainer())
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
            using (var _context = new AalborgZooContainer())
            {
                foreach (Order order in _context.OrderSet)
                {
                    if (order.Status == order.UnderConstruction && order.DepartmentID == department.Id)
                        return order;
                }
                return null;
            }
        }

        public AalborgZooContainer _contextForShopper;

        public List<Order> GetOrdersWithNoShoppinglist()
        {
            _contextForShopper = new AalborgZooContainer();
            return _contextForShopper.OrderSet
            .Include("OrderLines")
            .Include("Orderlines.ProductVersion")
            .Include("OrderLines.ProductVersion.Units")
            .Include("Orderlines.ProductVersion.Product")
            .Where(x => x.ShoppingList == null)
            .ToList();
        }

        public ShoppingList AddToShoppingList(List<Order> orders, int shopperId)
        {
            ShoppingList shoppingList = new ShoppingList();
            shoppingList.CreatedByID = shopperId;
            shoppingList.Shopper = (Shopper)_contextForShopper.EmployeeSet.FirstOrDefault(x => x is Shopper && x.Id == shopperId);
            shoppingList.DateCreated = DateTime.Now;

            shoppingList.Orders = orders;

            foreach (Order order in orders)
            {
                order.ShoppingList = shoppingList;
                _contextForShopper.Entry(order).State = System.Data.Entity.EntityState.Modified;
            }

            _contextForShopper.SaveChanges();

            _contextForShopper.ShoppingListSet.Add(shoppingList);
            _contextForShopper.Dispose();
            return shoppingList;
        }

        public List<OrderHistoryWrapper> GetOrdersFromDepartment(int departmentID)
        {
            using (var _context = new AalborgZooContainer())
            {
                List<Order> allOrders = _context.OrderSet.Where(x => x.DepartmentID == departmentID).Take(10).ToList();
                List<OrderHistoryWrapper> wrappers = new List<OrderHistoryWrapper>();

                foreach (Order order in allOrders)
                {
                    bool hasFruit = order.OrderLines.Any(x => x.ProductVersion.Supplier == "FrugtKarl");
                    bool hasOther = order.OrderLines.Any(x => x.ProductVersion.Supplier != "FrugtKarl");
                    wrappers.Add(new OrderHistoryWrapper(order, hasFruit, hasOther));
                }

                return wrappers;
            }
        }
    }
}
