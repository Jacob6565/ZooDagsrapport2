using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public interface IShoppingListRepository
    {
        ShoppingList GetActiveShoppingList();
        void Update(ShoppingList shoppingList);
        void Add(ShoppingList shoppingList);
    }
}
