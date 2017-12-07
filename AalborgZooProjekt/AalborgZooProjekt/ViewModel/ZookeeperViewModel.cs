using System;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;
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

        public void CheckIfOrderIsAllowedToBeFilledOut(Model.Database.Order order)
        {
            throw new NotImplementedException();
        }

        public void CheckIfOrderIsAllowedToBeSent(Model.Database.Order order)
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

        public Model.Database.Order GetCurrentOrder()
        {
            throw new NotImplementedException();
        }

        public Model.Database.Order GetOrderFromHistory()
        {
            throw new NotImplementedException();
        }

        public List<Model.Database.Order> GetOrderHistory()
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

        public void SaveOrder(Model.Database.Order order)
        {
            throw new NotImplementedException();
        }

        public void SearchForProduct(List<Product> productList, string search)
        {
            throw new NotImplementedException();
        }
    }
}
