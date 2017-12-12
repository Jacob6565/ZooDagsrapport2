using Moq;
using System.Linq;
using NUnit.Framework;
using AalborgZooProjekt.Model;
using System;

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
        //Forstår ikke hvorfor den tager så lang tid.
        public void AddOrderLine_ValidOrderLine_CanBeAdded()
        {
            //Arrange
            Mock<IOrderRepository> MockRep = new Mock<IOrderRepository>();
            Mock<Department> mockdep = new Mock<Department>();
            Order order = new Order(MockRep.Object, mockdep.Object);
            OrderLine firstOrderLine = new OrderLine();
            firstOrderLine.Quantity = 20;
            OrderLine secondOrderLine = new OrderLine();
            secondOrderLine.Quantity = 40;

            //Act
            order.AddOrderLine(firstOrderLine);
            order.AddOrderLine(secondOrderLine);

            //Assert
            Assert.AreEqual(order.OrderLines.Last(), secondOrderLine);
        }

        [Test]
        public void ChangeAmount_ValidAmount_CanBeChanged()
        {
            //Arrange
            Mock<IOrderRepository> MockRep = new Mock<IOrderRepository>();
            Mock<Department> mockdep = new Mock<Department>();
            Order order = new Order(MockRep.Object, mockdep.Object);
            OrderLine orderLine = new OrderLine();

            //Act
            orderLine.Quantity = 20;
            order.ChangeAmount(orderLine, 15);
            order.ChangeAmount(orderLine, 10);

            //Assert
            Assert.AreEqual(10, orderLine.Quantity);
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(0)]
        public void ChangeAmount_InvalidAmount_ExceptionThrown(int amount)
        {
            //Arrange
            Mock<IOrderRepository> MockRep = new Mock<IOrderRepository>();
            //Mocking deparment because its contructor interacts with database
            Mock<Department> mockdep = new Mock<Department>();

            Order order = new Order(MockRep.Object, mockdep.Object);
            OrderLine orderLine = new OrderLine();

            orderLine.Quantity = 20;

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => order.ChangeAmount(orderLine, amount));
        }

        [Test]
        public void ChangeUnit_ValidInput_UnitChanged()
        {
            
        }

        [Test]
        public void AttackZookeeperToOrder_ValidZookeeper_ZookeeperAdded()
        {
            //Arrange
            Mock<Zookeeper> mockZookeeper = new Mock<Zookeeper>();
            mockZookeeper.Object.Id = 2;

            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());

            //Act
            order.AttachZookeeperToOrder(mockZookeeper.Object);

            //Assert
            Assert.AreEqual(mockZookeeper.Object.Id, order.OrderedByID);
        }

        [Test]
        public void AttackZookeeperToOrder_ZookeeperAlreadyAttached_ExceptionThrown()
        {
            //Arrange
            Mock<Zookeeper> mockZookeeper = new Mock<Zookeeper>();
            mockZookeeper.Object.Id = 2;

            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());

            //Act
            order.AttachZookeeperToOrder(mockZookeeper.Object);

            //Assert
            Assert.Throws<ZookeeperAllReadyAddedException>(() => order.AttachZookeeperToOrder(mockZookeeper.Object));
        }
        [TestCase("Under Construction", ExpectedResult = true)]
        [TestCase("Sent", ExpectedResult = true)]
        public bool CanOrderBeChanged_ValidStatus_CanBeChanged(string status)
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            order.Status = status;

            //Act
            bool returnValue = order.CanOrderBeChanged();

            //Assert
            return returnValue;
        }
    }
}

