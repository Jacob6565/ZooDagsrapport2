﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AalborgZooProjekt.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AalborgZooContainer : DbContext
    {
        public AalborgZooContainer()
            : base("name=AalborgZooContainer1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DepartmentSpecificProduct> DepartmentSpecificProductSet { get; set; }
        public virtual DbSet<Product> ProductSet { get; set; }
        public virtual DbSet<ProductVersion> ProductVersionSet { get; set; }
        public virtual DbSet<ShoppingList> ShoppingListSet { get; set; }
        public virtual DbSet<Order> OrderSet { get; set; }
        public virtual DbSet<OrderLine> OrderLineSet { get; set; }
        public virtual DbSet<Department> DepartmentSet { get; set; }
        public virtual DbSet<DepartmentChange> DepartmentChangeSet { get; set; }
        public virtual DbSet<Employee> EmployeeSet { get; set; }
        public virtual DbSet<PasswordChanged> PasswordChangedSet { get; set; }
        public virtual DbSet<Unit> UnitSet { get; set; }
    }
}
