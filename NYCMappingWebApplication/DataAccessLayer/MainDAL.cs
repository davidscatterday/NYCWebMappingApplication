using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class MainDAL
    {
        public List<Select2DTO> GetAllBoroughs(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { id = "'BX'", text = "Bronx" });
            returnResult.Add(new Select2DTO() { id = "'BK'", text = "Brooklyn" });
            returnResult.Add(new Select2DTO() { id = "'MN'", text = "Manhattan" });
            returnResult.Add(new Select2DTO() { id = "'QN'", text = "Queens" });
            returnResult.Add(new Select2DTO() { id = "'SI'", text = "Staten Island" });
            return returnResult.Where(w => w.text.Contains(term)).ToList();
        }
        public List<SelectListItem> GetAllZoningDistricts()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "Residence", Value = "ZoneDist1 LIKE 'R%' AND ZoneDist1 NOT LIKE '%/%'" });
            returnResult.Add(new SelectListItem() { Text = "Commercial", Value = "ZoneDist1 LIKE 'C%' AND ZoneDist1 NOT LIKE '%/%'" });
            returnResult.Add(new SelectListItem() { Text = "Manufacturing", Value = "ZoneDist1 LIKE 'M%' AND ZoneDist1 NOT LIKE '%/%'" });
            returnResult.Add(new SelectListItem() { Text = "Mixed Manufacturing & Residential", Value = "ZoneDist1 LIKE 'M%' AND ZoneDist1 LIKE '%/R%'" });
            return returnResult;
        }
        public List<SelectListItem> GetAllCommercialOverlays()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "C1", Value = "(Overlay1 LIKE 'C1%' OR Overlay2 LIKE 'C1%')" });
            returnResult.Add(new SelectListItem() { Text = "C2", Value = "(Overlay1 LIKE 'C2%' OR Overlay2 LIKE 'C2%')" });
            return returnResult;
        }
        public List<Select2DTO> GetAllJobTypes(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { text = "New Building", id = "'NB'" });
            returnResult.Add(new Select2DTO() { text = "Demolition", id = "'DM'" });
            returnResult.Add(new Select2DTO() { text = "Alteration Type 1", id = "'A1'" });
            returnResult.Add(new Select2DTO() { text = "Alteration Type 2", id = "'A2'" });
            returnResult.Add(new Select2DTO() { text = "Alteration Type 3", id = "'A3'" });
            return returnResult.Where(w => w.text.Contains(term)).ToList();
        }
        public List<Select2DTO> GetAllWorkTypes(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { text = "Alteration", id = "'AL'" });
            returnResult.Add(new Select2DTO() { text = "Demolition & Removal", id = "'DM'" });
            returnResult.Add(new Select2DTO() { text = "Construction Equipment", id = "'EQ'" });
            returnResult.Add(new Select2DTO() { text = "Chute", id = "'CH'" });
            returnResult.Add(new Select2DTO() { text = "Fence", id = "'FN'" });
            returnResult.Add(new Select2DTO() { text = "Sidewalk Shed", id = "'SH'" });
            returnResult.Add(new Select2DTO() { text = "Scaffold", id = "'SF'" });
            returnResult.Add(new Select2DTO() { text = "Other-General Construction, Partitions, Marquees, BPP (Builder Pavement Plan), etc.", id = "'OT'" });
            returnResult.Add(new Select2DTO() { text = "Equipment Work", id = "'EW'" });
            returnResult.Add(new Select2DTO() { text = "Boiler", id = "'BL'" });
            returnResult.Add(new Select2DTO() { text = "Fire Alarm", id = "'FA'" });
            returnResult.Add(new Select2DTO() { text = "Fuel Burning", id = "'FB'" });
            returnResult.Add(new Select2DTO() { text = "Fire Suppression", id = "'FP'" });
            returnResult.Add(new Select2DTO() { text = "Fuel Storage", id = "'FS'" });
            returnResult.Add(new Select2DTO() { text = "Mechanical / HVAC", id = "'MH'" });
            returnResult.Add(new Select2DTO() { text = "Standpipe", id = "'SD'" });
            returnResult.Add(new Select2DTO() { text = "Sprinkler", id = "'SP'" });
            returnResult.Add(new Select2DTO() { text = "Foundation / Earthwork", id = "'FO'" });
            returnResult.Add(new Select2DTO() { text = "Earthwork Only", id = "'FO/EA'" });
            returnResult.Add(new Select2DTO() { text = "New Building", id = "'NB'" });
            returnResult.Add(new Select2DTO() { text = "Plumbing", id = "'PL'" });
            returnResult.Add(new Select2DTO() { text = "Sign", id = "'SG'" });
            return returnResult.Where(w => w.text.Contains(term)).ToList();
        }
        public List<Select2DTO> GetAllViolationTypes(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { text = "E-ELEVATOR", id = "'E-ELEVATOR'" });
            returnResult.Add(new Select2DTO() { text = "LL6291-LOCAL LAW 62/91 - BOILERS", id = "'LL6291-LOCAL LAW 62/91 - BOILERS'" });
            returnResult.Add(new Select2DTO() { text = "LBLVIO-LOW PRESSURE BOILER", id = "'LBLVIO-LOW PRESSURE BOILER'" });
            returnResult.Add(new Select2DTO() { text = "C-CONSTRUCTION", id = "'C-CONSTRUCTION'" });
            returnResult.Add(new Select2DTO() { text = "AEUHAZ1-FAIL TO CERTIFY CLASS 1", id = "'AEUHAZ1-FAIL TO CERTIFY CLASS 1'" });
            returnResult.Add(new Select2DTO() { text = "LL1081-LOCAL LAW 10/81 - ELEVATOR", id = "'LL1081-LOCAL LAW 10/81 - ELEVATOR'" });
            returnResult.Add(new Select2DTO() { text = "ACC1-(OTHER BLDGS TYPES) - ELEVATOR AFFIRMATION OF CORRECTION", id = "'ACC1-(OTHER BLDGS TYPES) - ELEVATOR AFFIRMATION OF CORRECTION'" });
            returnResult.Add(new Select2DTO() { text = "EVCAT1-ELEVATOR ANNUAL INSPECTION / TEST", id = "'EVCAT1-ELEVATOR ANNUAL INSPECTION / TEST'" });
            returnResult.Add(new Select2DTO() { text = "BENCH-FAILURE TO BENCHMARK", id = "'BENCH-FAILURE TO BENCHMARK'" });
            returnResult.Add(new Select2DTO() { text = "LANDMK-LANDMARK", id = "'LANDMK-LANDMARK'" });
            returnResult.Add(new Select2DTO() { text = "UB-UNSAFE BUILDINGS", id = "'UB-UNSAFE BUILDINGS'" });
            returnResult.Add(new Select2DTO() { text = "FISPNRF-NO REPORT AND / OR LATE FILING (FACADE)", id = "'FISPNRF-NO REPORT AND / OR LATE FILING (FACADE)'" });
            returnResult.Add(new Select2DTO() { text = "P-PLUMBING", id = "'P-PLUMBING'" });
            returnResult.Add(new Select2DTO() { text = "ES-ELECTRIC SIGNS", id = "'ES-ELECTRIC SIGNS'" });
            returnResult.Add(new Select2DTO() { text = "Z-ZONING", id = "'Z-ZONING'" });
            returnResult.Add(new Select2DTO() { text = "EVCAT5-NON-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST", id = "'EVCAT5-NON-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST'" });
            returnResult.Add(new Select2DTO() { text = "IMEGNCY-IMMEDIATE EMERGENCY", id = "'IMEGNCY-IMMEDIATE EMERGENCY'" });
            returnResult.Add(new Select2DTO() { text = "JVIOS-PRIVATE RESIDENTIAL ELEVATOR", id = "'JVIOS-PRIVATE RESIDENTIAL ELEVATOR'" });
            returnResult.Add(new Select2DTO() { text = "B-BOILER", id = "'B-BOILER'" });
            returnResult.Add(new Select2DTO() { text = "VCAT1-ELEVATOR ANNUAL INSPECTION / TEST", id = "'VCAT1-ELEVATOR ANNUAL INSPECTION / TEST'" });
            returnResult.Add(new Select2DTO() { text = "L1198-LOCAL LAW 11/98 - FACADE", id = "'L1198-LOCAL LAW 11/98 - FACADE'" });
            returnResult.Add(new Select2DTO() { text = "HBLVIO-HIGH PRESSURE BOILER", id = "'HBLVIO-HIGH PRESSURE BOILER'" });
            returnResult.Add(new Select2DTO() { text = "EARCX-FAILURE TO SUBMIT EER", id = "'EARCX-FAILURE TO SUBMIT EER'" });
            returnResult.Add(new Select2DTO() { text = "LL1198-LOCAL LAW 11/98 - FACADE", id = "'LL1198-LOCAL LAW 11/98 - FACADE'" });
            returnResult.Add(new Select2DTO() { text = "CMQ-MARQUEE", id = "'CMQ-MARQUEE'" });
            returnResult.Add(new Select2DTO() { text = "LL11/98-LOCAL LAW 11/98 - FACADE", id = "'LL11/98-LOCAL LAW 11/98 - FACADE'" });
            returnResult.Add(new Select2DTO() { text = "EGNCY-EMERGENCY", id = "'EGNCY-EMERGENCY'" });
            returnResult.Add(new Select2DTO() { text = "LL1080-LOCAL LAW 10/80 - FACADE", id = "'LL1080-LOCAL LAW 10/80 - FACADE'" });
            returnResult.Add(new Select2DTO() { text = "FISP-FACADE SAFETY PROGRAM", id = "'FISP-FACADE SAFETY PROGRAM'" });
            returnResult.Add(new Select2DTO() { text = "LANDMRK-LANDMARK", id = "'LANDMRK-LANDMARK'" });
            returnResult.Add(new Select2DTO() { text = "ACH1-(NYCHA) - ELEVATOR AFFIRMATION OF CORRECTION", id = "'ACH1-(NYCHA) - ELEVATOR AFFIRMATION OF CORRECTION'" });
            returnResult.Add(new Select2DTO() { text = "LL10/80-LOCAL LAW 10/80 - FACADE", id = "'LL10/80-LOCAL LAW 10/80 - FACADE'" });
            returnResult.Add(new Select2DTO() { text = "LL2604E-EMERGENCY POWER", id = "'LL2604E-EMERGENCY POWER'" });
            returnResult.Add(new Select2DTO() { text = "RWNRF-RETAINING WALL", id = "'RWNRF-RETAINING WALL'" });
            returnResult.Add(new Select2DTO() { text = "PA-PUBLIC ASSEMBLY", id = "'PA-PUBLIC ASSEMBLY'" });
            returnResult.Add(new Select2DTO() { text = "LL2604S-SPRINKLER", id = "'LL2604S-SPRINKLER'" });
            returnResult.Add(new Select2DTO() { text = "LL5-LOCAL LAW 5/73", id = "'LL5-LOCAL LAW 5/73'" });
            returnResult.Add(new Select2DTO() { text = "ACJ1-(PRIVATE RESIDENCE) - ELEVATOR AFFIRMATION OF CORRECTION", id = "'ACJ1-(PRIVATE RESIDENCE) - ELEVATOR AFFIRMATION OF CORRECTION'" });
            returnResult.Add(new Select2DTO() { text = "LL16-LOCAL LAW 16/84 - ELEVATOR", id = "'LL16-LOCAL LAW 16/84 - ELEVATOR'" });
            returnResult.Add(new Select2DTO() { text = "JVCAT5-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST", id = "'JVCAT5-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST'" });
            returnResult.Add(new Select2DTO() { text = "CS-SITE SAFETY", id = "'CS-SITE SAFETY'" });
            returnResult.Add(new Select2DTO() { text = "LL2604-PHOTOLUMINESCENT", id = "'LL2604-PHOTOLUMINESCENT'" });
            returnResult.Add(new Select2DTO() { text = "LL10/81-LOCAL LAW 10/81 - ELEVATOR", id = "'LL10/81-LOCAL LAW 10/81 - ELEVATOR'" });
            returnResult.Add(new Select2DTO() { text = "HVIOS-NYCHA ELEV ANNUAL INSPECTION/TEST", id = "'HVIOS-NYCHA ELEV ANNUAL INSPECTION/TEST'" });
            returnResult.Add(new Select2DTO() { text = "CLOS-PADLOCK", id = "'CLOS-PADLOCK'" });
            returnResult.Add(new Select2DTO() { text = "HVCAT5-NYCHA ELEVATOR PERIODIC INSPECTION/TEST", id = "'HVCAT5-NYCHA ELEVATOR PERIODIC INSPECTION/TEST'" });
            returnResult.Add(new Select2DTO() { text = "A-SEU", id = "'A-SEU'" });
            returnResult.Add(new Select2DTO() { text = "IMD-IMMEDIATE EMERGENCY", id = "'IMD-IMMEDIATE EMERGENCY'" });
            returnResult.Add(new Select2DTO() { text = "COMPBLD-STRUCTURALLY COMPROMISED BUILDING", id = "'COMPBLD-STRUCTURALLY COMPROMISED BUILDING'" });
            return returnResult.OrderBy(w => w.text).Where(w => w.text.Contains(term)).ToList();
        }
        public List<Select2DTO> GetAllViolationCategories(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { text = "V*-DOB VIOLATION - DISMISSED", id = "'V*-DOB VIOLATION - DISMISSED'" });
            returnResult.Add(new Select2DTO() { text = "V*-DOB VIOLATION - Resolved", id = "'V*-DOB VIOLATION - Resolved'" });
            returnResult.Add(new Select2DTO() { text = "V-DOB VIOLATION - ACTIVE", id = "'V-DOB VIOLATION - ACTIVE'" });
            return returnResult.Where(w => w.text.Contains(term)).ToList();
        }
        public List<SelectListItem> GetAllEvictionStatuses()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "Completed", Value = "1=1" });
            returnResult.Add(new SelectListItem() { Text = "Scheduled", Value = "1=2" });
            return returnResult;
        }
        public List<DatabaseAttributes> SearchDatabase(string sqlQuery)
        {
            List<DatabaseAttributes> returnResult = new List<DatabaseAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                returnResult = ctx.Database.SqlQuery<DatabaseAttributes>(sqlQuery).ToList();
            }
            return returnResult;
        }
    }
}