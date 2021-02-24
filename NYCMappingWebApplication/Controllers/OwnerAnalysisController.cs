using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            OwnerAnalysisData data = ownerAnalysisDAL.GetHpdRegistrationsByBBL(bbl);
            return View(data);
        }

        public JsonResult GetHpdAddress(string term)
        {
            List<Select2DTO> HpdAddressList = ownerAnalysisDAL.GetAllHpd_Addresses(term);
            return Json(new { HpdAddressList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHpdRegistrationsByBBL(string bbl)
        {
            OwnerAnalysisData data = ownerAnalysisDAL.GetHpdRegistrationsByBBL(bbl);
            return new JsonResult()
            {
                Data = data.lstPortfolio,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public FileResult DownloadOwnerAnalysis(string bbl)
        {
            OwnerAnalysisData data = ownerAnalysisDAL.GetHpdRegistrationsByBBL(bbl);
            byte[] excelBytes;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage xlPackage = new ExcelPackage())
            {
                ExcelWorksheet workSheetDemographics = xlPackage.Workbook.Worksheets.Add("OwnerAnalysis");

                workSheetDemographics.Cells["A1:D1"].Merge = true;
                workSheetDemographics.Cells["A1"].Value = "Location";
                workSheetDemographics.Cells["E1:F1"].Merge = true;
                workSheetDemographics.Cells["E1"].Value = "Information";
                workSheetDemographics.Cells["G1"].Value = "RS Units";
                workSheetDemographics.Cells["H1:I1"].Merge = true;
                workSheetDemographics.Cells["H1"].Value = "HPD Violations";
                workSheetDemographics.Cells["J1"].Value = "Evictions";
                workSheetDemographics.Cells["K1"].Value = "Landlord";
                workSheetDemographics.Cells["L1:M1"].Merge = true;
                workSheetDemographics.Cells["L1"].Value = "Last Sale";

                workSheetDemographics.Cells["A2"].Value = "Address";
                workSheetDemographics.Cells["B2"].Value = "Zipcode";
                workSheetDemographics.Cells["C2"].Value = "Borough";
                workSheetDemographics.Cells["D2"].Value = "BBL";
                workSheetDemographics.Cells["E2"].Value = "Built";
                workSheetDemographics.Cells["F2"].Value = "Units";
                workSheetDemographics.Cells["G2"].Value = "2019";
                workSheetDemographics.Cells["H2"].Value = "Open";
                workSheetDemographics.Cells["I2"].Value = "Total";
                workSheetDemographics.Cells["J2"].Value = "2019";
                workSheetDemographics.Cells["K2"].Value = "Officer/Owner";
                workSheetDemographics.Cells["L2"].Value = "Date";
                workSheetDemographics.Cells["M2"].Value = "Amount";

                int i = 3;
                foreach (Hpd_Registrations_Group item in data.lstPortfolio)
                {
                    workSheetDemographics.Cells["A" + i].Value = item.Address;
                    workSheetDemographics.Cells["B" + i].Value = item.Zip;
                    workSheetDemographics.Cells["C" + i].Value = item.Boro;
                    workSheetDemographics.Cells["D" + i].Value = item.bbl;
                    workSheetDemographics.Cells["E" + i].Value = item.yearbuilt;
                    workSheetDemographics.Cells["F" + i].Value = item.unitsres;
                    workSheetDemographics.Cells["G" + i].Value = item.rsunits2019;
                    workSheetDemographics.Cells["H" + i].Value = item.openviolations;
                    workSheetDemographics.Cells["I" + i].Value = item.totalviolations;
                    workSheetDemographics.Cells["J" + i].Value = item.totalevictions;
                    workSheetDemographics.Cells["K" + i].Value = item.OwnerName;
                    workSheetDemographics.Cells["L" + i].Value = item.DOC_DATE;
                    workSheetDemographics.Cells["M" + i].Value = item.DOC_AMOUNT;
                    i++;
                }

                //Header
                //SetHeaderStyle(workSheetDemographics, "A1:M1", "#4D7496");
                //SetHeaderStyle(workSheetDemographics, "A2:M2", "#C6BFF7");
                excelBytes = xlPackage.GetAsByteArray();
            }

            return File(excelBytes, "xlsx", "Data.xlsx");
        }

        private void SetHeaderStyle(ExcelWorksheet workSheet, string cells, string color)
        {
            var workSheetRange = workSheet.Cells[cells];
            workSheetRange.Style.Font.Bold = true;
            workSheetRange.Style.Border.Top.Style
                = workSheetRange.Style.Border.Left.Style
                = workSheetRange.Style.Border.Right.Style
                = workSheetRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheetRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheetRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(color));
            workSheetRange.Style.Font.Color.SetColor(Color.White);
            workSheetRange.AutoFitColumns(5, 100);
        }

    }
}