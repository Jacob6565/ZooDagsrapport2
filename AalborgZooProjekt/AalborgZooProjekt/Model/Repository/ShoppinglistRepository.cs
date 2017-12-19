using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class ShoppinglistRepository : IShoppingListRepository
    {
        public void Add(ShoppingList shoppingList)
        {
            using (var context = new AalborgZooContainer())
            {
                context.ShoppingListSet.Add(shoppingList);
                context.SaveChanges();
            }
            
        }

        public ShoppingList GetActiveShoppingList()
        {
            using(var context = new AalborgZooContainer())
            {
                ShoppingList shopList = context.ShoppingListSet.Include("Orders.OrderLines").Last();
                if (shopList != null && shopList.Status == 0)
                    return shopList;
                else
                    return null;
            }
        }

        public void Update(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }
    }
}
