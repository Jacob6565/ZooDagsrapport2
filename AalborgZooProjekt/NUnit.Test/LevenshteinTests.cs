using System;
using System.Text;
using System.Linq;
using NUnit.Framework;
using SearchAlgoritmeLibrary;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NUnit.Test.Exampleish
{
    [TestFixture]
    class LevenshteinTest
    {
        [Test]
        public void FindPossibleProducts()
        {
            
            Levenshtein levenshtein = new Levenshtein();
            Assert.AreEqual("æble", levenshtein.FindPossibleProducts("æb"));
        }
    }
}
