﻿using AalborgZooProjekt.Interfaces;
using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public partial class Shopper : IShopper
    {
        public bool CheckIfProductExist(Product product)
        {
            var db = new AalborgZooContainer();

            if (db.ProductSet.Where(x => x.Id == product.Id).First().Id == product.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteOrder(Order order)
        {
            var db = new AalborgZooContainer();

            order = db.OrderSet.Where(x => x.Id == order.Id).First();
            db.OrderSet.Remove(order);
        }

        public void DeleteProduct(Product product)
        {
            var db = new AalborgZooContainer();

            product = db.ProductSet.Where(x => x.Id == product.Id).First();
            db.ProductSet.Remove(product);
        }

        public int GetID()
        {
            return this.Id;
        }


        public void MakeProduct(int employeeID, string name)
        {
            var db = new AalborgZooContainer();

            Product newProduct = new Product()
            {
                DateCreated = DateTime.Now,
                CreatedByID = employeeID,
                Name = name
            };
            db.ProductSet.Add(newProduct);
            db.SaveChanges();
        }
    }
}
