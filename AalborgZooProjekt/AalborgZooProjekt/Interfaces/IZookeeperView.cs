using System.Collections.Generic;
using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.ViewModel
{
    public interface IZookeeperView
    {
        void SaveOrder(Order order);
        Order GetOrderFromHistory();
        List<Order> GetOrderHistory();
        List<Zookeeper> GetZookeepers();
        List<Product> GetProducts();
        Order GetCurrentOrder();
        void CloseProgram();
        void SearchForProduct(List<Product> productList, string search);
        void CheckIfOrderIsAllowedToBeSent(Order order);
        void CheckIfOrderIsAllowedToBeFilledOut(Order order);
        void CheckIfProductIsActive(Product product);
        void ChangeNumberForOrderLines(OrderLine orderline);
        void ChangeUnitForOrderLine(OrderLine orderline, Unit unit);
    }
}
