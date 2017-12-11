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
            GetDepProductListFromDb();
        }

        private Department department;
        private IProductDAL dbProductRep = new ProductDAL();

        private ObservableCollection<Product> depProducts;
        public ObservableCollection<Product> DepProducts { get; private set; }

        /// <summary>
        /// Gets the department specifik product for a given department, via the 
        /// database repository for Product
        /// </summary>
        void GetDepProductListFromDb()
        {
            var prodList = dbProductRep.GetDepartmentSpecifikProducts(department);
            ObservableCollection<Product> depProducts = new ObservableCollection<Product>(prodList);
        }
    }
}

