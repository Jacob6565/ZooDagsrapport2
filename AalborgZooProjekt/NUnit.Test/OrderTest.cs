using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Database;

namespace NUnit.Test
{
    [TestFixture]
    public class OrderCanBeConstructedTest
    {
        [Test]
        public void CreateDepartmentTest()
        {
            Department dep = new Department();

            Assert.IsNotNull(dep);
        }

        [Test]
        public void CreateOrderTest()
        {
            Order order = new Order();

            Assert.IsNotNull(order);
        }

        [Test]
        public void OrderLineCanBeConstructedTest()
        {
            OrderLine orderLine = new OrderLine();

            Assert.IsNotNull(orderLine);
        }

        [Test]
        public void AddOrderLineCanBeDoneTest()
        {

            //Arrange
            Department dep = new Department();
            Order order = new Order(dep);
            OrderLine orderLine = new OrderLine();

            //Act
            orderLine.Quantity = 20;
            order.AddOrderLine(orderLine);
            order.ChangeAmount(orderLine, 10);

            //Assert
            Assert.AreEqual(order.OrderLines.First(), orderLine);
        }

    }
}

