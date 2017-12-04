using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class DummyShopper
    {

        public DummyShopper(string username, string password)
        {
            Username = username;
            Password = password;
        }

        private string Username { get; set; }
        private string Password { get; set; }
    }
}
