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
        #region Commom Actions

        Mock<IOrderRepository> MockRep;
        private Order MakeOrder()
        {
            MockRep = new Mock<IOrderRepository>();
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
        public void CreateOrder_NotDefaultConstructor_GetsCreated()
        {
            Order order = MakeOrder();
            Assert.IsNotNull(order);
            MockRep.Verify(x => x.AddOrder(order), Times.Once());

        }

        [Test]
        public void OrderLineCanBeConstructedTest()
        {
            OrderLine orderLine = new OrderLine();

            Assert.IsNotNull(orderLine);
        }
       

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

            //Asserting whether the functions on the mocked object gets called.
            MockRep.Verify(x => x.UpdateOrder(order), Times.Exactly(2));
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
            
            //Assert
            Assert.AreEqual(15, orderLine.Quantity);
            MockRep.Verify(x => x.UpdateOrder(order), Times.Once());
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
            MockRep.Verify(x => x.UpdateOrder(order), Times.Exactly(0));
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
            
            ProductVersion.Units = units;
            orderline.ProductVersion = ProductVersion;
            order.OrderLines.Add(orderline);           

            //Act
            order.ChangeUnit(orderline, units.Last());

            //Assert
            Assert.AreEqual(units.Last().Id, order.OrderLines.Last().UnitID);
            MockRep.Verify(x => x.UpdateOrder(order), Times.Once());
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
            MockRep.Verify(x => x.UpdateOrder(order), Times.Once());
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
            MockRep.Verify(x => x.UpdateOrder(order), Times.Exactly(1));

        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = false)]
        public bool CanOrderBeChanged_StatusIsString_IfValidReturnsTrueElseFalse(int status)
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
            MockRep.Verify(x => x.UpdateOrder(order), Times.Once());

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
            MockRep.Verify(x => x.UpdateOrder(order), Times.Exactly(0));

        }

        [Test]
        public void CanOrderBeSend_None_CanBeSend()
        {
            //Arrange
            Order order = MakeOrder();
            order.OrderLines.Add(MakeOrderLine());
            order.Status = 0; //UnderContruktion
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
            order.Status = 1; //Sent
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
            order.Status = 0;
           
            //Act And Arrange
            Assert.Throws<OrderCanNotSendNoZookeeperException>(() => order.CanOrderBeSend());
        }

        [Test]
        public void CanOrderBeSend_NoOrderlines_ExceptionThrown()
        {
            //Arrange
            Order order = MakeOrder();
            order.Status = 0;
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;
            order.OrderedByID = zookeeper.Id;
            
            //Act And Arrange
            Assert.Throws<CanNotSendEmptyOrderException>(() => order.CanOrderBeSend());
        }

        [Test]
        public void SendOrder_CanBeSendWorks_OrderCanBeSend()
        {
            //Arrange
            Order order = MakeOrder();
            order.OrderLines.Add(MakeOrderLine());
            order.Status = 0;
            Zookeeper zookeeper = MakeZookeeper();
            zookeeper.Id = 2;
            order.OrderedByID = zookeeper.Id;
            ShoppingList shoppingList = new ShoppingList();
            
            //Act
            //Det er denne som giver fejl
            order.SendOrder(shoppingList);

            //Assert
            Assert.AreEqual(1, order.Status);
            Assert.AreEqual(order, shoppingList.Orders.Last());
            MockRep.Verify(x => x.UpdateOrder(order), Times.Once());
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
            MockRep.Verify(x => x.UpdateOrder(order), Times.Once());

        }

    }
}

