using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AalborgZooProjekt.Model
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order);
        Order GetOrder(int orderID);
        void UpdateOrder(Order order);
        Order GetUnfinishedOrder(Department department);
        List<Order> GetOrdersWithNoShoppinglist();
        ShoppingList AddToShoppingList(List<Order> orders, int shopperId);
        List<OrderHistoryWrapper> GetOrdersFromDepartment(int departmentID);
    }
}
