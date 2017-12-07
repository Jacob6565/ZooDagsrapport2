using System;
using System.Linq;
using System.Collections.Generic;
using AalborgZooProjekt.Interfaces;

namespace AalborgZooProjekt.Model
{
    public partial class Product : IProduct
    {

        public Product(IShopper shopper, string name, string supplier, List<Unit> units, bool active = true): this()
        {
            ProductVersion firstVersion = new ProductVersion();
            firstVersion.Supplier = supplier;
            firstVersion.Unit.Concat(units);
            firstVersion.IsActive = active;
            firstVersion.Name = name;
            firstVersion.Product = this;
            firstVersion.ProductId = this.Id;
            
            
            
        }

        public void ActivateProduct()
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            //Copying data from previous to new
            newVersion.IsActive = true;
            newVersion.OrderLines = previousVersion.OrderLines;
            newVersion.Name = previousVersion.Name;

            //Det burde da også bare kunne være "this",
            //men det burde nærmest ikke være der, da productversionerne
            //er gemt inde i et product.
            newVersion.Product = previousVersion.Product;
            newVersion.ProductId = previousVersion.ProductId;
            newVersion.Supplier = previousVersion.Supplier;
            newVersion.Unit = previousVersion.Unit;

            ProductVersions.Add(newVersion);   
            
        }

        public void AddProductUnit(Unit unitToAdd)
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            //Copying data from previous to new
            newVersion.IsActive = previousVersion.IsActive;
            newVersion.Name = previousVersion.Name;
            newVersion.OrderLines = previousVersion.OrderLines;
            newVersion.Product = previousVersion.Product;
            newVersion.ProductId = previousVersion.ProductId;
            newVersion.Supplier = previousVersion.Supplier;
            newVersion.Unit = previousVersion.Unit;
            
            newVersion.Unit.Add(unitToAdd);

            ProductVersions.Add(newVersion);
        }

        public void ChangeProductName(string name)
        {
            throw new NotImplementedException();
        }

        public void ChangeProductSupplier(string supplier)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfProductIsActive()
        {
            throw new NotImplementedException();
        }

        public void DeactivateProduct()
        {
            throw new NotImplementedException();
        }

        public void RemoveProductUnit(Unit unitToRemove)
        {
            throw new NotImplementedException();
        }
    }
}
