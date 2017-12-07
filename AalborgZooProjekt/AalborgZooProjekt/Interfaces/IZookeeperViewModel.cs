using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Database;

namespace AalborgZooProjekt.ViewModel
{
    interface IZookeeperViewModel
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
