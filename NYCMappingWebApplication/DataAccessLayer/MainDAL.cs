using NYCMappingWebApp.Entities;
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
        public List<SelectListItem> GetAllJobTypes()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "New Building", Value = "NB" });
            returnResult.Add(new SelectListItem() { Text = "Demolition", Value = "DM" });
            returnResult.Add(new SelectListItem() { Text = "Alteration Type 1", Value = "A1" });
            returnResult.Add(new SelectListItem() { Text = "Alteration Type 2", Value = "A2" });
            returnResult.Add(new SelectListItem() { Text = "Alteration Type 3", Value = "A3" });
            return returnResult;
        }
        public List<SelectListItem> GetAllWorkTypes()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "Alteration", Value = "AL" });
            returnResult.Add(new SelectListItem() { Text = "Demolition & Removal", Value = "DM" });
            returnResult.Add(new SelectListItem() { Text = "Construction Equipment", Value = "EQ" });
            returnResult.Add(new SelectListItem() { Text = "Chute", Value = "CH" });
            returnResult.Add(new SelectListItem() { Text = "Fence", Value = "FN" });
            returnResult.Add(new SelectListItem() { Text = "Sidewalk Shed", Value = "SH" });
            returnResult.Add(new SelectListItem() { Text = "Scaffold", Value = "SF" });
            returnResult.Add(new SelectListItem() { Text = "Other-General Construction, Partitions, Marquees, BPP (Builder Pavement Plan), etc.", Value = "OT" });
            returnResult.Add(new SelectListItem() { Text = "Equipment Work", Value = "EW" });
            returnResult.Add(new SelectListItem() { Text = "Boiler", Value = "BL" });
            returnResult.Add(new SelectListItem() { Text = "Fire Alarm", Value = "FA" });
            returnResult.Add(new SelectListItem() { Text = "Fuel Burning", Value = "FB" });
            returnResult.Add(new SelectListItem() { Text = "Fire Suppression", Value = "FP" });
            returnResult.Add(new SelectListItem() { Text = "Fuel Storage", Value = "FS" });
            returnResult.Add(new SelectListItem() { Text = "Mechanical / HVAC", Value = "MH" });
            returnResult.Add(new SelectListItem() { Text = "Standpipe", Value = "SD" });
            returnResult.Add(new SelectListItem() { Text = "Sprinkler", Value = "SP" });
            returnResult.Add(new SelectListItem() { Text = "Foundation / Earthwork", Value = "FO" });
            returnResult.Add(new SelectListItem() { Text = "Earthwork Only", Value = "FO/EA" });
            returnResult.Add(new SelectListItem() { Text = "New Building", Value = "NB" });
            returnResult.Add(new SelectListItem() { Text = "Plumbing", Value = "PL" });
            returnResult.Add(new SelectListItem() { Text = "Sign", Value = "SG" });
            return returnResult;
        }
        public List<SelectListItem> GetAllViolationTypes()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "E-ELEVATOR", Value = "E-ELEVATOR" });
            returnResult.Add(new SelectListItem() { Text = "LL6291-LOCAL LAW 62/91 - BOILERS", Value = "LL6291-LOCAL LAW 62/91 - BOILERS" });
            returnResult.Add(new SelectListItem() { Text = "LBLVIO-LOW PRESSURE BOILER", Value = "LBLVIO-LOW PRESSURE BOILER" });
            returnResult.Add(new SelectListItem() { Text = "C-CONSTRUCTION", Value = "C-CONSTRUCTION" });
            returnResult.Add(new SelectListItem() { Text = "AEUHAZ1-FAIL TO CERTIFY CLASS 1", Value = "AEUHAZ1-FAIL TO CERTIFY CLASS 1" });
            returnResult.Add(new SelectListItem() { Text = "LL1081-LOCAL LAW 10/81 - ELEVATOR", Value = "LL1081-LOCAL LAW 10/81 - ELEVATOR" });
            returnResult.Add(new SelectListItem() { Text = "ACC1-(OTHER BLDGS TYPES) - ELEVATOR AFFIRMATION OF CORRECTION", Value = "ACC1-(OTHER BLDGS TYPES) - ELEVATOR AFFIRMATION OF CORRECTION" });
            returnResult.Add(new SelectListItem() { Text = "EVCAT1-ELEVATOR ANNUAL INSPECTION / TEST", Value = "EVCAT1-ELEVATOR ANNUAL INSPECTION / TEST" });
            returnResult.Add(new SelectListItem() { Text = "BENCH-FAILURE TO BENCHMARK", Value = "BENCH-FAILURE TO BENCHMARK" });
            returnResult.Add(new SelectListItem() { Text = "LANDMK-LANDMARK", Value = "LANDMK-LANDMARK" });
            returnResult.Add(new SelectListItem() { Text = "UB-UNSAFE BUILDINGS", Value = "UB-UNSAFE BUILDINGS" });
            returnResult.Add(new SelectListItem() { Text = "FISPNRF-NO REPORT AND / OR LATE FILING (FACADE)", Value = "FISPNRF-NO REPORT AND / OR LATE FILING (FACADE)" });
            returnResult.Add(new SelectListItem() { Text = "P-PLUMBING", Value = "P-PLUMBING" });
            returnResult.Add(new SelectListItem() { Text = "ES-ELECTRIC SIGNS", Value = "ES-ELECTRIC SIGNS" });
            returnResult.Add(new SelectListItem() { Text = "Z-ZONING", Value = "Z-ZONING" });
            returnResult.Add(new SelectListItem() { Text = "EVCAT5-NON-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST", Value = "EVCAT5-NON-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST" });
            returnResult.Add(new SelectListItem() { Text = "IMEGNCY-IMMEDIATE EMERGENCY", Value = "IMEGNCY-IMMEDIATE EMERGENCY" });
            returnResult.Add(new SelectListItem() { Text = "JVIOS-PRIVATE RESIDENTIAL ELEVATOR", Value = "JVIOS-PRIVATE RESIDENTIAL ELEVATOR" });
            returnResult.Add(new SelectListItem() { Text = "B-BOILER", Value = "B-BOILER" });
            returnResult.Add(new SelectListItem() { Text = "VCAT1-ELEVATOR ANNUAL INSPECTION / TEST", Value = "VCAT1-ELEVATOR ANNUAL INSPECTION / TEST" });
            returnResult.Add(new SelectListItem() { Text = "L1198-LOCAL LAW 11/98 - FACADE", Value = "L1198-LOCAL LAW 11/98 - FACADE" });
            returnResult.Add(new SelectListItem() { Text = "HBLVIO-HIGH PRESSURE BOILER", Value = "HBLVIO-HIGH PRESSURE BOILER" });
            returnResult.Add(new SelectListItem() { Text = "EARCX-FAILURE TO SUBMIT EER", Value = "EARCX-FAILURE TO SUBMIT EER" });
            returnResult.Add(new SelectListItem() { Text = "LL1198-LOCAL LAW 11/98 - FACADE", Value = "LL1198-LOCAL LAW 11/98 - FACADE" });
            returnResult.Add(new SelectListItem() { Text = "CMQ-MARQUEE", Value = "CMQ-MARQUEE" });
            returnResult.Add(new SelectListItem() { Text = "LL11/98-LOCAL LAW 11/98 - FACADE", Value = "LL11/98-LOCAL LAW 11/98 - FACADE" });
            returnResult.Add(new SelectListItem() { Text = "EGNCY-EMERGENCY", Value = "EGNCY-EMERGENCY" });
            returnResult.Add(new SelectListItem() { Text = "LL1080-LOCAL LAW 10/80 - FACADE", Value = "LL1080-LOCAL LAW 10/80 - FACADE" });
            returnResult.Add(new SelectListItem() { Text = "FISP-FACADE SAFETY PROGRAM", Value = "FISP-FACADE SAFETY PROGRAM" });
            returnResult.Add(new SelectListItem() { Text = "LANDMRK-LANDMARK", Value = "LANDMRK-LANDMARK" });
            returnResult.Add(new SelectListItem() { Text = "ACH1-(NYCHA) - ELEVATOR AFFIRMATION OF CORRECTION", Value = "ACH1-(NYCHA) - ELEVATOR AFFIRMATION OF CORRECTION" });
            returnResult.Add(new SelectListItem() { Text = "LL10/80-LOCAL LAW 10/80 - FACADE", Value = "LL10/80-LOCAL LAW 10/80 - FACADE" });
            returnResult.Add(new SelectListItem() { Text = "LL2604E-EMERGENCY POWER", Value = "LL2604E-EMERGENCY POWER" });
            returnResult.Add(new SelectListItem() { Text = "RWNRF-RETAINING WALL", Value = "RWNRF-RETAINING WALL" });
            returnResult.Add(new SelectListItem() { Text = "PA-PUBLIC ASSEMBLY", Value = "PA-PUBLIC ASSEMBLY" });
            returnResult.Add(new SelectListItem() { Text = "LL2604S-SPRINKLER", Value = "LL2604S-SPRINKLER" });
            returnResult.Add(new SelectListItem() { Text = "LL5-LOCAL LAW 5/73", Value = "LL5-LOCAL LAW 5/73" });
            returnResult.Add(new SelectListItem() { Text = "ACJ1-(PRIVATE RESIDENCE) - ELEVATOR AFFIRMATION OF CORRECTION", Value = "ACJ1-(PRIVATE RESIDENCE) - ELEVATOR AFFIRMATION OF CORRECTION" });
            returnResult.Add(new SelectListItem() { Text = "LL16-LOCAL LAW 16/84 - ELEVATOR", Value = "LL16-LOCAL LAW 16/84 - ELEVATOR" });
            returnResult.Add(new SelectListItem() { Text = "JVCAT5-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST", Value = "JVCAT5-RESIDENTIAL ELEVATOR PERIODIC INSPECTION/TEST" });
            returnResult.Add(new SelectListItem() { Text = "CS-SITE SAFETY", Value = "CS-SITE SAFETY" });
            returnResult.Add(new SelectListItem() { Text = "LL2604-PHOTOLUMINESCENT", Value = "LL2604-PHOTOLUMINESCENT" });
            returnResult.Add(new SelectListItem() { Text = "LL10/81-LOCAL LAW 10/81 - ELEVATOR", Value = "LL10/81-LOCAL LAW 10/81 - ELEVATOR" });
            returnResult.Add(new SelectListItem() { Text = "HVIOS-NYCHA ELEV ANNUAL INSPECTION/TEST", Value = "HVIOS-NYCHA ELEV ANNUAL INSPECTION/TEST" });
            returnResult.Add(new SelectListItem() { Text = "CLOS-PADLOCK", Value = "CLOS-PADLOCK" });
            returnResult.Add(new SelectListItem() { Text = "HVCAT5-NYCHA ELEVATOR PERIODIC INSPECTION/TEST", Value = "HVCAT5-NYCHA ELEVATOR PERIODIC INSPECTION/TEST" });
            returnResult.Add(new SelectListItem() { Text = "A-SEU", Value = "A-SEU" });
            returnResult.Add(new SelectListItem() { Text = "IMD-IMMEDIATE EMERGENCY", Value = "IMD-IMMEDIATE EMERGENCY" });
            returnResult.Add(new SelectListItem() { Text = "COMPBLD-STRUCTURALLY COMPROMISED BUILDING", Value = "COMPBLD-STRUCTURALLY COMPROMISED BUILDING" });
            return returnResult.OrderBy(w => w.Text).ToList();
        }
        public List<SelectListItem> GetAllViolationCategories()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "V*-DOB VIOLATION - DISMISSED", Value = "V*-DOB VIOLATION - DISMISSED" });
            returnResult.Add(new SelectListItem() { Text = "V*-DOB VIOLATION - Resolved", Value = "V*-DOB VIOLATION - Resolved" });
            returnResult.Add(new SelectListItem() { Text = "V-DOB VIOLATION - ACTIVE", Value = "V-DOB VIOLATION - ACTIVE" });
            return returnResult;
        }
    }
}