//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class OrderLine
    {
        public int Id { get; set; }
        public string Quantity { get; set; }
        public string UnitID { get; set; }
        public int ProductVersionId { get; set; }
        public int OrderId { get; set; }
    
        public virtual ProductVersion ProductVersion { get; set; }
        public virtual Order Order { get; set; }
    }
}
