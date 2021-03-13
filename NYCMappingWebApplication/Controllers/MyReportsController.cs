using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Helpers;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Controllers
{
    public class MyReportsController : Controller
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        // GET: MyReports
        public ActionResult Preview()
        {
            return View(db.MyReports.Where(w => w.Username == GlobalVariables.GetFromCookie("NYCUser", "Username")).OrderByDescending(w => w.ID).ToList());
        }
    }
}