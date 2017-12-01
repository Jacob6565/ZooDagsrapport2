using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class ProductVersionDummy
    {
        public bool IsActive { get; set; }
            
        public ProductVersionDummy(bool isActive)
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
