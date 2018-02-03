using System;
using AalborgZooProjekt.Interfaces;
using AalborgZooProjekt.Model.Repository;

namespace AalborgZooProjekt.Model
{
    public partial class Zookeeper
    {
        public Zookeeper(string name) : this(new EmployeeRepository(), name, null) { }
        public Zookeeper(string name, Department department) : this(new EmployeeRepository(), name, department) { }

        public Zookeeper(IEmployeeRepository employeeRepository, string name, Department department): this()
        {
            this.Name = name;
            this.DateHired = DateTime.Now;
            this.DateStopped = null;
            this.Department = department;
            this.DepartmentChanges = null;

            //To save it in database.
            this.employeeRepository = employeeRepository;
            this.employeeRepository.AddEmployee(this);
        }

        private IEmployeeRepository employeeRepository;
    }
}
