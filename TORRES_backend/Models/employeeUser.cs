//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TORRES_backend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class employeeUser
    {
        public int empID { get; set; }
        public string empFirstName { get; set; }
        public string empLastName { get; set; }
        public string empPassword { get; set; }
        public Nullable<System.DateTime> createdAt { get; set; }
        public string empEmail { get; set; }
    }
}
