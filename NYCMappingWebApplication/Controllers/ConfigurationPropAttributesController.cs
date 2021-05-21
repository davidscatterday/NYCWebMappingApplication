using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Helpers;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Controllers
{
    public class ConfigurationPropAttributesController : Controller
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        MainDAL mainDAL = new MainDAL();
        // GET: ConfigurationPropAttributes
        public ActionResult Index()
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsAdmin") == "True"))
                return RedirectToAction("Login", "AppUsers");
            List<Configuration_PropAttributes> data = db.Configuration_PropAttributes.OrderBy(w => w.OrderNum).ToList();

            return View(data);
        }
        // POST: /ConfigurationPropAttributes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string command, List<Configuration_PropAttributes> data)
        {
            switch (command)
            {
                case "Save":
                    try
                    {
                        foreach (Configuration_PropAttributes attr in data)
                        {
                            db.Entry(attr).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        TempData["InfoMessage"] = "Attributes has been successfully edited";
                    }
                    catch (Exception ex)
                    {
                        string message = ex.InnerException != null ? "Message: " + ex.Message + Environment.NewLine + "InnerException: " + ex.InnerException.Message : "Message: " + ex.Message;
                        mainDAL.RecordInLogger("ERROR", "Save Prop Attributes", message, "", "");
                        TempData["ErrorMessage"] = "Some error happens and the attributes are not saved. Contact the Admin.";
                    }
                    break;
                
            }
            data = db.Configuration_PropAttributes.OrderBy(w => w.ID).ToList();

            return View(data);
        }
    }
}