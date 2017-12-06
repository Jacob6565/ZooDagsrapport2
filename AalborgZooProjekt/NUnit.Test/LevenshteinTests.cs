using System;
using System.Text;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using SearchAlgoritmeLibrary;
using AalborgZooProjekt.Database;
using System.Collections.Generic;

namespace NUnit.Test.Exampleish
{
    [TestFixture]
    class LevenshteinTest
    {
        [Test]
        public void FindPossibleProductsTest()
        {            
            Levenshtein levenshtein = new Levenshtein();
            List<Product> tempProducts = new List<Product>(5)
            {
                new Product()
                {
                    Name = "agurk"
                },
                new Product()
                {
                    Name = "æble"
                },
                new Product()
                {
                    Name = "banan"
                },
                new Product()
                {
                    Name = "pærer"
                }
            };

            Assert.AreEqual("æble", levenshtein.FindPossibleProducts("æb", tempProducts).First().Name);
        }
    }
}
