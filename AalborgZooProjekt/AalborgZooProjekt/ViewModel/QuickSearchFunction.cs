using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.ViewModel
{
    class QuickSearchFunction
    {
        /// <summary>
        /// Used to link a variable called distance to a product.
        /// </summary>
        class TempProduct
        {
            public OrderLine orderline;
            public int distance;
        }

        /// <summary>
        /// Contains all the products with .
        /// </summary>
        private List<TempProduct> tempOrderlines = new List<TempProduct>();

        /// <summary>
        /// Fills data into the list of TempProducts.
        /// </summary>
        private void MakeTempProducts(BindingList<OrderLine> orderline)
        {
            for (int i = 0; i < orderline.Count; i++)
            {
                tempOrderlines.Add(new TempProduct
                {
                    orderline = orderline[i],
                    distance = 0
                });
            }
        }
        
        public BindingList<OrderLine> SortProducts(string searchString, BindingList<OrderLine> products)
        {
            //Initialization method.
            MakeTempProducts(products);

            //Calculates the Levenshtein distance for each product          
            foreach (TempProduct b in tempOrderlines)
            {
                b.distance = LevenshteinDistance(searchString, b.orderline.ProductVersion.Name);
            }

            //Return the list of products sorted by the Levenshtein distance.
            List<OrderLine> temporaryList = tempOrderlines.OrderBy(x => x.distance).Select(x => x.orderline).ToList();
            return new BindingList<OrderLine>(temporaryList);            
        }

        public static BindingList<OrderLine>Sorting(string searchString, BindingList<OrderLine> products)
        {
            List<OrderLine> temp = new List<OrderLine>();
            searchString = searchString.ToLower();

            temp = products.ToList().Where(x => x.ProductVersion.Name.ToLower().StartsWith(searchString)).Select(x=> x).ToList();

            temp.AddRange(products.ToList().Where(x => x.ProductVersion.Name.ToLower().Contains(searchString) && !x.ProductVersion.Name.ToLower().StartsWith(searchString)).ToList());

            return new BindingList<OrderLine>(temp);
        }

        /// <summary>
        /// Calculates the levenshtein distance between string a and string b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int LevenshteinDistance(string a, string b)
        {
            int cost, min1, min2, min3;
            int[,] d = new int[a.Length + 1, b.Length + 1];

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

            for (int i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (int i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (int i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (int j = 1; j <= d.GetUpperBound(1); j += 1)
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
