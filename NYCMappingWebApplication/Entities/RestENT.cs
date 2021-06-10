using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NYCMappingWebApp.Entities
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class EvictionOutput
    {
        public string Borough { get; set; }
        public string ZipCode { get; set; }
        public string BldgArea { get; set; }
        public string ComArea { get; set; }
        public string ResArea { get; set; }
        public string NumFloors { get; set; }
        public string UnitsRes { get; set; }
        public string ZoneDist1 { get; set; }
        public string AssessTot { get; set; }
        public string YearBuilt { get; set; }
        public string BldgClass { get; set; }
        public string LandUse { get; set; }
        public string EVICTION_STATUS { get; set; }
        public string Scored_Labels { get; set; }
        public string Scored_Probabilities { get; set; }
    }

    public class JsonEvictionOutput
    {
        public string Results { get; set; }
        public string output1 { get; set; }
        public string type { get; set; }
        public string table { get; set; }
        public string value { get; set; }
        public string[] ColumnNames { get; set; }
        public string[] ColumnTypes { get; set; }
        public string[] Values { get; set; }
    }
}