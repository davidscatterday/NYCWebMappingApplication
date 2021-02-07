using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Helpers;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.DataAccessLayer
{
    public class MainDAL
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public List<Select2DTO> GetAllBoroughs(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { id = "'BX'", text = "Bronx" });
            returnResult.Add(new Select2DTO() { id = "'BK'", text = "Brooklyn" });
            returnResult.Add(new Select2DTO() { id = "'MN'", text = "Manhattan" });
            returnResult.Add(new Select2DTO() { id = "'QN'", text = "Queens" });
            returnResult.Add(new Select2DTO() { id = "'SI'", text = "Staten Island" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetAllBoroughsCP(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { id = "'005'", text = "Bronx" });
            returnResult.Add(new Select2DTO() { id = "'047'", text = "Brooklyn" });
            returnResult.Add(new Select2DTO() { id = "'061'", text = "Manhattan" });
            returnResult.Add(new Select2DTO() { id = "'081'", text = "Queens" });
            returnResult.Add(new Select2DTO() { id = "'085'", text = "Staten Island" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetAllBoroughsTA(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { id = "BX", text = "Bronx" });
            returnResult.Add(new Select2DTO() { id = "BK", text = "Brooklyn" });
            returnResult.Add(new Select2DTO() { id = "MN", text = "Manhattan" });
            returnResult.Add(new Select2DTO() { id = "QN", text = "Queens" });
            returnResult.Add(new Select2DTO() { id = "SI", text = "Staten Island" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetAllDistricts(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.Districts
                            where term == "" || d.DISTRICT1.ToLower().Contains(term.ToLower())
                            select new Select2DTO()
                            {
                                id = d.DISTRICTCODE.ToString(),
                                text = d.DISTRICT1
                            }).OrderBy(w => w.text).ToList();
            return returnResult;
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
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
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
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
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
            return returnResult.OrderBy(w => w.text).Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetAllViolationCategories(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { text = "V*-DOB VIOLATION - DISMISSED", id = "'V*-DOB VIOLATION - DISMISSED'" });
            returnResult.Add(new Select2DTO() { text = "V*-DOB VIOLATION - Resolved", id = "'V*-DOB VIOLATION - Resolved'" });
            returnResult.Add(new Select2DTO() { text = "V-DOB VIOLATION - ACTIVE", id = "'V-DOB VIOLATION - ACTIVE'" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<SelectListItem> GetAllEvictionStatuses()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "Completed", Value = "1=1" });
            returnResult.Add(new SelectListItem() { Text = "Scheduled", Value = "1=2" });
            return returnResult;
        }
        public List<SelectListItem> GetAllElevatorDeviceTypes()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult = (from el in db.Elevators
                            select new SelectListItem()
                            {
                                Value = el.elevatordevicetype,
                                Text = el.elevatordevicetype
                            }).OrderBy(w => w.Text).Distinct().ToList();
            return returnResult;
        }
        public List<SelectListItem> GetAllFilingTypes()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult = (from el in db.Elevators
                            select new SelectListItem()
                            {
                                Value = el.filing_type,
                                Text = el.filing_type
                            }).OrderBy(w => w.Text).Distinct().ToList();
            return returnResult;
        }
        public List<SelectListItem> GetAllFilingStatuses()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult = (from el in db.Elevators
                            select new SelectListItem()
                            {
                                Value = el.filing_status,
                                Text = el.filing_status
                            }).OrderBy(w => w.Text).Distinct().ToList();
            return returnResult;
        }
        public List<Select2DTO> GetTrendAnalysisEcbViolationTypes(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { text = "Boilers", id = "Boilers;B-BOILER" });
            returnResult.Add(new Select2DTO() { text = "Construction", id = "Construction;C-CONSTRUCTION" });
            returnResult.Add(new Select2DTO() { text = "Elevators", id = "Elevators;E-ELEVATOR" });
            returnResult.Add(new Select2DTO() { text = "HPD", id = "HPD" });
            returnResult.Add(new Select2DTO() { text = "Local Law", id = "Local Law" });
            returnResult.Add(new Select2DTO() { text = "Plumbing", id = "Plumbing;P-PLUMBING" });
            returnResult.Add(new Select2DTO() { text = "Quality of Life", id = "Quality of Life" });
            returnResult.Add(new Select2DTO() { text = "Signs", id = "Signs" });
            returnResult.Add(new Select2DTO() { text = "Unsafe Buildings", id = "UB-UNSAFE BUILDINGS" });
            returnResult.Add(new Select2DTO() { text = "Zoning", id = "Zoning;Z-ZONING" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetTrendAnalysisPermitWorkTypes(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { id = "BL", text = "BL-Boiler" });
            returnResult.Add(new Select2DTO() { id = "EQ", text = "EQ-Construction Equipment" });
            returnResult.Add(new Select2DTO() { id = "FA", text = "FA-Fire Alarm" });
            returnResult.Add(new Select2DTO() { id = "FB", text = "FB-Fuel Burning" });
            returnResult.Add(new Select2DTO() { id = "FP", text = "FP-Fire Suppression" });
            returnResult.Add(new Select2DTO() { id = "FS", text = "FS-Fuel Storage" });
            returnResult.Add(new Select2DTO() { id = "MH", text = "MH-Mechanical/HVAC" });
            returnResult.Add(new Select2DTO() { id = "NB", text = "NB-New Building" });
            returnResult.Add(new Select2DTO() { id = "OT", text = "OT-Other-General Construction, Partitions, Marquees, BPP (Builder Pavement Plan), etc." });
            returnResult.Add(new Select2DTO() { id = "PL", text = "PL-Plumbing" });
            returnResult.Add(new Select2DTO() { id = "SD", text = "SD-Standpipe" });
            returnResult.Add(new Select2DTO() { id = "SP", text = "SP-Sprinkler" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetTrendAnalysisPermitJobTypes(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult.Add(new Select2DTO() { id = "NB", text = "New Building" });
            returnResult.Add(new Select2DTO() { id = "DM", text = "Demolition" });
            returnResult.Add(new Select2DTO() { id = "A1", text = "Alteration Type 1" });
            returnResult.Add(new Select2DTO() { id = "A2", text = "Alteration Type 2" });
            returnResult.Add(new Select2DTO() { id = "A3", text = "Alteration Type 3" });
            return returnResult.Where(w => w.text.ToLower().Contains(term.ToLower())).ToList();
        }
        public List<Select2DTO> GetDesignationDescriptions(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.Designations
                            where !String.IsNullOrEmpty(d.DESCRIPTION) && (term == "" || d.DESCRIPTION.ToLower().Contains(term.ToLower()))
                            select new Select2DTO()
                            {
                                id = "'" + d.DESCRIPTION + "'",
                                text = d.DESCRIPTION
                            }).OrderBy(w => w.text).Distinct().ToList();
            return returnResult;
        }

        public List<SelectListItem> GetAllYesNoStatuses()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "Yes", Value = "Y" });
            returnResult.Add(new SelectListItem() { Text = "No", Value = "N" });
            return returnResult;
        }
        public List<DatabaseAttributes> SearchDatabase(string sqlQuery)
        {
            List<DatabaseAttributes> returnResult = new List<DatabaseAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                ctx.Database.CommandTimeout = 600;
                returnResult = ctx.Database.SqlQuery<DatabaseAttributes>(sqlQuery).ToList();
            }
            return returnResult;
        }
        public List<SelectListItem> GetAllFrequencies()
        {
            List<SelectListItem> returnResult = new List<SelectListItem>();
            returnResult.Add(new SelectListItem() { Text = "As-it-Happens", Value = "0" });
            returnResult.Add(new SelectListItem() { Text = "Daily", Value = "1" });
            returnResult.Add(new SelectListItem() { Text = "Weekly", Value = "7" });
            return returnResult;
        }

        public DatabaseMaxValues GetMaxValues()
        {
            DatabaseMaxValues result = new DatabaseMaxValues();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                result = ctx.Database.SqlQuery<DatabaseMaxValues>("EXEC dbo.GetMaxValues ").FirstOrDefault();
            }
            return result;
        }

        public List<MyAlertENT> GetMyAlerts(string username)
        {
            List<MyAlertENT> result = new List<MyAlertENT>();
            result = (from r in db.MyAlerts.AsEnumerable()
                      where r.Username == username
                      select new MyAlertENT
                      {
                          ID = r.ID,
                          AlertName = r.AlertName,
                          DateCreated = r.DateCreated,
                          DateCreatedString = String.Format("{0:MM/dd/yyyy}", r.DateCreated),
                          IsUnread = r.IsUnread.Value
                      }).ToList();
            return result;
        }

        public bool DeleteAlert(int AlertID)
        {
            try
            {
                MyAlert alert = db.MyAlerts.Find(AlertID);
                db.MyAlerts.Remove(alert);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MyReport> GetMyReports(string username)
        {
            List<MyReport> result = new List<MyReport>();
            result = (from r in db.MyReports.AsEnumerable()
                      where r.Username == username
                      select new MyReport
                      {
                          ID = r.ID,
                          Username = r.Username,
                          FileName = r.FileName,
                          ReportName = r.ReportName
                      }).ToList();
            return result;
        }

        public List<MyReport> DeleteReport(int ReportID)
        {
            try
            {
                MyReport report = db.MyReports.Find(ReportID);
                string myUsername = report.Username;
                string targetFolder = HttpContext.Current.Server.MapPath("~/Reports");
                string fileName = report.FileName;
                string targetPath = Path.Combine(targetFolder, fileName);

                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath);
                }

                db.MyReports.Remove(report);
                db.SaveChanges();

                List<MyReport> result = new List<MyReport>();
                result = (from r in db.MyReports.AsEnumerable()
                          where r.Username == myUsername
                          select new MyReport
                          {
                              ID = r.ID,
                              Username = r.Username,
                              FileName = r.FileName,
                              ReportName = r.ReportName
                          }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<MyReport>();
            }
        }

        public MyAlertObject ShowInfoForSelectedAlert(int AlertID)
        {
            MyAlertObject myDataObject = new MyAlertObject();
            MyAlert alert = db.MyAlerts.Find(AlertID);
            alert.IsUnread = false;
            db.Entry(alert).State = EntityState.Modified;
            db.SaveChanges();
            myDataObject.unreadAlerts = db.MyAlerts.Where(w => w.IsUnread.Value).Count();

            string sqlQuery = alert.AlertQuery;
            string formatedDate = alert.Last_DateCheck.Value.ToString("yyyy'-'MM'-'dd");
            string formatedViolationDate = alert.Last_DateCheck.Value.ToString("yyyyMMdd");
            sqlQuery += " AND (";
            if (alert.Last_OBJECTID.HasValue)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " p.OBJECTID > " + alert.Last_OBJECTID.Value;
                }
                else
                {
                    sqlQuery += " OR p.OBJECTID > " + alert.Last_OBJECTID.Value;
                }
            }
            if (alert.IsEnergySearch)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " en.generation_date > '" + formatedDate + "'";
                }
                else
                {
                    sqlQuery += " OR en.generation_date > '" + formatedDate + "'";
                }
            }
            if (alert.IsPermitSearch)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " pe.dobrundate > '" + formatedDate + "'";
                }
                else
                {
                    sqlQuery += " OR pe.dobrundate > '" + formatedDate + "'";
                }
            }
            if (alert.IsViolationSearch)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " (v.issue_date > '" + formatedViolationDate + "' AND v.issue_date LIKE '20%')";
                }
                else
                {
                    sqlQuery += " OR (v.issue_date > '" + formatedDate + "' AND v.issue_date LIKE '20%')";
                }
            }
            if (alert.IsEvictionSearch)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " (ev.EXECUTED_DATE > '" + formatedDate + "' AND ev.EXECUTED_DATE < '2030-01-01')";
                }
                else
                {
                    sqlQuery += " OR (ev.EXECUTED_DATE > '" + formatedDate + "' AND ev.EXECUTED_DATE < '2030-01-01')";
                }
            }
            if (alert.IsElevatorSearch)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " (el.filing_date > '" + formatedDate + "' AND el.filing_date < '2030-01-01')";
                }
                else
                {
                    sqlQuery += " OR (el.filing_date > '" + formatedDate + "' AND el.filing_date < '2030-01-01')";
                }
            }
            if (alert.IsPropertySalesSearch)
            {
                if (sqlQuery.EndsWith("("))
                {
                    sqlQuery += " (ps.sale_date > '" + formatedDate + "' AND ps.sale_date < '2030-01-01')";
                }
                else
                {
                    sqlQuery += " OR (ps.sale_date > '" + formatedDate + "' AND ps.sale_date < '2030-01-01')";
                }
            }
            sqlQuery += ")";
            myDataObject.result = SearchDatabase(sqlQuery);
            myDataObject.sqlQuery = sqlQuery;

            return myDataObject;
        }

        public List<Select2DTO> GetTopBBLs(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.Plutoes
                            where d.BBL.Contains(term)
                            select new Select2DTO()
                            {
                                id = d.BBL.ToString(),
                                text = d.BBL
                            }).Take(30).ToList();
            return returnResult;
        }

        public List<Select2DTO> GetTopAddresses(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.Plutoes
                            where d.Address.ToLower().Contains(term.ToLower())
                            select new Select2DTO()
                            {
                                id = d.Address.ToString(),
                                text = d.Address
                            }).Take(30).ToList();
            return returnResult;
        }

        public List<Select2DTO> GetCensusTracts11Digit(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.ConsumerProfiles
                            where (d.State + d.County + d.Tract).Contains(term)
                            select new Select2DTO()
                            {
                                id = d.County + d.Tract,
                                text = d.State + d.County + d.Tract
                            }).Take(30).ToList();
            return returnResult;
        }

        public ReturnLookaLike SearchLookaLikeByBBL(string bbl)
        {
            ReturnLookaLike result = new ReturnLookaLike();
            string sqlQuery = "select p.Borough,p.Address,p.AssessTot,p.YearBuilt,p.BldgArea,p.NumFloors from dbo.Pluto p where ";
            string whereClause = "";
            var plutoEntity = db.Plutoes.Where(w => w.BBL == bbl).FirstOrDefault();
            if (plutoEntity != null)
            {
                if (plutoEntity.AssessTot.HasValue)
                {
                    double tollerance = plutoEntity.AssessTot.Value / 50;
                    if (whereClause == "")
                    {
                        whereClause += "AssessTot >= " + (plutoEntity.AssessTot.Value - tollerance) + " AND AssessTot <= " + (plutoEntity.AssessTot.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND AssessTot >= " + (plutoEntity.AssessTot.Value - tollerance) + " AND AssessTot <= " + (plutoEntity.AssessTot.Value + tollerance);
                    }
                }
                if (plutoEntity.YearBuilt.HasValue)
                {
                    double tollerance = 2;
                    if (whereClause == "")
                    {
                        whereClause += "YearBuilt >= " + (plutoEntity.YearBuilt.Value - tollerance) + " AND YearBuilt <= " + (plutoEntity.YearBuilt.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND YearBuilt >= " + (plutoEntity.YearBuilt.Value - tollerance) + " AND YearBuilt <= " + (plutoEntity.YearBuilt.Value + tollerance);
                    }
                }
                if (plutoEntity.BldgArea.HasValue)
                {
                    double tollerance = plutoEntity.BldgArea.Value / 50;
                    if (whereClause == "")
                    {
                        whereClause += "BldgArea >= " + (plutoEntity.BldgArea.Value - tollerance) + " AND BldgArea <= " + (plutoEntity.BldgArea.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND BldgArea >= " + (plutoEntity.BldgArea.Value - tollerance) + " AND BldgArea <= " + (plutoEntity.BldgArea.Value + tollerance);
                    }
                }
                if (plutoEntity.NumFloors.HasValue)
                {
                    double tollerance = 1;
                    if (whereClause == "")
                    {
                        whereClause += "NumFloors >= " + (plutoEntity.NumFloors.Value - tollerance) + " AND NumFloors <= " + (plutoEntity.NumFloors.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND NumFloors >= " + (plutoEntity.NumFloors.Value - tollerance) + " AND NumFloors <= " + (plutoEntity.NumFloors.Value + tollerance);

                    }
                }
                sqlQuery += whereClause;
            }
            List<DatabaseAttributes> returnResult = new List<DatabaseAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                returnResult = ctx.Database.SqlQuery<DatabaseAttributes>(sqlQuery).ToList();
            }
            result.data = returnResult;
            result.sqlQuery = sqlQuery;
            return result;
        }

        public ReturnLookaLike SearchLookaLikeByAddresses(string adr)
        {
            ReturnLookaLike result = new ReturnLookaLike();

            string sqlQuery = "select p.Borough,p.Address,p.AssessTot,p.YearBuilt,p.BldgArea,p.NumFloors  from dbo.Pluto p where ";
            string whereClause = "";
            var plutoEntity = db.Plutoes.Where(w => w.Address == adr).FirstOrDefault();
            if (plutoEntity != null)
            {
                if (plutoEntity.AssessTot.HasValue)
                {
                    double tollerance = plutoEntity.AssessTot.Value / 50;
                    if (whereClause == "")
                    {
                        whereClause += "AssessTot >= " + (plutoEntity.AssessTot.Value - tollerance) + " AND AssessTot <= " + (plutoEntity.AssessTot.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND AssessTot >= " + (plutoEntity.AssessTot.Value - tollerance) + " AND AssessTot <= " + (plutoEntity.AssessTot.Value + tollerance);
                    }
                }
                if (plutoEntity.YearBuilt.HasValue)
                {
                    double tollerance = 2;
                    if (whereClause == "")
                    {
                        whereClause += "YearBuilt >= " + (plutoEntity.YearBuilt.Value - tollerance) + " AND YearBuilt <= " + (plutoEntity.YearBuilt.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND YearBuilt >= " + (plutoEntity.YearBuilt.Value - tollerance) + " AND YearBuilt <= " + (plutoEntity.YearBuilt.Value + tollerance);
                    }
                }
                if (plutoEntity.BldgArea.HasValue)
                {
                    double tollerance = plutoEntity.BldgArea.Value / 50;
                    if (whereClause == "")
                    {
                        whereClause += "BldgArea >= " + (plutoEntity.BldgArea.Value - tollerance) + " AND BldgArea <= " + (plutoEntity.BldgArea.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND BldgArea >= " + (plutoEntity.BldgArea.Value - tollerance) + " AND BldgArea <= " + (plutoEntity.BldgArea.Value + tollerance);
                    }
                }
                if (plutoEntity.NumFloors.HasValue)
                {
                    double tollerance = 1;
                    if (whereClause == "")
                    {
                        whereClause += "NumFloors >= " + (plutoEntity.NumFloors.Value - tollerance) + " AND NumFloors <= " + (plutoEntity.NumFloors.Value + tollerance);
                    }
                    else
                    {
                        whereClause += " AND NumFloors >= " + (plutoEntity.NumFloors.Value - tollerance) + " AND NumFloors <= " + (plutoEntity.NumFloors.Value + tollerance);
                    }
                }
                sqlQuery += whereClause;
            }
            List<DatabaseAttributes> returnResult = new List<DatabaseAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                returnResult = ctx.Database.SqlQuery<DatabaseAttributes>(sqlQuery).ToList();
            }
            result.data = returnResult;
            result.sqlQuery = sqlQuery;
            return result;
        }

        public ConsumerProfiles SearchConsumerProfilesDatabase(string sqlQuery)
        {
            ConsumerProfiles returnResult = new ConsumerProfiles();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                returnResult = ctx.Database.SqlQuery<ConsumerProfiles>(sqlQuery).FirstOrDefault();
            }
            return returnResult;
        }

        public List<ConsumerProfiles> SearchConsumerProfilesDatabaseList(string sqlQuery)
        {
            List<ConsumerProfiles> returnResult = new List<ConsumerProfiles>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                returnResult = ctx.Database.SqlQuery<ConsumerProfiles>(sqlQuery).ToList();
            }
            return returnResult;
        }

        public List<HeatmapAttributes> SearchDatabaseHeatMap(string StoredProcedure, string DateHmPsBasePeriod, string DateHmPsAnalysisPeriod, string DiffDaysHmPsBasePeriod
            , string DiffDaysHmPsAnalysisPeriod, string BoroughsTA, string DistrictsTA, string ZipCodeRangeTAFrom, string ZipCodeRangeTATo)
        {
            List<HeatmapAttributes> returnResult = new List<HeatmapAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                string StoredProcedureName = "TrendAnalysis_NumberOfPropertySales";
                if (StoredProcedure == "2")
                {
                    StoredProcedureName = "TrendAnalysis_AveragePropertySalesPrice";
                }
                DateTime BasePeriodFrom = GlobalVariables.ToNullableDateTime(DateHmPsBasePeriod).Value.AddDays(-Convert.ToInt32(DiffDaysHmPsBasePeriod));
                DateTime BasePeriodTo = GlobalVariables.ToNullableDateTime(DateHmPsBasePeriod).Value.AddDays(Convert.ToInt32(DiffDaysHmPsBasePeriod));
                DateTime AnalysisPeriodFrom = GlobalVariables.ToNullableDateTime(DateHmPsAnalysisPeriod).Value.AddDays(-Convert.ToInt32(DiffDaysHmPsAnalysisPeriod));
                DateTime AnalysisPeriodTo = GlobalVariables.ToNullableDateTime(DateHmPsAnalysisPeriod).Value.AddDays(Convert.ToInt32(DiffDaysHmPsAnalysisPeriod));
                var BasePeriodFromParametar = new SqlParameter("BasePeriodFrom", BasePeriodFrom);
                var BasePeriodToParametar = new SqlParameter("BasePeriodTo", BasePeriodTo);
                var AnalysisPeriodFromParametar = new SqlParameter("AnalysisPeriodFrom", AnalysisPeriodFrom);
                var AnalysisPeriodToParametar = new SqlParameter("AnalysisPeriodTo", AnalysisPeriodTo);
                var BoroughParametar = !String.IsNullOrEmpty(BoroughsTA) ? new SqlParameter("Borough", BoroughsTA) : new SqlParameter("Borough", DBNull.Value);
                var CDParametar = !String.IsNullOrEmpty(DistrictsTA) ? new SqlParameter("CD", DistrictsTA) : new SqlParameter("CD", DBNull.Value);
                var ZipCodeFromParametar = !String.IsNullOrEmpty(ZipCodeRangeTAFrom) ? new SqlParameter("ZipCodeFrom", ZipCodeRangeTAFrom) : new SqlParameter("ZipCodeFrom", DBNull.Value);
                var ZipCodeToParametar = !String.IsNullOrEmpty(ZipCodeRangeTATo) ? new SqlParameter("ZipCodeTo", ZipCodeRangeTATo) : new SqlParameter("ZipCodeTo", DBNull.Value);
                returnResult = ctx.Database.SqlQuery<HeatmapAttributes>("EXEC dbo." + StoredProcedureName + " @BasePeriodFrom, @BasePeriodTo, @AnalysisPeriodFrom, @AnalysisPeriodTo, @Borough, @CD, @ZipCodeFrom, @ZipCodeTo ",
                                        BasePeriodFromParametar, BasePeriodToParametar, AnalysisPeriodFromParametar, AnalysisPeriodToParametar, BoroughParametar, CDParametar, ZipCodeFromParametar, ZipCodeToParametar).ToList();
            }
            return returnResult;
        }

        public List<HeatmapAttributes> SearchDatabaseHeatMapViolations(string ViolationType, string StoredProcedure, string FormatedBasePeriodFrom, string FormatedBasePeriodTo, string FormatedAnalysisPeriodFrom
            , string FormatedAnalysisPeriodTo, string BoroughsTA, string DistrictsTA, string ZipCodeRangeTAFrom, string ZipCodeRangeTATo)
        {
            List<HeatmapAttributes> returnResult = new List<HeatmapAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                ctx.Database.CommandTimeout = 600;
                string StoredProcedureName = "TrendAnalysis_NumberOfEcbViolations";
                if (StoredProcedure == "2")
                {
                    StoredProcedureName = "TrendAnalysis_NumberOfDobViolations";
                }
                var ViolationTypeParametar = new SqlParameter("ViolationType", ViolationType);
                var BasePeriodFromParametar = new SqlParameter("BasePeriodFrom", FormatedBasePeriodFrom);
                var BasePeriodToParametar = new SqlParameter("BasePeriodTo", FormatedBasePeriodTo);
                var AnalysisPeriodFromParametar = new SqlParameter("AnalysisPeriodFrom", FormatedAnalysisPeriodFrom);
                var AnalysisPeriodToParametar = new SqlParameter("AnalysisPeriodTo", FormatedAnalysisPeriodTo);
                var BoroughParametar = !String.IsNullOrEmpty(BoroughsTA) ? new SqlParameter("Borough", BoroughsTA) : new SqlParameter("Borough", DBNull.Value);
                var CDParametar = !String.IsNullOrEmpty(DistrictsTA) ? new SqlParameter("CD", DistrictsTA) : new SqlParameter("CD", DBNull.Value);
                var ZipCodeFromParametar = !String.IsNullOrEmpty(ZipCodeRangeTAFrom) ? new SqlParameter("ZipCodeFrom", ZipCodeRangeTAFrom) : new SqlParameter("ZipCodeFrom", DBNull.Value);
                var ZipCodeToParametar = !String.IsNullOrEmpty(ZipCodeRangeTATo) ? new SqlParameter("ZipCodeTo", ZipCodeRangeTATo) : new SqlParameter("ZipCodeTo", DBNull.Value);
                returnResult = ctx.Database.SqlQuery<HeatmapAttributes>("EXEC dbo." + StoredProcedureName + " @ViolationType, @BasePeriodFrom, @BasePeriodTo, @AnalysisPeriodFrom, @AnalysisPeriodTo, @Borough, @CD, @ZipCodeFrom, @ZipCodeTo ",
                                        ViolationTypeParametar, BasePeriodFromParametar, BasePeriodToParametar, AnalysisPeriodFromParametar, AnalysisPeriodToParametar, BoroughParametar, CDParametar, ZipCodeFromParametar, ZipCodeToParametar).ToList();
            }
            return returnResult;
        }

        public List<HeatmapAttributes> SearchDatabaseHeatMapPermits(string PermitJobType, string PermitWorkType, string FormatedBasePeriodFrom, string FormatedBasePeriodTo, string FormatedAnalysisPeriodFrom
            , string FormatedAnalysisPeriodTo, string BoroughsTA, string DistrictsTA, string ZipCodeRangeTAFrom, string ZipCodeRangeTATo)
        {
            List<HeatmapAttributes> returnResult = new List<HeatmapAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                ctx.Database.CommandTimeout = 600;
                var PermitJobTypeParametar = new SqlParameter("PermitJobType", PermitJobType);
                var PermitWorkTypeParametar = new SqlParameter("PermitWorkType", PermitWorkType);
                var BasePeriodFromParametar = new SqlParameter("BasePeriodFrom", FormatedBasePeriodFrom);
                var BasePeriodToParametar = new SqlParameter("BasePeriodTo", FormatedBasePeriodTo);
                var AnalysisPeriodFromParametar = new SqlParameter("AnalysisPeriodFrom", FormatedAnalysisPeriodFrom);
                var AnalysisPeriodToParametar = new SqlParameter("AnalysisPeriodTo", FormatedAnalysisPeriodTo);
                var BoroughParametar = !String.IsNullOrEmpty(BoroughsTA) ? new SqlParameter("Borough", BoroughsTA) : new SqlParameter("Borough", DBNull.Value);
                var CDParametar = !String.IsNullOrEmpty(DistrictsTA) ? new SqlParameter("CD", DistrictsTA) : new SqlParameter("CD", DBNull.Value);
                var ZipCodeFromParametar = !String.IsNullOrEmpty(ZipCodeRangeTAFrom) ? new SqlParameter("ZipCodeFrom", ZipCodeRangeTAFrom) : new SqlParameter("ZipCodeFrom", DBNull.Value);
                var ZipCodeToParametar = !String.IsNullOrEmpty(ZipCodeRangeTATo) ? new SqlParameter("ZipCodeTo", ZipCodeRangeTATo) : new SqlParameter("ZipCodeTo", DBNull.Value);
                returnResult = ctx.Database.SqlQuery<HeatmapAttributes>("EXEC dbo.TrendAnalysis_NumberOfPermits @PermitJobType, @PermitWorkType, @BasePeriodFrom, @BasePeriodTo, @AnalysisPeriodFrom, @AnalysisPeriodTo, @Borough, @CD, @ZipCodeFrom, @ZipCodeTo ",
                                        PermitJobTypeParametar, PermitWorkTypeParametar, BasePeriodFromParametar, BasePeriodToParametar, AnalysisPeriodFromParametar, AnalysisPeriodToParametar, BoroughParametar, CDParametar, ZipCodeFromParametar, ZipCodeToParametar).ToList();
            }
            return returnResult;
        }

        public int UpdatePlutoDatatableTractIDs(string BBLs, int tractID, int OBJECTID)
        {
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                var BBL = BBLs.Split(',');
                foreach (string item in BBL)
                {
                    var BBLsParametar = new SqlParameter("BBLs", item);
                    var TractIDParametar = new SqlParameter("TractID", tractID);
                    ctx.Database.ExecuteSqlCommand("EXEC dbo.UpdateMapPlutoTractID @BBLs, @TractID ",
                        BBLsParametar, TractIDParametar);
                }
            }
            return OBJECTID;
        }

        public IndexData GetIndexData(string username)
        {
            IndexData result = new IndexData() { Username = "", TabIDs = new List<int>() };
            User_Tabs userTabs = db.User_Tabs.Where(w => w.Username == username).FirstOrDefault();
            if (userTabs != null)
            {
                result.Username = userTabs.Username;
                result.TabIDs = userTabs.TabIDs.Split(',').Select(s => int.Parse(s)).ToList();
            }
            return result;
        }
    }
}