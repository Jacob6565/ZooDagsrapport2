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
        public void Constructor_CorrectInput_Succes()
        {
            //Arrange
            //Setting up mockDAL
            //Mock<IProductDAL> mockDAL = new Mock<IProductDAL>();
            
            MockDAL dal = new MockDAL();
            Shopper shopper = new Shopper();
            string name = "Banan";
            string supplier = "Karl";
            List<Unit> units = new List<Unit>();
            units.Add(new Unit()
            {
                Name = "kg"

            });

            bool active = false;

            //Act
            Product product = new Product(dal, shopper, name, supplier, units, active);

            //Assert
            Assert.AreEqual(product, dal.mockDB.First());
          
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
