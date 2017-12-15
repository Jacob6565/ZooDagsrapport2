﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model.Database;

namespace AalborgZooProjekt.Model
{
    public class ShoppinglistRepository : IShoppingListRepository
    {
        public void Add(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }

        public ShoppingList GetActiveShoppingList()
        {
            using(var context = new AalborgZooContainer1())
            {
                ShoppingList shopList = context.ShoppingListSet.Include("Orders").Last();

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