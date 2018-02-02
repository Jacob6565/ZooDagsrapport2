using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    public class ReadFromExcelFile
    {
        private List<string> ReadExcelFile(string path)
        {
            List<string> StringsInExcelFile = new List<string>();
            using (var reader = new StreamReader(path, Encoding.UTF7))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    StringsInExcelFile.Add(line);
                }
            }
            return StringsInExcelFile;
        }

        public List<Product> GetProductsFromExcelIntoDatabase(string FilePath)
        {
            //Variables we need to store information in.
            List<Product> products = new List<Product>();
            string category = "";
            List<Unit> units = new List<Unit>();

            //Reading all the lines from the excel file.
            List<string> values = ReadExcelFile(FilePath);            

            //This is what we do for each of the lines in the excel file.
            foreach (string s in values)
            {
                //Since the excelfile is .csv we split with ';'.
                string[] entries = s.Split(';');

                //If the second entry in the excel file is null or whitespace it's not a product
                //but the line that contains the category aswell as the units for the following products.
                if (!String.IsNullOrWhiteSpace(entries[1]))
                {
                    category = entries[0];
                    units = new List<Unit>();
                    for (int i = 1; i < entries.Length; i++)
                    {
                        units.Add(new Unit(entries[i]));
                    }
                }
                else
                {
                    string name = entries[0];

                    //The products gets added to the database in the constructor.

                    //Jeg ved ikke helt med constructoren, da jeg ikke ved hvilken Shopper jeg skal sende med.
                    //Product temp = new Product("Shopper missing", name, "Not defined", units);

                    //products.Add(temp);
                }
            }
            return products;
        }
    }
}
