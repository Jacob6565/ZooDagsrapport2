using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    public interface IOrder
    {
        void AttachZookeeperToOrder(Zookeeper zookeeper); // Work in progress
        void AddOrderLine(OrderLine orderLine);
        void SaveComment(string comment);
        void RemoveZookeeperFromOrder();
        void RemoveOrderLine(OrderLine orderLine);
        bool CanOrderBeSend();
        void SendOrder();
        bool CanOrderBeChanged();
    }
}
