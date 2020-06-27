using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return returnResult.Where(w => w.text.Contains(term)).ToList();
        }
        public List<Select2DTO> GetAllDistricts(string term)
        {
            List<Select2DTO> returnResult = new List<Select2DTO>();
            returnResult = (from d in db.Districts
                            where d.DISTRICT1.Contains(term)
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
            //List<SelectListItem> returnResult = new List<SelectListItem>();
            //returnResult.Add(new SelectListItem() { Text = "Elevator", Value = "Elevator" });
            //returnResult.Add(new SelectListItem() { Text = "Accessibility Lift", Value = "Accessibility Lift" });
            //returnResult.Add(new SelectListItem() { Text = "Manlift", Value = "Manlift" });
            //returnResult.Add(new SelectListItem() { Text = "Dumbwaiter", Value = "Dumbwaiter" });
            //returnResult.Add(new SelectListItem() { Text = "Escalator", Value = "Escalator" });
            //returnResult.Add(new SelectListItem() { Text = "Personnel Hoist", Value = "Personnel Hoist" });
            //returnResult.Add(new SelectListItem() { Text = "Conveyor", Value = "Conveyor" });
            //return returnResult;
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
            //    returnResult.Add(new SelectListItem() { Text = "New Filing", Value = "New Filing" });
            // returnResult.Add(new SelectListItem() { Text = "PAA", Value = "PAA" });
           // return returnResult;
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
            //returnResult.Add(new SelectListItem() { Text = "Sign Off Review in Progress", Value = "Sign Off Review in Progress" });
            //returnResult.Add(new SelectListItem() { Text = "Objections to Sign Off", Value = "Objections to Sign Off" });
            //returnResult.Add(new SelectListItem() { Text = "Permit Entire", Value = "Permit Entire" });
            //returnResult.Add(new SelectListItem() { Text = "Withdrawn", Value = "Withdrawn" });
            //returnResult.Add(new SelectListItem() { Text = "Signed Off", Value = "Signed Off" });
            //returnResult.Add(new SelectListItem() { Text = "Objections", Value = "Objections" });
            //returnResult.Add(new SelectListItem() { Text = "On Hold - No Good Check", Value = "On Hold - No Good Check" });
            //returnResult.Add(new SelectListItem() { Text = "Approved", Value = "Approved" });
            //returnResult.Add(new SelectListItem() { Text = "Sign Off Request Initiated", Value = "Sign Off Request Initiated" });
            //returnResult.Add(new SelectListItem() { Text = "Pending Plan Examiner Assignment", Value = "Pending Plan Examiner Assignment" });
            //returnResult.Add(new SelectListItem() { Text = "Plan Examiner Review", Value = "Plan Examiner Review" });

            //return returnResult;
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
                            where d.Address.Contains(term)
                            select new Select2DTO()
                            {
                                id = d.Address.ToString(),
                                text = d.Address
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
    }
}