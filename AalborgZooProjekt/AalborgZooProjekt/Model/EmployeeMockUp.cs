using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public abstract class EmployeeMockUp
    {
        public int DateHired { get; set; }
        public string Name { get; set; }
        public int DateStopped { get; set; } 

        public EmployeeMockUp(int dateHired, string name, int dateStopped)
        {
            this.DateHired = dateHired;
            this.Name = name;
            this.DateStopped = dateStopped;
        }
        
    }
}
