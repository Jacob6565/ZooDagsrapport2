using AalborgZooProjekt.Interfaces;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;
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
            //Burde sådan noget har ikke være i et repository.
            var db = new AalborgZooContainer1();

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
            var db = new AalborgZooContainer1();

            order = db.OrderSet.Where(x => x.Id == order.Id).First();
            db.OrderSet.Remove(order);
        }

        public void DeleteProduct(Product product)
        {
            var db = new AalborgZooContainer1();

            product = db.ProductSet.Where(x => x.Id == product.Id).First();
            db.ProductSet.Remove(product);
            //I really want to test this
        }

        public int GetID()
        {
            return this.Id;
        }


        public void MakeProduct(int employeeID, string name)
        {
            //Burde sådan noget har ikke være i et repository.
            var db = new AalborgZooContainer1();

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
