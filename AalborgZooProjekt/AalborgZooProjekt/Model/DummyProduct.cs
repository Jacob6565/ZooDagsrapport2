using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProduct
    {

        public DummyProduct(string name)
        {
            Name = name;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /*
        private string _unit;

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        */

        public DummyProductVersion ProductVersion { get; set; }


        public void ActivateProduct(DummyProduct product)
        {
            if (!product.ProductVersion.IsActive)
            {
                product.ProductVersion.IsActive = true;
            }
        }

        public void DeactivateProduct(DummyProduct product)
        {
            if (product.ProductVersion.IsActive)
            {
                product.ProductVersion.IsActive = false;
            }
        }

        public void RemoveUnit(DummyProduct product)
        {
            product.ProductVersion.Unit.Name = string.Empty;
        }

    }
}
