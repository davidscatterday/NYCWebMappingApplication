//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class MyAlert
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string AlertName { get; set; }
        public string AlertQuery { get; set; }
        public Nullable<int> Last_OBJECTID { get; set; }
        public Nullable<System.DateTime> Last_DateCheck { get; set; }
        public Nullable<System.DateTime> Next_DateCheck { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int Frequency { get; set; }
        public bool IsEnergySearch { get; set; }
        public bool IsPermitSearch { get; set; }
        public bool IsViolationSearch { get; set; }
        public bool IsEvictionSearch { get; set; }
        public Nullable<bool> IsElevatorSearch { get; set; }
        public Nullable<bool> IsUnread { get; set; }
    }
}
