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
            ProductVersion firstProductVersion = MakeFirstProductVersion(name, supplier, units, active);

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
        private ProductVersion MakeFirstProductVersion(string name, string supplier, List<Unit> units, bool active)
        {
            ProductVersion firstProductVersion = new ProductVersion
            {
                Name = name,
                Product = this,
                ProductId = this.Id,
                IsActive = active,
                Units = units,
                Supplier = supplier
            };

            return firstProductVersion;
        }
        private ProductVersion DublicateProductVersion(ProductVersion productToCopy)
        {
            ProductVersion Copy = new ProductVersion
            {
                IsActive = productToCopy.IsActive,
                Name = productToCopy.Name,
                Product = productToCopy.Product,
                Supplier = productToCopy.Supplier,
                Units = productToCopy.Units.ToList(),
                ProductId = productToCopy.ProductId,
                OrderLines = productToCopy.OrderLines.ToList()
            };

            return Copy;
        }

        public void ActivateProduct()
        {
            ProductVersion newVersion, previousVersion;
           
            previousVersion = ProductVersions.Last();

            if (previousVersion.IsActive == false)
            {
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);

                //Adding the change
                newVersion.IsActive = true;
                
                this.ProductVersions.Add(newVersion);
                repository.UpdateProductVersionList(this);
            }
            else
            {
                throw new ProductAlreadyActivatedException();
            }
        }

        public void AddProductUnit(Unit unitToAdd)
        {
            ProductVersion newVersion, previousVersion;
            previousVersion = ProductVersions.Last();

            if (!previousVersion.Units.Contains(unitToAdd))
            {
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);

                //Adding the change
                newVersion.Units.Add(unitToAdd);

                this.ProductVersions.Add(newVersion);
                repository.UpdateProductVersionList(this);

            }
            else
            {
                throw new ProductAlreadyHaveUnitException();
            }

        }

        public void ChangeProductName(string name)
        {
            ProductVersion newVersion, previousVersion;
            previousVersion = this.ProductVersions.Last();

            if (previousVersion.Name != name)
            {
                this.Name = name;
                
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);

                //Adding the change
                newVersion.Name = name;

                this.ProductVersions.Add(newVersion);
                repository.UpdateProductVersionList(this);

            }
            else
            {
                throw new ProductAlreadyHaveThatNameException();
            }
        }

        public void ChangeProductSupplier(string supplier)
        {
            ProductVersion newVersion, previousVersion;
            previousVersion = ProductVersions.Last();

            //Copying data from previous to new
            if (previousVersion.Supplier != supplier)
            {
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);

                //Adding the change
                newVersion.Supplier = supplier;

                this.ProductVersions.Add(newVersion);
                repository.UpdateProductVersionList(this);
            }
            else
            {
                throw new AlreadyExistingSupplierException(supplier);
            }
        }

        public bool CheckIfProductIsActive()
        {
            if (ProductVersions.Count != 0)
            {
                return ProductVersions.Last().IsActive;
            }
            else return false;
        }

        public void DeactivateProduct()
        {
            ProductVersion newVersion, previousVersion;
            previousVersion = ProductVersions.Last();

            if (previousVersion.IsActive == true)
            {
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);

                //Adding the change
                newVersion.IsActive = false;

                this.ProductVersions.Add(newVersion);
                repository.UpdateProductVersionList(this);
            }
            else
            {
                throw new ProductAlreadyDeactivatedException(previousVersion.Name);
            }
        }

        public void RemoveProductUnit(Unit unitToRemove)
        {
            ProductVersion newVersion, previousVersion;
            previousVersion = ProductVersions.Last();

            if (previousVersion.Units.Contains(unitToRemove))
            {
                //Copying data from previous to new
                newVersion = DublicateProductVersion(previousVersion);

                //Adding the change
                newVersion.Units.Remove(unitToRemove);
                this.ProductVersions.Add(newVersion);
                repository.UpdateProductVersionList(this);
            }
            else
            {
                throw new UnitAlreadyRemovedException();
            }
        }
    }
}

