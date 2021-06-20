using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
    public class IndexData
    {
        public string Username { get; set; }
        public List<int> TabIDs { get; set; }
    }
    public class Select2DTO // as select2 is formed like id and text so we used DTO
    {
        public string id { get; set; }
        public string text { get; set; }
    }
    public class TableFeatures
    {
        public string Borough { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string BldgArea { get; set; }
        public string ComArea { get; set; }
        public string ResArea { get; set; }
        public string NumFloors { get; set; }
        public string UnitsRes { get; set; }
        public string ZoneDist1 { get; set; }
        public string Overlay1 { get; set; }
        public string Overlay2 { get; set; }
        public string AssessTot { get; set; }
        public string YearBuilt { get; set; }
        public string BldgClass { get; set; }
        public string energy_star_score { get; set; }
        public string source_eui_kbtu_ft { get; set; }
        public string site_eui_kbtu_ft { get; set; }
        public string annual_maximum_demand_kw { get; set; }
        public string total_ghg_emissions_metric { get; set; }
        public string OwnerName { get; set; }
        public string job_start_date { get; set; }
        public string job_type { get; set; }
        public string work_type { get; set; }
        public string issue_date { get; set; }
        public string violation_type { get; set; }
        public string violation_category { get; set; }
        public string executed_date { get; set; }
        public string CD { get; set; }
        public string DISTRICT { get; set; }
        public string OrganizationName { get; set; }
        public string Faith_Based_Organization { get; set; }
        public string Foundation { get; set; }
        public string New_York_City_Agency { get; set; }
        public string Nonprofit { get; set; }
    }
    public class DatabaseAttributes
    {
        public int OBJECTID { get; set; }
        public string BBL { get; set; }
        public string Borough { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? BldgArea { get; set; }
        public int? ComArea { get; set; }
        public int? ResArea { get; set; }
        public double? NumFloors { get; set; }
        public string UnitsRes { get; set; }
        public string ZoneDist1 { get; set; }
        public string Overlay1 { get; set; }
        public string Overlay2 { get; set; }
        public double? AssessTot { get; set; }
        public int? YearBuilt { get; set; }
        public string BldgClass { get; set; }
        public string OwnerName { get; set; }

        public int? energy_star_score { get; set; }
        public double? source_eui_kbtu_ft { get; set; }
        public double? site_eui_kbtu_ft { get; set; }
        public double? annual_maximum_demand_kw { get; set; }
        public double? total_ghg_emissions_metric { get; set; }

        private DateTime? _job_start_date;
        public DateTime? job_start_date
        {
            get { return _job_start_date; }
            set
            {
                _job_start_date = value;

                job_start_date_string_format = _job_start_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _job_start_date) : "";
            }
        }
        public string job_start_date_string_format { get; set; }
        public string job_type { get; set; }
        public string work_type { get; set; }

        private string _issue_date;
        public string issue_date
        {
            get { return _issue_date; }
            set
            {
                _issue_date = value;

                issue_date_string_format = _issue_date.Length == 8 ? _issue_date.Substring(4, 2) + "/" + _issue_date.Substring(6, 2) + "/" + _issue_date.Substring(0, 4) : "";
            }
        }
        public string issue_date_string_format { get; set; }
        public string violation_type { get; set; }
        public string violation_category { get; set; }

        private DateTime? _executed_date;
        public DateTime? executed_date
        {
            get { return _executed_date; }
            set
            {
                _executed_date = value;

                executed_date_string_format = _executed_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _executed_date) : "";
            }
        }
        public string executed_date_string_format { get; set; }
        public int? CD { get; set; }
        public string DISTRICT { get; set; }
        public string OrganizationName { get; set; }
        public string Faith_Based_Organization { get; set; }
        public string Foundation { get; set; }
        public string New_York_City_Agency { get; set; }
        public string Nonprofit { get; set; }
        public string elevatordevicetype { get; set; }
        public string job_number { get; set; }
        public string filing_type { get; set; }
        public string filing_status { get; set; }

        private DateTime? _filing_date;
        public DateTime? filing_date
        {
            get { return _filing_date; }
            set
            {
                _filing_date = value;

                filing_date_string_format = _filing_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _filing_date) : "";
            }
        }
        public string filing_date_string_format { get; set; }

        public double? AssessTotPerSqFt { get; set; }

        private DateTime? _sale_date;
        public DateTime? sale_date
        {
            get { return _sale_date; }
            set
            {
                _sale_date = value;

                sale_date_string_format = _sale_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _sale_date) : "";
            }
        }
        public string sale_date_string_format { get; set; }

        public string sale_price { get; set; }

        public string DESCRIPTION { get; set; }

        public string BusinessName { get; set; }
        public string GeneralContractor { get; set; }
        public string Architect { get; set; }
        public string TOTAL_CONSTRUCTION_FLOOR_AREA { get; set; }
        public string Proposed_Height { get; set; }
        public string Proposed_Occupancy { get; set; }
        public string Pre_Filing_Date { get; set; }
        public string owner_name { get; set; }
        public string owner_bus_name { get; set; }
        public string qewi_name { get; set; }
        public string qewi_bus_name { get; set; }

        public string RESPONDENT_NAME { get; set; }
        public string LandUse { get; set; }
        public string VIOLATION_DESCRIPTION { get; set; }
        public string Owner_Type { get; set; }
        private DateTime? _issuance_date;
        public DateTime? issuance_date
        {
            get { return _issuance_date; }
            set
            {
                _issuance_date = value;

                issuance_date_string_format = _issuance_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _issuance_date) : "";
            }
        }
        public string issuance_date_string_format { get; set; }
        public string PermitteName { get; set; }
        public string permittee_s_business_name { get; set; }
        public string permittee_s_license_type { get; set; }
    }

    public class DatabaseTopRecords
    {
        public List<DatabaseAttributes> attributes { get; set; }
        public int totalRecords { get; set; }
    }

    public class DatabaseMaxValues
    {
        public int? OBJECTID { get; set; }
        public DateTime? generation_date { get; set; }
        public DateTime? dobrundate { get; set; }
        public string issue_date { get; set; }
        public DateTime? EXECUTED_DATE { get; set; }
    }
    public class MyAlertENT
    {
        public int ID { get; set; }
        public string AlertName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string DateCreatedString { get; set; }
        public bool IsUnread { get; set; }
    }
    public class MyAlertObject
    {
        public List<DatabaseAttributes> result { get; set; }
        public string sqlQuery { get; set; }
        public int unreadAlerts { get; set; }
    }
    public class ReturnLookaLike
    {
        public List<DatabaseAttributes> data { get; set; }
        public string sqlQuery { get; set; }
    }
    public class DataConsumerProfiles
    {
        public ConsumerProfiles SelectedArea { get; set; }
        public ConsumerProfiles NewYorkCity { get; set; }
        public string Variables { get; set; }
    }
    public class ConsumerProfiles
    {
        public string State { get; set; }
        public string County { get; set; }
        public string Tract { get; set; }
        public string Year { get; set; }
        public int? DP05_0001E { get; set; }
        public int? DP05_0002E { get; set; }
        public double? DP05_0002PE { get; set; }
        public int? DP05_0003E { get; set; }
        public double? DP05_0003PE { get; set; }
        public int? DP05_0004E { get; set; }
        public int? DP05_0009E { get; set; }
        public int? DP05_0010E { get; set; }
        public int? DP05_0011E { get; set; }
        public int? DP05_0012E { get; set; }
        public int? DP05_0013E { get; set; }
        public int? DP05_0014E { get; set; }
        public int? DP05_0015E { get; set; }
        public int? DP05_0016E { get; set; }
        public int? DP02_0001E { get; set; }
        public double? DP02_0001PE { get; set; }
        public int? DP02_0002E { get; set; }
        public double? DP02_0002PE { get; set; }
        public int? DP02_0003E { get; set; }
        public double? DP02_0003PE { get; set; }
        public int? DP02_0006E { get; set; }
        public double? DP02_0006PE { get; set; }
        public int? DP02_0008E { get; set; }
        public double? DP02_0008PE { get; set; }
        public int? DP02_0010E { get; set; }
        public double? DP02_0010PE { get; set; }
        public int? DP02_0011E { get; set; }
        public double? DP02_0011PE { get; set; }
        public int? DP02_0012E { get; set; }
        public double? DP02_0012PE { get; set; }
        public int? DP02_0024E { get; set; }
        public double? DP02_0024PE { get; set; }
        public int? DP02_0025E { get; set; }
        public double? DP02_0025PE { get; set; }
        public int? DP02_0026E { get; set; }
        public double? DP02_0026PE { get; set; }
        public int? DP02_0027E { get; set; }
        public double? DP02_0027PE { get; set; }
        public int? DP02_0028E { get; set; }
        public double? DP02_0028PE { get; set; }
        public int? DP02_0029E { get; set; }
        public double? DP02_0029PE { get; set; }
        public int? DP02_0030E { get; set; }
        public double? DP02_0030PE { get; set; }
        public int? DP02_0031E { get; set; }
        public double? DP02_0031PE { get; set; }
        public int? DP02_0032E { get; set; }
        public double? DP02_0032PE { get; set; }
        public int? DP02_0033E { get; set; }
        public double? DP02_0033PE { get; set; }
        public int? DP02_0034E { get; set; }
        public double? DP02_0034PE { get; set; }
        public int? DP02_0035E { get; set; }
        public double? DP02_0035PE { get; set; }
        public int? DP02_0052E { get; set; }
        public double? DP02_0052PE { get; set; }
        public int? DP02_0053E { get; set; }
        public double? DP02_0053PE { get; set; }
        public int? DP02_0054E { get; set; }
        public double? DP02_0054PE { get; set; }
        public int? DP02_0055E { get; set; }
        public double? DP02_0055PE { get; set; }
        public int? DP02_0056E { get; set; }
        public double? DP02_0056PE { get; set; }
        public int? DP02_0057E { get; set; }
        public double? DP02_0057PE { get; set; }
        public int? DP02_0058E { get; set; }
        public double? DP02_0058PE { get; set; }
        public int? DP02_0059E { get; set; }
        public double? DP02_0059PE { get; set; }
        public int? DP02_0060E { get; set; }
        public double? DP02_0060PE { get; set; }
        public int? DP02_0061E { get; set; }
        public double? DP02_0061PE { get; set; }
        public int? DP02_0062E { get; set; }
        public double? DP02_0062PE { get; set; }
        public int? DP02_0063E { get; set; }
        public double? DP02_0063PE { get; set; }
        public int? DP02_0064E { get; set; }
        public double? DP02_0064PE { get; set; }
        public int? DP02_0070E { get; set; }
        public double? DP02_0070PE { get; set; }
        public int? DP02_0071E { get; set; }
        public double? DP02_0071PE { get; set; }
        public int? DP02_0074E { get; set; }
        public double? DP02_0074PE { get; set; }
        public int? DP02_0075E { get; set; }
        public double? DP02_0075PE { get; set; }
        public int? DP02_0076E { get; set; }
        public double? DP02_0076PE { get; set; }
        public int? DP02_0078E { get; set; }
        public double? DP02_0078PE { get; set; }
        public int? DP02_0079E { get; set; }
        public double? DP02_0079PE { get; set; }
        public int? DP02_0080E { get; set; }
        public double? DP02_0080PE { get; set; }
        public int? DP02_0081E { get; set; }
        public double? DP02_0081PE { get; set; }
        public int? DP02_0082E { get; set; }
        public double? DP02_0082PE { get; set; }
        public int? DP02_0083E { get; set; }
        public double? DP02_0083PE { get; set; }
        public int? DP02_0084E { get; set; }
        public double? DP02_0084PE { get; set; }
        public int? DP02_0150E { get; set; }
        public double? DP02_0150PE { get; set; }
        public int? DP02_0151E { get; set; }
        public double? DP02_0151PE { get; set; }
        public int? DP02_0152E { get; set; }
        public double? DP02_0152PE { get; set; }
        public int? DP03_0001E { get; set; }
        public double? DP03_0001PE { get; set; }
        public int? DP03_0003E { get; set; }
        public double? DP03_0003PE { get; set; }
        public int? DP03_0004E { get; set; }
        public double? DP03_0004PE { get; set; }
        public int? DP03_0005E { get; set; }
        public double? DP03_0005PE { get; set; }
        public int? DP03_0007E { get; set; }
        public double? DP03_0007PE { get; set; }
        public int? DP03_0009E { get; set; }
        public double? DP03_0009PE { get; set; }
        public int? DP03_0010E { get; set; }
        public double? DP03_0010PE { get; set; }
        public int? DP03_0012E { get; set; }
        public double? DP03_0012PE { get; set; }
        public int? DP03_0013E { get; set; }
        public double? DP03_0013PE { get; set; }
        public int? DP03_0051E { get; set; }
        public double? DP03_0051PE { get; set; }
        public int? DP03_0052E { get; set; }
        public double? DP03_0052PE { get; set; }
        public int? DP03_0053E { get; set; }
        public double? DP03_0053PE { get; set; }
        public int? DP03_0054E { get; set; }
        public double? DP03_0054PE { get; set; }
        public int? DP03_0055E { get; set; }
        public double? DP03_0055PE { get; set; }
        public int? DP03_0056E { get; set; }
        public double? DP03_0056PE { get; set; }
        public int? DP03_0057E { get; set; }
        public double? DP03_0057PE { get; set; }
        public int? DP03_0058E { get; set; }
        public double? DP03_0058PE { get; set; }
        public int? DP03_0059E { get; set; }
        public double? DP03_0059PE { get; set; }
        public int? DP03_0060E { get; set; }
        public double? DP03_0060PE { get; set; }
        public int? DP03_0061E { get; set; }
        public double? DP03_0061PE { get; set; }
        public int? DP03_0063E { get; set; }
        public double? DP03_0063PE { get; set; }
        public int? DP03_0066E { get; set; }
        public double? DP03_0066PE { get; set; }
        public int? DP03_0068E { get; set; }
        public double? DP03_0068PE { get; set; }
        public int? DP03_0069E { get; set; }
        public double? DP03_0069PE { get; set; }
        public int? DP03_0070E { get; set; }
        public double? DP03_0070PE { get; set; }
        public int? DP03_0071E { get; set; }
        public double? DP03_0071PE { get; set; }
        public int? DP03_0072E { get; set; }
        public double? DP03_0072PE { get; set; }
        public int? DP03_0073E { get; set; }
        public double? DP03_0073PE { get; set; }
        public int? DP03_0095E { get; set; }
        public double? DP03_0095PE { get; set; }
        public int? DP03_0096E { get; set; }
        public double? DP03_0096PE { get; set; }
        public int? DP03_0097E { get; set; }
        public double? DP03_0097PE { get; set; }
        public int? DP03_0098E { get; set; }
        public double? DP03_0098PE { get; set; }
        public int? DP03_0099E { get; set; }
        public double? DP03_0099PE { get; set; }
        public int? DP03_0102E { get; set; }
        public double? DP03_0102PE { get; set; }
        public int? DP03_0103E { get; set; }
        public double? DP03_0103PE { get; set; }
        public int? DP03_0104E { get; set; }
        public double? DP03_0104PE { get; set; }
        public int? DP03_0105E { get; set; }
        public double? DP03_0105PE { get; set; }
        public int? DP03_0106E { get; set; }
        public double? DP03_0106PE { get; set; }
        public int? DP03_0107E { get; set; }
        public double? DP03_0107PE { get; set; }
        public int? DP03_0108E { get; set; }
        public double? DP03_0108PE { get; set; }
        public int? DP03_0109E { get; set; }
        public double? DP03_0109PE { get; set; }
        public int? DP03_0110E { get; set; }
        public double? DP03_0110PE { get; set; }
        public int? DP03_0111E { get; set; }
        public double? DP03_0111PE { get; set; }
        public int? DP03_0112E { get; set; }
        public double? DP03_0112PE { get; set; }
        public int? DP03_0113E { get; set; }
        public double? DP03_0113PE { get; set; }
        public int? DP03_0133E { get; set; }
        public double? DP03_0133PE { get; set; }
        public int? DP03_0134E { get; set; }
        public double? DP03_0134PE { get; set; }
        public int? DP03_0135E { get; set; }
        public double? DP03_0135PE { get; set; }
        public int? DP04_0006E { get; set; }
        public double? DP04_0006PE { get; set; }
        public int? DP04_0007E { get; set; }
        public double? DP04_0007PE { get; set; }
        public int? DP04_0008E { get; set; }
        public double? DP04_0008PE { get; set; }
        public int? DP04_0009E { get; set; }
        public double? DP04_0009PE { get; set; }
        public int? DP04_0010E { get; set; }
        public double? DP04_0010PE { get; set; }
        public int? DP04_0011E { get; set; }
        public double? DP04_0011PE { get; set; }
        public int? DP04_0012E { get; set; }
        public double? DP04_0012PE { get; set; }
        public int? DP04_0013E { get; set; }
        public double? DP04_0013PE { get; set; }
        public int? DP04_0027E { get; set; }
        public double? DP04_0027PE { get; set; }
        public int? DP04_0028E { get; set; }
        public double? DP04_0028PE { get; set; }
        public int? DP04_0029E { get; set; }
        public double? DP04_0029PE { get; set; }
        public int? DP04_0030E { get; set; }
        public double? DP04_0030PE { get; set; }
        public int? DP04_0031E { get; set; }
        public double? DP04_0031PE { get; set; }
        public int? DP04_0032E { get; set; }
        public double? DP04_0032PE { get; set; }
        public int? DP04_0033E { get; set; }
        public double? DP04_0033PE { get; set; }
        public int? DP04_0034E { get; set; }
        public double? DP04_0034PE { get; set; }
        public int? DP04_0035E { get; set; }
        public double? DP04_0035PE { get; set; }
        public int? DP04_0036E { get; set; }
        public double? DP04_0036PE { get; set; }
        public int? DP04_0037E { get; set; }
        public double? DP04_0037PE { get; set; }
        public int? DP04_0038E { get; set; }
        public double? DP04_0038PE { get; set; }
        public int? DP04_0039E { get; set; }
        public double? DP04_0039PE { get; set; }
        public int? DP04_0040E { get; set; }
        public double? DP04_0040PE { get; set; }
        public int? DP04_0041E { get; set; }
        public double? DP04_0041PE { get; set; }
        public int? DP04_0042E { get; set; }
        public double? DP04_0042PE { get; set; }
        public int? DP04_0043E { get; set; }
        public double? DP04_0043PE { get; set; }
        public int? DP04_0044E { get; set; }
        public double? DP04_0044PE { get; set; }
        public int? DP04_0045E { get; set; }
        public double? DP04_0045PE { get; set; }
        public int? DP04_0046E { get; set; }
        public double? DP04_0046PE { get; set; }
        public int? DP04_0047E { get; set; }
        public double? DP04_0047PE { get; set; }
        public int? DP04_0050E { get; set; }
        public double? DP04_0050PE { get; set; }
        public int? DP04_0051E { get; set; }
        public double? DP04_0051PE { get; set; }
        public int? DP04_0052E { get; set; }
        public double? DP04_0052PE { get; set; }
        public int? DP04_0053E { get; set; }
        public double? DP04_0053PE { get; set; }
        public int? DP04_0054E { get; set; }
        public double? DP04_0054PE { get; set; }
        public int? DP04_0055E { get; set; }
        public double? DP04_0055PE { get; set; }
        public int? DP04_0056E { get; set; }
        public double? DP04_0056PE { get; set; }
        public int? DP04_0076E { get; set; }
        public double? DP04_0076PE { get; set; }
        public int? DP04_0077E { get; set; }
        public double? DP04_0077PE { get; set; }
        public int? DP04_0078E { get; set; }
        public double? DP04_0078PE { get; set; }
        public int? DP04_0079E { get; set; }
        public double? DP04_0079PE { get; set; }
        public int? DP04_0090E { get; set; }
        public double? DP04_0090PE { get; set; }
        public int? DP04_0091E { get; set; }
        public double? DP04_0091PE { get; set; }
        public int? DP04_0092E { get; set; }
        public double? DP04_0092PE { get; set; }
        public int? DP04_0093E { get; set; }
        public double? DP04_0093PE { get; set; }
        public int? DP04_0094E { get; set; }
        public double? DP04_0094PE { get; set; }
        public int? DP04_0095E { get; set; }
        public double? DP04_0095PE { get; set; }
        public int? DP04_0096E { get; set; }
        public double? DP04_0096PE { get; set; }
        public int? DP04_0097E { get; set; }
        public double? DP04_0097PE { get; set; }
        public int? DP04_0098E { get; set; }
        public double? DP04_0098PE { get; set; }
        public int? DP04_0099E { get; set; }
        public double? DP04_0099PE { get; set; }
        public int? DP04_0100E { get; set; }
        public double? DP04_0100PE { get; set; }
        public int? DP04_0101E { get; set; }
        public double? DP04_0101PE { get; set; }
        public int? DP04_0110E { get; set; }
        public double? DP04_0110PE { get; set; }
        public int? DP04_0111E { get; set; }
        public double? DP04_0111PE { get; set; }
        public int? DP04_0112E { get; set; }
        public double? DP04_0112PE { get; set; }
        public int? DP04_0113E { get; set; }
        public double? DP04_0113PE { get; set; }
        public int? DP04_0114E { get; set; }
        public double? DP04_0114PE { get; set; }
        public int? DP04_0115E { get; set; }
        public double? DP04_0115PE { get; set; }
        public int? DP04_0126E { get; set; }
        public double? DP04_0126PE { get; set; }
        public int? DP04_0127E { get; set; }
        public double? DP04_0127PE { get; set; }
        public int? DP04_0128E { get; set; }
        public double? DP04_0128PE { get; set; }
        public int? DP04_0129E { get; set; }
        public double? DP04_0129PE { get; set; }
        public int? DP04_0130E { get; set; }
        public double? DP04_0130PE { get; set; }
        public int? DP04_0131E { get; set; }
        public double? DP04_0131PE { get; set; }
        public int? DP04_0132E { get; set; }
        public double? DP04_0132PE { get; set; }
        public int? DP04_0133E { get; set; }
        public double? DP04_0133PE { get; set; }
        public int? DP04_0134E { get; set; }
        public double? DP04_0134PE { get; set; }
        public int? DP04_0136E { get; set; }
        public double? DP04_0136PE { get; set; }
        public int? DP04_0137E { get; set; }
        public double? DP04_0137PE { get; set; }
        public int? DP04_0138E { get; set; }
        public double? DP04_0138PE { get; set; }
        public int? DP04_0139E { get; set; }
        public double? DP04_0139PE { get; set; }
        public int? DP04_0140E { get; set; }
        public double? DP04_0140PE { get; set; }
        public int? DP04_0141E { get; set; }
        public double? DP04_0141PE { get; set; }
        public int? DP04_0142E { get; set; }
        public double? DP04_0142PE { get; set; }
    }
    public class HeatmapAttributes
    {
        public int CD { get; set; }
        public string DISTRICT { get; set; }
        public Int64? BasePeriodValue { get; set; }
        public Int64? AnalysisPeriodValue { get; set; }
        public Int64? diff_value { get; set; }
        public decimal? diff_percentage { get; set; }
    }
    public class SpiderChartAttributes
    {
        public string CenterPoint { get; set; }
        public string CenterPointValue { get; set; }
        public int CountNum { get; set; }
    }
}