using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    class OrderlineUniter
    {
        public List<OrderWrapper> QuantityPerProduct = new List<OrderWrapper>();

        public void Sort()
        {
            QuantityPerProduct.Sort();
        }
    }

    class OrderWrapper : IComparable
    {
        public OrderWrapper(int quantity, Unit unit, ProductVersion prodV)
        {
            Quantity = quantity;
            OrderedUnit = unit;
            ProdV = prodV;
        }

        public ProductVersion ProdV;
        public int Quantity;
        public Unit OrderedUnit;

        public int CompareTo(object obj)
        {
            return ProdV.Name.CompareTo(obj.ToString());
        }

        public override string ToString()
        {
            return ProdV.Name;
        }
    }

    public class OrderHistoryWrapper : INotifyPropertyChanged
    {
        public OrderHistoryWrapper(Order order, bool hasFruit, bool hasOther)
        {
            OrderHistory = order;
            HasFruit = hasFruit;
            HasOther = hasOther;
        }

        public OrderHistoryWrapper(Order order) : this(order, false, false)
        {

        }

        private Order _order;
        public Order OrderHistory
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged("HistoryOrders");
            }
        }

        private bool _hasFruit;
        public bool HasFruit { get => _hasFruit; set { _hasFruit = value; } }

        private bool _hasOther;
        public bool HasOther { get => _hasOther; set { _hasOther = value; } }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
