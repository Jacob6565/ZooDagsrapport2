using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    interface IOrder
    {
        void AttachZookeeperToOrder(Order order, Zookeeper zookeeper); // Work in progress
        void AddOrderLine(OrderLine orderLine);
        void SaveComment(Order order, string comment);
        void RemoveZookeeperFromOrder(Zookeeper zookeeper);
        void RemoveOrderLine(OrderLine orderLine);
    }
}
