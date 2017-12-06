using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProductVersion
    {
        public bool IsActive { get; set; }
        public DummyUnit Unit { get; set; }

        public DummyProductVersion(bool isActive)
        {
            this.IsActive = isActive;
        }

        public DummyProductVersion(DummyUnit unit)
        {
            Unit = unit;
        }

        public void ActivateProduct(DummyProductVersion product)
        {
            if (!product.IsActive)
            {
                IsActive = true;
            }
        }

        public void DeactivateProduct(DummyProductVersion product)
        {
            if (product.IsActive)
            {
                IsActive = false;
            }
        }

        public void RemoveUnit(DummyProductVersion product)
        {
            product.Unit.Name = string.Empty;
        }

    }
}
