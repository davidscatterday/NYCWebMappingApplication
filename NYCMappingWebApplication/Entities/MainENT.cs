using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
    public class IndexData
    {
        public string Boroughs { get; set; }
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
    }
}