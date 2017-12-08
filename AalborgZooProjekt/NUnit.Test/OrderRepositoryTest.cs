using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AalborgZooProjekt.Model;
using Moq;
using System.Data.Entity;


namespace NUnit.Test
{
    [TestFixture]
    public class OrderRepositoryTest
    {
        [Test]
        public void AddOrderToDatabaseTest()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<AalborgZooContainer1>();
            mockContext.Setup(m => m.OrderSet).Returns(() => mockSet.Object);


            IOrderRepository dbRep = new OrderRepository(mockContext.Object);

            Order order = new Order();

            //Act
            dbRep.AddOrder(order);

            //Assert
            Assert.AreEqual(dbRep.GetOrder(order.Id), order);
        }

    }
}
