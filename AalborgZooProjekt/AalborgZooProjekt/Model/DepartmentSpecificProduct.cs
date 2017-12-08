using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public partial class DepartmentSpecificProduct
    {
        public DepartmentSpecificProduct(Department department, Product product)
        {
            this.Department = department;
            this.Product = product;
        }
    }
}
