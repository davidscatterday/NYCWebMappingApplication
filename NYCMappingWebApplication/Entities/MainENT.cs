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
        public string Address { get; set; }
        public string Block { get; set; }
        public string Lot { get; set; }
        public string EnergyStarScore { get; set; }
        public string SourceEUI { get; set; }
        public string SiteEUI { get; set; }
        public string AnnualMaximumDemand { get; set; }
        public string TotalGHGEmissions { get; set; }
    }
}