using System;
using AalborgZooProjekt.Interfaces;


namespace AalborgZooProjekt.Model
{
    public partial class Zookeeper : IZookeeper
    {
        public void ChangeDepartment(Department department)
        {
            DepartmentChange oldDepartment = new DepartmentChange();
            oldDepartment.DateChanged = DateTime.Now.ToString();
            oldDepartment.DepartmentID = this.DepartmentId;

        }

        public void GetID()
        {
            throw new NotImplementedException();
        }

        public void MakeOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
