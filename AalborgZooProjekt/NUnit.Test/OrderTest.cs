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
        #region Commom Actions
        private Order MakeOrder()
        {
            Mock<IOrderRepository> MockRep = new Mock<IOrderRepository>();
            Department dep = new Department();
            Order order = new Order(MockRep.Object, dep);
            return order;
        }

        private OrderLine MakeOrderLine()
        {
            return new OrderLine();
        }

        private Zookeeper MakeZookeeper()
        {
            return new Zookeeper();
        }
        private ProductVersion MakeProductVersion()
        {
            return new ProductVersion();
        }
        #endregion

        [Test]
        public void AddOrderLine_ValidOrderLine_CanBeAdded()
        {
            //Arrange
            Order order = MakeOrder();
            OrderLine firstOrderLine = MakeOrderLine();
            firstOrderLine.Quantity = 20;
            OrderLine secondOrderLine = MakeOrderLine();
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
            Order order = MakeOrder();
            OrderLine orderLine = MakeOrderLine();
            orderLine.Quantity = 20;

            //Act
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
            Order order = MakeOrder();
            OrderLine orderLine = MakeOrderLine();
            orderLine.Quantity = 20;
            order.OrderLines.Add(orderLine);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => order.ChangeAmount(orderLine, amount));
        }

        [Test]
        public void ChangeUnit_ValidInput_UnitChanged()
        {
            //Arrange
            Order order = MakeOrder();
            OrderLine orderline = MakeOrderLine();

            ProductVersion ProductVersion = MakeProductVersion();
            List<Unit> units = new List<Unit>()
            {
                new Unit()
                {
                    Name = "kg",
                    Id = 2
                }
            };
            
            ProductVersion.Unit = units;
            orderline.ProductVersion = ProductVersion;
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
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;

            Order order = MakeOrder();

            //Act
            order.AttachZookeeperToOrder(zookeeper);

            //Assert
            Assert.AreEqual(zookeeper.Id, order.OrderedByID);
        }

        [Test]
        public void AttackZookeeperToOrder_ZookeeperAlreadyAttached_ExceptionThrown()
        {
            //Arrange
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;

            Order order = MakeOrder();

            //Act
            order.AttachZookeeperToOrder(zookeeper);

            //Assert
            Assert.Throws<ZookeeperAllReadyAddedException>(() => order.AttachZookeeperToOrder(zookeeper));
        }

        [TestCase("Under Construction", ExpectedResult = true)]
        [TestCase("Sent", ExpectedResult = true)]
        public bool CanOrderBeChanged_ValidStatus_CanBeChanged(string status)
        {
            //Arrange
            Order order = MakeOrder();
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
            ProductVersion ProductVersion = MakeProductVersion();
            ProductVersion.IsActive = true;
            Order order = MakeOrder();
            OrderLine orderline = MakeOrderLine();

            //Act
            order.ChangeProduct(orderline, ProductVersion);

            //Arrange
            Assert.AreEqual(ProductVersion, orderline.ProductVersion);
        }

        [Test]
        public void ChangeProduct_DeactivatedProductVersion_ExceptionThrown()
        {
            //Arrange   
            ProductVersion productVersion = MakeProductVersion();
            productVersion.IsActive = false;
            Order order = MakeOrder();
            OrderLine orderline = MakeOrderLine();

            //Act and Assert
            Assert.Throws<ProductVersionIsNotActiveException>(() => order.ChangeProduct(orderline, productVersion));
        }

        [Test]
        public void CanOrderBeSend_None_CanBeSend()
        {
            //Arrange
            Order order = MakeOrder();
            order.OrderLines.Add(MakeOrderLine());
            order.Status = "Under Construction";
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;
            order.OrderedByID = zookeeper.Id;

            //Act
            bool result = order.CanOrderBeSend();

            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void CanOrderBeSend_WrongStatus_ExceptionThrown()
        {
            //Arrange
            Order order = MakeOrder();
            order.OrderLines.Add(MakeOrderLine());
            order.Status = "Sent";
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;
            order.OrderedByID = zookeeper.Id;

            //Act And Arrange
            Assert.Throws<OrderIsNotUnderAnSendableStateException>(() => order.CanOrderBeSend());
            
        }

        [Test]
        public void CanOrderBeSend_NoZookeeperAttached_ExceptionThrown()
        {
            //Arrange
            Order order = MakeOrder();
            order.OrderLines.Add(MakeOrderLine());
            order.Status = "Under Construction";
           
            //Act And Arrange
            Assert.Throws<OrderCanNotSendNoZookeeperException>(() => order.CanOrderBeSend());
        }

        [Test]
        public void CanOrderBeSend_NoOrderlines_ExceptionThrown()
        {
            //Arrange
            Order order = MakeOrder();
            order.Status = "Under Construction";
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;
            order.OrderedByID = zookeeper.Id;
            
            //Act And Arrange
            Assert.Throws<CanNotSendEmptyOrderException>(() => order.CanOrderBeSend());
        }

        [Test]
        public void SendOrder_CanBeSendWork_OrderCanBeSend()
        {
            //Arrange

            Order order = MakeOrder();
            order.OrderLines.Add(MakeOrderLine());
            order.Status = "Under Construction";
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;
            order.OrderedByID = zookeeper.Id;
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
            Order order = MakeOrder();
            OrderLine orderline = MakeOrderLine();
            order.OrderLines.Add(orderline);

            //Act
            order.RemoveOrderLine(orderline);

            //Assert
            Assert.IsTrue(!order.OrderLines.Contains(orderline));
        }

    }
}

