﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class torresfullstackdbEntities : DbContext
    {
        public torresfullstackdbEntities()
            : base("name=torresfullstackdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<adminuser> adminusers { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<class_code_tb> class_code_tb { get; set; }
    }
}
