using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Database;
using GalaSoft.MvvmLight;

namespace AalborgZooProjekt.ViewModel
{
    class ZookeeperViewModel : ViewModelBase, IZookeeperViewModel
    {
        public void ChangeNumberForOrderLines(OrderLine orderline)
        {
            throw new NotImplementedException();
        }

        public void ChangeUnitForOrderLine(OrderLine orderline, Unit unit)
        {
            throw new NotImplementedException();
        }

        public void CheckIfOrderIsAllowedToBeFilledOut(Order order)
        {
            throw new NotImplementedException();
        }

        public void CheckIfOrderIsAllowedToBeSent(Order order)
        {
            throw new NotImplementedException();
        }

        public void CheckIfProductIsActive(Product product)
        {
            throw new NotImplementedException();
        }

        public void CloseProgram()
        {
            throw new NotImplementedException();
        }

        public Order GetCurrentOrder()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderFromHistory()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrderHistory()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public List<Zookeeper> GetZookeepers()
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void SearchForProduct(List<Product> productList, string search)
        {
            throw new NotImplementedException();
        }
    }
}
