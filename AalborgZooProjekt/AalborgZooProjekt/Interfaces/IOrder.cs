using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    interface IOrder
    {
        void AttachZookeeperToOrder(Zookeeper zookeeper); // Work in progress
        void AddOrderLine(OrderLine orderLine);
        void SaveComment(string comment);
        void RemoveZookeeperFromOrder(Zookeeper zookeeper);
        void RemoveOrderLine(OrderLine orderLine);
        bool CanOrderBeSend();
        void SendOrder();
        bool IsOrderFilledOut();
        bool CanOrderBeChanged();

        //Belonged to OderLine initially, but it would nice to be able to call CanOrderBeChanged b4
        void ChangeUnit(OrderLine orderLine, Unit unit);
        void ChangeAmount(OrderLine orderLine, int amount);
    }
}
