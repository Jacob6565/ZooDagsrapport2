using Moq;
using System;
using System.Linq;
using NUnit.Framework;
using AalborgZooProjekt.Model;
using System.Collections.Generic;
using AalborgZooProjekt.Interfaces;

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
            order.AddOrderLine(orderLine);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => order.ChangeAmount(orderLine, amount));
        }

        [Test]
        public void ChangeUnit_ValidInput_UnitChanged()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            OrderLine orderline = new OrderLine();
            //det virker åbenbart ikke hvis det er mock
            ProductVersion mockProductVersion = new ProductVersion();
            List<Unit> units = new List<Unit>()
            {
                new Unit()
                {
                    Name = "kg",
                    Id = 2
                }
            };
            
            mockProductVersion.Unit = units;
            orderline.ProductVersion = mockProductVersion;
            order.AddOrderLine(orderline);           
            //Act
            order.ChangeUnit(orderline, units.Last());

            //Assert
            Assert.AreEqual(units.Last().Id, order.OrderLines.Last().UnitID);
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

        [Test]
        public void ChangeProduct_ActiveProductVersion_ProductChanged()
        {
            //Arrange   
            Mock<ProductVersion> mockProductVersion = new Mock<ProductVersion>();
            mockProductVersion.Object.IsActive = true;
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            OrderLine orderline = new OrderLine();

            //Act
            order.ChangeProduct(orderline, mockProductVersion.Object);

            //Arrange
            Assert.AreEqual(mockProductVersion.Object, orderline.ProductVersion);
        }

        [Test]
        public void ChangeProduct_DeactivatedProductVersion_ExceptionThrown()
        {
            //Arrange   
            Mock<ProductVersion> mockProductVersion = new Mock<ProductVersion>();
            mockProductVersion.Object.IsActive = false;
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            OrderLine orderline = new OrderLine();

            //Act and Assert
            Assert.Throws<ProductVersionIsNotActiveException>(() => order.ChangeProduct(orderline, mockProductVersion.Object));
        }

        [Test]
        public void CanOrderBeSend_None_CanBeSend()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            order.OrderLines.Add(new OrderLine());
            order.Status = "Under Construction";
            Mock<Zookeeper> mockZookeeper = new Mock<Zookeeper>();
            mockZookeeper.Object.Id = 2;
            order.OrderedByID = mockZookeeper.Object.Id;

            //Act
            bool result = order.CanOrderBeSend();

            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void CanOrderBeSend_WrongStatus_ExceptionThrown()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            order.OrderLines.Add(new OrderLine());
            order.Status = "Sent";
            Mock<Zookeeper> mockZookeeper = new Mock<Zookeeper>();
            mockZookeeper.Object.Id = 2;
            order.OrderedByID = mockZookeeper.Object.Id;

            //Act And Arrange
            Assert.Throws<OrderIsNotUnderAnSendableStateException>(() => order.CanOrderBeSend());
            
        }

        [Test]
        public void CanOrderBeSend_NoZookeeperAttached_ExceptionThrown()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            order.OrderLines.Add(new OrderLine());
            order.Status = "Under Construction";
           
            //Act And Arrange
            Assert.Throws<OrderCanNotSendNoZookeeperException>(() => order.CanOrderBeSend());
        }

        [Test]
        public void CanOrderBeSend_NoOrderlines_ExceptionThrown()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            order.Status = "Under Construction";
            Mock<Zookeeper> mockZookeeper = new Mock<Zookeeper>();
            mockZookeeper.Object.Id = 2;
            order.OrderedByID = mockZookeeper.Object.Id;
            
            //Act And Arrange
            Assert.Throws<CanNotSendEmptyOrderException>(() => order.CanOrderBeSend());
        }

        [Test]
        public void SendOrder_CanBeSendWork_OrderCanBeSend()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            order.OrderLines.Add(new OrderLine());
            order.Status = "Under Construction";
            Mock<Zookeeper> mockZookeeper = new Mock<Zookeeper>();
            mockZookeeper.Object.Id = 2;
            order.OrderedByID = mockZookeeper.Object.Id;
            //Making mock of shoppinglist because we only need it to keep data and use at input to function
            //Therefore we don't make a real one because it will interact with the database when data gets stored.
            ShoppingList mockShoppingList = new ShoppingList();
            
            //Act
            order.SendOrder(mockShoppingList);

            //Assert
            Assert.AreEqual("Sent", order.Status);
            Assert.AreEqual(order, mockShoppingList.Orders.Last());
        }

        [Test]
        public void RemoveOrderLine_ValidOrderLine_CanBeRemoved()
        {
            //Arrange
            Mock<IOrderRepository> mockRep = new Mock<IOrderRepository>();
            Order order = new Order(mockRep.Object, new Department());
            OrderLine orderline = new OrderLine();
            order.OrderLines.Add(orderline);

            //Act
            order.RemoveOrderLine(orderline);

            //Assert
            Assert.IsTrue(!order.OrderLines.Contains(orderline));
        }

    }
}

