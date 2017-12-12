using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public interface IProductDAL
    {
        void AddProduct(Product product);
        void ProductVersionList(Product product);
        List<Product> GetDepartmentProducts(Department department);
    }
}
