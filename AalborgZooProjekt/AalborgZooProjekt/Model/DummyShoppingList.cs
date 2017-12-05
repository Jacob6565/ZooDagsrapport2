using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyShoppingList
    {
        public DummyShoppingList(string createdByID, string dateordered, string datecreated, string status)
        {
            CreatedByID = createdByID;
            DateOrdered = dateordered;
            DateCreated = datecreated;
            Status = status;
        }


        public string CreatedByID { get; set; }
        public string DateOrdered { get; set; }
        public string DateCreated { get; set; }
        public string Status      { get; set; }

    }
}
