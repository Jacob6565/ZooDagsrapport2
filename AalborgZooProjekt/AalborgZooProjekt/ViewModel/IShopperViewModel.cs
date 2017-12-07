using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model.Database;

namespace AalborgZooProjekt.ViewModel
{
    interface IShopperViewModel
    {
        
        void ActivateProduct(Product product); // done - atm in DummyProduct
        void DeActivateProduct(Product product); // done - atm in DummyProduct
        void RemoveUnitFromProduct(Product product); // done - atm in DummyProduct
        void RemoveOrderLine();
        void SaveShoppingList();
        void GetDepartmentSpecificProducts();
        void GetShoppingListFromHistory();
        void GetShoppingListHistory();
        void GetCurrentShoppingList();
        void GetProducts(); // working on this - C.C.C
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
