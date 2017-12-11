using Moq;
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
        [Test]
        public void ActivateProduct_ProductIsDeactivated_GetsActivated()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act
            product.ActivateProduct();

            //Assert
            Assert.AreEqual(true, product.ProductVersions.Last().IsActive);
        }

        [Test]
        public void ActivateProduct_ProductIsActivated_ThrowsException()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, true);

            //Act and Assert
            Assert.Throws<ProductAlreadyActivatedException>(() => product.ActivateProduct());
        }

        [Test]
        public void DeactivateProduct_ProductIsActivated_GetsDeactivated()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, true);

            //Act
            product.DeactivateProduct();

            //Assert
            Assert.AreEqual(false, product.ProductVersions.Last().IsActive);
        }

        [Test]
        public void DeactivateProduct_ProductIsDeactivated_ThrowsException()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act and Assert
            Assert.Throws<ProductAlreadyDeactivatedException>(() => product.DeactivateProduct());
        }

        [Test]
        public void AddProductUnit_UnitCanBeAdded_UnitAdded()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Unit kg = new Unit()
            {
                Name = "kg"
            };

            units.Add(kg);

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            Unit UnitToAdd = new Unit()
            {
                Name = "kasser"
            };

            //Act
            product.AddProductUnit(UnitToAdd);

            //Assert
            Assert.IsTrue(product.ProductVersions.Last().Unit.Contains(UnitToAdd));
        }

        [Test]
        public void AddProductUnit_UnitAlredyAdded_ExceptionThrown()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Unit kg = new Unit()
            {
                Name = "kg"
            };
            Unit UnitToAdd = new Unit()
            {
                Name = "kasser"
            };

            units.Add(kg);
            units.Add(UnitToAdd);

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act And Assert
            Assert.Throws<ProductAlreadyHaveUnitException>(() => product.AddProductUnit(UnitToAdd));

        }

        [Test]
        public void RemoveProductUnit_UnitCanBeRemoved_UnitRemoved()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
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
            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act
            product.RemoveProductUnit(kg);

            //Assert
            Assert.IsTrue(!product.ProductVersions.Last().Unit.Contains(kg));
        }

        [Test]
        public void RemoveProductUnit_UnitDoNotExist_ExceptionThrown()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
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

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act and Assert
            product.RemoveProductUnit(UnitToRemove);
            Assert.Throws<UnitAlreadyRemovedException>(() => product.RemoveProductUnit(UnitToRemove));

        }

        [Test]
        public void ChangeProductName_NameIsNew_NameChanged()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            string newName = "Banan";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Unit kasser = new Unit()
            {
                Name = "kasser"
            };
            Unit kg = new Unit()
            {
                Name = "kg"
            };
            units.Add(kasser);
            units.Add(kg);

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act
            product.ChangeProductName(newName);

            //Assert
            Assert.AreEqual(newName, product.ProductVersions.Last().Name);
        }

        [Test]
        public void ChangeProductName_NameIsNotNew_ExceptionThrown()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            string newName = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            Unit kasser = new Unit()
            {
                Name = "kasser"
            };
            Unit kg = new Unit()
            {
                Name = "kg"
            };
            units.Add(kasser);
            units.Add(kg);

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act and Assert
            Assert.Throws<ProductAlreadyHaveThatNameException>(() => product.ChangeProductName(newName));

        }

        [Test]
        public void ChangeProductSupplier_SupplierIsNew_SupplierChanged()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            string newSupplier = "Ole";
            List<Unit> units = new List<Unit>();
            Unit kasser = new Unit()
            {
                Name = "kasser"
            };
            Unit kg = new Unit()
            {
                Name = "kg"
            };
            units.Add(kasser);
            units.Add(kg);

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act
            product.ChangeProductSupplier(newSupplier);

            //Assert
            Assert.AreEqual(newSupplier, product.ProductVersions.Last().Supplier);
        }

        [Test]
        public void ChangeProductSupplier_SupplierIsNotNew_ExceptionThrown()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "Karl";
            string newSupplier = "Karl";
            List<Unit> units = new List<Unit>();
            Unit kasser = new Unit()
            {
                Name = "kasser"
            };
            Unit kg = new Unit()
            {
                Name = "kg"
            };
            units.Add(kasser);
            units.Add(kg);

            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, false);

            //Act And Assert
            Assert.Throws<AlreadyExistingSupplierException>(() => product.ChangeProductSupplier(newSupplier));

        }
    }

    class MockDAL : IProductDAL
    {
        public List<Product> mockDB = new List<Product>();

        public void AddProduct(Product product)
        {
            mockDB.Add(product);
        }

        public void ProductVersionList(Product product)
        {
            Product outdated = mockDB.SingleOrDefault(x => x.Id == product.Id);
            outdated = product;
        }
    }
}
