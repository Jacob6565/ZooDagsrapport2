using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public partial class OrderLine : INotifyPropertyChanged
    {
        public int ChangeQuantity
        {
            get { return Quantity; }
            set
            {
                if (value >= 0)
                {
                    Quantity = value;
                    OnPropertyChanged("Quantity");
                }
                else
                {
                    Quantity = 0;
                }
            }
        }

        public Unit ChangeUnit
        {
            get { return Unit; }
            set
            {
                Unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
