using System;
using AalborgZooProjekt.Interfaces;

namespace AalborgZooProjekt.Model
{
    public partial class Order : IOrder
    {
        public Order(IOrderRepository rep, Department department) : this()
        {
            DepartmentID = department.Id;
            DateCreated = GetDate();
            Status = _underConstruction;
            

            //Repository to update, get and add from database
            dbRep = rep;           

            //Adds the order in database
            dbRep.AddOrder(this);
        }
        public Order(Department department) : this(new OrderRepository(), department){}

        private IOrderRepository dbRep;

        private string _underConstruction = "Under Construction";
        private string _sent = "Sent";

        /// <summary>
        /// Simple function that returns the current date, using the DateTime.Today() function
        /// </summary>
        /// <returns></returns>
        private DateTime GetDate()
        {
            return DateTime.Today;
        }

        /// <summary>
        /// Adds an orderline to the order, the actual parameter input orderLine will be an empty orderLine for our system
        /// </summary>
        /// <param name="orderLine"></param>
        public void AddOrderLine(OrderLine orderLine)
        {
            if (orderLine != null)
                OrderLines.Add(orderLine);

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// Attaches a zookeeper to an order, only one zookeeper can be attached to one order
        /// </summary>
        /// <param name="zookeeper"></param>

        public void AttachZookeeperToOrder(Zookeeper zookeeper)
        {
            if (OrderedByID == zookeeper.Id)
                throw new ZookeeperAllReadyAddedException();
            else if (zookeeper != null)
                OrderedByID = zookeeper.Id;

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// Determines if the order is in a changeable state, by checking the string status 
        /// </summary>
        /// <returns></returns>
        public bool CanOrderBeChanged()
        {
            return (String.Equals(_underConstruction, Status) || String.Equals(_sent, Status));
        }

        /// <summary>
        /// Determines if the order is sendable, by checking the order state, the orderline, and Zookeeper ID attached to it
        /// </summary>
        /// <returns></returns>
        public bool CanOrderBeSend()
        {
            bool canOrderBeSend = true;

            //An order can not be send when in another state than "Editable"
            if (!String.Equals(Status, _underConstruction))
            {
                canOrderBeSend = false;
                throw new OrderIsNotUnderAnSendableStateException();
            }

            //Naive check. An order can not be send with an Zookeeper ID of less than 1, as that is considered as no Zookeeper
            else if (OrderedByID < 1)
            {
                canOrderBeSend = false;
                throw new OrderCanNotSendNoZookeeperException();
            }

            //An order can not be send without orderlines
            else if (OrderLines.Count < 1)
            {
                canOrderBeSend = false;
                throw new CanNotSendEmptyOrderException();
            }

            return canOrderBeSend;
        }

        public void ChangeProduct(OrderLine orderLine, ProductVersion productVersion)
        {
            if (productVersion == null)
                throw new NullReferenceException();
            else if (productVersion.IsActive == true)
                orderLine.ProductVersion = productVersion;
            else if (productVersion.IsActive == false)
                throw new ProductVersionIsNotActiveException();

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// Changes the quantity of an orderline in the current order
        /// </summary>
        /// <param name="orderLine"></param>
        /// <param name="amount"></param>
        public void ChangeAmount(OrderLine orderLine, int amount)
        {
            if (amount > 0)
                orderLine.Quantity = amount;
            else if (amount <= 0)
                throw new ArgumentOutOfRangeException();

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// Changes the unit of an orderline in the current order
        /// </summary>
        /// <param name="orderLine"></param>
        /// <param name="unit"></param>
        public void ChangeUnit(OrderLine orderLine, Unit unit)
        {
            if (unit != null && orderLine.ProductVersion.Unit.Contains(unit))
                orderLine.UnitID = unit.Id;
            else if (unit == null)
                throw new NullReferenceException();
            else if (!orderLine.ProductVersion.Unit.Contains(unit))
                throw new ProductVersionDoesNotContainGivenUnitException();

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// Removes orderline from the current order
        /// </summary>
        /// <param name="orderLine"></param>
        public void RemoveOrderLine(OrderLine orderLine)
        {
            if (CanOrderBeChanged())
                OrderLines.Remove(orderLine);

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// Changes the stored OrderedByID to -1 so symbolize no attached Zookeeper
        /// </summary>
        /// <param name="zookeeper"></param>
        public void RemoveZookeeperFromOrder()
        {
            OrderedByID = -1;

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        

        /// <summary>
        /// Changes the current order comment
        /// </summary>
        /// <param name="comment"></param>
        public void SaveComment(string comment)
        {
            Note = comment;

            //Updates the order in database
            dbRep.UpdateOrder(this);
        }

        /// <summary>
        /// When the zookeepers send the order to the shopper, it changes the order state
        /// </summary>
        public void SendOrder(ShoppingList shoppingsList)
        {
            if (CanOrderBeSend())
            {
                Status = _sent;
                DateOrdered = GetDate();
                shoppingsList.AddOrder(this);

                //Updates the order in database
                dbRep.UpdateOrder(this);
            }
        }
    }
}
