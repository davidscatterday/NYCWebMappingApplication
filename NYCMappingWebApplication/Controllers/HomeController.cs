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
            return View();
        }

        public JsonResult GetBoroughs(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughs(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
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

        public JsonResult SearchDatabase(string sqlQuery)
        {
            var data = mainDAL.SearchDatabase(sqlQuery);

            //return Json(data, JsonRequestBehavior.AllowGet);
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
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

        //public JsonResult SaveReport(string ReportName, string TableFeatures)
        //{
        //    string msg = "Report saved successfully";
        //    try
        //    {
        //        StringBuilder csvContent = new StringBuilder();
        //        List<TableFeatures> lstTableFeatures = JsonConvert.DeserializeObject<List<TableFeatures>>(TableFeatures);

        //        for (int i = 0; i < lstTableFeatures.Count; i++)
        //        {
        //            if (i == 0)
        //            {
        //                csvContent.AppendLine(CreateCsvLine(lstTableFeatures[i], true));
        //                csvContent.AppendLine(CreateCsvLine(lstTableFeatures[i], false));
        //            }
        //            else
        //            {
        //                csvContent.AppendLine(CreateCsvLine(lstTableFeatures[i], false));
        //            }
        //        }

        //        string targetFolder = Server.MapPath("~/Reports");
        //        string fileName = ReportName + "_" + DateTime.Now.Ticks.ToString() + ".csv";
        //        string targetPath = Path.Combine(targetFolder, fileName);

        //        System.IO.File.AppendAllText(targetPath, csvContent.ToString());

        //        MyReport myReport = new MyReport()
        //        {
        //            Username = User.Identity.Name,
        //            ReportName = ReportName,
        //            FileName = fileName
        //        };
        //        db.MyReports.Add(myReport);
        //        db.SaveChanges();

        //        return Json(new { msg }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //        return Json(new { msg }, JsonRequestBehavior.AllowGet);
        //    }

        //}

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

            return csvLine;
        }
    }
}