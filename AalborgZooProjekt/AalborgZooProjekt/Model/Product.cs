using System;
using System.Linq;
using System.Collections.Generic;
using AalborgZooProjekt.Interfaces;

namespace AalborgZooProjekt.Model
{
    public partial class Product : IProduct
    {
        public Product(Shopper shopper, string name, string supplier, List<Unit> units, bool active = true): this()
        {
            ProductVersion firstProductVersion = MakeFirstVersion(name, supplier, units, active);

            this.Name = name;
            //-1 indicating it is null.
            this.DeletedByID = -1;
            this.DateDeleted = null;
            this.CreatedByID = shopper.GetID();
            this.DateCreated = DateTime.Now.ToString();
            this.ProductVersions.Add(firstProductVersion);

            //AddProductToDatabase(this);       
        }

        /// <summary>
        /// Creates the first version of the product.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="supplier"></param>
        /// <param name="units"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        private ProductVersion MakeFirstVersion(string name, string supplier, List<Unit> units, bool active)
        {
            ProductVersion firstProductVersion = new ProductVersion();

            firstProductVersion.Name = name;
            firstProductVersion.Product = this;
            firstProductVersion.ProductId = this.Id;
            firstProductVersion.IsActive = active;
            firstProductVersion.Unit.Concat(units);
            firstProductVersion.Supplier = supplier;

            return firstProductVersion;
        }

       

        public void ActivateProduct()
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            if (previousVersion.IsActive != true)
            {


                //Copying data from previous to new
                newVersion.IsActive = true;
                newVersion.Name = previousVersion.Name;

                //Det burde da også bare kunne være "this",
                //men det burde nærmest ikke være der, da productversionerne
                //er gemt inde i et product.
                newVersion.Product = previousVersion.Product;
                newVersion.Supplier = previousVersion.Supplier;
                newVersion.Unit = previousVersion.Unit.ToList();
                newVersion.ProductId = previousVersion.ProductId;
                newVersion.OrderLines = previousVersion.OrderLines.ToList();

                this.ProductVersions.Add(newVersion);
                //UpdateDatabase();
            }
            else
            {
                throw new ProductAlreadyActivatedException();
            }
            
        }

        public void AddProductUnit(Unit unitToAdd)
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            if (!previousVersion.Unit.Contains(unitToAdd))
            {
                //Copying data from previous to new
                newVersion.Name = previousVersion.Name;
                newVersion.Product = previousVersion.Product;
                newVersion.IsActive = previousVersion.IsActive;
                newVersion.Supplier = previousVersion.Supplier;
                newVersion.Unit = previousVersion.Unit.ToList();
                newVersion.ProductId = previousVersion.ProductId;
                newVersion.OrderLines = previousVersion.OrderLines.ToList();

                //Adding the change
                newVersion.Unit.Add(unitToAdd);

                this.ProductVersions.Add(newVersion);
                //UpdateDatabase();
            }
            else
            {
                throw new ProductAlreadyHaveUnitException();
            }

        }

        public void ChangeProductName(string name)
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();
            if (previousVersion.Name != name)
            {


                //Copying data from previous to new
                this.Name = name;
                newVersion.Name = name;
                newVersion.Unit = previousVersion.Unit.ToList();
                newVersion.Product = previousVersion.Product;
                newVersion.Supplier = previousVersion.Supplier;
                newVersion.IsActive = previousVersion.IsActive;
                newVersion.ProductId = previousVersion.ProductId;
                newVersion.OrderLines = previousVersion.OrderLines.ToList();

                this.ProductVersions.Add(newVersion);
            }
            else
            {
                throw new ProductAlreadyHaveThatNameException();
            }
        }

        public void ChangeProductSupplier(string supplier)
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            //Copying data from previous to new
            if (previousVersion.Supplier != supplier)
            {
                newVersion.Supplier = supplier;
                newVersion.Name = previousVersion.Name;
                newVersion.Unit = previousVersion.Unit.ToList();
                newVersion.Product = previousVersion.Product;
                newVersion.IsActive = previousVersion.IsActive;
                newVersion.ProductId = previousVersion.ProductId;
                newVersion.OrderLines = previousVersion.OrderLines.ToList();

                this.ProductVersions.Add(newVersion);
            }
            else
            {
                throw new AlreadyExistingSupplierException("supplier");
            }
        }

        public bool CheckIfProductIsActive()
        {
            return ProductVersions.First().IsActive;
        }

        public void DeactivateProduct()
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            //Copying data from previous to new
            if (previousVersion.IsActive == true)
            { 
                newVersion.IsActive = false;
                newVersion.Name = previousVersion.Name;
                newVersion.Unit = previousVersion.Unit.ToList();

                //Det burde da også bare kunne være "this",
                //men det burde nærmest ikke være der, da productversionerne
                //er gemt inde i et product.
                newVersion.Product = previousVersion.Product;
                newVersion.Supplier = previousVersion.Supplier;
                newVersion.ProductId = previousVersion.ProductId;
                newVersion.OrderLines = previousVersion.OrderLines.ToList();

                this.ProductVersions.Add(newVersion);
            }
            else
            {
                throw new ProductAlreadyDeactivatedException(previousVersion.Name);
            }
        }

        public void RemoveProductUnit(Unit unitToRemove)
        {
            ProductVersion newVersion, previousVersion;
            newVersion = new ProductVersion();
            previousVersion = ProductVersions.First();

            if (previousVersion.Unit.Contains(unitToRemove))
            {
                //Copying data from previous to new
                newVersion.Name = previousVersion.Name;
                newVersion.Unit = previousVersion.Unit.ToList();
                newVersion.Product = previousVersion.Product;
                newVersion.IsActive = previousVersion.IsActive;
                newVersion.Supplier = previousVersion.Supplier;
                newVersion.ProductId = previousVersion.ProductId;
                newVersion.OrderLines = previousVersion.OrderLines.ToList();

                //Adding the change by removing the unit
                newVersion.Unit.Remove(unitToRemove);
                this.ProductVersions.Add(newVersion);
                //UpdateDatabase();
            }
            else
            {
                throw new UnitAlreadyRemovedException(unitToRemove.Name);
            }
        }
    }
}
