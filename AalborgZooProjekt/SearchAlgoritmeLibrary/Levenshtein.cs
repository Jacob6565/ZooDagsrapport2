using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SearchAlgoritmeLibrary
{
    public class Levenshtein
    {
        class TempProduct
        {
            public string Product;//Product i rigtige tilfælde
            public int distance;           
        }

        private List<TempProduct> _dummyProducts = new List<TempProduct>();

        private List<string> names = new List<string>()
        {
            "Æbler",
            "Pærer",
            "Agurk",
            "Banan",
            "Solsikkekerner",
            "Havregryn",
            "Salat",
            "Grønkål",
            "Tomater",
            "Kartofler",
            "Broccoli",
        };

        private void makeDummyProducts()
        {
            for(int i = 0; i < names.Count; i++)
            {
                _dummyProducts[i].Product = names[i];
                _dummyProducts[i].distance = 0;
            }
        }

        public List<string> FindPossibleProducts(string searchString)
        {
            List<string> Products = new List<string>();
            foreach (TempProduct b in _dummyProducts)
            {
                b.distance = LevenshteinDistance(searchString, b.Product);
            }
            
            Products = _dummyProducts.OrderBy(x => x.distance).Select(x => x.Product).ToList();
            return Products;
        }

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

        private int LookALike()
        {
            return 0;
        }

    }
}
