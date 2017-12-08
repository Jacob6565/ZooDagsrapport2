using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using AalborgZooProjekt.Database;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace AalborgZooProjekt
{
    public class Owner
    {
        public int ID;
        public string Name;

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class House
    {
        public int ID;
        public Owner HouseOwner;
    }

    public class DummyViewModel : ViewModelBase
    {
        private List<Model.DummyProduct> _dummyFruit = new List<Model.DummyProduct>();
        public List<Model.DummyProduct> DummyFruitList
        {
            get { return _dummyFruit; }
            set { _dummyFruit = value; }
        }

        private List<Model.DummyProduct> _dummyOtherFood = new List<Model.DummyProduct>();
        public List<Model.DummyProduct> DummyOtherFoodList
        {
            get { return _dummyOtherFood; }
            set { _dummyOtherFood = value; }
        }

        public List<Model.DummyOrder> DummyOrderList { get; set; } = new List<Model.DummyOrder>();

        ObservableCollection<Unit> DummyUnitList = new ObservableCollection<Unit>();


        public DummyViewModel()
        {
            DummyUnitList.Add(new Unit() { Name = "kg" });
            DummyUnitList.Add(new Unit() { Name = "styks" });
            DummyUnitList.Add(new Unit() { Name = "kasse(r)" });

            //PopulateDatabase();
            string[] lines = File.ReadAllLines("../../DummyFruit.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                Model.DummyProduct dummyProduct = new Model.DummyProduct(product);
                dummyProduct.Units = DummyUnitList;
                DummyFruitList.Add(dummyProduct);
            }

            lines = File.ReadAllLines("../../DummyOtherFood.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                Model.DummyProduct dummyProduct = new Model.DummyProduct(product);
                dummyProduct.Units = DummyUnitList;
                DummyOtherFoodList.Add(dummyProduct);
            }

            lines = File.ReadAllLines("../../DummyOrders.txt");
            foreach (string orders in lines)
            {
                DummyOrderList.Add(new Model.DummyOrder(orders));
            }


        }

        public void PopulateDatabase()
        {
            using (var db = new AalborgZooContainer1())
            {
                for (int i = 0; i < 5; i++)
                {
                    Employee emp = new Employee() { DateHired = i.ToString(), Name = $"Emp{i}", DateStopped = i + 1.ToString(), Id = i };
                    db.EmployeeSet.Add(emp);

                    Database.DummyProduct prod = new Database.DummyProduct()
                    {
                        CreatedByID = i.ToString(),
                        DateDeleted = i + 1.ToString(),
                        DateCreated = i.ToString(),
                        DeletedByID = i.ToString(),
                        Name = i.ToString(),
                    };
                    db.ProductSet.Add(prod);

                    Department dep = new Department()
                    {
                        Name = i.ToString(),
                        DateDeleted = i + 1.ToString(),
                        DateCreated = i.ToString(),
                    };
                    db.DepartmentSet.Add(dep);

                    DepartmentSpecificProduct depSP = new DepartmentSpecificProduct()
                    {
                        DepartmentId = dep.Id,
                        ProductId = prod.Id,
                    };
                    db.DepartmentSpecificProductSet.Add(depSP);

                    Zookeeper zookeeper = new Zookeeper()
                    {
                        Name = i.ToString(),
                        DateHired = i.ToString(),
                        DateStopped = i.ToString(),
                        DepartmentId = dep.Id,
                    };
                    db.EmployeeSet.Add(zookeeper);

                    ProductVersion prodV = new ProductVersion()
                    {
                        IsActive = 1.ToString(),
                        Supplier = i.ToString(),
                        CreatedByID = i + 1.ToString(),
                        DateCreated = i.ToString(),
                        ProductId = prod.Id,
                    };
                    db.ProductVersionSet.Add(prodV);

                    db.SaveChanges();

                    Shopper shopper = new Shopper()
                    {
                        DateHired = i.ToString(),
                        DateStopped = i.ToString() + i.ToString(),
                        Name = i.ToString(),
                        Password = i.ToString(),
                        Username = i.ToString()
                    };
                    db.EmployeeSet.Add(shopper);

                    ShoppingList list = new ShoppingList()
                    {
                        CreatedByID = i.ToString(),
                        DateCreated = i.ToString(),
                        Status = i.ToString(),
                        ShopperId = shopper.Id,
                    };
                    db.ShoppingListSet.Add(list);


                    Database.DummyOrder order = new Database.DummyOrder()
                    {
                        DepartmentID = dep.Id.ToString(),
                        OrderedByID = zookeeper.Id.ToString(),
                        DateOrdered = i.ToString(),
                        DateCancelled = i.ToString(),
                        Note = i.ToString(),
                        DateCreated = i.ToString(),
                        DeletedByID = 0.ToString(),
                        Status = i.ToString(),
                        ShoppingListId = 0,
                    };
                    db.OrderSet.Add(order);


                    //We only want a single kg instance.
                    Unit unit;
                    if(db.UnitSet.Any())
                    {
                        unit = db.UnitSet.First();
                    }
                    else
                    {
                        unit = new Unit()
                        {
                            Name = "kg",
                        };
                        db.UnitSet.Add(unit);
                    }

                    OrderLine orderLine = new OrderLine()
                    {

                        Quantity = i.ToString(),
                        UnitID = i + 1.ToString(),
                        ProductVersionId = prodV.Id,
                    };
                    db.OrderLineSet.Add(orderLine);

                    PasswordChanged pwc = new PasswordChanged()
                    {
                        DateChanged = i.ToString(),
                        ShopperId = shopper.Id,
                    };
                    db.PasswordChangedSet.Add(pwc);

                    db.SaveChanges();
                }
            }
        }
    }
}