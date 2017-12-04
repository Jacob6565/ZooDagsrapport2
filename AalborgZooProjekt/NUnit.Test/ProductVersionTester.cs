using System;
using AalborgZooProjekt;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AalborgZooProjekt.Model;

namespace NUnit.Test
{
    [TestFixture]
    public class ProductVersionTester
    {
        [TestCase(true, "Frugt Karl")]
        [TestCase(false, "Frugt Karl")]
        public void TestActive(bool active, string supplier)
        {
            string today = DateTime.Today.ToString();
            ProductVersion product = new ProductVersion(active, supplier, today);

            product.MakeActive(product);

            Assert.IsTrue(product.IsActive);
        }

        [TestCase(true, "Frugt Karl")]
        [TestCase(false, "Frugt Karl")]
        public void TestDeactivate(bool active, string supplier)
        {
            string today = DateTime.Today.ToString();
            ProductVersion product = new ProductVersion(active, supplier, today);

            product.DeactivateProduct(product);

            Assert.IsFalse(product.IsActive);
        }

        [Test]
        public void TestRemoveProductUnit()
        {
            //Arrange
            DummyUnit kasse = new DummyUnit("Kasse");
            DummyProductVersion product = new DummyProductVersion(kasse);

            //Act
            product.RemoveUnit(product);

            //Assert
            Assert.IsEmpty(product.Unit.Name);

        }

    }
}
