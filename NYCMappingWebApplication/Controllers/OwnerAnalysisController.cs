using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Controllers
{
    public class OwnerAnalysisController : Controller
    {
        OwnerAnalysisDAL ownerAnalysisDAL = new OwnerAnalysisDAL();
        // GET: OwnerAnalysis/Preview
        public ActionResult Preview(string bbl)
        {
            List<Hpd_Registrations_Group> data = ownerAnalysisDAL.GetHpdRegistrationsByBBL(bbl);
            return View(data);
        }

        public JsonResult GetHpdAddress(string term)
        {
            List<Select2DTO> HpdAddressList = ownerAnalysisDAL.GetAllHpd_Addresses(term);
            return Json(new { HpdAddressList }, JsonRequestBehavior.AllowGet);
        }

    }
}