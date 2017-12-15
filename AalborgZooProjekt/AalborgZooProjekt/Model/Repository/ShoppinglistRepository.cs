using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model.Database;

namespace AalborgZooProjekt.Model
{
    public class ShoppinglistRepository : IShoppingListRepository
    {
        public ShoppingList GetActiveShoppingList()
        {
            using(var context = new AalborgZooContainer1())
            {
                ShoppingList shopList = context.ShoppingListSet.Include("Orders").Last();

                if (shopList != null)
                    return shopList;
                else
                    throw new NullReferenceException();
            }
        }
    }
}
