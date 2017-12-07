using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Interfaces;


namespace AalborgZooProjekt.Model
{
    public partial class Order : IOrder
    {
        public Order(Department department)
        {
            DepartmentID = department.Id;
            DateCreated = GetDate();
            Status = _underConstruction;
        }

        private string _underConstruction = "Under Construction";
        private string _sent = "Sent";


        /// <summary>
        /// Simple function that returns the current date, using the DateTime.Today() function
        /// </summary>
        /// <returns></returns>
        private string GetDate()
        {
            return DateTime.Today.ToString();
        }

        /// <summary>
        /// Adds an orderline to the order
        /// </summary>
        /// <param name="orderLine"></param>
        public void AddOrderLine(OrderLine orderLine)
        {
            if (orderLine != null)
                OrderLines.Add(orderLine);
        }

        /// <summary>
        /// Attaches a zookeeper to an order, only one zookeeper can be attached to one order
        /// </summary>
        /// <param name="zookeeper"></param>
        public void AttachZookeeperToOrder(Zookeeper zookeeper)
        {
            if (OrderedByID > 1)
                throw new ZookeeperAllReadyAddedException();
            if (zookeeper != null)
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
        /// Determines if the order is sendable, by checking the order state, the orderline, and Zookeeper ID attached to it
        /// </summary>
        /// <returns></returns>
        public bool CanOrderBeSend()
        {
            bool canOrderBeSend = true;

            //An order can not be send when in another state than "Editable"
            if (!String.Equals(Status, _underConstruction))
                canOrderBeSend = false;

            //Naive check. An order can not be send with an Zookeeper ID of less than 1, as that is considered as no Zookeeper
            else if (OrderedByID < 1)
                canOrderBeSend = false;

            //An order can not be send without orderlines
            else if (OrderLines.Count < 1)
                canOrderBeSend = false;

            return canOrderBeSend;
        }

        /// <summary>
        /// Changes the quantity of an orderline in the current order
        /// </summary>
        /// <param name="orderLine"></param>
        /// <param name="amount"></param>
        public void ChangeAmount(OrderLine orderLine, int amount)
        {
            if (amount >= 0)
               orderLine.Quantity = amount;
        }

        /// <summary>
        /// Changes the unit of an orderline in the current order
        /// </summary>
        /// <param name="orderLine"></param>
        /// <param name="unit"></param>
        public void ChangeUnit(OrderLine orderLine, Unit unit)
        {
            if(unit != null)
               orderLine.UnitID = unit.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsOrderFilledOut()
        {
            throw new NotImplementedException();
        }

        public void RemoveOrderLine(OrderLine orderLine)
        {
            if (CanOrderBeChanged())
                OrderLines.Remove(orderLine);
        }

        public void RemoveZookeeperFromOrder(Zookeeper zookeeper)
        {
            OrderedByID = -1;
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
        public void SendOrder()
        {
            Status = _sent;
            
            //MISSING ACTUAL SENDING FUNCTIONALITY 
        }
    }
}
