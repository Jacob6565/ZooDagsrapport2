using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AalborgZooProjekt.Model;
using AalborgZooProjekt.Model.Database;

namespace AalborgZooProjekt.ViewModel
{
    public static class Populater
    {
        public static AalborgZooContainer1 db = new AalborgZooContainer1();
        static public void PopulateDatabase()
        {
            using (db = new AalborgZooContainer1())
            {
                /* Shopper */

                Shopper shp = new Shopper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Name = "Tobias Palludan",
                    Password = "1234",
                    Username = "LoginName"
                };
                db.EmployeeSet.Add(shp);

                shp = new Shopper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Name = "Mikkel Jessen",
                    Password = "1234",
                    Username = "LoginName",
                };
                db.EmployeeSet.Add(shp);

                db.SaveChanges();
                /* Departments */

                Department depSydAmerika = new Department()
                {
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "Sydamerika",
                };
                db.DepartmentSet.Add(depSydAmerika);

                Department depAfrika = new Department()
                {
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "Sydamerika",
                };
                db.DepartmentSet.Add(depAfrika);

                db.SaveChanges();
                /* Zookeepers */

                Zookeeper zkp = new Zookeeper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Department = depSydAmerika,
                    Name = "Anh Tuan Truong"
                };
                db.EmployeeSet.Add(zkp);

                zkp = new Zookeeper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Department = depSydAmerika,
                    Name = "Casper Corfitz Christensen"
                };
                db.EmployeeSet.Add(zkp);

                zkp = new Zookeeper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Department = depSydAmerika,
                    Name = "Kristoffer Jensen"
                };
                db.EmployeeSet.Add(zkp);

                zkp = new Zookeeper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Department = depSydAmerika,
                    Name = "Jacob Gosch"
                };
                db.EmployeeSet.Add(zkp);

                Zookeeper zkpAfrika = new Zookeeper()
                {
                    DateHired = DateTime.Today.AddMonths(-1),
                    Department = depAfrika,
                    Name = "Afrika Medarbejder, burde ikke vises"
                };
                db.EmployeeSet.Add(zkpAfrika);

                db.SaveChanges();
                /* Product */

                Product prodBanan = new Product()
                {
                    CreatedByID = zkp.Id,
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "Banan",
                };
                db.ProductSet.Add(prodBanan);

                Product prodPære = new Product()
                {
                    CreatedByID = zkp.Id,
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "Pære",
                };
                db.ProductSet.Add(prodPære);

                Product prodÆble = new Product()
                {
                    CreatedByID = zkp.Id,
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "Æble",
                };
                db.ProductSet.Add(prodÆble);

                Product prodAbrikos = new Product()
                {
                    CreatedByID = zkp.Id,
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "Abrikos",
                };
                db.ProductSet.Add(prodAbrikos);

                Product prodTuttiFruttiMix = new Product()
                {
                    CreatedByID = zkp.Id,
                    DateCreated = DateTime.Today.AddMonths(-1),
                    Name = "TuttiFruttiMix, burde ikke vises",
                };
                db.ProductSet.Add(prodTuttiFruttiMix);

                db.SaveChanges();
                /* Units */

                ICollection<Unit> unitCollection = new List<Unit>();

                Unit unitKg = new Unit()
                {
                    Name = "kg"
                };
                db.UnitSet.Add(unitKg);
                unitCollection.Add(unitKg);

                Unit unitKs = new Unit()
                {
                    Name = "kasse(r)"
                };
                db.UnitSet.Add(unitKs);
                unitCollection.Add(unitKs);

                Unit unitStk = new Unit()
                {
                    Name = "styks"
                };
                db.UnitSet.Add(unitStk);
                unitCollection.Add(unitStk);

                db.SaveChanges();
                /* Product versions */

                ProductVersion prodvBanan = new ProductVersion()
                {
                    Name = "Banan",
                    Product = prodBanan,
                    IsActive = true,
                    Supplier = "FrugtKarl",
                    Units = unitCollection,
                    DateCreated = DateTime.Today,
                    CreatedByID = shp.Id
                };
                db.ProductVersionSet.Add(prodvBanan);

                ProductVersion prodvÆble = new ProductVersion()
                {
                    Name = "Æble",
                    Product = prodÆble,
                    IsActive = true,
                    Supplier = "FrugtKarl",
                    Units = unitCollection,
                    DateCreated = DateTime.Today,
                };
                db.ProductVersionSet.Add(prodvÆble);

                ProductVersion prodvPære = new ProductVersion()
                {
                    Name = "Pære",
                    Product = prodÆble,
                    IsActive = true,
                    Supplier = "FrugtKarl",
                    DateCreated = DateTime.Today,
                    Units = unitCollection
                };
                db.ProductVersionSet.Add(prodvPære);

                ProductVersion prodvAbrikos = new ProductVersion()
                {
                    Name = "Abrikos",
                    Product = prodÆble,
                    IsActive = true,
                    Supplier = "FrugtKarl",
                    DateCreated = DateTime.Today,
                    Units = unitCollection
                };
                db.ProductVersionSet.Add(prodvAbrikos);

                ProductVersion prodvTuttiFrutti = new ProductVersion()
                {
                    Name = "Tutti Frutti Mix, burde ikke vises",
                    Product = prodÆble,
                    IsActive = true,
                    Supplier = "Tivoli",
                    DateCreated = DateTime.Today,
                    Units = unitCollection
                };
                db.ProductVersionSet.Add(prodvTuttiFrutti);

                db.SaveChanges();
                /* Department Specific Products */

                DepartmentSpecificProduct departmentSpecificProduct = new DepartmentSpecificProduct()
                {
                    Department = depSydAmerika,
                    Product = prodBanan
                };
                db.DepartmentSpecificProductSet.Add(departmentSpecificProduct);

                departmentSpecificProduct = new DepartmentSpecificProduct()
                {
                    Department = depSydAmerika,
                    Product = prodÆble
                };
                db.DepartmentSpecificProductSet.Add(departmentSpecificProduct);

                departmentSpecificProduct = new DepartmentSpecificProduct()
                {
                    Department = depSydAmerika,
                    Product = prodPære
                };
                db.DepartmentSpecificProductSet.Add(departmentSpecificProduct);

                departmentSpecificProduct = new DepartmentSpecificProduct()
                {
                    Department = depSydAmerika,
                    Product = prodAbrikos
                };
                db.DepartmentSpecificProductSet.Add(departmentSpecificProduct);

                departmentSpecificProduct = new DepartmentSpecificProduct()
                {
                    Department = depAfrika,
                    Product = prodTuttiFruttiMix
                };
                db.DepartmentSpecificProductSet.Add(departmentSpecificProduct);

                db.SaveChanges();
                /* Shopping list */

                ShoppingList shoppingList = new ShoppingList()
                {
                    CreatedByID = shp.Id,
                    DateCreated = DateTime.Today,
                    Shopper = shp,
                    Status = 2,

                };
                db.ShoppingListSet.Add(shoppingList);

                db.SaveChanges();
                /* Orders */

                Order order = new Order()
                {
                    DateCreated = DateTime.Today,
                    Status = 2,
                    DepartmentID = depSydAmerika.Id,
                    OrderedByID = zkp.Id,
                    ShoppingList = shoppingList,
                    DateOrdered = DateTime.Today,
                };
                db.OrderSet.Add(order);

                db.SaveChanges();
                /* OrderLine */

                OrderLine orderLineBanan = new OrderLine()
                {
                    Order = order,
                    ProductVersion = prodvBanan,
                    Quantity = 15,
                    UnitID = unitKg.Id
                };
                db.OrderLineSet.Add(orderLineBanan);

                OrderLine orderLineÆble = new OrderLine()
                {
                    Order = order,
                    ProductVersion = prodvÆble,
                    Quantity = 15,
                    UnitID = unitKg.Id
                };
                db.OrderLineSet.Add(orderLineÆble);

                OrderLine orderLinePære = new OrderLine()
                {
                    Order = order,
                    ProductVersion = prodvPære,
                    Quantity = 15,
                    UnitID = unitKg.Id
                };
                db.OrderLineSet.Add(orderLinePære);

                OrderLine orderLineAbrikos = new OrderLine()
                {
                    Order = order,
                    ProductVersion = prodvAbrikos,
                    Quantity = 15,
                    UnitID = unitKg.Id
                };
                db.OrderLineSet.Add(orderLineAbrikos);

                OrderLine orderLineTuttiFruttiMix = new OrderLine()
                {
                    Order = order,
                    ProductVersion = prodvTuttiFrutti,
                    Quantity = 15,
                    UnitID = unitKg.Id
                };
                db.OrderLineSet.Add(orderLineTuttiFruttiMix);

                db.SaveChanges();
            }
        }

        static public void DeleteDatabase()
        {
            using (db = new AalborgZooContainer1())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM [DepartmentChangeSet] DBCC CHECKIDENT ([DepartmentChangeSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [PasswordChangedSet] DBCC CHECKIDENT ([PasswordChangedSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [DepartmentSpecificProductSet] DBCC CHECKIDENT ([DepartmentSpecificProductSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [OrderLineSet] DBCC CHECKIDENT ([OrderLineSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [EmployeeSet] DBCC CHECKIDENT ([EmployeeSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [ShoppingListSet] DBCC CHECKIDENT ([ShoppingListSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [OrderSet] DBCC CHECKIDENT ([OrderSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [UnitProductVersion]");
                db.Database.ExecuteSqlCommand("DELETE FROM [ProductVersionSet] DBCC CHECKIDENT ([ProductVersionSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [DepartmentSet] DBCC CHECKIDENT ([DepartmentSet], RESEED, 0)");
                //db..RemoveRange(db.DepartmentChangeSet.Where(x => true));
                //db.DepartmentChangeSet.RemoveRange(db.DepartmentChangeSet.Where(x => true));
                db.Database.ExecuteSqlCommand("DELETE FROM [ProductSet] DBCC CHECKIDENT ([ProductSet], RESEED, 0)");
                db.Database.ExecuteSqlCommand("DELETE FROM [UnitSet] DBCC CHECKIDENT ([UnitSet], RESEED, 0)");
            }
        }
    }
}
