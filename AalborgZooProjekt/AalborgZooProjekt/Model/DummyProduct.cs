using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProduct
    {
        public DummyProduct(string product)
        {
            Name = product;
        }

        public string Name { get; set; }
    }
}
