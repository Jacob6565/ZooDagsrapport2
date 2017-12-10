using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    /// <summary>
    /// The class which Product.cs uses when it needs to 
    /// </summary>
    public class ProductDAL : IProductDAL
    {
        private AalborgZooContainer1 _context;

        public ProductDAL()
        {
            _context = new AalborgZooContainer1();
        }

        public void AddProduct(Product product)
        {
            using (_context)
            {
                _context.ProductSet.Add(product);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Opdates the product and thereby also the ProductVersions contained in the product objekt.
        /// </summary>
        /// <param name="product"></param>
        public void ProductVersionList(Product product)
        {

            using (_context)
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
