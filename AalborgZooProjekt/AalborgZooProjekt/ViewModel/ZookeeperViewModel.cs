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
            
        }

        private Department department;
        private IProductDAL dbProductRep = new ProductDAL();

        public ObservableCollection<OrderLine> DepOrderLines
        {
            get
            {
                return GetDepProductListFromDb();
            } 
            private set { }
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

            return orderlines;
        }
    }
}

