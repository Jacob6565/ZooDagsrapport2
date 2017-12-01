using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class Order
    {
        public string DateOrdered { get; set; }
        public string DateCancelled { get; set; }
        public string Note { get; set; }
        public string DateCreated { get; set; }
        public string Status { get; set; }

        public Order(string dateOrdered, string dateCancelled, string note, string dateCreated, string Status)
        {

        }
    }
}
