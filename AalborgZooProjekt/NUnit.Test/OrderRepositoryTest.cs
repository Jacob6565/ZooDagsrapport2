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
    public class OrderRepositoryTest
    {
        [Test]
        public void AddOrderToDatabaseTest()
        {
            //Arrange
            Order order = new Order();
            IOrderRepository dbRep = new OrderRepository();

            //Act
            dbRep.AddOrder(order);

            //Assert
            Assert.AreEqual(dbRep.GetOrder(order.Id), order);
        }

    }
}
