using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AalborgZooProjekt.Model;

namespace NUnit.Test
{
    [TestFixture]
    class ProductTester
    {
        [Test]
        public void TestActivateProduct()
        {
            // Arrange
            DummyProduct product = new DummyProduct("Æbler")
            {
                ProductVersion = new DummyProductVersion(false)
            };

            // Act
            product.ActivateProduct(product);

            // Assert
            Assert.AreEqual(product.ProductVersion.IsActive, true);
        }

        [Test]
        public void TestDeactivateProduct()
        {
            // Arrange
            DummyProduct product = new DummyProduct("Æbler")
            {
                ProductVersion = new DummyProductVersion(true)
            };

            // Act
            product.DeactivateProduct(product);

            // Assert
            Assert.AreEqual(product.ProductVersion.IsActive, false);
        }

        
        [Test]
        public void TestRemoveProductUnit()
        {
            //Arrange
            DummyProduct product = new DummyProduct("Æbler")
            {
                ProductVersion = new DummyProductVersion("kilo")
            };

            //Act
            product.RemoveUnit(product);

            //Assert
            Assert.IsEmpty(product.ProductVersion.Unit.Name);
        }
        
    }
}
