using AalborgZooProjekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Interfaces
{
    public interface IShopper
    {
        // Needs to take virables that is needed to create a Product
        void MakeProduct();

        // Search database for a specific order and see if it exist in the database
        void CheckIfOrderExist(Order order);

        // Takes in an Order and removes it from the database
        void DeleteOrder(Order order);

        // Takes a Product and removes it from the the database
        void DeleteProduct(Product product);
    }
}
