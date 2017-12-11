using GalaSoft.MvvmLight;
using System.Collections.Generic;
using AalborgZooProjekt.Model;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System;
using AalborgZooProjekt.Model;

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

            string[] lines = File.ReadAllLines("../../Model/DummyStuff/DummyFruit.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                Model.DummyProduct dummyProduct = new Model.DummyProduct(product);
                dummyProduct.Units = DummyUnitList;
                DummyFruitList.Add(dummyProduct);
            }

            lines = File.ReadAllLines("../../Model/DummyStuff/DummyOtherFood.txt", Encoding.UTF7);
            foreach (string product in lines)
            {
                Model.DummyProduct dummyProduct = new Model.DummyProduct(product);
                dummyProduct.Units = DummyUnitList;
                DummyOtherFoodList.Add(dummyProduct);
            }

            lines = File.ReadAllLines("../../Model/DummyStuff/DummyHistoryEntries.txt");
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
                    Employee emp = new Employee()
                    {
                        DateHired = DateTime.Today,
                        Name = $"Emp{i}",
                        DateStopped = DateTime.Today,
                    };
                    db.EmployeeSet.Add(emp);

                    Product prod = new Product()
                    {
                        CreatedByID = i,
                        DateDeleted = DateTime.Today,
                        DateCreated = DateTime.Today,
                        DeletedByID = i,
                        Name = i.ToString(),
                    };
                    db.ProductSet.Add(prod);

                    Department dep = new Department()
                    {
                        Name = i.ToString(),
                        DateDeleted = DateTime.Today,
                        DateCreated = DateTime.Today,
                    };
                    db.DepartmentSet.Add(dep);

                    DepartmentSpecificProduct depSP = new DepartmentSpecificProduct(dep, prod);
                    db.DepartmentSpecificProductSet.Add(depSP);

                    Zookeeper zookeeper = new Zookeeper()
                    {
                        Name = i.ToString(),
                        DateHired = DateTime.Today,
                        DateStopped = DateTime.Today,
                        DepartmentId = dep.Id,
                    };
                    db.EmployeeSet.Add(zookeeper);

                    ProductVersion prodV = new ProductVersion()
                    {
                        IsActive = true,
                        Supplier = i.ToString(),
                        CreatedByID = i,
                        DateCreated = DateTime.Today,
                        ProductId = prod.Id,
                    };
                    db.ProductVersionSet.Add(prodV);

                    db.SaveChanges();

                    Shopper shopper = new Shopper()
                    {
                        DateHired = DateTime.Today,
                        DateStopped = DateTime.Today,
                        Name = i.ToString(),
                        Password = i.ToString(),
                        Username = i.ToString()
                    };
                    db.EmployeeSet.Add(shopper);

                    ShoppingList list = new ShoppingList()
                    {
                        CreatedByID = i,
                        DateCreated = DateTime.Today,
                        Status = i,
                        ShopperId = shopper.Id,
                    };
                    db.ShoppingListSet.Add(list);


                    Order order = new Order()
                    {
                        DepartmentID = dep.Id,
                        OrderedByID = zookeeper.Id,
                        DateOrdered = DateTime.Today,
                        DateCancelled = DateTime.Today,
                        Note = i.ToString(),
                        DateCreated = DateTime.Today,
                        DeletedByID = shopper.Id,
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

                        Quantity = i,
                        UnitID = unit.Id,
                        ProductVersionId = prodV.Id,
                    };
                    db.OrderLineSet.Add(orderLine);

                    PasswordChanged pwc = new PasswordChanged()
                    {
                        DateChanged = DateTime.Today,
                        ShopperId = shopper.Id,
                    };
                    db.PasswordChangedSet.Add(pwc);

                    db.SaveChanges();
                }
            }
        }
    }
}