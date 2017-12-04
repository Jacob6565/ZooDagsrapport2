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
        public string DateCreated { get; set; }
        public string Note { get; set; }
        public string OrderedByID { get; set; }
        public string Status { get; set; }

        public Order(string dateCreated)
        {
            this.DateOrdered = "Yet to be Ordered";
            this.DateCancelled = "Not Cancelled";
            this.DateCreated = dateCreated.ToString();
            this.OrderedByID = "Rikke";
            this.Note = "No attached note";
            this.Status = "At the office";
        }

        public Order(string note, string dateCreated)
        {
            this.DateOrdered = "Yet to be ordered";
            this.DateCancelled = "Not Cancelled";
            this.DateCreated = dateCreated.ToString();
            this.Note = note;
            this.Status = "At the office";
        }

        public Order(string dateCancelled, string note, string dateCreated, string status)
        {
            this.DateOrdered = "Not Ordered";
            this.DateCancelled = dateCancelled;
            this.DateCreated = dateCreated.ToString();
            this.Note = note;
            this.Status = status;
        }

        public Order(string dateOrdered, string dateCancelled, string note, string dateCreated, string status)
        {
            this.DateOrdered = dateOrdered;
            this.DateCancelled = dateCancelled;
            this.DateCreated = dateCreated.ToString();
            this.Note = note;
            this.Status = status;
        }

        public void CancelOrder()
        {
            this.DateCancelled = DateTime.Today.ToString();
        }

        public string NoteToOrder()
        {
            return this.Note = "Send ASAP";
        }

        public void RemoveOrderedBySetDepartmentInstead()
        {
            this.OrderedByID = "Sydamerika";
        }
    }
}
