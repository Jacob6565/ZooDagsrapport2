using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.ViewModel
{
    interface IZookeeperViewModel
    {
        void CancelOrder();
        void RemoveOrderLine();
        void RemoveZookeeperFromOrder();
        void RemoveDepartmentSpecificProduct();
        void SaveComment();
        void SaveOrder();
        void GetOrderFromHistory();
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
