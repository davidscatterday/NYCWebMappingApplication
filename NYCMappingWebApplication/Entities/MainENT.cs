using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
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
}