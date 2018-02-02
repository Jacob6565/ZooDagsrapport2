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
using System.Windows.Controls.Primitives;
using AalborgZooProjekt.View.ZooKeeper;

namespace AalborgZooProjekt.ViewModel
{
    public class ZookeeperViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ZookeeperViewModel()
        {
            if (false)
                Populater.DeleteDatabase();

            if (false)
                Populater.PopulateDatabase();

            //Load departments from database
            Departments = new BindingList<Department>(dbDepartmentRep.GetDepartments());

            //Temporary, should get department from view, as you log in
            _department = DepartmentFromName("Sydamerika");

            //Load zookeepers from department
            DepZookeeperList = new BindingList<Zookeeper>(GetDepartmentZookeepers(_department));

            SetupOrder();

            //AllOrderLines is used to contain the original orderlines.
            AllOrderLines = GetDepProductListFromDb().ToList();

            //DepOrderLines is the used to represent orderlines in the view.
            //And will be modified upon using QuickSearch.
            DepOrderLines = new BindingList<OrderLine>(AllOrderLines);
        }

        public List<Product> AllProductsInCatalog = new List<Product>();

        //I setter så gøre sådan at en funktion som opdatere DepOrderlines bliver kaldt.
        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }

            set
            {
                searchString = value;
                DepOrderLines = SortDepOrderLines(searchString);
                OnPropertyChanged("DepOrderLines");
                OnPropertyChanged("SearchString");
            }
        }

        internal void SetNewContent(UserControl _content)
        {
            ContentWindow = _content;
        }

        private UserControl _content;
        public UserControl ContentWindow
        {
            get
            {
                if (_content == null)
                {
                    FeedTab ft = new FeedTab();
                    ft.DataContext = this;
                    SetNewContent(ft);
                }
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged("ContentWindow");
            }
        }

        private BindingList<OrderLine> SortDepOrderLines(string sortstring)
        {
            //AllOrderLines contains the original list, and will therefore
            //provide the same starting point for the sort each time.
            BindingList<OrderLine> temp = new BindingList<OrderLine>(AllOrderLines);

            temp = QuickSearchFunction.Sorting(sortstring, temp);

            return temp;
        }

        public static void UnitChanged(object sender, object newUnit)
        {
            ComboBox comboBoxItem = (ComboBox)sender;
            OrderLine parent = (OrderLine)comboBoxItem.DataContext;

            parent.ChangeUnit = (Unit)newUnit;
        }

        private string _selectedUnit;
        public string SelectedUnit
        {
            get { return _selectedUnit; }
            set { _selectedUnit = value; OnPropertyChanged("Unit"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //The order that are being created/edited in the Zookeeper view
        private Order _orderInTheMaking;
        public Order OrderInTheMaking
        {
            get { return _orderInTheMaking; }
            private set { _orderInTheMaking = value; }
        }

        /// <summary>
        /// Temporary way of "choosing" a department in the program
        /// </summary>
        /// <param name="name"></param>
        Department DepartmentFromName(string name)
        {
            foreach (Department department in Departments)
            {
                if (string.Equals(name, department.Name))
                    return department;
            }

            return null;
        }

        //Simulates a chosen departement with the needed information for the current implemented functionality
        private Department _department = new Department();
        public Department Department
        {
            get { return _department; }
            set
            {
                if (value != null)
                    _department = value;
                else
                    throw new NullReferenceException();
            }
        }

        //Product repository which is used as a data access layer to access Product in database
        private IProductRepository dbProductRep = new ProductRepository();

        //Order repository which is used as a data access layer to access Order in database
        private IOrderRepository dbOrderRep = new OrderRepository();

        //Department repository which is used as a data access layer to access Department in database
        private IDepartmentRepository dbDepartmentRep = new DeparmentRepository();

        //This is used to store all the OrderLines.
        public List<OrderLine> AllOrderLines { get; set; } = new List<OrderLine>();

        /*This bindinglist is used in view to illustrate the departmentspecific products for the chosen department*/
        public BindingList<OrderLine> DepOrderLines { get; set; } = new BindingList<OrderLine>();
        public BindingList<OrderLine> DepOrderList { get; set; } = new BindingList<OrderLine>();

        public BindingList<Department> _departments = new BindingList<Department>();
        public BindingList<Department> Departments
        {
            get
            {
                return _departments;
            }
            private set
            {
                _departments = value;
            }
        }

        List<Zookeeper> GetDepartmentZookeepers(Department department)
        {
            return department.Zookeepers.ToList();
        }

        private BindingList<Zookeeper> _depZookeeperList = new BindingList<Zookeeper>();
        public BindingList<Zookeeper> DepZookeeperList
        {
            get
            {
                return _depZookeeperList;
            }
            set
            {
                _depZookeeperList = value;
            }
        }

        public Zookeeper ActiveZookeeper { get; private set; }

        public TextBox OrderNote { get; set; } = new TextBox();
        /// <summary>
        /// Responsible for setting up the order, if a unfinished order exist it will be loaded, otherwise there will
        /// be created a new order.
        /// </summary>
        private void SetupOrder()
        {
            Order unfinishedOrder = dbOrderRep.GetUnfinishedOrder(_department);

            if (unfinishedOrder == null)
            {
                OrderInTheMaking = new Order(_department);
            }
            else if (unfinishedOrder != null)
            {
                OrderInTheMaking = unfinishedOrder;
            }

        }

        /// <summary>
        /// Gets the department specific product for a given department, via the 
        /// database repository for Product
        /// </summary>
        BindingList<OrderLine> GetDepProductListFromDb()
        {
            BindingList<OrderLine> orderlines = new BindingList<OrderLine>();
            var prodList = dbProductRep.GetDepartmentProductsWithUnits(_department);

            foreach (Product product in prodList)
            {
                OrderLine tempOrderLine = new OrderLine();

                tempOrderLine.ProductVersion = product.ProductVersions.Last();

                orderlines.Add(tempOrderLine);
            }

            return orderlines;
        }

        private ObservableCollection<OrderHistoryWrapper> _historyOrders;
        public ObservableCollection<OrderHistoryWrapper> HistoryOrders
        {
            get
            {
                return _historyOrders ?? new ObservableCollection<OrderHistoryWrapper>(new OrderRepository().GetOrdersFromDepartment(1));
            }

            set
            {
                _historyOrders = value;
                OnPropertyChanged("HistoryOrders");
            }
        }

        private ObservableCollection<OrderHistoryWrapper> UpdateHistoryOrders(int departmentID)
        {
            return new ObservableCollection<OrderHistoryWrapper>(new OrderRepository().GetOrdersFromDepartment(1));
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

        public void DeleteSearch(object context) => SearchString = "";

        public bool CanBeDeleted(object context) => String.IsNullOrEmpty(searchString) ? false : true;

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

        private RelayCommand<object> _deleteSearchCommand;
        public RelayCommand<object> DeleteSearchCommand
        {
            get
            {
                return _deleteSearchCommand
                    ?? (_deleteSearchCommand = new RelayCommand<object>(DeleteSearch, CanBeDeleted));
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
        /// A bool that describes if it is legal to send the order, this bool is bound the sendorder button in view)
        /// </summary>
        private bool _canBeSend = true;
        public bool CanBeSend
        {
            get { return _canBeSend; }
            private set { _canBeSend = value; }
        }


        /// <summary>
        /// Assembles order by adding the orderLines and zookeeper chosen in view
        /// </summary>
        private void AssembleOrder()
        {
            OrderInTheMaking.OrderLines = DepOrderList;
            OrderInTheMaking.AttachZookeeperToOrder(ActiveZookeeper);
            OrderInTheMaking.Note = OrderNote.Text;

        }


        /// <summary>
        /// Functionality for actually sending an order, if it is legal it will be added to the database and added to
        /// the current shoppinglist
        /// </summary>
        /// <param name="order"></param>
        public async void Sendorder(object context)
        {
            if (CanBeSend)
            {
                //Assembles order so it can be send
                AssembleOrder();

                CanBeSend = false;
                await Task.Run(() => OrderInTheMaking.SendOrder());

                //Clears the order and the view
                OrderInTheMaking = new Order(_department);
                ClearOrders(context);
                CanBeSend = true;
                await Task.Run(() => HistoryOrders = UpdateHistoryOrders(1));
            }
        }

        public bool CanSendOrder(object context)
        {
            if (IsZookeeperChosen() && DepOrderList.Count != 0)
            {
                return true;
            }
            return false;
        }

        private RelayCommand<object> _sendOrderCommand;
        public RelayCommand<object> SendOrderCommand
        {
            get
            {
                return _sendOrderCommand
                    ?? (_sendOrderCommand = new RelayCommand<object>(Sendorder, CanSendOrder));
            }
        }

        private RelayCommand<Zookeeper> _zookeeperChosen;
        public RelayCommand<Zookeeper> ZookeeperChosen
        {
            get
            {
                return _zookeeperChosen
                    ?? (_zookeeperChosen = new RelayCommand<Zookeeper>(ZookeeperSelected));
            }
        }

        public void ZookeeperSelected(Zookeeper zookeeper)
        {
            ActiveZookeeper = zookeeper;
        }
        public bool IsZookeeperChosen()
        {
            return ActiveZookeeper != null;
        }

        private void ClearOrders(object context)
        {
            foreach (OrderLine ol in DepOrderList)
            {
                ol.ChangeQuantity = 0;
            }
            DepOrderList.Clear();
            OrderNote.Text = "";
            ////TODO Clear Radiobuttons properly
        }

        private RelayCommand<object> _changeDepSpecificProducts;
        public RelayCommand<object> ChangeDepSpecificProducts => _changeDepSpecificProducts
                    //Jeg ved godt at dette ikke er den rigtige måde, men det var bare lige en midlertidlig løsning :D
                    ?? (_changeDepSpecificProducts = new RelayCommand<object>((x) => new DepartmentSpecifikList().Show()));

        private RelayCommand<OrderHistoryWrapper> _historyEntryChosen;
        public RelayCommand<OrderHistoryWrapper> HistoryEntryChosen
        {
            get
            {
                return _historyEntryChosen
                    ?? (_historyEntryChosen = new RelayCommand<OrderHistoryWrapper>(ShowHistoryEntry));
            }
        }


        private void RemoveProductFromDepartmentSpecificList(object context)
        {
            var selectedItems = context as List<ProductVersion>;

        }
        private bool CanRemoveProductFromDepartmentSpecificList(object context)
        {
            return true;
        }

        private RelayCommand<object> _removeProductFromDepartmentSpecificListCommand;
        public RelayCommand<object> RemoveProductFromDepartmentSpecificListCommand =>
            _removeProductFromDepartmentSpecificListCommand ??
            (_removeProductFromDepartmentSpecificListCommand =
            new RelayCommand<object>(RemoveProductFromDepartmentSpecificList, CanRemoveProductFromDepartmentSpecificList));
        public void ShowHistoryEntry(OrderHistoryWrapper entrySelected)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.DataContext = entrySelected;
            historyWindow.ShowDialog();
        }
    }
}