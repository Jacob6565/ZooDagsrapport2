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
        void GetOrderHistory();
        void GetZookeepers();
        void GetProducts();
        void GetCurrentOrder();
        void CloseProgram();
        void MakeOrder();
        void SearchForProduct();
        void AddDepartmentSpecificProduct();
        void AddOrderLine();
        void AttachZookeeperToOrder();
        void CheckIfOrderIsAllowedToBeSent();
        void CheckIfOrderIsAllowedToBeFilledOut();
        void CheckIfProductIsActive();
        void ChangeNumberForOrderLines();
        void ChangeDepartmentForZookeeper();
        void ChangeUnitForOrderLine();
    }
}
