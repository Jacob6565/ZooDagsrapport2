using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public abstract class DummyEmployee
    {
        public DummyEmployee(string dateHired, string name, string dateStopped)
        {
            dateHired = DateHired;
            name = Name;
            dateStopped = DateStopped;
        }


        protected string DateHired { get; set; }
        protected string Name { get; set; }
        protected string DateStopped { get; set; }
    }
}
