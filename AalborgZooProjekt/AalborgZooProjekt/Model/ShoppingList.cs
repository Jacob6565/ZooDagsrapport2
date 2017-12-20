using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Interfaces;

namespace AalborgZooProjekt.Model
{
    public partial class ShoppingList : IShoppingList
    {
        public ShoppingList(IShoppingListRepository repository) : this()
        {
            ShoppingListRep = repository;
            DateCreated = DateTime.Now;
            Status = _underConstruction;
        }

        private const int _underConstruction = 0;
        
        public IShoppingListRepository ShoppingListRep { get; private set; }

        public void AddOrder(Order order)
        {
            this.Orders.Add(order);
        }
    }
}
