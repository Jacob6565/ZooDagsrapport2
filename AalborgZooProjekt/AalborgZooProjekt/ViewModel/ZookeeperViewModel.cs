using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using AalborgZooProjekt.Model;
using System.Collections.ObjectModel;

namespace AalborgZooProjekt.ViewModel
{
    public class ZookeeperViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZookeeperViewModel()
        {
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
            var prodList = dbProductRep.GetDepartmentProducts(department);
            
            foreach (Product product in prodList)
            {
                OrderLine tempOrderLine = new OrderLine();
                tempOrderLine.ProductVersion = product.ProductVersions.Last();

                orderlines.Add(tempOrderLine);
            }

            if (orderlines.Count == 0)
                throw new Exception();

            orderlines.First().ProductVersion.Unit.First().

            return orderlines;
        }
    }
}

