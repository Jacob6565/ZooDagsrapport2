using AalborgZooProjekt.Interfaces;
using System;
using System.Collections.Generic;

namespace AalborgZooProjekt.Model
{
    public partial class Department : IDepartment
    {
        /// <summary>
        /// Adds the product under the department thus making it a department specific product.
        /// If the product already can be found under the department it will throw an exception.
        /// </summary>
        public void AddDepartmentSpecificProduct(Department department, Product product)
        {
            var departmentSpecificProduct = new DepartmentSpecificProduct(department, product);

            if (department.DepartmentSpecificProducts.Contains(departmentSpecificProduct))
            {
                throw new DepartmentAlreadyDepartmentSpecificProductException();
            }
            else
            {
                department.DepartmentSpecificProducts.Add(departmentSpecificProduct);
            }
        }

        /// <summary>
        /// Removes the product form the department specific product.
        /// If the product cannot be found under the department specific products an exception will be thrown.
        /// </summary>
        public void RemoveDepartmentSpecificProduct(Department department, DepartmentSpecificProduct dSProduct)
        {
            if (department.DepartmentSpecificProducts.Contains(dSProduct))
            {
                department.DepartmentSpecificProducts.Remove(dSProduct);
            }
            else
            {
                throw new DepartmentNotDepartmentSpecificProductException();
            }
        }

        /// <summary>
        /// Vil gerne snakke sammen med en om denne her.
        /// </summary>
        public void ChangeDepartmentForZookeeper(DepartmentChange departmentChange)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// In charge of making a new order.
        /// </summary>
        public Order MakeOrder()
        {
            Order order = new Order();

            return order;
        }


        /// <summary>
        /// Sets the order's cancellation fate to the current date it is executed.
        /// </summary>
        public void CancelOrder(Order order)
        {
            order.DateCancelled = DateTime.Today;
        }
    }
}
