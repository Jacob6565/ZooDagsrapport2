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
    public class OrderTester
    {
        [TestCase()]
        public void CancelOrder()
        {
            var dateCreated = DateTime.Today.ToString();

            Order testOrder = new Order(dateCreated);

            testOrder.CancelOrder();
            Assert.AreEqual(DateTime.Today.ToString(), testOrder.DateCancelled);
        }

        [TestCase()]
        public void AddNoteToOrder()
        {
            var dateCreated = DateTime.Today.ToString();


            Order testOrder = new Order(dateCreated);
            testOrder.NoteToOrder();

            Assert.IsNotEmpty(testOrder.Note);
        }

        //[TestCase()]
        //public void AutoSaveOrder()
        //{
        //    DateTime date = new DateTime(2, 12, 2017);
        //    var dateCreated = date.ToString();

        //    Order testOrder = new Order(dateCreated);

        //    Assert.IsTrue();
        //}

        [TestCase()]
        public void RemoveEmployeeFromOrder()
        {
            var dateCreated = DateTime.Today.ToString();

            Order testOrder = new Order(dateCreated);

            testOrder.RemoveOrderedBySetDepartmentInstead();
            Assert.AreEqual("Sydamerika", testOrder.OrderedByID);
        }

        [TestCase()]
        public void AddEmployeeToOrder()
        {
            var dateCreated = DateTime.Today.ToString();

            Order testOrder = new Order(dateCreated);
        }
    }
}
