﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Interfaces;

namespace AalborgZooProjekt.Model
{
    public partial class ShoppingList : IShoppingList
    {
        public void AddOrder(Order order)
        {
            this.Orders.Add(order);
        }
    }
}
