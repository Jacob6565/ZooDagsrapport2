using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.ViewModel
{
    interface IShopperViewModel
    {
        void ActivateProduct();
        void DeActivateProduct();
        void RemoveUnitFromProduct(DummyProductVersion product);
        void RemoveOrderLine();
        void SaveShoppingList();
        void GetDepartmentSpecificProducts();
        void GetShoppingListFromHistory();
        void GetShoppingListHistory();
        void GetCurrentShoppingList();
        void GetProducts();
        void MakeCompleteShoppingList();
        void CloseProgram();
        void EstablishDepartment();
        void EstablishZookeeper();
        void EstablishShopper();
        void EstablishShoppingList();
        void EstablishProduct();
        void DeleteDepartment();
        void DeleteOrder();
        void DeleteZookeeper();
        void DeleteShopper();
        void DeleteProduct();
        void SearchForProduct();
        void AddOrderLine();
        void AddProductUnit();
        void CheckShoppingList();
        void CheckIfOrderMayVary();
        void CheckIfOrderExist();
        void ExecuteShoppingList();
        void ChangeNumberForOrderLine();
        void ChangeUnitForOrderLine();
        void ChangeProductSupplier();
        void ChangeProductName();
    }
}
