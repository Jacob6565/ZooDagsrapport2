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
            DepOrderLines = GetDepProductListFromDb();

            SetupOrder();

        }

        //The order that are being created/edited in the Zookeeper view
        private Order _orderInTheMaking;
        public Order OrderInTheMaking
        {
            get { return _orderInTheMaking; }
            private set { _orderInTheMaking = value; }
        }

        /// <summary>
        /// Responsible for setting up the order, if a unfinished order exist it will be loaded, otherwise there will
        /// be created a new order.
        /// </summary>
        private void SetupOrder()
        {
            Order unfinishedOrder = dbOrderRep.GetUnfinishedOrder(department);

            if (unfinishedOrder == null)
            {
                OrderInTheMaking = new Order(department);
            }
            else if (unfinishedOrder != null)
            {
                OrderInTheMaking = unfinishedOrder;
            }

        }

        //Simulates a chosen departement with the needed information for the current implemented functionality
        private Department department = new Department() {Name = "Sydamerika", Id = 100, DateCreated = DateTime.Now};

        //Product repository which is used as a data access layer between Product and database in model
        private IProductRepository dbProductRep = new ProductRepository();

        //Order repository which is used as a data access layer between Product and database in model
        private IOrderRepository dbOrderRep = new OrderRepository();


        /*This bindinglist is used in view to illustrate the departmentspecific products for the chosen department*/
        private BindingList<OrderLine> _depOrderLines = new BindingList<OrderLine>();
        public BindingList<OrderLine> DepOrderLines { get {return _depOrderLines; } private set {_depOrderLines = value; }}

        public BindingList<OrderLine> DepOrderList { get; set; } = new BindingList<OrderLine>();

        /// <summary>
        /// Gets the department specific product for a given department, via the 
        /// database repository for Product
        /// </summary>
        BindingList<OrderLine> GetDepProductListFromDb()
        {
            BindingList<OrderLine> orderlines = new BindingList<OrderLine>();

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


            return orderlines;
        }

        public void ChangeAmount(object context)
        {
            StackPanel sp = context as StackPanel;
            TextBox box = sp.Children.OfType<TextBox>().Single();
            int intAmount = Int32.Parse(box.Text);
            OrderLine ol = box.DataContext as OrderLine;
            if (sp.Children.OfType<Button>().First().IsFocused)
            {
                ol.ChangeQuantity++; 
            }
            else
            {
                ol.ChangeQuantity--;
            }
            ChangeOrderList(box);
        }

        private RelayCommand<object> _addCommand;
        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand
                    ?? (_addCommand = new RelayCommand<object>(ChangeAmount));
            }
        }

        private RelayCommand<object> _subtractCommand;
        public RelayCommand<object> SubtractCommand
        {
            get
            {
                return _subtractCommand
                    ?? (_subtractCommand = new RelayCommand<object>(ChangeAmount, CanChangeAmount));
            }
        }


        public bool CanChangeAmount(object context)
        {
            StackPanel sp = context as StackPanel;
            OrderLine ol = sp.DataContext as OrderLine;
            if (ol != null)
            {
                return ol.Quantity != 0;
            }
            else return false;
        }


        public void ChangeOrderList(TextBox tb)
        {
            OrderLine ol = tb.DataContext as OrderLine;
            if (ol.Quantity > 0 && !DepOrderList.Contains(ol))
            {
                DepOrderList.Add(ol);
            }
            else if (ol.Quantity == 0 && DepOrderList.Contains(ol))
            {
                DepOrderList.Remove(ol);
            }
        }

        /// <summary>
        /// A bool that describes if it is legal to send the order, (this bool is bound the sendorder button in view)
        /// </summary>
        private bool _canBeSend = true;
        public bool CanBeSend
        {
            get { return _canBeSend; }
            private set { _canBeSend = value; }
        }

        /// <summary>
        /// Functionality for actually sending an order, if it is legal it will be added to the database and added to
        /// the current shoppinglist
        /// </summary>
        /// <param name="order"></param>
        public void Sendorder(Order order)
        {
            if (CanBeSend)
            {
                CanBeSend = false;
                order.SendOrder();
                OrderInTheMaking = new Order() {DepartmentID = department.Id };
                throw new Exception();
            }
        }

        /// <summary>
        /// The button that sends an order in the view
        /// </summary>
        private RelayCommand _sendOrderCommand;
        public RelayCommand SendOrderCommand
        {
            get
            {
                return _sendOrderCommand
                  ?? (_sendOrderCommand = new RelayCommand(
                    () =>
                    {
                        Sendorder(OrderInTheMaking);   
                    }));
            }
        }
    }
}