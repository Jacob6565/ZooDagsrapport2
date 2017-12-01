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
            var ordered = DateTime.Today.AddDays(-1).ToString();
            var created = DateTime.Today.AddDays(-4).ToString();

            Order order = new Order("Careful of the bananas", created);

            order.CancelOrder();
            Assert.AreEqual(DateTime.Today.ToString(), order.DateCancelled);
        }

    }
}
