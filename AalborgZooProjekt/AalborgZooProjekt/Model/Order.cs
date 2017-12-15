using System;
using AalborgZooProjekt.Interfaces;
using System.Collections.Generic;

namespace AalborgZooProjekt.Model
{
    public partial class Order : IOrder
    {
        public Order(Department department) : this(new OrderRepository(), department) { }
        public Order(IOrderRepository orderRep, Department department) : this()
        {
            DepartmentID = department.Id;
            DateCreated = GetDate();
            Status = _underConstruction;        
        }

        private IOrderRepository dbRep = new OrderRepository();


        private int _underConstruction = 0;
        public int UnderConstruction { get { return _underConstruction; } }

        private int _sent = 1;
        public int Sent{ get { return _sent; }}

        /// <summary>
        /// Simple function that returns the current date, using the DateTime.Today() function
        /// </summary>
        /// <returns></returns>W
        private DateTime GetDate()
        {
            return DateTime.Today;
        }

        /// <summary>
        /// Adds an orderline to the order, the actual parameter input orderLine will be an empty orderLine for 
        /// our system
        /// </summary>
        /// <param name="orderLine"></param>
        public void AddOrderLine(OrderLine orderLine)
        {
            if (orderLine != null)
            {
                OrderLines.Add(orderLine);
            }

            else
                throw new ArgumentNullException();
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
        /// Determines if the order is sendable, by checking the order state, the orderline, and Zookeeper 
        /// ID attached to it
        /// </summary>
        /// <returns></returns>
        public bool CanOrderBeSend()
        {
            bool canOrderBeSend = true;

            //An order can not be send when in another state than "Editable"
            if (Status != UnderConstruction)
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
        }

        /// <summary>
        /// Changes the unit of an orderline in the current order
        /// </summary>
        /// <param name="orderLine"></param>
        /// <param name="unit"></param>
        public void ChangeUnit(OrderLine orderLine, Unit unit)
        {
            if (unit != null && orderLine.ProductVersion.Units.Contains(unit))
                orderLine.UnitID = unit.Id;
            else if (unit == null)
                throw new NullReferenceException();
            else if (!orderLine.ProductVersion.Units.Contains(unit))
                throw new ProductVersionDoesNotContainGivenUnitException();
        }

        /// <summary>
        /// Removes orderline from the current order
        /// </summary>
        /// <param name="orderLine"></param>
        public void RemoveOrderLine(OrderLine orderLine)
        {
            if (CanOrderBeChanged())
                OrderLines.Remove(orderLine);
        }

        /// <summary>
        /// Changes the stored OrderedByID to 0 so symbolize no attached Zookeeper
        /// </summary>
        /// <param name="zookeeper"></param>
        public void RemoveZookeeperFromOrder()
        {
            OrderedByID = 0;
        }


        /// <summary>
        /// Changes the current order comment
        /// </summary>
        /// <param name="comment"></param>
        public void SaveComment(string comment)
        {
            Note = comment;
        }

        /// <summary>
        /// When the zookeepers send the order to the shopper, it changes the order state
        /// </summary>
        public void SendOrder(ShoppingList shoppingList)
        {
            if (CanOrderBeSend())
            {
                Status = _sent;
                DateOrdered = GetDate();

                IShoppingListRepository dbShopListRep = new ShoppinglistRepository();
                //ShoppingList shoppingList = dbShopListRep.GetActiveShoppingList();

                //Temporary
                if (shoppingList == null)
                {
                    shoppingList = new ShoppingList();
                }

                shoppingList.AddOrder(this);

                //Updates the order in database and adds it to shoppinglist in database
                dbRep.AddOrder(this);
                dbShopListRep.Update(shoppingList);
            }
        }
    }
}
