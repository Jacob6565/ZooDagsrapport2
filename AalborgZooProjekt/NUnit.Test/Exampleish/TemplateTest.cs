using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AalborgZooProjekt;
using AalborgZooProjekt.ViewModel;
using AalborgZooProjekt.Model;

/* To do test driven development (tdm) use tests to drive the development of the code.
 * Write a test then, run it, secure that it fails the first time. Then implement the 
 * simplest solution that makes the test pass, then write a more elaborate test, until
 * the functionallity is sufficient.
 */


namespace NUnit.Test
{
    [TestFixture]
    public class TestViewModel
    {
        /* Tested that the TestThisClass returns 0 when you don't add a number 
         * And it does as the variable is "number" is initialized to 0 */

        [Test]
        public void TestAdd0ToTTCReturns0()
        {
            // Arrange
            TestThisClass ttc = new TestThisClass(new Adder1For1());
            // Act           
            // Assert
            Assert.AreEqual(0, ttc.GetNumer);
        }

        /* Now we want to be able to add a number to TestThisClass so we have to implement a 
         * simple solution. Im gonna implement a add number function */    
         
        /* The test then fails first time as nothing has been implemented, then we can start to 
         * implement the solution a bit better. */
        [Test]
        public void TestAdd1ToTTCReturns1()
        {
            // Arrange
            TestThisClass ttc = new TestThisClass(new Adder1For1());
            // Act           
            ttc.AddNumber(1);
            // Assert
            Assert.AreEqual(1, ttc.GetNumer);
        }

        /* What if we wanted to be able to double each number added in the same function */
        [Test]
        public void TestAdd1ToTTCReturns2()
        {
            // Arrange
            TestThisClass ttc = new TestThisClass(new Adder1For2());
            // Act           
            ttc.AddNumber(1);
            // Assert
            Assert.AreEqual(2, ttc.GetNumer);
        }

        /* A method to test what if it works if you add numbers multiple times with 1for1*/

        [Test]
        public void TestAdd3ToTTCReturns3()
        {
            // Arrange
            TestThisClass ttc = new TestThisClass(new Adder1For1());
            // Act           
            ttc.AddNumber(1);
            ttc.AddNumber(1);
            ttc.AddNumber(1);
            // Assert
            Assert.AreEqual(3, ttc.GetNumer);
        }


        [Test]
        public void TestAdd3ToTTCReturns6()
        {
            // Arrange
            TestThisClass ttc = new TestThisClass(new Adder1For2());
            // Act           
            ttc.AddNumber(1);
            ttc.AddNumber(1);
            ttc.AddNumber(1);
            // Assert
            Assert.AreEqual(6, ttc.GetNumer);
        }


    }
}
