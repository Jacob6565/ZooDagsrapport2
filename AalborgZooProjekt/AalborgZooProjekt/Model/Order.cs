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

        public Order(string note, string dateCreated)
        {
            this.DateOrdered = "Not Ordered";
            this.DateCancelled = "Not Cancelled";
            this.Note = note;
            this.DateCreated = dateCreated.ToString();
            this.Status = "At the office";
        }

        public Order(string dateCancelled, string note, string dateCreated, string status)
        {
            this.DateOrdered = "Not Ordered";
            this.DateCancelled = dateCancelled;
            this.Note = note;
            this.DateCreated = dateCreated.ToString();
            this.Status = status;
        }

        public Order(string dateOrdered, string dateCancelled, string note, string dateCreated, string status)
        {
            this.DateOrdered = dateOrdered;
            this.DateCancelled = dateCancelled;
            this.Note = note;
            this.DateCreated = dateCreated.ToString();
            this.Status = status;
        }

        public void CancelOrder()
        {
            this.DateCancelled = DateTime.Today.ToString();
        }
    }
}
