using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;

namespace AalborgZooProjekt.ViewModel
{
    public class Populater
    {
        static public void PopulateDatabase()
        {
            using (var db = new AalborgZooContainer1())
            {
                for (int i = 5; i < 10; i++)
                {
                    Employee emp = new Employee()
                    {
                        DateHired = DateTime.Today,
                        Name = $"Emp{i}",
                        DateStopped = DateTime.Today,
                    };
                    db.EmployeeSet.Add(emp);

                    Model.Database.Product prod = new Model.Database.Product()
                    {
                        CreatedByID = i,
                        DateDeleted = DateTime.Today,
                        DateCreated = DateTime.Today,
                        DeletedByID = i,
                        Name = "Banan",
                    };
                    db.ProductSet.Add(prod);

                    Model.Database.Department dep = new Model.Database.Department()
                    {
                        Name = "Sydamerika",
                        DateDeleted = DateTime.Today,
                        DateCreated = DateTime.Today,
                    };
                    db.DepartmentSet.Add(dep);

                    Model.Database.Zookeeper zookeeper = new Model.Database.Zookeeper()
                    {
                        Name = "Tuan",
                        DateHired = DateTime.Today,
                        DateStopped = DateTime.Today,
                        DepartmentId = dep.Id,
                    };
                    db.EmployeeSet.Add(zookeeper);

                    //We only want a single kg instance.
                    Model.Database.Unit unit;
                    if (db.UnitSet.Any())
                    {
                        unit = db.UnitSet.First();
                    }
                    else
                    {
                        unit = new Model.Database.Unit()
                        {
                            Name = "kg",
                        };
                        db.UnitSet.Add(unit);
                    }

                    Model.Database.ProductVersion prodV = new Model.Database.ProductVersion()
                    {
                        IsActive = true,
                        Supplier = i.ToString(),
                        CreatedByID = i,
                        DateCreated = DateTime.Today,
                        ProductId = prod.Id,
                        Name = "Banan",
                        Product = prod,
                    };
                    prodV.Units.Add(unit);
                    db.ProductVersionSet.Add(prodV);

                    db.SaveChanges();

                    Model.Database.Shopper shopper = new Model.Database.Shopper()
                    {
                        DateHired = DateTime.Today,
                        DateStopped = DateTime.Today,
                        Name = i.ToString(),
                        Password = i.ToString(),
                        Username = i.ToString()
                    };
                    db.EmployeeSet.Add(shopper);

                    Model.Database.ShoppingList list = new Model.Database.ShoppingList()
                    {
                        CreatedByID = i,
                        DateCreated = DateTime.Today,
                        Status = "Editable",
                        ShopperId = shopper.Id,
                    };
                    db.ShoppingListSet.Add(list);


                    Model.Database.Order order = new Model.Database.Order()
                    {
                        DepartmentID = dep.Id,
                        OrderedByID = zookeeper.Id,
                        DateOrdered = DateTime.Today,
                        DateCancelled = DateTime.Today,
                        Note = i.ToString(),
                        DateCreated = DateTime.Today,
                        DeletedByID = shopper.Id,
                        Status = i,
                        ShoppingListId = 0,
                    };
                    db.OrderSet.Add(order);



                    Model.Database.OrderLine orderLine = new Model.Database.OrderLine()
                    {

                        Quantity = i,
                        UnitID = unit.Id,
                        ProductVersionId = prodV.Id,
                    };
                    db.OrderLineSet.Add(orderLine);

                    Model.Database.PasswordChanged pwc = new Model.Database.PasswordChanged()
                    {
                        DateChanged = DateTime.Today,
                        ShopperId = shopper.Id,
                    };
                    db.PasswordChangedSet.Add(pwc);

                    Model.Database.DepartmentSpecificProduct depSpecificProduct = new Model.Database.DepartmentSpecificProduct()
                    {
                        ProductId = prod.Id,
                        DepartmentId = dep.Id
                    };
                    db.DepartmentSpecificProductSet.Add(depSpecificProduct);

                    db.SaveChanges();

                }
            }
        }
    }
}
