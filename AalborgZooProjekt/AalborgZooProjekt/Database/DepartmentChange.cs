//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AalborgZooProjekt.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepartmentChange
    {
        public int Id { get; set; }
        public string DepartmentID { get; set; }
        public string DateChanged { get; set; }
        public string ZookeeperID { get; set; }
        public Nullable<int> ZookeeperId1 { get; set; }
    
        public virtual Zookeeper Zookeeper { get; set; }
    }
}