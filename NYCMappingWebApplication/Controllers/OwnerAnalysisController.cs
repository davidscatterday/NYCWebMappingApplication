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
                ExcelWorksheet workSheetOwnerAnalysis = xlPackage.Workbook.Worksheets.Add("OwnerAnalysis");

                workSheetOwnerAnalysis.Cells["A1:D1"].Merge = true;
                workSheetOwnerAnalysis.Cells["A1"].Value = "Location";
                workSheetOwnerAnalysis.Cells["E1:F1"].Merge = true;
                workSheetOwnerAnalysis.Cells["E1"].Value = "Information";
                workSheetOwnerAnalysis.Cells["G1"].Value = "RS Units";
                workSheetOwnerAnalysis.Cells["H1:I1"].Merge = true;
                workSheetOwnerAnalysis.Cells["H1"].Value = "HPD Violations";
                workSheetOwnerAnalysis.Cells["J1"].Value = "Evictions";
                workSheetOwnerAnalysis.Cells["K1"].Value = "Landlord";
                workSheetOwnerAnalysis.Cells["L1:M1"].Merge = true;
                workSheetOwnerAnalysis.Cells["L1"].Value = "Last Sale";

                workSheetOwnerAnalysis.Cells["A2"].Value = "Address";
                workSheetOwnerAnalysis.Cells["B2"].Value = "Zipcode";
                workSheetOwnerAnalysis.Cells["C2"].Value = "Borough";
                workSheetOwnerAnalysis.Cells["D2"].Value = "BBL";
                workSheetOwnerAnalysis.Cells["E2"].Value = "Built";
                workSheetOwnerAnalysis.Cells["F2"].Value = "Units";
                workSheetOwnerAnalysis.Cells["G2"].Value = "2019";
                workSheetOwnerAnalysis.Cells["H2"].Value = "Open";
                workSheetOwnerAnalysis.Cells["I2"].Value = "Total";
                workSheetOwnerAnalysis.Cells["J2"].Value = "2019";
                workSheetOwnerAnalysis.Cells["K2"].Value = "Officer/Owner";
                workSheetOwnerAnalysis.Cells["L2"].Value = "Date";
                workSheetOwnerAnalysis.Cells["M2"].Value = "Amount";

                int i = 3;
                foreach (Hpd_Registrations_Group item in data.lstPortfolio)
                {
                    workSheetOwnerAnalysis.Cells["A" + i].Value = item.Address;
                    workSheetOwnerAnalysis.Cells["B" + i].Value = item.Zip;
                    workSheetOwnerAnalysis.Cells["C" + i].Value = item.Boro;
                    workSheetOwnerAnalysis.Cells["D" + i].Value = item.bbl;
                    workSheetOwnerAnalysis.Cells["E" + i].Value = item.yearbuilt;
                    workSheetOwnerAnalysis.Cells["F" + i].Value = item.unitsres;
                    workSheetOwnerAnalysis.Cells["G" + i].Value = item.rsunits2019;
                    workSheetOwnerAnalysis.Cells["H" + i].Value = item.openviolations;
                    workSheetOwnerAnalysis.Cells["I" + i].Value = item.totalviolations;
                    workSheetOwnerAnalysis.Cells["J" + i].Value = item.totalevictions;
                    workSheetOwnerAnalysis.Cells["K" + i].Value = item.OwnerName;
                    workSheetOwnerAnalysis.Cells["L" + i].Value = item.DOC_DATE;
                    workSheetOwnerAnalysis.Cells["M" + i].Value = item.DOC_AMOUNT;
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