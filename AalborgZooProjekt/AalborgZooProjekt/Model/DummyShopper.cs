using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class DummyShopper : DummyEmployee
    {

        public DummyShopper() : base 
        {

        }

        private string Username { get; set; }
        private string Password { get; set; }
    }
}
