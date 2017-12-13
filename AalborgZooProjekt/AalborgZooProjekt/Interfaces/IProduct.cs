using AalborgZooProjekt.Model;

namespace AalborgZooProjekt.Interfaces
{
    public interface IProduct
    {
        void RemoveProductUnit(Unit unitToRemove);
        void DeactivateProduct();
        void ActivateProduct();
        void AddProductUnit(Unit unitToAdd);
        void ChangeProductSupplier(string supplier);
        bool CheckIfProductIsActive();
        void ChangeProductName(string name);
    }
}
