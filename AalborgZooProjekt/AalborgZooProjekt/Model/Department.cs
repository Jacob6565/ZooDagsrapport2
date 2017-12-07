using AalborgZooProjekt.Interfaces;
using System;
using System.Collections.Generic;

namespace AalborgZooProjekt.Model
{
    public partial class Department : IDepartment
    {
        List<DepartmentSpecificProduct> departmentSpecificProductList = new List<DepartmentSpecificProduct>();

        /// <summary>
        /// Methods for adding and removing a Department Specific Product
        /// </summary>
        public void AddDepartmentSpecificProduct(Department department, Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveDepartmentSpecificProduct(List<DepartmentSpecificProduct> dSProductList, DepartmentSpecificProduct dSProduct)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void CancelOrder(Order order)
        {
            order.DateCancelled = DateTime.Today.ToString("DD/MM/YYYY");
        }
    }
}
