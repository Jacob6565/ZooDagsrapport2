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
            Status = _isEditable;
        }

        private string _isEditable = "Editable";

        private string GetDate()
        {
            return DateTime.Today.ToString();
        }


        public void AddOrderLine(OrderLine orderLine)
        {
            OrderLines.Add(orderLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zookeeper"></param>
        public void AttachZookeeperToOrder(Zookeeper zookeeper)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanOrderBeChanged()
        {
            return String.Equals("Editable", Status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanOrderBeSend()
        {
            bool canOrderBeSend = true;

            //An order can not be send when in another state than "Editable"
            if (!String.Equals(Status, _isEditable))
                canOrderBeSend = false;

            //An order can not be send with an Zookeeper ID of less than 1, as that is considered as no Zookeeper
            else if (OrderedByID < 1)
                canOrderBeSend = false;

            //An order can not be send without orderlines
            else if (OrderLines.Count < 1)
                canOrderBeSend = false;

            return canOrderBeSend;
        }

        public void ChangeAmount(OrderLine orderLine, int amount)
        {
            throw new NotImplementedException();
        }

        public void ChangeUnit(OrderLine orderLine, Unit unit)
        {
            throw new NotImplementedException();
        }

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

        public void SaveComment(string comment)
        {
            Note = comment;
        }

        public void SendOrder()
        {
            Status = "Send";

            //MISSING ACTUAL SENDING FUNCTIONALITY 
        }
    }
}
