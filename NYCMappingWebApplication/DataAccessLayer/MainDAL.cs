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
    }
}