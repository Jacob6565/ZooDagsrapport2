using System;
using AalborgZooProjekt.Interfaces;


namespace AalborgZooProjekt.Model
{
    public partial class Zookeeper : IZookeeper
    {
        /// <summary>
        /// Changes Zookeeper's deparment and saves the old.
        /// </summary>
        /// <param name="department"></param>
        public void ChangeDepartment(Department department)
        {
            DepartmentChange oldDepartment = new DepartmentChange
            {
                DateChanged = DateTime.Now.ToString(),
                DepartmentID = this.DepartmentId,
                ZookeeperID = this.Id,
                Zookeeper = this
            };

            //Saving the change
            this.DepartmentChanges.Add(oldDepartment);

            //Applying changes
            this.Department = department;
            this.DepartmentId = department.Id;
            
        }

        public void MakeOrder(Order order)
        {
            order.AttachZookeeperToOrder(this);
            ShoppingList ordersShoppingList = order.ShoppingList;

            //Sending order
            order.SendOrder(ordersShoppingList);
        }
    }
}
