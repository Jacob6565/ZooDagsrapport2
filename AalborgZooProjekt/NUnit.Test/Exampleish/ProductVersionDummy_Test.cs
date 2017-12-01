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
    public class ProductVersionDummy_Test
    {
        public bool IsActive { get; set; }

        public ProductVersionDummy_Test(bool isActive)
        {
            this.IsActive = isActive;
        }

        public void ActivateProduct()
        {
            if (!IsActive)
            {
                IsActive = true;
            }
        }


    }
}
