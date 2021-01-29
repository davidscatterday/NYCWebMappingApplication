using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class OwnerAnalysisDAL
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public List<Hpd_Registrations_Group> GetHpdRegistrationsByBBL(string bbl)
        {
            List<Hpd_Registrations_Group> lstRegistrations = new List<Hpd_Registrations_Group>();
            List<string> lstRegistrationIDs = new List<string>();

            hpd_registrations hpd_registration = db.hpd_registrations.Where(w => w.bbl == bbl).FirstOrDefault();

            if (hpd_registration != null)
            {
                List<hpd_business_addresses> hpd_business_addresses = db.hpd_business_addresses.Where(w => w.uniqregids.Contains(hpd_registration.RegistrationID)).ToList();
                if (hpd_business_addresses.Count > 0)
                {
                    foreach (hpd_business_addresses item in hpd_business_addresses)
                    {
                        lstRegistrationIDs.AddRange(item.uniqregids.Split(',').Select(p => p.Trim()).ToList());
                    }
                }
                List<hpd_contacts> lstHpd_Contacts = db.hpd_contacts.Where(w => w.RegistrationID == hpd_registration.RegistrationID && !String.IsNullOrEmpty(w.FirstName) && !String.IsNullOrEmpty(w.LastName) && (w.Type == "CorporateOwner" || w.Type == "HeadOfficer" || w.Type == "IndividualOwner")).ToList();
                List<Hpd_Contact_Names> contactNames = new List<Hpd_Contact_Names>();
                foreach (hpd_contacts item in lstHpd_Contacts)
                {
                    if (!contactNames.Any(w => w.FirstName == item.FirstName && w.LastName == item.LastName))
                    {
                        contactNames.Add(new Hpd_Contact_Names() { FirstName = item.FirstName, LastName = item.LastName });
                    }
                }
                foreach (Hpd_Contact_Names item in contactNames)
                {
                    List<hpd_contacts> lstContact = db.hpd_contacts.Where(w => w.FirstName == item.FirstName && w.LastName == item.LastName).ToList();
                    foreach (hpd_contacts itemContact in lstContact)
                    {
                        if (!lstRegistrationIDs.Contains(itemContact.RegistrationID))
                        {
                            lstRegistrationIDs.Add(itemContact.RegistrationID);
                        }
                    }
                }
                using (var ctx = new NYC_Web_Mapping_AppEntities())
                {
                    string RegIDs = string.Join(",", lstRegistrationIDs);
                    var RegIDsParametar = !String.IsNullOrEmpty(RegIDs) ? new SqlParameter("RegIDs", RegIDs) : new SqlParameter("RegIDs", DBNull.Value);
                    lstRegistrations = ctx.Database.SqlQuery<Hpd_Registrations_Group>("EXEC dbo.hpd_GetRegistrationsByRegIDs @RegIDs ", RegIDsParametar).ToList();
                }
                //lstRegistrations = db.hpd_registrations.Where(w => lstRegistrationIDs.Contains(w.RegistrationID)).ToList();
            }

            return lstRegistrations;
        }
        public List<Select2DTO> GetAllHpd_Addresses(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                var termParametar = !String.IsNullOrEmpty(term) ? new SqlParameter("term", term) : new SqlParameter("term", DBNull.Value);
                returnResult = ctx.Database.SqlQuery<Select2DTO>("EXEC dbo.hpd_getAllAddresses @term ", termParametar).ToList();
            }
            return returnResult;
        }
    }
}