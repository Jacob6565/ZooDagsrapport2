using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    public interface IDepartment
    {
        void RemoveDepartmentSpecificProduct(DepartmentSpecificProduct dSProduct);
        void AddDepartmentSpecificProduct();
        void ChangeDepartmentForZookeeper(Zookeeper zookeeper);
    }
}
