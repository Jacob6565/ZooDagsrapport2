using AalborgZooProjekt.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProduct
    {
        public DummyProduct(string product)
        {
            Name = product;
        }

        public string Name { get; set; }
        public ObservableCollection<Unit> Units;
    }
}
