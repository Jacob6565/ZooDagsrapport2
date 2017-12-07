using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    interface IDepartmentModel
    {
        void RemoveDepartmentSpecificProduct(DepartmentSpecificProduct dSProduct);
        void AddDepartmentSpecificProduct();
        void ChangeDepartmentForZookeeper(Zookeeper zookeeper);
    }
}
