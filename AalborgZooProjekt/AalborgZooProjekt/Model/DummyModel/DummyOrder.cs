using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model.DummyModel
{
    class DummyOrder
    {
        public DummyOrder(string good)
        {
            Good = good;
        }

        public string Good;

        public List<Tuple<string, double>> Orders = new List<Tuple<string, double>>();
    }
}
