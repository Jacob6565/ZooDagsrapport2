using AalborgZooProjekt.Model;
using System.Collections.Generic;

namespace AalborgZooProjekt.Interfaces
{
    public interface IDepartment
    {
        Order MakeOrder();
        void CancelOrder(Order order);
        void AddDepartmentSpecificProduct(Department department, Product product);
        void RemoveDepartmentSpecificProduct(Department department, DepartmentSpecificProduct dSProduct);
        void ChangeDepartmentForZookeeper(DepartmentChange departmentChange);
    }
}
