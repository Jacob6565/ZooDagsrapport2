using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.DummyModel
{
    public class DummyOrder
    {
        public DummyOrder(string good)
        {
            Good = good;
        }

        public string Good { get; set; }

        public List<OrderLine> Orders { get; set; } = new List<OrderLine>();
    }
    public class OrderLine
    {
        public OrderLine(string unit, double amount)
        {
            Unit = unit;
            Amount = amount;
        }

        public string Unit { get; set; }
        public double Amount { get; set; }
    }
}
