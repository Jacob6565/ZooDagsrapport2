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
            
        public DummyProductVersion(bool isActive)
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

        public void DeactivateProduct()
        {
            if (IsActive)
            {
                IsActive = false;
            }
        }

    }
}
