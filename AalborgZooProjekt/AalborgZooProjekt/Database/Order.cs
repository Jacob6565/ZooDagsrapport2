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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Orders = new HashSet<OrderLine>();
        }
    
        public int Id { get; set; }
        public int ShoppinglistId { get; set; }
        public int ZookeeperId { get; set; }
        public int DepartmentId { get; set; }
    
        public virtual Shoppinglist Shoppinglist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLine> Orders { get; set; }
        public virtual Zookeeper Zookeeper { get; set; }
        public virtual Department Department { get; set; }
    }
}
