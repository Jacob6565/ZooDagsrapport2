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
        void CancelOrder(Order order); // Work in progress.. AT
        void RemoveOrderLine(OrderLine orderLine);
        void RemoveZookeeperFromOrder(Zookeeper zookeeper);
        void RemoveDepartmentSpecificProduct(DepartmentSpecifikProduct dSProduct);
        void SaveComment(Order order, string comment);
        void SaveOrder(Order order);
        Order GetOrderFromHistory();
        List<Order> GetOrderHistory();
        List<Zookeeper> GetZookeepers();
        List<Product> GetProducts();
        Order GetCurrentOrder();
        void CloseProgram();
        void MakeOrder(Order order);
        void SearchForProduct(List<Product> productList, string search);
        void AddDepartmentSpecificProduct();
        void AddOrderLine(OrderLine orderLine);
        void AttachZookeeperToOrder(Order order, Zookeeper zookeeper);
        void CheckIfOrderIsAllowedToBeSent();
        void CheckIfOrderIsAllowedToBeFilledOut();
        void CheckIfProductIsActive(Product product);
        void ChangeNumberForOrderLines(OrderLine orderline);
        void ChangeDepartmentForZookeeper(Zookeeper zookeeper);
        void ChangeUnitForOrderLine(OrderLine orderline, Unit unit);       
    }
}
