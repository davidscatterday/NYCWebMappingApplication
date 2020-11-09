using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Dynamic;

namespace NYCMappingWebApp.Controllers
{
    public class HomeController : Controller
    {
        MainDAL mainDAL = new MainDAL();
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.ZoningDistricts = mainDAL.GetAllZoningDistricts();
            ViewBag.CommercialOverlays = mainDAL.GetAllCommercialOverlays();
            ViewBag.EvictionStatuses = mainDAL.GetAllEvictionStatuses();
            ViewBag.ElevatorDeviceTypes = mainDAL.GetAllElevatorDeviceTypes();
            ViewBag.FilingTypes = mainDAL.GetAllFilingTypes();
            ViewBag.FilingStatuses = mainDAL.GetAllFilingStatuses();
            ViewBag.YesNoStatuses = mainDAL.GetAllYesNoStatuses();
            ViewBag.Frequencies = mainDAL.GetAllFrequencies();
            return View();
        }

        public JsonResult GetBoroughs(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughs(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoroughsCP(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughsCP(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistricts(string term)
        {
            var DistrictsList = mainDAL.GetAllDistricts(term);
            return Json(new { DistrictsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllJobTypes(string term)
        {
            var JobTypesList = mainDAL.GetAllJobTypes(term);
            return Json(new { JobTypesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllWorkTypes(string term)
        {
            var WorkTypesList = mainDAL.GetAllWorkTypes(term);
            return Json(new { WorkTypesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllViolationTypes(string term)
        {
            var ViolationTypesList = mainDAL.GetAllViolationTypes(term);
            return Json(new { ViolationTypesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllViolationCategories(string term)
        {
            var ViolationCategoriesList = mainDAL.GetAllViolationCategories(term);
            return Json(new { ViolationCategoriesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTopBBLs(string term)
        {
            var BBLList = mainDAL.GetTopBBLs(term);
            return Json(new { BBLList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTopAddresses(string term)
        {
            var AddressList = mainDAL.GetTopAddresses(term);
            return Json(new { AddressList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCensusTracts11Digit(string term)
        {
            var DataList = mainDAL.GetCensusTracts11Digit(term);
            return Json(new { DataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchLookaLikeByBBL(string bbl)
        {
            ReturnLookaLike data = mainDAL.SearchLookaLikeByBBL(bbl);
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchLookaLikeByAddresses(string adr)
        {
            ReturnLookaLike data = mainDAL.SearchLookaLikeByAddresses(adr);
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchDatabase(string sqlQuery)
        {
            var data = mainDAL.SearchDatabase(sqlQuery);
            
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchConsumerProfilesDatabaseList(string sqlQuery)
        {
            var data = mainDAL.SearchConsumerProfilesDatabaseList(sqlQuery);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchDatabaseHeatMap(string sqlQuery)
        {
            var data = mainDAL.SearchDatabaseHeatMap(sqlQuery);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult GetMyAlerts()
        {
            var data = mainDAL.GetMyAlerts(User.Identity.Name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAlert(int AlertID)
        {
            var res = mainDAL.DeleteAlert(AlertID);
            var data = mainDAL.GetMyAlerts(User.Identity.Name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyReports()
        {
            var data = mainDAL.GetMyReports(User.Identity.Name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowInfoForSelectedAlert(int AlertID)
        {
            var data = mainDAL.ShowInfoForSelectedAlert(AlertID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDatabaseReport(string ReportName, string sqlQuery)
        {
            string msg = "Report saved successfully";
            try
            {
                StringBuilder csvContent = new StringBuilder();
                List<DatabaseAttributes> lstTableFeatures = mainDAL.SearchDatabase(sqlQuery);

                for (int i = 0; i < lstTableFeatures.Count; i++)
                {
                    if (i == 0)
                    {
                        csvContent.AppendLine(CreateCsvLine(lstTableFeatures[i], true));
                        csvContent.AppendLine(CreateCsvLine(lstTableFeatures[i], false));
                    }
                    else
                    {
                        csvContent.AppendLine(CreateCsvLine(lstTableFeatures[i], false));
                    }
                }

                string targetFolder = Server.MapPath("~/Reports");
                string fileName = ReportName + "_" + DateTime.Now.Ticks.ToString() + ".csv";
                string targetPath = Path.Combine(targetFolder, fileName);

                System.IO.File.AppendAllText(targetPath, csvContent.ToString());

                MyReport myReport = new MyReport()
                {
                    Username = User.Identity.Name,
                    ReportName = ReportName,
                    FileName = fileName
                };
                db.MyReports.Add(myReport);
                db.SaveChanges();

                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeleteReport(int ReportID)
        {
            var res = mainDAL.DeleteReport(ReportID);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateAlert(string AlertName, string AlertFrequency, string AlertQuery, bool IsPlutoSearch, bool IsEnergySearch, bool IsPermitSearch, bool IsViolationSearch, bool IsEvictionSearch, bool IsElevatorSearch, bool IsPropertySalesSearch)
        {
            string msg = "Alert created successfully";
            try
            {
                DatabaseMaxValues result = IsPlutoSearch ? mainDAL.GetMaxValues() : new DatabaseMaxValues();
                MyAlert myAlert = new MyAlert()
                {
                    Username = User.Identity.Name,
                    AlertName = AlertName,
                    AlertQuery = AlertQuery,
                    Frequency = Convert.ToInt32(AlertFrequency),
                    Last_OBJECTID = IsPlutoSearch ? result.OBJECTID : null,
                    IsEnergySearch = IsEnergySearch,
                    IsPermitSearch = IsPermitSearch,
                    IsViolationSearch = IsViolationSearch,
                    IsEvictionSearch = IsEvictionSearch,
                    IsElevatorSearch = IsElevatorSearch,
                    IsPropertySalesSearch = IsPropertySalesSearch,
                    DateCreated = DateTime.Now,
                    Last_DateCheck = DateTime.Now,
                    Next_DateCheck = AlertFrequency == "7" ? DateTime.Now.AddDays(7) : DateTime.Now.AddDays(1),
                    IsUnread = false
                };
                db.MyAlerts.Add(myAlert);
                db.SaveChanges();
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }
        }
        public string CreateCsvLine(DatabaseAttributes elem, bool isHeader)
        {
            string csvLine = isHeader ? "Borough,Address" : elem.Borough + "," + elem.Address;
            if (elem.ZipCode != null)
                csvLine += isHeader ? ",Zip Code" : "," + elem.ZipCode;
            if (elem.Latitude != null)
                csvLine += isHeader ? ",Latitude" : "," + elem.Latitude;
            if (elem.Longitude != null)
                csvLine += isHeader ? ",Longitude" : "," + elem.Longitude;
            if (elem.BldgArea != null)
                csvLine += isHeader ? ",Total Building Floor Area" : "," + elem.BldgArea;
            if (elem.ComArea != null)
                csvLine += isHeader ? ",Commercial Floor Area" : "," + elem.ComArea;
            if (elem.ResArea != null)
                csvLine += isHeader ? ",Residential Floor Area" : "," + elem.ResArea;
            if (elem.NumFloors != null)
                csvLine += isHeader ? ",Number of Floors" : "," + elem.NumFloors;
            if (elem.UnitsRes != null)
                csvLine += isHeader ? ",Residential Units" : "," + elem.UnitsRes;
            if (elem.ZoneDist1 != null)
                csvLine += isHeader ? ",Zoning District" : "," + elem.ZoneDist1;
            if (elem.Overlay1 != null)
                csvLine += isHeader ? ",Commercial Overlay 1" : "," + elem.Overlay1;
            if (elem.Overlay2 != null)
                csvLine += isHeader ? ",Commercial Overlay 2" : "," + elem.Overlay2;
            if (elem.AssessTot != null)
                csvLine += isHeader ? ",Assessed Total Value" : "," + elem.AssessTot;
            if (elem.YearBuilt != null)
                csvLine += isHeader ? ",Year Built" : "," + elem.YearBuilt;
            if (elem.BldgClass != null)
                csvLine += isHeader ? ",Building Class" : "," + elem.BldgClass;
            if (elem.energy_star_score != null)
                csvLine += isHeader ? ",Energy Star Score" : "," + elem.energy_star_score;
            if (elem.source_eui_kbtu_ft != null)
                csvLine += isHeader ? ",Source EUI" : "," + elem.source_eui_kbtu_ft;
            if (elem.site_eui_kbtu_ft != null)
                csvLine += isHeader ? ",Site EUI" : "," + elem.site_eui_kbtu_ft;
            if (elem.annual_maximum_demand_kw != null)
                csvLine += isHeader ? ",Annual Maximum Demand" : "," + elem.annual_maximum_demand_kw;
            if (elem.total_ghg_emissions_metric != null)
                csvLine += isHeader ? ",Total GHG Emissions" : "," + elem.total_ghg_emissions_metric;
            if (elem.OwnerName != null)
                csvLine += isHeader ? ",Owner Name" : "," + elem.OwnerName;
            if (elem.job_start_date != null)
                csvLine += isHeader ? ",Job Start Date" : "," + elem.job_start_date_string_format;
            if (elem.job_type != null)
                csvLine += isHeader ? ",Job Type" : "," + elem.job_type;
            if (elem.work_type != null)
                csvLine += isHeader ? ",Work Type" : "," + elem.work_type;
            if (elem.issue_date != null)
                csvLine += isHeader ? ",Issue Date" : "," + elem.issue_date_string_format;
            if (elem.violation_type != null)
                csvLine += isHeader ? ",Violation Type" : "," + elem.violation_type;
            if (elem.violation_category != null)
                csvLine += isHeader ? ",Violation Category" : "," + elem.violation_category;
            if (elem.executed_date != null)
                csvLine += isHeader ? ",Executed Date" : "," + elem.executed_date_string_format;
            if (elem.DISTRICT != null)
                csvLine += isHeader ? ",District" : "," + elem.DISTRICT;
            if (elem.OrganizationName != null)
            {
                string orgName = elem.OrganizationName.Contains(",") ? String.Format("\"{0}\"", elem.OrganizationName) : elem.OrganizationName;
                csvLine += isHeader ? ",Organization Name" : "," + orgName;
            }
            if (elem.Faith_Based_Organization != null)
                csvLine += isHeader ? ",Faith Based Organization" : "," + elem.Faith_Based_Organization;
            if (elem.Foundation != null)
                csvLine += isHeader ? ",Foundation" : "," + elem.Foundation;
            if (elem.New_York_City_Agency != null)
                csvLine += isHeader ? ",New York City Agency" : "," + elem.New_York_City_Agency;
            if (elem.Nonprofit != null)
                csvLine += isHeader ? ",Nonprofit" : "," + elem.Nonprofit;
            if (elem.elevatordevicetype != null)
                csvLine += isHeader ? ",Elevator Device Type" : "," + elem.elevatordevicetype;
            if (elem.job_number != null)
                csvLine += isHeader ? ",Job Number" : "," + elem.job_number;
            if (elem.filing_type != null)
                csvLine += isHeader ? ",Filing Type" : "," + elem.filing_type;
            if (elem.filing_status != null)
                csvLine += isHeader ? ",Filing Status" : "," + elem.filing_status;
            if (elem.filing_date != null)
                csvLine += isHeader ? ",Filing Date" : "," + elem.filing_date_string_format;
            if (elem.AssessTotPerSqFt != null)
                csvLine += isHeader ? ",Assessed Value per Square Foot" : "," + elem.AssessTotPerSqFt;
            if (elem.sale_date != null)
                csvLine += isHeader ? ",Sale Date" : "," + elem.sale_date_string_format;
            if (elem.sale_price != null)
                csvLine += isHeader ? ",Sale Price" : "," + elem.sale_price;
            return csvLine;
          
          
        }
    }
}