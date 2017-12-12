using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model;

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

                    Product prod = new Product()
                    {
                        CreatedByID = i,
                        DateDeleted = DateTime.Today,
                        DateCreated = DateTime.Today,
                        DeletedByID = i,
                        Name = "Banan",
                    };
                    db.ProductSet.Add(prod);

                    Department dep = new Department()
                    {
                        Name = "Sydamerika",
                        DateDeleted = DateTime.Today,
                        DateCreated = DateTime.Today,
                    };
                    db.DepartmentSet.Add(dep);

                    Zookeeper zookeeper = new Zookeeper()
                    {
                        Name = "Tuan",
                        DateHired = DateTime.Today,
                        DateStopped = DateTime.Today,
                        DepartmentId = dep.Id,
                    };
                    db.EmployeeSet.Add(zookeeper);

                    //We only want a single kg instance.
                    Unit unit;
                    if (db.UnitSet.Any())
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

                    ProductVersion prodV = new ProductVersion()
                    {
                        IsActive = true,
                        Supplier = i.ToString(),
                        CreatedByID = i,
                        DateCreated = DateTime.Today,
                        ProductId = prod.Id,
                        Name = "Banan",
                        Product = prod,
                    };
                    prodV.Unit.Add(unit);
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
                        Status = "Editable",
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

                    DepartmentSpecificProduct depSpecificProduct = new DepartmentSpecificProduct(dep, prod)
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
