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
        void MakeProduct(int employeeID, string name); // done - not tested

        // Search database for a specific order and see if it exist in the database
        bool CheckIfProductExist(Product product); // done - not tested

        // Takes in an Order and removes it from the database
        void DeleteOrder(Order order); // done - not tested

        // Takes a Product and removes it from the the database
        void DeleteProduct(Product product); // done - not tested

        //Return the ID of the shopper
        int GetID();
    }
}
