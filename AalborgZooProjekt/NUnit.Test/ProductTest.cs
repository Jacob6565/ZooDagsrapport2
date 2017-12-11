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
        public void Deactivateproduct_ProductIsDeactivated_ThrowsException()
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
