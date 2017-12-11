using System;
using NUnit.Framework;
using AalborgZooProjekt.Model;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace NUnit.Test
{   
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void ActivateProduct()
        {
            //Arrange
            Mock<IProductDAL> MockDAL = new Mock<IProductDAL>();
            
            string name = "Æble";
            Shopper shopper = new Shopper();
            string supplier = "karl";
            bool IsActive = true;
            List<Unit> units = new List<Unit>();
            Product product = new Product(MockDAL.Object, shopper, name, supplier, units, IsActive);
            
            //Act
            

            //Assert
            
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
