using AalborgZooProjekt.Interfaces;
using System;
using System.Collections.Generic;

namespace AalborgZooProjekt.Model
{
    public partial class Department : IDepartment
    {

        /// <summary>
        /// Methods for adding and removing a Department Specific Product
        /// </summary>
        public void AddDepartmentSpecificProduct(Department department, Product product)
        {
            var departmentSpecificProduct = new DepartmentSpecificProduct(department, product);


            if (!department.DepartmentSpecifikProducts.Contains(departmentSpecificProduct))
            {
                department.DepartmentSpecifikProducts.Add(departmentSpecificProduct);
            }
            else
            {
                throw new DepartmentAlreadyDepartmentSpecificProductException();
            }
        }

        public void RemoveDepartmentSpecificProduct(Department department, DepartmentSpecificProduct dSProduct)
        {
            if (department.DepartmentSpecifikProducts.Contains(dSProduct))
            {
                department.DepartmentSpecifikProducts.Remove(dSProduct);
            }
            else
            {
                throw new DepartmentNotDepartmentSpecificProductException();
            }
        }

        /// <summary>
        /// Method for department change for a Zookeeper
        /// </summary>
        public void ChangeDepartmentForZookeeper(DepartmentChange departmentChange)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Methods for Orders
        /// </summary>
        public Order MakeOrder()
        {
            Order order = new Order();

            return order;
        }

        public void CancelOrder(Order order)
        {
            order.DateCancelled = DateTime.Today.ToString("DD/MM/YYYY");
        }
    }
}
