using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using AalborgZooProjekt.Database;
using System.Data.Entity;

namespace SearchAlgoritmeLibrary
{
    public class Levenshtein
    {
        /// <summary>
        /// Used to link a variable called distance to a product.
        /// </summary>
        class TempProduct
        {
            public Product product;
            public int distance;
        }
        /// <summary>
        /// Stores all the products from the database.
        /// </summary>
        List<Product> products = new List<Product>();

        /// <summary>
        /// Contains all the products with .
        /// </summary>
        private List<TempProduct> tempProducts = new List<TempProduct>();

        /// <summary>
        /// Reads products from database.
        /// </summary>
        private void FetchProductsFromDB()
        {
            using (var db = new AalborgZooContainer1())
            {
                //Ved ikke helt om det skal være ProductVersionSet eller ProductSet.               
                products = db.ProductSet.Select(x => x).ToList();
            }
        }

        /// <summary>
        /// Fills data into the list of TempProducts.
        /// </summary>
        private void makeTempProducts()
        {
            for (int i = 0; i < products.Count; i++)
            {
                tempProducts[i].product = products[i];
                tempProducts[i].distance = 0;
            }
        }

        /// <summary>
        /// Finds products which names reminiscent of the inputstring
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Product> FindPossibleProducts(string searchString)
        {
            //Initialization methods.
            FetchProductsFromDB();
            makeTempProducts();

            //Calculates the Levenshtain distance for each products name            
            foreach (TempProduct b in tempProducts)
            {
                b.distance = LevenshteinDistance(searchString, b.product.Name);
            }
            //Return the list of products sorted by the Levenshtein distance.
            return tempProducts.OrderBy(x => x.distance).Select(x => x.product).ToList();
            
        }

        /// <summary>
        /// Calculates the levenshtein distance between string a and string b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            Int32 cost;
            Int32[,] d = new int[a.Length + 1, b.Length + 1];
            Int32 min1;
            Int32 min2;
            Int32 min3;

            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));
                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];
        }
    }
}
