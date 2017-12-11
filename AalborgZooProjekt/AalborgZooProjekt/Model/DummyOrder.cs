using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public class DummyOrder : INotifyPropertyChanged
    {
        public DummyOrder(string name)
        {
            Name = name;
        }

        public int Id{ get; set; }
        public string Name { get; set; }
        public int Amount { get; set; } = 0;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
