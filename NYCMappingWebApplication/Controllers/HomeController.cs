using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

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
            return View();
        }

        public JsonResult GetBoroughs(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughs(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveReport(string ReportName, string TableFeatures)
        {
            string msg = "Report saved successfully";
            try
            {
                List<TableFeatures> lstTableFeatures = JsonConvert.DeserializeObject<List<TableFeatures>>(TableFeatures);

                StringBuilder csvContent = new StringBuilder();
                csvContent.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", "Borough", "Address", "Block", "Lot", "Energy Star Score", "Source EUI", "Site EUI", "Annual Maximum Demand", "Total GHG Emissions"));

                foreach (TableFeatures item in lstTableFeatures)
                {
                    csvContent.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", item.Borough, item.Address, item.Block, item.Lot, item.EnergyStarScore, item.SourceEUI, item.SiteEUI, item.AnnualMaximumDemand, item.TotalGHGEmissions));
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

    }
}