using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        void UpdateProductVersionList(Product product);
        List<Product> GetDepartmentProducts(Department department);
        List<Product> GetDepartmentProductsWithUnits(Department department);
        ICollection<Unit> GetProductUnits(Product product);
        List<Product> GetAllProducts();
    }
}
