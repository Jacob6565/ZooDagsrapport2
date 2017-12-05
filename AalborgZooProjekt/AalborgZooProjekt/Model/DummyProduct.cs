using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProduct
    {

        public DummyProduct(string name)
        {
            Name = name;
            Unit = "kg";
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _unit;

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }


    }
}
