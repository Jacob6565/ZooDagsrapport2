using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using AalborgZooProjekt.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;

namespace AalborgZooProjekt.ViewModel
{
    public class ZookeeperViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZookeeperViewModel()
        {
            AddAmount = new RelayCommand<object>(ChangeAmount);
            DepOrderLines = GetDepProductListFromDb();
        }

        private Department department;
        private IProductRepository dbProductRep = new ProductRepository();

        private ObservableCollection<OrderLine> _depOrderLines = new ObservableCollection<OrderLine>();
        public ObservableCollection<OrderLine> DepOrderLines
        {
            get
            {
                return _depOrderLines;
            } 
            private set
            {
                _depOrderLines = value;
            }
        }

        public BindingList<OrderLine> DepOrderList { get; set; } = new BindingList<OrderLine>();

        /// <summary>
        /// Gets the department specific product for a given department, via the 
        /// database repository for Product
        /// </summary>
        ObservableCollection<OrderLine> GetDepProductListFromDb()
        {
            ObservableCollection<OrderLine> orderlines = new ObservableCollection<OrderLine>();

            department = new Department()
            {
                Name = "Sydamerika"
            };
            var prodList = dbProductRep.GetDepartmentProductsWithUnits(department);
            
            foreach (Product product in prodList)
            {
                OrderLine tempOrderLine = new OrderLine();

                tempOrderLine.ProductVersion = product.ProductVersions.Last();

                orderlines.Add(tempOrderLine);
            }

            if (orderlines.Count == 0)
                throw new Exception();


            return orderlines;
        }

        public RelayCommand<object> AddAmount { get; set; }
        public RelayCommand<object> SubtractAmount { get; set; } 

        public void ChangeAmount(object context)
        {
            StackPanel sp = context as StackPanel;
            TextBox box = sp.Children.OfType<TextBox>().First();
            int intAmount = Int32.Parse(box.Text);
            if (sp.Children.OfType<Button>().First().IsFocused)
            {
                intAmount++;
            }
            else intAmount--;
            box.Text = intAmount.ToString();

            ChangeOrderList(box);
        }


        public void ChangeOrderList(TextBox tb)
        {
            OrderLine ol = tb.DataContext as OrderLine;
            if(!DepOrderList.Contains(ol))
            {
                DepOrderList.Add(ol);
                RaisePropertyChanged("added");
            }
        }
    }
}

