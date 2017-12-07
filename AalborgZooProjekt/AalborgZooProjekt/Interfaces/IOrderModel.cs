using AalborgZooProjekt.Database;

namespace AalborgZooProjekt.Interfaces
{
    interface IOrderModel
    {
        void AttachZookeeperToOrder(Order order, Zookeeper zookeeper); // Work in progress
        void AddOrderLine(OrderLine orderLine);
        void SaveComment(Order order, string comment);
        void CancelOrder(Order order);
        void RemoveZookeeperFromOrder(Zookeeper zookeeper);
        void RemoveOrderLine(OrderLine orderLine);
    }
}
