using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class OwnerAnalysisDAL
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public OwnerAnalysisData GetHpdRegistrationsByBBL(string bbl)
        {
            OwnerAnalysisData data = new OwnerAnalysisData();
            List<Hpd_Registrations_Group> lstRegistrations = new List<Hpd_Registrations_Group>();
            List<string> lstRegistrationIDs = new List<string>();
            List<string> lstBusinessEntities = new List<string>();
            List<string> lstBusinessAddresses = new List<string>();

            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                var BBLParametar = !String.IsNullOrEmpty(bbl) ? new SqlParameter("BBL", bbl) : new SqlParameter("BBL", DBNull.Value);
                List<Hpd_Wow_Bldgs> lstWowBldgsRegistrations = ctx.Database.SqlQuery<Hpd_Wow_Bldgs>("EXEC NYC_Owner_Analysis.dbo.search_getRegistrationsByBBL @BBL ", BBLParametar).ToList();

                if (lstWowBldgsRegistrations.Count > 0)
                {
                    foreach (Hpd_Wow_Bldgs item in lstWowBldgsRegistrations)
                    {
                        data.FullAddress = item.FullAddress;
                        data.GoogleStreetViewLink = System.Configuration.ConfigurationManager.AppSettings["GoogleApiLink"] + "?size=800x500&location=" + item.latitude + "," + item.longitude + "&key=AIzaSyCuf0Ca1EvxogvbZQKOBl_40y0UWm4Fk30";
                        lstRegistrationIDs.Add(item.RegistrationID);

                        var RegIDParametar1 = !String.IsNullOrEmpty(item.RegistrationID) ? new SqlParameter("RegID", item.RegistrationID) : new SqlParameter("RegID", DBNull.Value);
                        List<Hpd_Contact_Names> lstHpd_Contact_Names = ctx.Database.SqlQuery<Hpd_Contact_Names>("EXEC NYC_Owner_Analysis.dbo.search_getContactsByRegID @RegID ", RegIDParametar1).ToList();
                        string sqlQuery = "SELECT DISTINCT RegistrationID FROM [NYC_Owner_Analysis].[dbo].[hpd_contacts] WHERE ";
                        string whereClauseContactNames = "";
                        foreach (Hpd_Contact_Names itemContactName in lstHpd_Contact_Names)
                        {
                            if (itemContactName.Type == "Agent")
                            {
                                data.Agent = itemContactName.FirstName + " " + itemContactName.LastName;
                            }
                            else if (itemContactName.Type == "SiteManager")
                            {
                                data.SiteManager = itemContactName.FirstName + " " + itemContactName.LastName;
                            }
                            if (itemContactName.CorporationName != null && !lstBusinessEntities.Contains(itemContactName.CorporationName.Trim(' ', '.')))
                            {
                                lstBusinessEntities.Add(itemContactName.CorporationName.Trim(' ', '.'));
                            }
                            if ((itemContactName.Type == "CorporateOwner" || itemContactName.Type == "HeadOfficer" || itemContactName.Type == "IndividualOwner") && !String.IsNullOrEmpty(itemContactName.FirstName) && !String.IsNullOrEmpty(itemContactName.LastName))
                            {
                                if (whereClauseContactNames == "")
                                {
                                    whereClauseContactNames = "(FirstName = '" + itemContactName.FirstName + "' AND LastName = '" + itemContactName.LastName + "')";
                                }
                                else
                                {
                                    whereClauseContactNames += " OR (FirstName = '" + itemContactName.FirstName + "' AND LastName = '" + itemContactName.LastName + "')";
                                }
                            }
                            if (!String.IsNullOrEmpty(itemContactName.BusinessHouseNumber) || !String.IsNullOrEmpty(itemContactName.BusinessStreetName) || !String.IsNullOrEmpty(itemContactName.BusinessApartment) || !String.IsNullOrEmpty(itemContactName.BusinessZip))
                            {
                                string BusinessHouseNumber = !String.IsNullOrEmpty(itemContactName.BusinessHouseNumber) ? "BusinessHouseNumber = '" + itemContactName.BusinessHouseNumber + "'" : "BusinessHouseNumber IS NULL";
                                string BusinessStreetName = !String.IsNullOrEmpty(itemContactName.BusinessStreetName) ? "BusinessStreetName = '" + itemContactName.BusinessStreetName + "'" : "BusinessStreetName IS NULL";
                                string BusinessZip = !String.IsNullOrEmpty(itemContactName.BusinessZip) ? "BusinessZip = '" + itemContactName.BusinessZip + "'" : "BusinessZip IS NULL";
                                string businessAddress = itemContactName.BusinessHouseNumber + " " + itemContactName.BusinessStreetName + " " + itemContactName.BusinessApartment + " " + itemContactName.BusinessZip;
                                if (!lstBusinessAddresses.Contains(businessAddress))
                                {
                                    lstBusinessAddresses.Add(businessAddress);
                                    if (whereClauseContactNames == "")
                                    {
                                        whereClauseContactNames = "(" + BusinessHouseNumber + " AND " + BusinessStreetName + " AND " + BusinessZip + ")";
                                    }
                                    else
                                    {
                                        whereClauseContactNames += " OR (" + BusinessHouseNumber + " AND " + BusinessStreetName + " AND " + BusinessZip + ")";
                                    }
                                }
                            }
                        }
                        if (lstBusinessEntities.Count > 0)
                        {
                            foreach (string businessEntity in lstBusinessEntities)
                            {
                                if (whereClauseContactNames == "")
                                {
                                    whereClauseContactNames = "(CorporationName LIKE '%" + businessEntity + "%')";
                                }
                                else
                                {
                                    whereClauseContactNames += " OR (CorporationName LIKE '%" + businessEntity + "%')";
                                }
                            }
                        }
                        if (whereClauseContactNames != "")
                        {
                            sqlQuery = sqlQuery + whereClauseContactNames;
                            List<Hpd_Contact_Names> lstContacts = ctx.Database.SqlQuery<Hpd_Contact_Names>(sqlQuery).ToList();
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
                    string bldgsQuery = "SELECT r.RegistrationID, (r.HouseNumber + ' ' + r.StreetName) AS Address, r.Zip, r.Boro, r.BBL, r.yearbuilt, r.unitsres, r.rsunits2019, r.openviolations, r.totalviolations, r.totalevictions, r.OwnerName, r.DOC_DATE, r.DOC_AMOUNT FROM [NYC_Owner_Analysis].[dbo].[wow_bldgs] r WHERE r.RegistrationID IN (" + RegIDs + ")";
                    lstRegistrations = ctx.Database.SqlQuery<Hpd_Registrations_Group>(bldgsQuery).ToList();
                }
            }

            foreach (Hpd_Registrations_Group group in lstRegistrations)
            {
                string borough = group.bbl.Substring(0, 1);
                string block = group.bbl.Substring(1, 5);
                string lot = group.bbl.Substring(6);
                group.ACRIS_DocumentsLink = System.Configuration.ConfigurationManager.AppSettings["ACRIS_DocumentsLink"] + "?borough=" + borough + "&block=" + block + "&lot=" + lot;
                group.DOB_BuildingProfileLink = System.Configuration.ConfigurationManager.AppSettings["DOB_BuildingProfileLink"] + "?boro=" + borough + "&block=" + block + "&lot=" + lot;
                group.ANHD_DAP_PortalLink = System.Configuration.ConfigurationManager.AppSettings["ANHD_DAP_PortalLink"] + group.bbl;
            }
            data.BusinessEntities = string.Join(", ", lstBusinessEntities);
            data.lstPortfolio = lstRegistrations;
            data.selectedBuilding = lstRegistrations.Where(w => w.bbl == bbl).FirstOrDefault();
            return data;
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
        public EvictionOutput GetEvictionProbabilitiesByBBL(string bbl)
        {
            Pluto data = db.Plutoes.Where(w => w.BBL == bbl).FirstOrDefault();
            string BldgArea = data.BldgArea.HasValue ? data.BldgArea.ToString() : "";
            string ComArea = data.ComArea.HasValue ? data.ComArea.ToString() : "";
            string ResArea = data.ResArea.HasValue ? data.ResArea.ToString() : "";
            string NumFloors = data.NumFloors.HasValue ? data.NumFloors.ToString() : "";
            string UnitsRes = data.UnitsRes.HasValue ? data.UnitsRes.ToString() : "";
            string AssessTot = data.AssessTot.HasValue ? data.AssessTot.ToString() : "";
            string YearBuilt = data.YearBuilt.HasValue ? data.YearBuilt.ToString() : "";
            EvictionOutput evOutput = RestDAL.InvokeRequestResponseEvictionStatus15KProperties(bbl, data.Borough, data.ZipCode, BldgArea, ComArea, ResArea, NumFloors, UnitsRes, data.ZoneDist1, AssessTot, YearBuilt, data.BldgClass, data.LandUse, null);
            return evOutput;
        }
    }
}