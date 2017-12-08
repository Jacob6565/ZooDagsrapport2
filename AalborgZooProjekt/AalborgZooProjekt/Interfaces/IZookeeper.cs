using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    public interface IZookeeper
    {
        void MakeOrder(Order order);
        void ChangeDepartment(Department department);
        void GetID();
    }
}
