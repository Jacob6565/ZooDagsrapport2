﻿using Moq;
using System;
using System.Linq;
using NUnit.Framework;
using AalborgZooProjekt.Model;
using System.Collections.Generic;

namespace NUnit.Test
{
    [TestFixture]
    public class ProductTest
    {
        #region Common

        Mock<IProductRepository> MockRep;

        /// <summary>
        /// Creates a product and returns it.
        /// </summary>
        /// <returns></returns>
        private Product InitializeProduct()
        {
            return InitializeProduct(false);
        }
        private Product InitializeProduct(bool active)
        {
            return InitializeProduct(new List<Unit>(), active);
        }
        private Product InitializeProduct(List<Unit> units, bool active)
        {
            MockRep = new Mock<IProductRepository>();
            string name = "ProductName";
            Shopper shopper = new Shopper();
            string supplier = "Supplier";
            List<Unit> Units = units.ToList();
            Product product = new Product(MockRep.Object, shopper, name, supplier, Units, active);
            
            return product;
        }
        #endregion

        [Test]
        public void CreateProduct_ValidInput_ProductIsCreated()
        {
            Mock<IProductRepository> MockRep = new Mock<IProductRepository>();
            string name = "ProductName";
            Shopper shopper = new Shopper();
            string supplier = "Supplier";
            List<Unit> Units = new List<Unit>();
            bool active = true;
            Product product = new Product(MockRep.Object, shopper, name, supplier, Units, active);

            Assert.IsNotNull(product);
            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(name, product.ProductVersions.Last().Name);
            Assert.AreEqual(shopper.Id, product.ProductVersions.Last().CreatedByID);
            Assert.IsTrue(Units.SequenceEqual(product.ProductVersions.Last().Units));
            Assert.AreEqual(active, product.ProductVersions.Last().IsActive);
        }

        [Test]
        public void ActivateProduct_ProductIsDeactivated_GetsActivated()
        {
            //Arrange
            Product product = InitializeProduct(false);
            
            //Act
            product.ActivateProduct();

            //Assert
            Assert.AreEqual(true, product.ProductVersions.Last().IsActive);
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void ActivateProduct_ProductIsActivated_ThrowsException()
        {
            //Arrange
            Product product = InitializeProduct(true);

            //Act and Assert
            Assert.Throws<ProductAlreadyActivatedException>(() => product.ActivateProduct());
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Never());
        }

        [Test]
        public void DeactivateProduct_ProductIsActivated_GetsDeactivated()
        {
            //Arrange
            Product product = InitializeProduct(true);
            
            //Act
            product.DeactivateProduct();

            //Assert
            Assert.AreEqual(false, product.ProductVersions.Last().IsActive);
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void DeactivateProduct_ProductIsDeactivated_ThrowsException()
        {
            //Arrange
            Product product = InitializeProduct(false);

            //Act and Assert
            Assert.Throws<ProductAlreadyDeactivatedException>(() => product.DeactivateProduct());
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Never());

        }

        [Test]
        public void AddProductUnit_UnitCanBeAdded_UnitAdded()
        {
            //Arrange
            Product product = InitializeProduct();
            
            Unit UnitToAdd = new Unit()
            {
                Name = "kasser"
            };

            //Act
            product.AddProductUnit(UnitToAdd);

            //Assert
            Assert.IsTrue(product.ProductVersions.Last().Units.Contains(UnitToAdd));
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void AddProductUnit_UnitAlredyAdded_ExceptionThrown()
        {
            //Arrange
            Product product = InitializeProduct();

            Unit UnitToAdd = new Unit()
            {
                Name = "kasser"
            };

            //Act
            product.AddProductUnit(UnitToAdd);

            //Assert
            Assert.Throws<ProductAlreadyHaveUnitException>(() => product.AddProductUnit(UnitToAdd));
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void RemoveProductUnit_UnitCanBeRemoved_UnitRemoved()
        {
            //Arrange
            List<Unit> units = new List<Unit>();
            Unit kg = new Unit()
            {
                Name = "kg"
            };
            Unit kasser = new Unit()
            {
                Name = "kasser"
            };

            units.Add(kg);
            units.Add(kasser);

            Product product = InitializeProduct(units, true);

            //Act
            product.RemoveProductUnit(kg);

            //Assert
            Assert.IsTrue(!product.ProductVersions.Last().Units.Contains(kg));
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void RemoveProductUnit_UnitAlreadyRemoved_ExceptionThrown()
        {
            //Arrange
            List<Unit> units = new List<Unit>();
            Unit kasser = new Unit()
            {
                Name = "kasser"
            };
            Unit UnitToRemove = new Unit()
            {
                Name = "kg"
            };
            units.Add(kasser);
            units.Add(UnitToRemove);

            Product product = InitializeProduct(units, true);

            //Act and Assert
            product.RemoveProductUnit(UnitToRemove);
            Assert.Throws<UnitAlreadyRemovedException>(() => product.RemoveProductUnit(UnitToRemove));
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());

        }

        [Test]
        public void ChangeProductName_NameIsNew_NameChanged()
        {
            //Arrange
            Product product = InitializeProduct(true);
            string newName = "Banan";

            //Act
            product.ChangeProductName(newName);

            //Assert
            Assert.AreEqual(newName, product.ProductVersions.Last().Name);
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void ChangeProductName_NameIsNotNew_ExceptionThrown()
        {
            //Arrange
            Product product = InitializeProduct(true);
            product.Name = "ProductName";
            string newName = "ProductName";
           
            //Act and Assert
            Assert.Throws<ProductAlreadyHaveThatNameException>(() => product.ChangeProductName(newName));
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Never());

        }

        [Test]
        public void ChangeProductSupplier_SupplierIsNew_SupplierChanged()
        {
            //Arrange
            Product product = InitializeProduct(true);
            string newSupplier = "Ole";
          
            //Act
            product.ChangeProductSupplier(newSupplier);

            //Assert
            Assert.AreEqual(newSupplier, product.ProductVersions.Last().Supplier);
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Once());
        }

        [Test]
        public void ChangeProductSupplier_SupplierIsNotNew_ExceptionThrown()
        {
            //Arrange
            Product product = InitializeProduct(true);
            string newSupplierButSame = "Supplier";

            //Act And Assert
            Assert.Throws<AlreadyExistingSupplierException>(() => product.ChangeProductSupplier(newSupplierButSame));
            MockRep.Verify(x => x.UpdateProductVersionList(product), Times.Never());

        }
    }    
}
