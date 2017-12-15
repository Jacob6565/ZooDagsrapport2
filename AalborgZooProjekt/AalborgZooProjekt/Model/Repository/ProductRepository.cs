using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    /// <summary>
    /// The class which Product.cs uses when it needs to 
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        public void AddProduct(Product product)
        {
            using (var _context = new AalborgZooContainer1())
            {
                _context.ProductSet.Add(product);
                _context.SaveChanges();
            }
        }


        /// <summary>
        /// Gets all product from the database that are departmentspecicproduct to a given department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<Product> GetDepartmentProducts(Department department)
        {
            List<Product> departmentProductList = new List<Product>();
            using (var _context = new AalborgZooContainer1())
            {
                foreach (DepartmentSpecificProduct depProduct in _context.DepartmentSpecificProductSet)
                {
                    if (depProduct.Product.CheckIfProductIsActive() && String.Equals(depProduct.Department.Name,department.Name))
                        departmentProductList.Add(depProduct.Product);
                }
            }

            return departmentProductList;
        }

        public List<Product> GetDepartmentProductsWithUnits(Department department)
        {
            List<Product> departmentProductList = new List<Product>();
            using (var _context = new AalborgZooContainer1())
            {
                foreach (DepartmentSpecificProduct depProduct in _context.DepartmentSpecificProductSet.Include("Product.ProductVersions.Units"))
                {
                    if (depProduct.Product.CheckIfProductIsActive() && String.Equals(depProduct.Department.Name, department.Name))
                        departmentProductList.Add(depProduct.Product);
                }
            }

            return departmentProductList;
        }

        public ICollection<Unit> GetProductUnits(Product product)
        {
            using (var _context = new AalborgZooContainer1())
            {
                return _context.ProductSet.FirstOrDefault(x => x.Id == product.Id).ProductVersions.Last().Units;
            }
        }

        /// <summary>
        /// Opdates the product and thereby also the ProductVersions contained in the product objekt.
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProductVersionList(Product product)
        {
            using (var _context = new AalborgZooContainer1())
            {
                //Finds the current product in the database.
                Product Outdated = _context.ProductSet.SingleOrDefault(x => x.Id == product.Id);
                if (Outdated != null)
                {
                    //Replaces it with the new version.
                    Outdated = product;

                    _context.SaveChanges();
                }
                else
                {
                    throw new ProductDoNotExistInDatabaseException(product.Name);
                }
            }
        }
    }
}