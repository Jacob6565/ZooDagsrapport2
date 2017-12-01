using NUnit.Framework;
using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Test
{
    [TestFixture]
    public class testingtests
    {
       
        [Test]
        public void TestActivateProduct()
        {
            // Arrange
            DummyProductVersion DPV = new DummyProductVersion(false);
            
            // Act
            DPV.ActivateProduct();

            // Assert
            Assert.AreEqual(DPV.IsActive, true);
        }

        [Test]
        public void TestDeactivateProduct()
        {
            // Arrange
            DummyProductVersion DPV = new DummyProductVersion(true);

            // Act
            DPV.DeactivateProduct();

            // Assert
            Assert.AreEqual(DPV.IsActive, false);
        }


    }
}
