﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NYCMappingWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NYC_Web_Mapping_AppEntities : DbContext
    {
        public NYC_Web_Mapping_AppEntities()
            : base("name=NYC_Web_Mapping_AppEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MyReport> MyReports { get; set; }
        public virtual DbSet<Elevator> Elevators { get; set; }
        public virtual DbSet<ConsumerProfile> ConsumerProfiles { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Tab> Tabs { get; set; }
        public virtual DbSet<User_Tabs> User_Tabs { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<Pluto> Plutoes { get; set; }
        public virtual DbSet<MyAlert> MyAlerts { get; set; }
        public virtual DbSet<Configuration_PropAttributes> Configuration_PropAttributes { get; set; }
    }
}
