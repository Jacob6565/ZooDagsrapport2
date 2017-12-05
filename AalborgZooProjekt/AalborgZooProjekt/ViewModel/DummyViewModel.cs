using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace AalborgZooProjekt
{
    public class DummyViewModel : ViewModelBase
    {
        private List<DummyProduct> _dummyFood = new List<DummyProduct>();

        public List<DummyProduct> DummyFoodList
        {
            get { return _dummyFood; }
            set { _dummyFood = value; }
        }

        public List<DummyOrder> DummyOrderList { get; set; } = new List<DummyOrder>();

        public DummyViewModel()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "aalborgzoo.database.windows.net";
            builder.UserID = ConfigurationManager.AppSettings["dbUserId"];
            builder.Password = ConfigurationManager.AppSettings["dbUserPwd"];
            builder.InitialCatalog = "AalborgZooFoder";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
            }

            string[] lines = File.ReadAllLines("../../DummyProducts.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                DummyFoodList.Add(new DummyProduct(product));
            }
            lines = File.ReadAllLines("../../DummyOrders.txt");
            foreach (string orders in lines)
            {
                DummyOrderList.Add(new DummyOrder(orders));
            }


        }
    }
}