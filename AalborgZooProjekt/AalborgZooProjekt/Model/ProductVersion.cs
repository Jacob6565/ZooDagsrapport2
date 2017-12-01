using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class ProductVersion
    {
        public bool IsActive { get; set; }
        public string Supplier { get; set; }
        public string DateCreated { get; set; }

        public ProductVersion(bool isActive, string supplier, string dateCreated)
        {
            this.IsActive = isActive;
            this.Supplier = supplier;
            this.DateCreated = dateCreated;
        }
    }
}
