using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyProduct : INotifyPropertyChanged
    {
        public DummyProduct(string product)
        {
            Name = product;
        }

        public int Id;
        public string Name { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
        public Unit unit;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
