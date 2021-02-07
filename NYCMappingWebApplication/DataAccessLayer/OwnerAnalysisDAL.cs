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

            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                var BBLParametar = !String.IsNullOrEmpty(bbl) ? new SqlParameter("BBL", bbl) : new SqlParameter("BBL", DBNull.Value);
                List<Hpd_Wow_Bldgs> lstWowBldgsRegistrations = ctx.Database.SqlQuery<Hpd_Wow_Bldgs>("EXEC NYC_Owner_Analysis.dbo.search_getRegistrationsByBBL @BBL ", BBLParametar).ToList();

                if (lstWowBldgsRegistrations.Count > 0)
                {
                    foreach(Hpd_Wow_Bldgs item in lstWowBldgsRegistrations)
                    {
                        lstRegistrationIDs.Add(item.RegistrationID);
                        var RegIDParametar = !String.IsNullOrEmpty(item.RegistrationID) ? new SqlParameter("RegID", item.RegistrationID) : new SqlParameter("RegID", DBNull.Value);
                        List<Hpd_Business_Addresses> lstHpd_Business_Addresses = ctx.Database.SqlQuery<Hpd_Business_Addresses>("EXEC NYC_Owner_Analysis.dbo.search_getBusinessRegistrationsByRegID @RegID ", RegIDParametar).ToList();
                        foreach (Hpd_Business_Addresses itemBusinessAddress in lstHpd_Business_Addresses)
                        {
                            lstRegistrationIDs.AddRange(itemBusinessAddress.regids.Split(',').Select(p => p.Trim()).ToList());
                        }

                        var RegIDParametar1 = !String.IsNullOrEmpty(item.RegistrationID) ? new SqlParameter("RegID", item.RegistrationID) : new SqlParameter("RegID", DBNull.Value);
                        List<Hpd_Contact_Names> lstHpd_Contact_Names = ctx.Database.SqlQuery<Hpd_Contact_Names>("EXEC NYC_Owner_Analysis.dbo.search_getContactsByRegID @RegID ", RegIDParametar1).ToList();
                        foreach (Hpd_Contact_Names itemContactName in lstHpd_Contact_Names)
                        {
                            var FirstNameParametar = !String.IsNullOrEmpty(itemContactName.FirstName) ? new SqlParameter("FirstName", itemContactName.FirstName) : new SqlParameter("FirstName", DBNull.Value);
                            var LastNameParametar = !String.IsNullOrEmpty(itemContactName.LastName) ? new SqlParameter("LastName", itemContactName.LastName) : new SqlParameter("LastName", DBNull.Value);
                            List<Hpd_Contact_Names> lstContacts = ctx.Database.SqlQuery<Hpd_Contact_Names>("EXEC NYC_Owner_Analysis.dbo.search_getContactsByName @FirstName, @LastName ", FirstNameParametar, LastNameParametar).ToList();
                            foreach (Hpd_Contact_Names itemContact in lstContacts)
                            {
                                if (!lstRegistrationIDs.Contains(itemContact.RegistrationID))
                                {
                                    lstRegistrationIDs.Add(itemContact.RegistrationID);
                                }
                            }
                        }
                    }
                    string RegIDs = string.Join(",", lstRegistrationIDs);
                    var RegIDsParametar = !String.IsNullOrEmpty(RegIDs) ? new SqlParameter("RegIDs", RegIDs) : new SqlParameter("RegIDs", DBNull.Value);
                    lstRegistrations = ctx.Database.SqlQuery<Hpd_Registrations_Group>("EXEC NYC_Owner_Analysis.dbo.search_GetWowBldgsByRegIDs @RegIDs ", RegIDsParametar).ToList();
                }
            }

            //hpd_registrations hpd_registration = db.hpd_registrations.Where(w => w.bbl == bbl).FirstOrDefault();

            //if (hpd_registration != null)
            //{
            //    List<hpd_business_addresses> hpd_business_addresses = db.hpd_business_addresses.Where(w => w.uniqregids.Contains(hpd_registration.RegistrationID)).ToList();
            //    if (hpd_business_addresses.Count > 0)
            //    {
            //        foreach (hpd_business_addresses item in hpd_business_addresses)
            //        {
            //            lstRegistrationIDs.AddRange(item.uniqregids.Split(',').Select(p => p.Trim()).ToList());
            //        }
            //    }
            //    List<hpd_contacts> lstHpd_Contacts = db.hpd_contacts.Where(w => w.RegistrationID == hpd_registration.RegistrationID && !String.IsNullOrEmpty(w.FirstName) && !String.IsNullOrEmpty(w.LastName) && (w.Type == "CorporateOwner" || w.Type == "HeadOfficer" || w.Type == "IndividualOwner")).ToList();
            //    List<Hpd_Contact_Names> contactNames = new List<Hpd_Contact_Names>();
            //    foreach (hpd_contacts item in lstHpd_Contacts)
            //    {
            //        if (!contactNames.Any(w => w.FirstName == item.FirstName && w.LastName == item.LastName))
            //        {
            //            contactNames.Add(new Hpd_Contact_Names() { FirstName = item.FirstName, LastName = item.LastName });
            //        }
            //    }
            //    foreach (Hpd_Contact_Names item in contactNames)
            //    {
            //        List<hpd_contacts> lstContact = db.hpd_contacts.Where(w => w.FirstName == item.FirstName && w.LastName == item.LastName).ToList();
            //        foreach (hpd_contacts itemContact in lstContact)
            //        {
            //            if (!lstRegistrationIDs.Contains(itemContact.RegistrationID))
            //            {
            //                lstRegistrationIDs.Add(itemContact.RegistrationID);
            //            }
            //        }
            //    }
            //    using (var ctx = new NYC_Web_Mapping_AppEntities())
            //    {
            //        string RegIDs = string.Join(",", lstRegistrationIDs);
            //        var RegIDsParametar = !String.IsNullOrEmpty(RegIDs) ? new SqlParameter("RegIDs", RegIDs) : new SqlParameter("RegIDs", DBNull.Value);
            //        lstRegistrations = ctx.Database.SqlQuery<Hpd_Registrations_Group>("EXEC NYC_Owner_Analysis.dbo.GetRegistrationsByRegIDs @RegIDs ", RegIDsParametar).ToList();
            //    }
            //    //lstRegistrations = db.hpd_registrations.Where(w => lstRegistrationIDs.Contains(w.RegistrationID)).ToList();
            //}

            return lstRegistrations;
        }
        public List<Select2DTO> GetAllHpd_Addresses(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                var termParametar = !String.IsNullOrEmpty(term) ? new SqlParameter("term", term) : new SqlParameter("term", DBNull.Value);
                returnResult = ctx.Database.SqlQuery<Select2DTO>("EXEC NYC_Owner_Analysis.dbo.search_getAllAddresses @term ", termParametar).ToList();
            }
            return returnResult;
        }
    }
}