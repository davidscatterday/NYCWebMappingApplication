using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
    public class Hpd_Contact_Names
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Hpd_Registrations_Group
    {
        public string RegistrationID { get; set; }
        public string Boro { get; set; }
        public string Zip { get; set; }
        public string bbl { get; set; }
        public string Address { get; set; }
        public DateTime? sale_date { get; set; }
        public string sale_price { get; set; }
    }
}