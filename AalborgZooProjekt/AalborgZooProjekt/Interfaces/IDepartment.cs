using AalborgZooProjekt.Model;
using System.Collections.Generic;

namespace AalborgZooProjekt.Interfaces
{
    interface IDepartment
    {
        Order MakeOrder();
        void CancelOrder(Order order);
        void AddDepartmentSpecificProduct(Department department, Product product);
        void RemoveDepartmentSpecificProduct(List<DepartmentSpecificProduct> dSProductList, DepartmentSpecificProduct dSProduct);
        void ChangeDepartmentForZookeeper(DepartmentChange departmentChange);
    }
}
