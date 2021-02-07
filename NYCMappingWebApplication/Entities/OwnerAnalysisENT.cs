using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
    public class Hpd_Contact_Names
    {
        public string RegistrationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Hpd_Registrations_Group
    {
        public string RegistrationID { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Boro { get; set; }
        public string bbl { get; set; }
        public string yearbuilt { get; set; }
        public string unitsres { get; set; }
        public int? rsunits2019 { get; set; }
        public int? openviolations { get; set; }
        public int? totalviolations { get; set; }
        public int? totalevictions { get; set; }
        public string OwnerName { get; set; }
        public string DOC_DATE { get; set; }
        public string DOC_AMOUNT { get; set; }
    }
    public class Hpd_Wow_Bldgs
    {
        public string RegistrationID { get; set; }
    }
    public class Hpd_Business_Addresses
    {
        public string regids { get; set; }
    }
}