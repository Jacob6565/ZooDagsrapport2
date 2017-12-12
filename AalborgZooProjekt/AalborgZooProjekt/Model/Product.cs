using System;
using System.Linq;
using System.Collections.Generic;
using AalborgZooProjekt.Interfaces;

namespace AalborgZooProjekt.Model
{
    public partial class Product : IProduct
    {
        //Used when accessing the database.
        private IProductRepository repository;

        public Product(Shopper shopper, string name, string supplier, List<Unit> units, bool active = true)
            : this(new ProductRepository(), shopper, name, supplier, units, active) { }

        public Product(IProductRepository repository, Shopper shopper, string name, string supplier, List<Unit> units, bool active = true) : this()
        {
            ProductVersion firstProductVersion = MakeProductVersion(name, supplier, units, active);

            this.Name = name;
            //-1 indicating it is null.
            this.DeletedByID = -1;
            this.CreatedByID = shopper.GetID();
            this.DateCreated = DateTime.Now;
            this.ProductVersions.Add(firstProductVersion);

            //Contains the methods needed to update the database
            this.repository = repository;
            this.repository.AddProduct(this);
        }


        /// <summary>
        /// Creates the first version of the product.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="supplier"></param>
        /// <param name="units"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        private ProductVersion MakeProductVersion(string name, string supplier, List<Unit> units, bool active)
        {
            ProductVersion firstProductVersion = new ProductVersion();

            firstProductVersion.Name = name;
            firstProductVersion.Product = this;
            firstProductVersion.ProductId = this.Id;
            firstProductVersion.IsActive = active;
            firstProductVersion.Unit = units;
            firstProductVersion.Supplier = supplier;

            return firstProductVersion;
        }
        private ProductVersion DublicateProductVersion(ProductVersion productToCopy)
        {
            ProductVersion Copy = new ProductVersion();

            Copy.IsActive = productToCopy.IsActive;
            Copy.Name = productToCopy.Name;
            Copy.Product = productToCopy.Product;
            Copy.Supplier = productToCopy.Supplier;
            Copy.Unit = productToCopy.Unit.ToList();
            Copy.ProductId = productToCopy.ProductId;
            Copy.OrderLines = productToCopy.OrderLines.ToList();

            return Copy;
        }

        public void ActivateProduct()
        {
            ProductVersion newVersion, previousVersion;
           
            previousVersion = ProductVersions.Last();

            if (previousVersion.IsActive != true)
            {
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);
                //Adding the change
                newVersion.IsActive = true;
                
                this.ProductVersions.Add(newVersion);
                repository.ProductVersionList(this);
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
            previousVersion = ProductVersions.Last();

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
                repository.ProductVersionList(this);

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
            previousVersion = ProductVersions.Last();
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
                repository.ProductVersionList(this);

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
            previousVersion = ProductVersions.Last();

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
                repository.ProductVersionList(this);

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
            previousVersion = ProductVersions.Last();

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
                repository.ProductVersionList(this);

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
            previousVersion = ProductVersions.Last();

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
                repository.ProductVersionList(this);

            }
            else
            {
                throw new UnitAlreadyRemovedException();
            }
        }
    }
}

