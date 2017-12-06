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
        
        void CancelOrder(Order order);
        void RemoveOrderLine(OrderLine orderLine);
        void RemoveZookeeperFromOrder(Zookeeper zookeeper);
        void RemoveDepartmentSpecificProduct(DepartmentSpecifikProduct dSProduct);
        void SaveComment(Order order, string comment);
        void SaveOrder(Order order);
        Order GetOrderFromHistory();
        void GetOrderHistory(List<Order> orderList);
        void GetZookeepers(List<Zookeeper> zookeeperList);
        void GetProducts(List<Product> productList);
        void GetCurrentOrder();
        void CloseProgram();
        void MakeOrder(Order order);
        void SearchForProduct(List<Product> productList, string search);
        void AddDepartmentSpecificProduct();
        void AddOrderLine(OrderLine orderLine);
        void AttachZookeeperToOrder(Order order, Zookeeper zookeeper);
        void CheckIfOrderIsAllowedToBeSent();
        void CheckIfOrderIsAllowedToBeFilledOut();
        void CheckIfProductIsActive();
        void ChangeNumberForOrderLines();
        void ChangeDepartmentForZookeeper();
        void ChangeUnitForOrderLine(Unit unit);
        
    }
}
