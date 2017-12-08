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
        public void GetOrderFromDatabase()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<AalborgZooContainer1>();
            mockContext.Setup(m => m.OrderSet).Returns(() => mockSet.Object);

            IOrderRepository dbRep = new OrderRepository(mockContext.Object);

            Assert.AreSame(mockContext.Object.OrderSet.Count(), 5);

            //Act
            Order order = dbRep.GetOrder(1);

            //Assert
            Assert.IsNotNull(order);
        }

        [Test]
        public void AddOrderToDatabaseTest()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<AalborgZooContainer1>();
            mockContext.Setup(m => m.OrderSet).Returns(() => mockSet.Object);

            IOrderRepository dbRep = new OrderRepository(mockContext.Object);

            Order order = new Order();
            order.Id = 11;

            //Act
            dbRep.AddOrder(order);

            //Assert
            Assert.AreEqual(dbRep.GetOrder(6), order);
            Assert.AreEqual(dbRep.GetOrder(order.Id), 11);
            Assert.AreEqual(dbRep.GetOrder(order.Id), 6);
        }

    }
}
