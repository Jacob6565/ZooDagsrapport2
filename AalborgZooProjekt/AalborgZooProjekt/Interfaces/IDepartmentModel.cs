using AalborgZooProjekt.Database;

namespace AalborgZooProjekt.Interfaces
{
    interface IDepartmentModel
    {
        void RemoveDepartmentSpecificProduct(DepartmentSpecificProduct dSProduct);
        void AddDepartmentSpecificProduct();
        void ChangeDepartmentForZookeeper(Zookeeper zookeeper);
    }
}
