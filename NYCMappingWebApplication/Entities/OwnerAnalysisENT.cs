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
        public string Type { get; set; }
        public string CorporationName { get; set; }
        public string BusinessHouseNumber { get; set; }
        public string BusinessStreetName { get; set; }
        public string BusinessApartment { get; set; }
        public string BusinessZip { get; set; }
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
        public string ACRIS_DocumentsLink { get; set; }
        public string DOB_BuildingProfileLink { get; set; }
        public string ANHD_DAP_PortalLink { get; set; }
    }
    public class Hpd_Wow_Bldgs
    {
        public string RegistrationID { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string FullAddress { get; set; }
    }
    public class Hpd_Business_Addresses
    {
        public string regids { get; set; }
    }
    public class OwnerAnalysisData
    {
        public List<Hpd_Registrations_Group> lstPortfolio { get; set; }
        public Hpd_Registrations_Group selectedBuilding { get; set; }
        public string ACRIS_DocumentsLink { get; set; }
        public string DOB_BuildingProfileLink { get; set; }
        public string ANHD_DAP_PortalLink { get; set; }
        public string borough { get; set; }
        public string block { get; set; }
        public string lot { get; set; }
        public string BusinessEntities { get; set; }
        public string Agent { get; set; }
        public string SiteManager { get; set; }
        public string GoogleStreetViewLink { get; set; }
        public string FullAddress { get; set; }
    }
}