using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProduct
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<Units> _unit = new List<Units>();
        public  List<Units> Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public DummyProduct(string name)
        {
            Name = name;
            Unit.Add(new Units("kg", 5));
            Unit.Add(new Units("kasser", 55));
            Unit.Add(new Units("tons", 2));
        }
    }

    public class Units
    {
        public Units(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
