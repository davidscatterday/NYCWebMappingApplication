using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using NYCMappingWebApp.Models;

namespace NYCMappingWebApp.Controllers
{
    public class ConsumerProfilesController : Controller
    {
        MainDAL mainDAL = new MainDAL();
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        // GET: ConsumerProfiles/Preview
        public ActionResult Preview(string tracts)
        {
            List<string> lstBronxTracts = new List<string>();
            List<string> lstBrooklinTracts = new List<string>();
            List<string> lstManhattanTracts = new List<string>();
            List<string> lstQueensTracts = new List<string>();
            List<string> lstStatenIslandTracts = new List<string>();
            DataConsumerProfiles data = new DataConsumerProfiles();
            string selectStatement = "";
            foreach (string variable in GlobalVariables.ConsumerProfilesVariables.Split(','))
            {
                if (variable.EndsWith("PE") || variable == "DP05_0004E" || variable == "DP03_0063E" || variable == "DP03_0069E" || variable == "DP03_0071E" || variable == "DP03_0073E" || variable == "DP04_0037E" || variable == "DP04_0101E")
                {
                    selectStatement += "ROUND(avg(" + variable + "),1) as " + variable + ",";
                }
                else
                {
                    selectStatement += "sum(" + variable + ") as " + variable + ",";
                }
            }
            selectStatement = selectStatement.Substring(0, selectStatement.Length - 1);
            string sqlQuery = "Select " + selectStatement + " From ConsumerProfiles Where ";
            data.NewYorkCity = mainDAL.SearchConsumerProfilesDatabase(sqlQuery + "1=1");
            string[] splitByTract = tracts.Split(',');
            foreach (string item in splitByTract)
            {
                var splitByItem = item.Split(':');
                sqlQuery += "(Tract = '" + splitByItem[0] + "' AND County = '" + splitByItem[1] + "') OR ";
                switch (splitByItem[1])
                {
                    case ("005"): lstBronxTracts.Add(splitByItem[0].Substring(1, splitByItem[0].Length - 3)); break;
                    case ("047"): lstBrooklinTracts.Add(splitByItem[0].Substring(1, splitByItem[0].Length - 3)); break;
                    case ("061"): lstManhattanTracts.Add(splitByItem[0].Substring(1, splitByItem[0].Length - 3)); break;
                    case ("081"): lstQueensTracts.Add(splitByItem[0].Substring(1, splitByItem[0].Length - 3)); break;
                    case ("085"): lstStatenIslandTracts.Add(splitByItem[0].Substring(1, splitByItem[0].Length - 3)); break;
                    default: break;
                }
            }
            string headerMessage = "<b>" + splitByTract.Count() + " Census Tracts<b>";
            if (lstBronxTracts.Count > 0)
            {
                headerMessage += "<br/><b>Bronx:</b> " + string.Join(", ", lstBronxTracts.OrderBy(x => int.Parse(x)));
            }
            if (lstBrooklinTracts.Count > 0)
            {
                headerMessage += "<br/><b>Brooklin:</b> " + string.Join(", ", lstBrooklinTracts.OrderBy(x => int.Parse(x)));
            }
            if (lstManhattanTracts.Count > 0)
            {
                headerMessage += "<br/><b>Manhattan:</b> " + string.Join(", ", lstManhattanTracts.OrderBy(x => int.Parse(x)));
            }
            if (lstQueensTracts.Count > 0)
            {
                headerMessage += "<br/><b>Queens:</b> " + string.Join(", ", lstQueensTracts.OrderBy(x => int.Parse(x)));
            }
            if (lstStatenIslandTracts.Count > 0)
            {
                headerMessage += "<br/><b>Staten Island:</b> " + string.Join(", ", lstStatenIslandTracts.OrderBy(x => int.Parse(x)));
            }
            TempData["HeaderMessage"] = headerMessage;
            sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 3);
            data.SelectedArea = mainDAL.SearchConsumerProfilesDatabase(sqlQuery);
            return View(data);
        }

        public JsonResult SaveDatabaseReportConsumerProfile(string ReportName, string Tracts)
        {
            string msg = "Report saved successfully";
            try
            {
                DataConsumerProfiles data = new DataConsumerProfiles();
                string selectStatement = "";
                foreach (string variable in GlobalVariables.ConsumerProfilesVariables.Split(','))
                {
                    if (variable.EndsWith("PE") || variable == "DP05_0004E" || variable == "DP03_0063E" || variable == "DP03_0069E" || variable == "DP03_0071E" || variable == "DP03_0073E" || variable == "DP04_0037E" || variable == "DP04_0101E")
                    {
                        selectStatement += "ROUND(avg(" + variable + "),1) as " + variable + ",";
                    }
                    else
                    {
                        selectStatement += "sum(" + variable + ") as " + variable + ",";
                    }
                }
                selectStatement = selectStatement.Substring(0, selectStatement.Length - 1);
                string sqlQuery = "Select " + selectStatement + " From ConsumerProfiles Where ";
                data.NewYorkCity = mainDAL.SearchConsumerProfilesDatabase(sqlQuery + "1=1");
                string[] splitByTract = Tracts.Split(',');
                foreach (string item in splitByTract)
                {
                    var splitByItem = item.Split(':');
                    sqlQuery += "(Tract = '" + splitByItem[0] + "' AND County = '" + splitByItem[1] + "') OR ";
                }
                sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 3);
                data.SelectedArea = mainDAL.SearchConsumerProfilesDatabase(sqlQuery);
                byte[] excelBytes;

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage xlPackage = new ExcelPackage())
                {
                    #region Demographics
                    ExcelWorksheet workSheetDemographics = xlPackage.Workbook.Worksheets.Add("Demographics");

                    workSheetDemographics.Cells["A1:G1"].Merge = true;
                    workSheetDemographics.Cells["A1"].Value = "Age and Sex";
                    workSheetDemographics.Cells["A2:A3"].Merge = true;
                    workSheetDemographics.Cells["B2:C2"].Merge = true;
                    workSheetDemographics.Cells["B2"].Value = "Selected Area";
                    workSheetDemographics.Cells["D2:E2"].Merge = true;
                    workSheetDemographics.Cells["D2"].Value = "New York City";
                    workSheetDemographics.Cells["F2:G2"].Merge = true;
                    workSheetDemographics.Cells["F2"].Value = "Difference";

                    workSheetDemographics.Cells["B3"].Value = "Number";
                    workSheetDemographics.Cells["C3"].Value = "Percent";
                    workSheetDemographics.Cells["D3"].Value = "Number";
                    workSheetDemographics.Cells["E3"].Value = "Percent";
                    workSheetDemographics.Cells["F3"].Value = "Number";
                    workSheetDemographics.Cells["G3"].Value = "Pctg. Pt.";

                    workSheetDemographics.Cells["A4"].Value = "Total population";
                    workSheetDemographics.Cells["B4"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0001E);
                    workSheetDemographics.Cells["C4"].Value = "100%";
                    workSheetDemographics.Cells["D4"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0001E);
                    workSheetDemographics.Cells["E4"].Value = "100%";
                    workSheetDemographics.Cells["F4"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0001E - data.NewYorkCity.DP05_0001E));
                    workSheetDemographics.Cells["G4"].Value = "0.0";

                    workSheetDemographics.Cells["A5"].Value = "Male";
                    workSheetDemographics.Cells["B5"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0002E);
                    workSheetDemographics.Cells["C5"].Value = data.SelectedArea.DP05_0002E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0002E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D5"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0002E);
                    workSheetDemographics.Cells["E5"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0002E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F5"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0002E - data.NewYorkCity.DP05_0002E));
                    workSheetDemographics.Cells["G5"].Value = data.SelectedArea.DP05_0002E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0002E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0002E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A6"].Value = "Female";
                    workSheetDemographics.Cells["B6"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0003E);
                    workSheetDemographics.Cells["C6"].Value = data.SelectedArea.DP05_0003E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0003E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D6"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0003E);
                    workSheetDemographics.Cells["E6"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0003E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F6"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0003E - data.NewYorkCity.DP05_0003E));
                    workSheetDemographics.Cells["G6"].Value = data.SelectedArea.DP05_0003E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0003E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0003E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A7"].Value = "Sex ratio (males per 100 females)";
                    workSheetDemographics.Cells["B7"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0004E);
                    workSheetDemographics.Cells["C7"].Value = "";
                    workSheetDemographics.Cells["D7"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0004E);
                    workSheetDemographics.Cells["E7"].Value = "";
                    workSheetDemographics.Cells["F7"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0004E - data.NewYorkCity.DP05_0004E));
                    workSheetDemographics.Cells["G7"].Value = "";

                    workSheetDemographics.Cells["A8"].Value = "20 to 24 years";
                    workSheetDemographics.Cells["B8"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0009E);
                    workSheetDemographics.Cells["C8"].Value = data.SelectedArea.DP05_0009E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0009E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D8"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0009E);
                    workSheetDemographics.Cells["E8"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0009E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F8"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0009E - data.NewYorkCity.DP05_0009E));
                    workSheetDemographics.Cells["G8"].Value = data.SelectedArea.DP05_0009E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0009E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0009E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A9"].Value = "25 to 34 years";
                    workSheetDemographics.Cells["B9"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0010E);
                    workSheetDemographics.Cells["C9"].Value = data.SelectedArea.DP05_0010E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0010E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D9"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0010E);
                    workSheetDemographics.Cells["E9"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0010E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F9"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0010E - data.NewYorkCity.DP05_0010E));
                    workSheetDemographics.Cells["G9"].Value = data.SelectedArea.DP05_0010E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0010E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0010E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A10"].Value = "35 to 44 years";
                    workSheetDemographics.Cells["B10"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0011E);
                    workSheetDemographics.Cells["C10"].Value = data.SelectedArea.DP05_0011E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0011E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D10"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0011E);
                    workSheetDemographics.Cells["E10"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0011E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F10"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0011E - data.NewYorkCity.DP05_0011E));
                    workSheetDemographics.Cells["G10"].Value = data.SelectedArea.DP05_0011E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0011E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0011E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A11"].Value = "45 to 54 years";
                    workSheetDemographics.Cells["B11"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0012E);
                    workSheetDemographics.Cells["C11"].Value = data.SelectedArea.DP05_0012E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0012E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D11"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0012E);
                    workSheetDemographics.Cells["E11"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0012E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F11"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0012E - data.NewYorkCity.DP05_0012E));
                    workSheetDemographics.Cells["G11"].Value = data.SelectedArea.DP05_0012E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0012E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0012E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A12"].Value = "55 to 59 years";
                    workSheetDemographics.Cells["B12"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0013E);
                    workSheetDemographics.Cells["C12"].Value = data.SelectedArea.DP05_0013E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0013E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D12"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0013E);
                    workSheetDemographics.Cells["E12"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0013E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F12"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0013E - data.NewYorkCity.DP05_0013E));
                    workSheetDemographics.Cells["G12"].Value = data.SelectedArea.DP05_0013E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0013E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0013E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A13"].Value = "60 to 64 years";
                    workSheetDemographics.Cells["B13"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0014E);
                    workSheetDemographics.Cells["C13"].Value = data.SelectedArea.DP05_0014E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0014E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D13"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0014E);
                    workSheetDemographics.Cells["E13"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0014E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F13"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0014E - data.NewYorkCity.DP05_0014E));
                    workSheetDemographics.Cells["G13"].Value = data.SelectedArea.DP05_0014E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0014E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0014E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A14"].Value = "65 to 74 years";
                    workSheetDemographics.Cells["B14"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0015E);
                    workSheetDemographics.Cells["C14"].Value = data.SelectedArea.DP05_0015E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0015E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D14"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0015E);
                    workSheetDemographics.Cells["E14"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0015E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F14"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0015E - data.NewYorkCity.DP05_0015E));
                    workSheetDemographics.Cells["G14"].Value = data.SelectedArea.DP05_0015E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0015E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0015E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";

                    workSheetDemographics.Cells["A15"].Value = "75 to 84 years";
                    workSheetDemographics.Cells["B15"].Value = String.Format("{0:n0}", data.SelectedArea.DP05_0016E);
                    workSheetDemographics.Cells["C15"].Value = data.SelectedArea.DP05_0016E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP05_0016E.Value) / data.SelectedArea.DP05_0001E.Value, 1) + "%" : "";
                    workSheetDemographics.Cells["D15"].Value = String.Format("{0:n0}", data.NewYorkCity.DP05_0016E);
                    workSheetDemographics.Cells["E15"].Value = Math.Round((double)(100 * data.NewYorkCity.DP05_0016E.Value) / data.NewYorkCity.DP05_0001E.Value, 1) + "%";
                    workSheetDemographics.Cells["F15"].Value = String.Format("{0:n0}", (data.SelectedArea.DP05_0016E - data.NewYorkCity.DP05_0016E));
                    workSheetDemographics.Cells["G15"].Value = data.SelectedArea.DP05_0016E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP05_0016E.Value) / data.SelectedArea.DP05_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP05_0016E.Value) / data.NewYorkCity.DP05_0001E.Value, 1), 1).ToString() : "";
                    workSheetDemographics.Cells[workSheetDemographics.Dimension.Address].AutoFitColumns();
                    //Header
                    SetHeaderStyle(workSheetDemographics, "A1");
                    #endregion Demographics
                    #region Social
                    ExcelWorksheet workSheetSocial = xlPackage.Workbook.Worksheets.Add("Social");

                    workSheetSocial.Cells["A1:G1"].Merge = true;
                    workSheetSocial.Cells["A1"].Value = "Household Type";
                    workSheetSocial.Cells["A2:A3"].Merge = true;
                    workSheetSocial.Cells["B2:C2"].Merge = true;
                    workSheetSocial.Cells["B2"].Value = "Selected Area";
                    workSheetSocial.Cells["D2:E2"].Merge = true;
                    workSheetSocial.Cells["D2"].Value = "New York City";
                    workSheetSocial.Cells["F2:G2"].Merge = true;
                    workSheetSocial.Cells["F2"].Value = "Difference";

                    workSheetSocial.Cells["B3"].Value = "Number";
                    workSheetSocial.Cells["C3"].Value = "Percent";
                    workSheetSocial.Cells["D3"].Value = "Number";
                    workSheetSocial.Cells["E3"].Value = "Percent";
                    workSheetSocial.Cells["F3"].Value = "Number";
                    workSheetSocial.Cells["G3"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A4"].Value = "Total households";
                    workSheetSocial.Cells["B4"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0001E);
                    workSheetSocial.Cells["C4"].Value = "100%";
                    workSheetSocial.Cells["D4"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0001E);
                    workSheetSocial.Cells["E4"].Value = "100%";
                    workSheetSocial.Cells["F4"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0001E - data.NewYorkCity.DP02_0001E));
                    workSheetSocial.Cells["G4"].Value = "0.0";

                    workSheetSocial.Cells["A5"].Value = "Family households (families)";
                    workSheetSocial.Cells["B5"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0002E);
                    workSheetSocial.Cells["C5"].Value = data.SelectedArea.DP02_0002E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0002E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D5"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0002E);
                    workSheetSocial.Cells["E5"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0002E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F5"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0002E - data.NewYorkCity.DP02_0002E));
                    workSheetSocial.Cells["G5"].Value = data.SelectedArea.DP02_0002E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0002E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0002E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A6"].Value = "With own children of the householder under 18 years";
                    workSheetSocial.Cells["B6"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0003E);
                    workSheetSocial.Cells["C6"].Value = data.SelectedArea.DP02_0003E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0003E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D6"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0003E);
                    workSheetSocial.Cells["E6"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0003E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F6"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0003E - data.NewYorkCity.DP02_0003E));
                    workSheetSocial.Cells["G6"].Value = data.SelectedArea.DP02_0003E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0003E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0003E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A7"].Value = "Male householder, no wife present, family";
                    workSheetSocial.Cells["B7"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0006E);
                    workSheetSocial.Cells["C7"].Value = data.SelectedArea.DP02_0006E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0006E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D7"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0006E);
                    workSheetSocial.Cells["E7"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0006E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F7"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0006E - data.NewYorkCity.DP02_0006E));
                    workSheetSocial.Cells["G7"].Value = data.SelectedArea.DP02_0006E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0006E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0006E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A8"].Value = "Female householder, no husband present, family";
                    workSheetSocial.Cells["B8"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0008E);
                    workSheetSocial.Cells["C8"].Value = data.SelectedArea.DP02_0008E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0008E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D8"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0008E);
                    workSheetSocial.Cells["E8"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0008E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F8"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0008E - data.NewYorkCity.DP02_0008E));
                    workSheetSocial.Cells["G8"].Value = data.SelectedArea.DP02_0008E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0008E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0008E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A9"].Value = "Nonfamily households";
                    workSheetSocial.Cells["B9"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0010E);
                    workSheetSocial.Cells["C9"].Value = data.SelectedArea.DP02_0010E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0010E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D9"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0010E);
                    workSheetSocial.Cells["E9"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0010E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F9"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0010E - data.NewYorkCity.DP02_0010E));
                    workSheetSocial.Cells["G9"].Value = data.SelectedArea.DP02_0010E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0010E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0010E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A10"].Value = "Householder living alone";
                    workSheetSocial.Cells["B10"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0011E);
                    workSheetSocial.Cells["C10"].Value = data.SelectedArea.DP02_0011E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0011E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D10"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0011E);
                    workSheetSocial.Cells["E10"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0011E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F10"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0011E - data.NewYorkCity.DP02_0011E));
                    workSheetSocial.Cells["G10"].Value = data.SelectedArea.DP02_0011E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0011E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0011E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A11"].Value = "65 years and over";
                    workSheetSocial.Cells["B11"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0012E);
                    workSheetSocial.Cells["C11"].Value = data.SelectedArea.DP02_0012E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0012E.Value) / data.SelectedArea.DP02_0001E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D11"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0012E);
                    workSheetSocial.Cells["E11"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0012E.Value) / data.NewYorkCity.DP02_0001E.Value, 1) + "%";
                    workSheetSocial.Cells["F11"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0012E - data.NewYorkCity.DP02_0012E));
                    workSheetSocial.Cells["G11"].Value = data.SelectedArea.DP02_0012E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0012E.Value) / data.SelectedArea.DP02_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0012E.Value) / data.NewYorkCity.DP02_0001E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A13:G13"].Merge = true;
                    workSheetSocial.Cells["A13"].Value = "Marital Status";
                    workSheetSocial.Cells["A14:A15"].Merge = true;
                    workSheetSocial.Cells["B14:C14"].Merge = true;
                    workSheetSocial.Cells["B14"].Value = "Selected Area";
                    workSheetSocial.Cells["D14:E14"].Merge = true;
                    workSheetSocial.Cells["D14"].Value = "New York City";
                    workSheetSocial.Cells["F14:G14"].Merge = true;
                    workSheetSocial.Cells["F14"].Value = "Difference";

                    workSheetSocial.Cells["B15"].Value = "Number";
                    workSheetSocial.Cells["C15"].Value = "Percent";
                    workSheetSocial.Cells["D15"].Value = "Number";
                    workSheetSocial.Cells["E15"].Value = "Percent";
                    workSheetSocial.Cells["F15"].Value = "Number";
                    workSheetSocial.Cells["G15"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A16"].Value = "Males 15 years and over";
                    workSheetSocial.Cells["B16"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0024E);
                    workSheetSocial.Cells["C16"].Value = "100%";
                    workSheetSocial.Cells["D16"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0024E);
                    workSheetSocial.Cells["E16"].Value = "100%";
                    workSheetSocial.Cells["F16"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0024E - data.NewYorkCity.DP02_0024E));
                    workSheetSocial.Cells["G16"].Value = "0.0";

                    workSheetSocial.Cells["A17"].Value = "Never married";
                    workSheetSocial.Cells["B17"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0025E);
                    workSheetSocial.Cells["C17"].Value = data.SelectedArea.DP02_0025E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0025E.Value) / data.SelectedArea.DP02_0024E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D17"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0025E);
                    workSheetSocial.Cells["E17"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0025E.Value) / data.NewYorkCity.DP02_0024E.Value, 1) + "%";
                    workSheetSocial.Cells["F17"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0025E - data.NewYorkCity.DP02_0025E));
                    workSheetSocial.Cells["G17"].Value = data.SelectedArea.DP02_0025E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0025E.Value) / data.SelectedArea.DP02_0024E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0025E.Value) / data.NewYorkCity.DP02_0024E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A18"].Value = "Now married, except separated";
                    workSheetSocial.Cells["B18"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0026E);
                    workSheetSocial.Cells["C18"].Value = data.SelectedArea.DP02_0026E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0026E.Value) / data.SelectedArea.DP02_0024E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D18"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0026E);
                    workSheetSocial.Cells["E18"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0026E.Value) / data.NewYorkCity.DP02_0024E.Value, 1) + "%";
                    workSheetSocial.Cells["F18"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0026E - data.NewYorkCity.DP02_0026E));
                    workSheetSocial.Cells["G18"].Value = data.SelectedArea.DP02_0026E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0026E.Value) / data.SelectedArea.DP02_0024E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0026E.Value) / data.NewYorkCity.DP02_0024E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A19"].Value = "Separated";
                    workSheetSocial.Cells["B19"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0027E);
                    workSheetSocial.Cells["C19"].Value = data.SelectedArea.DP02_0027E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0027E.Value) / data.SelectedArea.DP02_0024E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D19"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0027E);
                    workSheetSocial.Cells["E19"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0027E.Value) / data.NewYorkCity.DP02_0024E.Value, 1) + "%";
                    workSheetSocial.Cells["F19"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0027E - data.NewYorkCity.DP02_0027E));
                    workSheetSocial.Cells["G19"].Value = data.SelectedArea.DP02_0027E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0027E.Value) / data.SelectedArea.DP02_0024E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0027E.Value) / data.NewYorkCity.DP02_0024E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A20"].Value = "Widowed";
                    workSheetSocial.Cells["B20"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0028E);
                    workSheetSocial.Cells["C20"].Value = data.SelectedArea.DP02_0028E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0028E.Value) / data.SelectedArea.DP02_0024E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D20"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0028E);
                    workSheetSocial.Cells["E20"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0028E.Value) / data.NewYorkCity.DP02_0024E.Value, 1) + "%";
                    workSheetSocial.Cells["F20"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0028E - data.NewYorkCity.DP02_0028E));
                    workSheetSocial.Cells["G20"].Value = data.SelectedArea.DP02_0028E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0028E.Value) / data.SelectedArea.DP02_0024E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0028E.Value) / data.NewYorkCity.DP02_0024E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A21"].Value = "Divorced";
                    workSheetSocial.Cells["B21"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0029E);
                    workSheetSocial.Cells["C21"].Value = data.SelectedArea.DP02_0029E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0029E.Value) / data.SelectedArea.DP02_0024E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D21"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0029E);
                    workSheetSocial.Cells["E21"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0029E.Value) / data.NewYorkCity.DP02_0024E.Value, 1) + "%";
                    workSheetSocial.Cells["F21"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0029E - data.NewYorkCity.DP02_0029E));
                    workSheetSocial.Cells["G21"].Value = data.SelectedArea.DP02_0029E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0029E.Value) / data.SelectedArea.DP02_0024E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0029E.Value) / data.NewYorkCity.DP02_0024E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A22"].Value = "Females 15 years and over";
                    workSheetSocial.Cells["B22"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0030E);
                    workSheetSocial.Cells["C22"].Value = "100%";
                    workSheetSocial.Cells["D22"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0030E);
                    workSheetSocial.Cells["E22"].Value = "100%";
                    workSheetSocial.Cells["F22"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0030E - data.NewYorkCity.DP02_0030E));
                    workSheetSocial.Cells["G22"].Value = "0.0";

                    workSheetSocial.Cells["A23"].Value = "Never married";
                    workSheetSocial.Cells["B23"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0031E);
                    workSheetSocial.Cells["C23"].Value = data.SelectedArea.DP02_0031E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0031E.Value) / data.SelectedArea.DP02_0030E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D23"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0031E);
                    workSheetSocial.Cells["E23"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0031E.Value) / data.NewYorkCity.DP02_0030E.Value, 1) + "%";
                    workSheetSocial.Cells["F23"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0031E - data.NewYorkCity.DP02_0031E));
                    workSheetSocial.Cells["G23"].Value = data.SelectedArea.DP02_0031E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0031E.Value) / data.SelectedArea.DP02_0030E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0031E.Value) / data.NewYorkCity.DP02_0030E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A24"].Value = "Now married, except separated";
                    workSheetSocial.Cells["B24"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0032E);
                    workSheetSocial.Cells["C24"].Value = data.SelectedArea.DP02_0032E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0032E.Value) / data.SelectedArea.DP02_0030E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D24"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0032E);
                    workSheetSocial.Cells["E24"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0032E.Value) / data.NewYorkCity.DP02_0030E.Value, 1) + "%";
                    workSheetSocial.Cells["F24"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0032E - data.NewYorkCity.DP02_0032E));
                    workSheetSocial.Cells["G24"].Value = data.SelectedArea.DP02_0032E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0032E.Value) / data.SelectedArea.DP02_0030E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0032E.Value) / data.NewYorkCity.DP02_0030E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A25"].Value = "Separated";
                    workSheetSocial.Cells["B25"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0033E);
                    workSheetSocial.Cells["C25"].Value = data.SelectedArea.DP02_0033E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0033E.Value) / data.SelectedArea.DP02_0030E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D25"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0033E);
                    workSheetSocial.Cells["E25"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0033E.Value) / data.NewYorkCity.DP02_0030E.Value, 1) + "%";
                    workSheetSocial.Cells["F25"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0033E - data.NewYorkCity.DP02_0033E));
                    workSheetSocial.Cells["G25"].Value = data.SelectedArea.DP02_0033E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0033E.Value) / data.SelectedArea.DP02_0030E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0033E.Value) / data.NewYorkCity.DP02_0030E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A26"].Value = "Widowed";
                    workSheetSocial.Cells["B26"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0034E);
                    workSheetSocial.Cells["C26"].Value = data.SelectedArea.DP02_0034E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0034E.Value) / data.SelectedArea.DP02_0030E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D26"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0034E);
                    workSheetSocial.Cells["E26"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0034E.Value) / data.NewYorkCity.DP02_0030E.Value, 1) + "%";
                    workSheetSocial.Cells["F26"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0034E - data.NewYorkCity.DP02_0034E));
                    workSheetSocial.Cells["G26"].Value = data.SelectedArea.DP02_0034E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0034E.Value) / data.SelectedArea.DP02_0030E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0034E.Value) / data.NewYorkCity.DP02_0030E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A27"].Value = "Divorced";
                    workSheetSocial.Cells["B27"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0035E);
                    workSheetSocial.Cells["C27"].Value = data.SelectedArea.DP02_0035E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0035E.Value) / data.SelectedArea.DP02_0030E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D27"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0035E);
                    workSheetSocial.Cells["E27"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0035E.Value) / data.NewYorkCity.DP02_0030E.Value, 1) + "%";
                    workSheetSocial.Cells["F27"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0035E - data.NewYorkCity.DP02_0035E));
                    workSheetSocial.Cells["G27"].Value = data.SelectedArea.DP02_0035E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0035E.Value) / data.SelectedArea.DP02_0030E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0035E.Value) / data.NewYorkCity.DP02_0030E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A29:G29"].Merge = true;
                    workSheetSocial.Cells["A29"].Value = "School Enrollment";
                    workSheetSocial.Cells["A30:A31"].Merge = true;
                    workSheetSocial.Cells["B30:C30"].Merge = true;
                    workSheetSocial.Cells["B30"].Value = "Selected Area";
                    workSheetSocial.Cells["D30:E30"].Merge = true;
                    workSheetSocial.Cells["D30"].Value = "New York City";
                    workSheetSocial.Cells["F30:G30"].Merge = true;
                    workSheetSocial.Cells["F30"].Value = "Difference";

                    workSheetSocial.Cells["B31"].Value = "Number";
                    workSheetSocial.Cells["C31"].Value = "Percent";
                    workSheetSocial.Cells["D31"].Value = "Number";
                    workSheetSocial.Cells["E31"].Value = "Percent";
                    workSheetSocial.Cells["F31"].Value = "Number";
                    workSheetSocial.Cells["G31"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A32"].Value = "Population 3 years and over enrolled in school";
                    workSheetSocial.Cells["B32"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0052E);
                    workSheetSocial.Cells["C32"].Value = "100%";
                    workSheetSocial.Cells["D32"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0052E);
                    workSheetSocial.Cells["E32"].Value = "100%";
                    workSheetSocial.Cells["F32"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0052E - data.NewYorkCity.DP02_0052E));
                    workSheetSocial.Cells["G32"].Value = "0.0";

                    workSheetSocial.Cells["A33"].Value = "Nursery school, preschool";
                    workSheetSocial.Cells["B33"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0053E);
                    workSheetSocial.Cells["C33"].Value = data.SelectedArea.DP02_0053E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0053E.Value) / data.SelectedArea.DP02_0052E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D33"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0053E);
                    workSheetSocial.Cells["E33"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0053E.Value) / data.NewYorkCity.DP02_0052E.Value, 1) + "%";
                    workSheetSocial.Cells["F33"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0053E - data.NewYorkCity.DP02_0053E));
                    workSheetSocial.Cells["G33"].Value = data.SelectedArea.DP02_0053E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0053E.Value) / data.SelectedArea.DP02_0052E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0053E.Value) / data.NewYorkCity.DP02_0052E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A34"].Value = "Kindergarten";
                    workSheetSocial.Cells["B34"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0054E);
                    workSheetSocial.Cells["C34"].Value = data.SelectedArea.DP02_0054E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0054E.Value) / data.SelectedArea.DP02_0052E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D34"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0054E);
                    workSheetSocial.Cells["E34"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0054E.Value) / data.NewYorkCity.DP02_0052E.Value, 1) + "%";
                    workSheetSocial.Cells["F34"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0054E - data.NewYorkCity.DP02_0054E));
                    workSheetSocial.Cells["G34"].Value = data.SelectedArea.DP02_0054E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0054E.Value) / data.SelectedArea.DP02_0052E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0054E.Value) / data.NewYorkCity.DP02_0052E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A35"].Value = "Elementary school (grades 1-8)";
                    workSheetSocial.Cells["B35"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0055E);
                    workSheetSocial.Cells["C35"].Value = data.SelectedArea.DP02_0055E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0055E.Value) / data.SelectedArea.DP02_0052E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D35"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0055E);
                    workSheetSocial.Cells["E35"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0055E.Value) / data.NewYorkCity.DP02_0052E.Value, 1) + "%";
                    workSheetSocial.Cells["F35"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0055E - data.NewYorkCity.DP02_0055E));
                    workSheetSocial.Cells["G35"].Value = data.SelectedArea.DP02_0055E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0055E.Value) / data.SelectedArea.DP02_0052E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0055E.Value) / data.NewYorkCity.DP02_0052E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A36"].Value = "High school (grades 9-12)";
                    workSheetSocial.Cells["B36"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0056E);
                    workSheetSocial.Cells["C36"].Value = data.SelectedArea.DP02_0056E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0056E.Value) / data.SelectedArea.DP02_0052E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D36"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0056E);
                    workSheetSocial.Cells["E36"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0056E.Value) / data.NewYorkCity.DP02_0052E.Value, 1) + "%";
                    workSheetSocial.Cells["F36"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0056E - data.NewYorkCity.DP02_0056E));
                    workSheetSocial.Cells["G36"].Value = data.SelectedArea.DP02_0056E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0056E.Value) / data.SelectedArea.DP02_0052E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0056E.Value) / data.NewYorkCity.DP02_0052E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A37"].Value = "College or graduate school";
                    workSheetSocial.Cells["B37"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0057E);
                    workSheetSocial.Cells["C37"].Value = data.SelectedArea.DP02_0057E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0057E.Value) / data.SelectedArea.DP02_0052E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D37"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0057E);
                    workSheetSocial.Cells["E37"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0057E.Value) / data.NewYorkCity.DP02_0052E.Value, 1) + "%";
                    workSheetSocial.Cells["F37"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0057E - data.NewYorkCity.DP02_0057E));
                    workSheetSocial.Cells["G37"].Value = data.SelectedArea.DP02_0057E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0057E.Value) / data.SelectedArea.DP02_0052E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0057E.Value) / data.NewYorkCity.DP02_0052E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A39:G39"].Merge = true;
                    workSheetSocial.Cells["A39"].Value = "Educational Attainment";
                    workSheetSocial.Cells["A40:A41"].Merge = true;
                    workSheetSocial.Cells["B40:C40"].Merge = true;
                    workSheetSocial.Cells["B40"].Value = "Selected Area";
                    workSheetSocial.Cells["D40:E40"].Merge = true;
                    workSheetSocial.Cells["D40"].Value = "New York City";
                    workSheetSocial.Cells["F40:G40"].Merge = true;
                    workSheetSocial.Cells["F40"].Value = "Difference";

                    workSheetSocial.Cells["B41"].Value = "Number";
                    workSheetSocial.Cells["C41"].Value = "Percent";
                    workSheetSocial.Cells["D41"].Value = "Number";
                    workSheetSocial.Cells["E41"].Value = "Percent";
                    workSheetSocial.Cells["F41"].Value = "Number";
                    workSheetSocial.Cells["G41"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A42"].Value = "Population 25 years and over";
                    workSheetSocial.Cells["B42"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0058E);
                    workSheetSocial.Cells["C42"].Value = "100%";
                    workSheetSocial.Cells["D42"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0058E);
                    workSheetSocial.Cells["E42"].Value = "100%";
                    workSheetSocial.Cells["F42"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0058E - data.NewYorkCity.DP02_0058E));
                    workSheetSocial.Cells["G42"].Value = "0.0";

                    workSheetSocial.Cells["A43"].Value = "Less than 9th grade";
                    workSheetSocial.Cells["B43"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0059E);
                    workSheetSocial.Cells["C43"].Value = data.SelectedArea.DP02_0059E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0059E.Value) / data.SelectedArea.DP02_0058E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D43"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0059E);
                    workSheetSocial.Cells["E43"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0059E.Value) / data.NewYorkCity.DP02_0058E.Value, 1) + "%";
                    workSheetSocial.Cells["F43"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0059E - data.NewYorkCity.DP02_0059E));
                    workSheetSocial.Cells["G43"].Value = data.SelectedArea.DP02_0059E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0059E.Value) / data.SelectedArea.DP02_0058E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0059E.Value) / data.NewYorkCity.DP02_0058E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A44"].Value = "9th to 12th grade, no diploma";
                    workSheetSocial.Cells["B44"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0060E);
                    workSheetSocial.Cells["C44"].Value = data.SelectedArea.DP02_0060E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0060E.Value) / data.SelectedArea.DP02_0058E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D44"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0060E);
                    workSheetSocial.Cells["E44"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0060E.Value) / data.NewYorkCity.DP02_0058E.Value, 1) + "%";
                    workSheetSocial.Cells["F44"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0060E - data.NewYorkCity.DP02_0060E));
                    workSheetSocial.Cells["G44"].Value = data.SelectedArea.DP02_0060E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0060E.Value) / data.SelectedArea.DP02_0058E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0060E.Value) / data.NewYorkCity.DP02_0058E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A45"].Value = "High school graduate (includes equivalency)";
                    workSheetSocial.Cells["B45"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0061E);
                    workSheetSocial.Cells["C45"].Value = data.SelectedArea.DP02_0061E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0061E.Value) / data.SelectedArea.DP02_0058E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D45"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0061E);
                    workSheetSocial.Cells["E45"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0061E.Value) / data.NewYorkCity.DP02_0058E.Value, 1) + "%";
                    workSheetSocial.Cells["F45"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0061E - data.NewYorkCity.DP02_0061E));
                    workSheetSocial.Cells["G45"].Value = data.SelectedArea.DP02_0061E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0061E.Value) / data.SelectedArea.DP02_0058E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0061E.Value) / data.NewYorkCity.DP02_0058E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A46"].Value = "Some college, no degree";
                    workSheetSocial.Cells["B46"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0062E);
                    workSheetSocial.Cells["C46"].Value = data.SelectedArea.DP02_0062E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0062E.Value) / data.SelectedArea.DP02_0058E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D46"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0062E);
                    workSheetSocial.Cells["E46"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0062E.Value) / data.NewYorkCity.DP02_0058E.Value, 1) + "%";
                    workSheetSocial.Cells["F46"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0062E - data.NewYorkCity.DP02_0062E));
                    workSheetSocial.Cells["G46"].Value = data.SelectedArea.DP02_0062E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0062E.Value) / data.SelectedArea.DP02_0058E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0062E.Value) / data.NewYorkCity.DP02_0058E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A47"].Value = "Associate's degree";
                    workSheetSocial.Cells["B47"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0063E);
                    workSheetSocial.Cells["C47"].Value = data.SelectedArea.DP02_0063E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0063E.Value) / data.SelectedArea.DP02_0058E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D47"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0063E);
                    workSheetSocial.Cells["E47"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0063E.Value) / data.NewYorkCity.DP02_0058E.Value, 1) + "%";
                    workSheetSocial.Cells["F47"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0063E - data.NewYorkCity.DP02_0063E));
                    workSheetSocial.Cells["G47"].Value = data.SelectedArea.DP02_0063E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0063E.Value) / data.SelectedArea.DP02_0058E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0063E.Value) / data.NewYorkCity.DP02_0058E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A48"].Value = "Bachelor's degree";
                    workSheetSocial.Cells["B48"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0064E);
                    workSheetSocial.Cells["C48"].Value = data.SelectedArea.DP02_0064E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0064E.Value) / data.SelectedArea.DP02_0058E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D48"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0064E);
                    workSheetSocial.Cells["E48"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0064E.Value) / data.NewYorkCity.DP02_0058E.Value, 1) + "%";
                    workSheetSocial.Cells["F48"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0064E - data.NewYorkCity.DP02_0064E));
                    workSheetSocial.Cells["G48"].Value = data.SelectedArea.DP02_0064E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0064E.Value) / data.SelectedArea.DP02_0058E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0064E.Value) / data.NewYorkCity.DP02_0058E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A50:G50"].Merge = true;
                    workSheetSocial.Cells["A50"].Value = "Disability Status Of The Civilian Noninstitutionalized Population";
                    workSheetSocial.Cells["A51:A52"].Merge = true;
                    workSheetSocial.Cells["B51:C51"].Merge = true;
                    workSheetSocial.Cells["B51"].Value = "Selected Area";
                    workSheetSocial.Cells["D51:E51"].Merge = true;
                    workSheetSocial.Cells["D51"].Value = "New York City";
                    workSheetSocial.Cells["F51:G51"].Merge = true;
                    workSheetSocial.Cells["F51"].Value = "Difference";

                    workSheetSocial.Cells["B52"].Value = "Number";
                    workSheetSocial.Cells["C52"].Value = "Percent";
                    workSheetSocial.Cells["D52"].Value = "Number";
                    workSheetSocial.Cells["E52"].Value = "Percent";
                    workSheetSocial.Cells["F52"].Value = "Number";
                    workSheetSocial.Cells["G52"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A53"].Value = "Total Civilian Noninstitutionalized Population";
                    workSheetSocial.Cells["B53"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0070E);
                    workSheetSocial.Cells["C53"].Value = "100%";
                    workSheetSocial.Cells["D53"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0070E);
                    workSheetSocial.Cells["E53"].Value = "100%";
                    workSheetSocial.Cells["F53"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0070E - data.NewYorkCity.DP02_0070E));
                    workSheetSocial.Cells["G53"].Value = "0.0";

                    workSheetSocial.Cells["A54"].Value = "With a disability";
                    workSheetSocial.Cells["B54"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0071E);
                    workSheetSocial.Cells["C54"].Value = data.SelectedArea.DP02_0071E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0071E.Value) / data.SelectedArea.DP02_0070E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D54"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0071E);
                    workSheetSocial.Cells["E54"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0071E.Value) / data.NewYorkCity.DP02_0070E.Value, 1) + "%";
                    workSheetSocial.Cells["F54"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0071E - data.NewYorkCity.DP02_0071E));
                    workSheetSocial.Cells["G54"].Value = data.SelectedArea.DP02_0071E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0071E.Value) / data.SelectedArea.DP02_0070E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0071E.Value) / data.NewYorkCity.DP02_0070E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A55"].Value = "18 to 64 years";
                    workSheetSocial.Cells["B55"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0074E);
                    workSheetSocial.Cells["C55"].Value = data.SelectedArea.DP02_0074E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0074E.Value) / data.SelectedArea.DP02_0070E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D55"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0074E);
                    workSheetSocial.Cells["E55"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0074E.Value) / data.NewYorkCity.DP02_0070E.Value, 1) + "%";
                    workSheetSocial.Cells["F55"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0074E - data.NewYorkCity.DP02_0074E));
                    workSheetSocial.Cells["G55"].Value = data.SelectedArea.DP02_0074E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0074E.Value) / data.SelectedArea.DP02_0070E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0074E.Value) / data.NewYorkCity.DP02_0070E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A56"].Value = "With a disability";
                    workSheetSocial.Cells["B56"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0075E);
                    workSheetSocial.Cells["C56"].Value = data.SelectedArea.DP02_0075E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0075E.Value) / data.SelectedArea.DP02_0070E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D56"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0075E);
                    workSheetSocial.Cells["E56"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0075E.Value) / data.NewYorkCity.DP02_0070E.Value, 1) + "%";
                    workSheetSocial.Cells["F56"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0075E - data.NewYorkCity.DP02_0075E));
                    workSheetSocial.Cells["G56"].Value = data.SelectedArea.DP02_0075E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0075E.Value) / data.SelectedArea.DP02_0070E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0075E.Value) / data.NewYorkCity.DP02_0070E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A57"].Value = "65 years and over";
                    workSheetSocial.Cells["B57"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0076E);
                    workSheetSocial.Cells["C57"].Value = data.SelectedArea.DP02_0076E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0076E.Value) / data.SelectedArea.DP02_0070E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D57"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0076E);
                    workSheetSocial.Cells["E57"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0076E.Value) / data.NewYorkCity.DP02_0070E.Value, 1) + "%";
                    workSheetSocial.Cells["F57"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0076E - data.NewYorkCity.DP02_0076E));
                    workSheetSocial.Cells["G57"].Value = data.SelectedArea.DP02_0076E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0076E.Value) / data.SelectedArea.DP02_0070E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0076E.Value) / data.NewYorkCity.DP02_0070E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A59:G59"].Merge = true;
                    workSheetSocial.Cells["A59"].Value = "Residence 1 Year Ago";
                    workSheetSocial.Cells["A60:A61"].Merge = true;
                    workSheetSocial.Cells["B60:C60"].Merge = true;
                    workSheetSocial.Cells["B60"].Value = "Selected Area";
                    workSheetSocial.Cells["D60:E60"].Merge = true;
                    workSheetSocial.Cells["D60"].Value = "New York City";
                    workSheetSocial.Cells["F60:G60"].Merge = true;
                    workSheetSocial.Cells["F60"].Value = "Difference";

                    workSheetSocial.Cells["B61"].Value = "Number";
                    workSheetSocial.Cells["C61"].Value = "Percent";
                    workSheetSocial.Cells["D61"].Value = "Number";
                    workSheetSocial.Cells["E61"].Value = "Percent";
                    workSheetSocial.Cells["F61"].Value = "Number";
                    workSheetSocial.Cells["G61"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A62"].Value = "Population 1 year and over";
                    workSheetSocial.Cells["B62"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0078E);
                    workSheetSocial.Cells["C62"].Value = "100%";
                    workSheetSocial.Cells["D62"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0078E);
                    workSheetSocial.Cells["E62"].Value = "100%";
                    workSheetSocial.Cells["F62"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0078E - data.NewYorkCity.DP02_0078E));
                    workSheetSocial.Cells["G62"].Value = "0.0";

                    workSheetSocial.Cells["A63"].Value = "Same house";
                    workSheetSocial.Cells["B63"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0079E);
                    workSheetSocial.Cells["C63"].Value = data.SelectedArea.DP02_0079E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0079E.Value) / data.SelectedArea.DP02_0078E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D63"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0079E);
                    workSheetSocial.Cells["E63"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0079E.Value) / data.NewYorkCity.DP02_0078E.Value, 1) + "%";
                    workSheetSocial.Cells["F63"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0079E - data.NewYorkCity.DP02_0079E));
                    workSheetSocial.Cells["G63"].Value = data.SelectedArea.DP02_0079E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0079E.Value) / data.SelectedArea.DP02_0078E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0079E.Value) / data.NewYorkCity.DP02_0078E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A64"].Value = "Different house in the U.S.";
                    workSheetSocial.Cells["B64"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0080E);
                    workSheetSocial.Cells["C64"].Value = data.SelectedArea.DP02_0080E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0080E.Value) / data.SelectedArea.DP02_0078E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D64"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0080E);
                    workSheetSocial.Cells["E64"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0080E.Value) / data.NewYorkCity.DP02_0078E.Value, 1) + "%";
                    workSheetSocial.Cells["F64"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0080E - data.NewYorkCity.DP02_0080E));
                    workSheetSocial.Cells["G64"].Value = data.SelectedArea.DP02_0080E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0080E.Value) / data.SelectedArea.DP02_0078E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0080E.Value) / data.NewYorkCity.DP02_0078E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A65"].Value = "Same county";
                    workSheetSocial.Cells["B65"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0081E);
                    workSheetSocial.Cells["C65"].Value = data.SelectedArea.DP02_0081E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0081E.Value) / data.SelectedArea.DP02_0078E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D65"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0081E);
                    workSheetSocial.Cells["E65"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0081E.Value) / data.NewYorkCity.DP02_0078E.Value, 1) + "%";
                    workSheetSocial.Cells["F65"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0081E - data.NewYorkCity.DP02_0081E));
                    workSheetSocial.Cells["G65"].Value = data.SelectedArea.DP02_0081E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0081E.Value) / data.SelectedArea.DP02_0078E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0081E.Value) / data.NewYorkCity.DP02_0078E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A66"].Value = "Different county";
                    workSheetSocial.Cells["B66"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0082E);
                    workSheetSocial.Cells["C66"].Value = data.SelectedArea.DP02_0082E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0082E.Value) / data.SelectedArea.DP02_0078E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D66"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0082E);
                    workSheetSocial.Cells["E66"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0082E.Value) / data.NewYorkCity.DP02_0078E.Value, 1) + "%";
                    workSheetSocial.Cells["F66"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0082E - data.NewYorkCity.DP02_0082E));
                    workSheetSocial.Cells["G66"].Value = data.SelectedArea.DP02_0082E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0082E.Value) / data.SelectedArea.DP02_0078E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0082E.Value) / data.NewYorkCity.DP02_0078E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A67"].Value = "Same state";
                    workSheetSocial.Cells["B67"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0083E);
                    workSheetSocial.Cells["C67"].Value = data.SelectedArea.DP02_0083E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0083E.Value) / data.SelectedArea.DP02_0078E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D67"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0083E);
                    workSheetSocial.Cells["E67"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0083E.Value) / data.NewYorkCity.DP02_0078E.Value, 1) + "%";
                    workSheetSocial.Cells["F67"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0083E - data.NewYorkCity.DP02_0083E));
                    workSheetSocial.Cells["G67"].Value = data.SelectedArea.DP02_0083E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0083E.Value) / data.SelectedArea.DP02_0078E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0083E.Value) / data.NewYorkCity.DP02_0078E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A68"].Value = "Different state";
                    workSheetSocial.Cells["B68"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0084E);
                    workSheetSocial.Cells["C68"].Value = data.SelectedArea.DP02_0084E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0084E.Value) / data.SelectedArea.DP02_0078E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D68"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0084E);
                    workSheetSocial.Cells["E68"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0084E.Value) / data.NewYorkCity.DP02_0078E.Value, 1) + "%";
                    workSheetSocial.Cells["F68"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0084E - data.NewYorkCity.DP02_0084E));
                    workSheetSocial.Cells["G68"].Value = data.SelectedArea.DP02_0084E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0084E.Value) / data.SelectedArea.DP02_0078E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0084E.Value) / data.NewYorkCity.DP02_0078E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A70:G70"].Merge = true;
                    workSheetSocial.Cells["A70"].Value = "Computers and Internet Use";
                    workSheetSocial.Cells["A71:A72"].Merge = true;
                    workSheetSocial.Cells["B71:C71"].Merge = true;
                    workSheetSocial.Cells["B71"].Value = "Selected Area";
                    workSheetSocial.Cells["D71:E71"].Merge = true;
                    workSheetSocial.Cells["D71"].Value = "New York City";
                    workSheetSocial.Cells["F71:G71"].Merge = true;
                    workSheetSocial.Cells["F71"].Value = "Difference";

                    workSheetSocial.Cells["B72"].Value = "Number";
                    workSheetSocial.Cells["C72"].Value = "Percent";
                    workSheetSocial.Cells["D72"].Value = "Number";
                    workSheetSocial.Cells["E72"].Value = "Percent";
                    workSheetSocial.Cells["F72"].Value = "Number";
                    workSheetSocial.Cells["G72"].Value = "Pctg. Pt.";

                    workSheetSocial.Cells["A73"].Value = "Total households";
                    workSheetSocial.Cells["B73"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0150E);
                    workSheetSocial.Cells["C73"].Value = "100%";
                    workSheetSocial.Cells["D73"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0150E);
                    workSheetSocial.Cells["E73"].Value = "100%";
                    workSheetSocial.Cells["F73"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0150E - data.NewYorkCity.DP02_0150E));
                    workSheetSocial.Cells["G73"].Value = "0.0";

                    workSheetSocial.Cells["A74"].Value = "With a computer";
                    workSheetSocial.Cells["B74"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0151E);
                    workSheetSocial.Cells["C74"].Value = data.SelectedArea.DP02_0151E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0151E.Value) / data.SelectedArea.DP02_0150E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D74"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0151E);
                    workSheetSocial.Cells["E74"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0151E.Value) / data.NewYorkCity.DP02_0150E.Value, 1) + "%";
                    workSheetSocial.Cells["F74"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0151E - data.NewYorkCity.DP02_0151E));
                    workSheetSocial.Cells["G74"].Value = data.SelectedArea.DP02_0151E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0151E.Value) / data.SelectedArea.DP02_0150E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0151E.Value) / data.NewYorkCity.DP02_0150E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells["A75"].Value = "With a broadband Internet subscription";
                    workSheetSocial.Cells["B75"].Value = String.Format("{0:n0}", data.SelectedArea.DP02_0152E);
                    workSheetSocial.Cells["C75"].Value = data.SelectedArea.DP02_0152E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP02_0152E.Value) / data.SelectedArea.DP02_0150E.Value, 1) + "%" : "";
                    workSheetSocial.Cells["D75"].Value = String.Format("{0:n0}", data.NewYorkCity.DP02_0152E);
                    workSheetSocial.Cells["E75"].Value = Math.Round((double)(100 * data.NewYorkCity.DP02_0152E.Value) / data.NewYorkCity.DP02_0150E.Value, 1) + "%";
                    workSheetSocial.Cells["F75"].Value = String.Format("{0:n0}", (data.SelectedArea.DP02_0152E - data.NewYorkCity.DP02_0152E));
                    workSheetSocial.Cells["G75"].Value = data.SelectedArea.DP02_0152E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP02_0152E.Value) / data.SelectedArea.DP02_0150E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP02_0152E.Value) / data.NewYorkCity.DP02_0150E.Value, 1), 1).ToString() : "";

                    workSheetSocial.Cells[workSheetSocial.Dimension.Address].AutoFitColumns();
                    //Header
                    SetHeaderStyle(workSheetSocial, "A1");
                    SetHeaderStyle(workSheetSocial, "A13");
                    SetHeaderStyle(workSheetSocial, "A29");
                    SetHeaderStyle(workSheetSocial, "A39");
                    SetHeaderStyle(workSheetSocial, "A50");
                    SetHeaderStyle(workSheetSocial, "A59");
                    SetHeaderStyle(workSheetSocial, "A70");
                    #endregion Social
                    #region Economic
                    ExcelWorksheet workSheetEconomic = xlPackage.Workbook.Worksheets.Add("Economic");

                    workSheetEconomic.Cells["A1:G1"].Merge = true;
                    workSheetEconomic.Cells["A1"].Value = "Employment Status";
                    workSheetEconomic.Cells["A2:A3"].Merge = true;
                    workSheetEconomic.Cells["B2:C2"].Merge = true;
                    workSheetEconomic.Cells["B2"].Value = "Selected Area";
                    workSheetEconomic.Cells["D2:E2"].Merge = true;
                    workSheetEconomic.Cells["D2"].Value = "New York City";
                    workSheetEconomic.Cells["F2:G2"].Merge = true;
                    workSheetEconomic.Cells["F2"].Value = "Difference";

                    workSheetEconomic.Cells["B3"].Value = "Number";
                    workSheetEconomic.Cells["C3"].Value = "Percent";
                    workSheetEconomic.Cells["D3"].Value = "Number";
                    workSheetEconomic.Cells["E3"].Value = "Percent";
                    workSheetEconomic.Cells["F3"].Value = "Number";
                    workSheetEconomic.Cells["G3"].Value = "Pctg. Pt.";

                    workSheetEconomic.Cells["A4"].Value = "Population 16 years and over";
                    workSheetEconomic.Cells["B4"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0001E);
                    workSheetEconomic.Cells["C4"].Value = "100%";
                    workSheetEconomic.Cells["D4"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0001E);
                    workSheetEconomic.Cells["E4"].Value = "100%";
                    workSheetEconomic.Cells["F4"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0001E - data.NewYorkCity.DP03_0001E));
                    workSheetEconomic.Cells["G4"].Value = "0.0";

                    workSheetEconomic.Cells["A5"].Value = "In labor force - Civilian labor force";
                    workSheetEconomic.Cells["B5"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0003E);
                    workSheetEconomic.Cells["C5"].Value = data.SelectedArea.DP03_0003E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0003E.Value) / data.SelectedArea.DP03_0001E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D5"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0003E);
                    workSheetEconomic.Cells["E5"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0003E.Value) / data.NewYorkCity.DP03_0001E.Value, 1) + "%";
                    workSheetEconomic.Cells["F5"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0003E - data.NewYorkCity.DP03_0003E));
                    workSheetEconomic.Cells["G5"].Value = data.SelectedArea.DP03_0003E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0003E.Value) / data.SelectedArea.DP03_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0003E.Value) / data.NewYorkCity.DP03_0001E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A6"].Value = "Employed";
                    workSheetEconomic.Cells["B6"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0004E);
                    workSheetEconomic.Cells["C6"].Value = data.SelectedArea.DP03_0004E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0004E.Value) / data.SelectedArea.DP03_0001E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D6"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0004E);
                    workSheetEconomic.Cells["E6"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0004E.Value) / data.NewYorkCity.DP03_0001E.Value, 1) + "%";
                    workSheetEconomic.Cells["F6"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0004E - data.NewYorkCity.DP03_0004E));
                    workSheetEconomic.Cells["G6"].Value = data.SelectedArea.DP03_0004E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0004E.Value) / data.SelectedArea.DP03_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0004E.Value) / data.NewYorkCity.DP03_0001E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A7"].Value = "Unemployed";
                    workSheetEconomic.Cells["B7"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0005E);
                    workSheetEconomic.Cells["C7"].Value = data.SelectedArea.DP03_0005E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0005E.Value) / data.SelectedArea.DP03_0001E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D7"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0005E);
                    workSheetEconomic.Cells["E7"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0005E.Value) / data.NewYorkCity.DP03_0001E.Value, 1) + "%";
                    workSheetEconomic.Cells["F7"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0005E - data.NewYorkCity.DP03_0005E));
                    workSheetEconomic.Cells["G7"].Value = data.SelectedArea.DP03_0005E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0005E.Value) / data.SelectedArea.DP03_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0005E.Value) / data.NewYorkCity.DP03_0001E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A8"].Value = "Not in labor force";
                    workSheetEconomic.Cells["B8"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0007E);
                    workSheetEconomic.Cells["C8"].Value = data.SelectedArea.DP03_0007E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0007E.Value) / data.SelectedArea.DP03_0001E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D8"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0007E);
                    workSheetEconomic.Cells["E8"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0007E.Value) / data.NewYorkCity.DP03_0001E.Value, 1) + "%";
                    workSheetEconomic.Cells["F8"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0007E - data.NewYorkCity.DP03_0007E));
                    workSheetEconomic.Cells["G8"].Value = data.SelectedArea.DP03_0007E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0007E.Value) / data.SelectedArea.DP03_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0007E.Value) / data.NewYorkCity.DP03_0001E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A9"].Value = "Unemployment Rate";
                    workSheetEconomic.Cells["B9"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0009E);
                    workSheetEconomic.Cells["C9"].Value = data.SelectedArea.DP03_0009E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0009E.Value) / data.SelectedArea.DP03_0001E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D9"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0009E);
                    workSheetEconomic.Cells["E9"].Value = data.NewYorkCity.DP03_0009E.HasValue ? Math.Round((double)(100 * data.NewYorkCity.DP03_0009E.Value) / data.NewYorkCity.DP03_0001E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["F9"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0009E - data.NewYorkCity.DP03_0009E));
                    workSheetEconomic.Cells["G9"].Value = data.SelectedArea.DP03_0009E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0009E.Value) / data.SelectedArea.DP03_0001E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0009E.Value) / data.NewYorkCity.DP03_0001E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A10"].Value = "Females 16 years and over";
                    workSheetEconomic.Cells["B10"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0010E);
                    workSheetEconomic.Cells["C10"].Value = "100%";
                    workSheetEconomic.Cells["D10"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0010E);
                    workSheetEconomic.Cells["E10"].Value = "100%";
                    workSheetEconomic.Cells["F10"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0010E - data.NewYorkCity.DP03_0010E));
                    workSheetEconomic.Cells["G10"].Value = "0.0";

                    workSheetEconomic.Cells["A11"].Value = "In labor force - Civilian labor force";
                    workSheetEconomic.Cells["B11"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0012E);
                    workSheetEconomic.Cells["C11"].Value = data.SelectedArea.DP03_0012E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0012E.Value) / data.SelectedArea.DP03_0010E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D11"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0012E);
                    workSheetEconomic.Cells["E11"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0012E.Value) / data.NewYorkCity.DP03_0010E.Value, 1) + "%";
                    workSheetEconomic.Cells["F11"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0012E - data.NewYorkCity.DP03_0012E));
                    workSheetEconomic.Cells["G11"].Value = data.SelectedArea.DP03_0012E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0012E.Value) / data.SelectedArea.DP03_0010E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0012E.Value) / data.NewYorkCity.DP03_0010E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A12"].Value = "Employed";
                    workSheetEconomic.Cells["B12"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0013E);
                    workSheetEconomic.Cells["C12"].Value = data.SelectedArea.DP03_0013E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0013E.Value) / data.SelectedArea.DP03_0010E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D12"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0013E);
                    workSheetEconomic.Cells["E12"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0013E.Value) / data.NewYorkCity.DP03_0010E.Value, 1) + "%";
                    workSheetEconomic.Cells["F12"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0013E - data.NewYorkCity.DP03_0013E));
                    workSheetEconomic.Cells["G12"].Value = data.SelectedArea.DP03_0013E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0013E.Value) / data.SelectedArea.DP03_0010E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0013E.Value) / data.NewYorkCity.DP03_0010E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A14:G14"].Merge = true;
                    workSheetEconomic.Cells["A14"].Value = "Income and Benefits";
                    workSheetEconomic.Cells["A15:A16"].Merge = true;
                    workSheetEconomic.Cells["B15:C15"].Merge = true;
                    workSheetEconomic.Cells["B15"].Value = "Selected Area";
                    workSheetEconomic.Cells["D15:E15"].Merge = true;
                    workSheetEconomic.Cells["D15"].Value = "New York City";
                    workSheetEconomic.Cells["F15:G15"].Merge = true;
                    workSheetEconomic.Cells["F15"].Value = "Difference";

                    workSheetEconomic.Cells["B16"].Value = "Number";
                    workSheetEconomic.Cells["C16"].Value = "Percent";
                    workSheetEconomic.Cells["D16"].Value = "Number";
                    workSheetEconomic.Cells["E16"].Value = "Percent";
                    workSheetEconomic.Cells["F16"].Value = "Number";
                    workSheetEconomic.Cells["G16"].Value = "Pctg. Pt.";

                    workSheetEconomic.Cells["A17"].Value = "Total households";
                    workSheetEconomic.Cells["B17"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0051E);
                    workSheetEconomic.Cells["C17"].Value = "100%";
                    workSheetEconomic.Cells["D17"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0051E);
                    workSheetEconomic.Cells["E17"].Value = "100%";
                    workSheetEconomic.Cells["F17"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0051E - data.NewYorkCity.DP03_0051E));
                    workSheetEconomic.Cells["G17"].Value = "0.0";

                    workSheetEconomic.Cells["A18"].Value = "Less than $10,000";
                    workSheetEconomic.Cells["B18"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0052E);
                    workSheetEconomic.Cells["C18"].Value = data.SelectedArea.DP03_0052E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0052E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D18"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0052E);
                    workSheetEconomic.Cells["E18"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0052E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F18"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0052E - data.NewYorkCity.DP03_0052E));
                    workSheetEconomic.Cells["G18"].Value = data.SelectedArea.DP03_0052E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0052E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0052E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A19"].Value = "$10,000 to $14,999";
                    workSheetEconomic.Cells["B19"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0053E);
                    workSheetEconomic.Cells["C19"].Value = data.SelectedArea.DP03_0053E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0053E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D19"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0053E);
                    workSheetEconomic.Cells["E19"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0053E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F19"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0053E - data.NewYorkCity.DP03_0053E));
                    workSheetEconomic.Cells["G19"].Value = data.SelectedArea.DP03_0053E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0053E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0053E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A20"].Value = "$15,000 to $24,999";
                    workSheetEconomic.Cells["B20"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0054E);
                    workSheetEconomic.Cells["C20"].Value = data.SelectedArea.DP03_0054E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0054E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D20"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0054E);
                    workSheetEconomic.Cells["E20"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0054E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F20"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0054E - data.NewYorkCity.DP03_0054E));
                    workSheetEconomic.Cells["G20"].Value = data.SelectedArea.DP03_0054E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0054E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0054E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A21"].Value = "$25,000 to $34,999";
                    workSheetEconomic.Cells["B21"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0055E);
                    workSheetEconomic.Cells["C21"].Value = data.SelectedArea.DP03_0055E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0055E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D21"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0055E);
                    workSheetEconomic.Cells["E21"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0055E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F21"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0055E - data.NewYorkCity.DP03_0055E));
                    workSheetEconomic.Cells["G21"].Value = data.SelectedArea.DP03_0055E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0055E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0055E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A22"].Value = "$35,000 to $49,999";
                    workSheetEconomic.Cells["B22"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0056E);
                    workSheetEconomic.Cells["C22"].Value = data.SelectedArea.DP03_0056E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0056E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D22"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0056E);
                    workSheetEconomic.Cells["E22"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0056E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F22"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0056E - data.NewYorkCity.DP03_0056E));
                    workSheetEconomic.Cells["G22"].Value = data.SelectedArea.DP03_0056E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0056E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0056E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A23"].Value = "$50,000 to $74,999";
                    workSheetEconomic.Cells["B23"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0057E);
                    workSheetEconomic.Cells["C23"].Value = data.SelectedArea.DP03_0057E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0057E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D23"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0057E);
                    workSheetEconomic.Cells["E23"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0057E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F23"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0057E - data.NewYorkCity.DP03_0057E));
                    workSheetEconomic.Cells["G23"].Value = data.SelectedArea.DP03_0057E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0057E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0057E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A24"].Value = "$75,000 to $99,999";
                    workSheetEconomic.Cells["B24"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0058E);
                    workSheetEconomic.Cells["C24"].Value = data.SelectedArea.DP03_0058E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0058E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D24"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0058E);
                    workSheetEconomic.Cells["E24"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0058E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F24"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0058E - data.NewYorkCity.DP03_0058E));
                    workSheetEconomic.Cells["G24"].Value = data.SelectedArea.DP03_0058E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0058E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0058E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A25"].Value = "$100,000 to $149,999";
                    workSheetEconomic.Cells["B25"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0059E);
                    workSheetEconomic.Cells["C25"].Value = data.SelectedArea.DP03_0059E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0059E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D25"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0059E);
                    workSheetEconomic.Cells["E25"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0059E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F25"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0059E - data.NewYorkCity.DP03_0059E));
                    workSheetEconomic.Cells["G25"].Value = data.SelectedArea.DP03_0059E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0059E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0059E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A26"].Value = "$150,000 to $199,999";
                    workSheetEconomic.Cells["B26"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0060E);
                    workSheetEconomic.Cells["C26"].Value = data.SelectedArea.DP03_0060E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0060E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D26"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0060E);
                    workSheetEconomic.Cells["E26"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0060E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F26"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0060E - data.NewYorkCity.DP03_0060E));
                    workSheetEconomic.Cells["G26"].Value = data.SelectedArea.DP03_0060E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0060E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0060E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A27"].Value = "$200,000 or more";
                    workSheetEconomic.Cells["B27"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0061E);
                    workSheetEconomic.Cells["C27"].Value = data.SelectedArea.DP03_0061E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0061E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D27"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0061E);
                    workSheetEconomic.Cells["E27"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0061E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F27"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0061E - data.NewYorkCity.DP03_0061E));
                    workSheetEconomic.Cells["G27"].Value = data.SelectedArea.DP03_0061E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0061E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0061E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A28"].Value = "Mean household income (dollars)";
                    workSheetEconomic.Cells["B28"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0063E);
                    workSheetEconomic.Cells["C28"].Value = "";
                    workSheetEconomic.Cells["D28"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0063E);
                    workSheetEconomic.Cells["E28"].Value = "";
                    workSheetEconomic.Cells["F28"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0063E - data.NewYorkCity.DP03_0063E));
                    workSheetEconomic.Cells["G28"].Value = "";

                    workSheetEconomic.Cells["A29"].Value = "With Social Security";
                    workSheetEconomic.Cells["B29"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0066E);
                    workSheetEconomic.Cells["C29"].Value = data.SelectedArea.DP03_0066E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0066E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D29"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0066E);
                    workSheetEconomic.Cells["E29"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0066E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F29"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0066E - data.NewYorkCity.DP03_0066E));
                    workSheetEconomic.Cells["G29"].Value = data.SelectedArea.DP03_0066E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0066E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0066E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A30"].Value = "With retirement income";
                    workSheetEconomic.Cells["B30"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0068E);
                    workSheetEconomic.Cells["C30"].Value = data.SelectedArea.DP03_0068E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0068E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D30"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0068E);
                    workSheetEconomic.Cells["E30"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0068E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F30"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0068E - data.NewYorkCity.DP03_0068E));
                    workSheetEconomic.Cells["G30"].Value = data.SelectedArea.DP03_0068E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0068E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0068E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A31"].Value = "Mean retirement income (dollars)";
                    workSheetEconomic.Cells["B31"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0069E);
                    workSheetEconomic.Cells["C31"].Value = "";
                    workSheetEconomic.Cells["D31"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0069E);
                    workSheetEconomic.Cells["E31"].Value = "";
                    workSheetEconomic.Cells["F31"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0069E - data.NewYorkCity.DP03_0069E));
                    workSheetEconomic.Cells["G31"].Value = "";

                    workSheetEconomic.Cells["A32"].Value = "With Supplemental Security Income";
                    workSheetEconomic.Cells["B32"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0070E);
                    workSheetEconomic.Cells["C32"].Value = data.SelectedArea.DP03_0070E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0070E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D32"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0070E);
                    workSheetEconomic.Cells["E32"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0070E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F32"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0070E - data.NewYorkCity.DP03_0070E));
                    workSheetEconomic.Cells["G32"].Value = data.SelectedArea.DP03_0070E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0070E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0070E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A33"].Value = "Mean Supplemental Security Income (dollars)";
                    workSheetEconomic.Cells["B33"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0071E);
                    workSheetEconomic.Cells["C33"].Value = "";
                    workSheetEconomic.Cells["D33"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0071E);
                    workSheetEconomic.Cells["E33"].Value = "";
                    workSheetEconomic.Cells["F33"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0071E - data.NewYorkCity.DP03_0071E));
                    workSheetEconomic.Cells["G33"].Value = "";

                    workSheetEconomic.Cells["A34"].Value = "With cash public assistance income";
                    workSheetEconomic.Cells["B34"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0072E);
                    workSheetEconomic.Cells["C34"].Value = data.SelectedArea.DP03_0072E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0072E.Value) / data.SelectedArea.DP03_0051E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D34"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0072E);
                    workSheetEconomic.Cells["E34"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0072E.Value) / data.NewYorkCity.DP03_0051E.Value, 1) + "%";
                    workSheetEconomic.Cells["F34"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0072E - data.NewYorkCity.DP03_0072E));
                    workSheetEconomic.Cells["G34"].Value = data.SelectedArea.DP03_0072E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0072E.Value) / data.SelectedArea.DP03_0051E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0072E.Value) / data.NewYorkCity.DP03_0051E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A35"].Value = "Mean cash public assistance income (dollars)";
                    workSheetEconomic.Cells["B35"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0073E);
                    workSheetEconomic.Cells["C35"].Value = "";
                    workSheetEconomic.Cells["D35"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0073E);
                    workSheetEconomic.Cells["E35"].Value = "";
                    workSheetEconomic.Cells["F35"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0073E - data.NewYorkCity.DP03_0073E));
                    workSheetEconomic.Cells["G35"].Value = "";

                    workSheetEconomic.Cells["A37:G37"].Merge = true;
                    workSheetEconomic.Cells["A37"].Value = "Health Insurance Coverage";
                    workSheetEconomic.Cells["A38:A39"].Merge = true;
                    workSheetEconomic.Cells["B38:C38"].Merge = true;
                    workSheetEconomic.Cells["B38"].Value = "Selected Area";
                    workSheetEconomic.Cells["D38:E38"].Merge = true;
                    workSheetEconomic.Cells["D38"].Value = "New York City";
                    workSheetEconomic.Cells["F38:G38"].Merge = true;
                    workSheetEconomic.Cells["F38"].Value = "Difference";

                    workSheetEconomic.Cells["B39"].Value = "Number";
                    workSheetEconomic.Cells["C39"].Value = "Percent";
                    workSheetEconomic.Cells["D39"].Value = "Number";
                    workSheetEconomic.Cells["E39"].Value = "Percent";
                    workSheetEconomic.Cells["F39"].Value = "Number";
                    workSheetEconomic.Cells["G39"].Value = "Pctg. Pt.";

                    workSheetEconomic.Cells["A40"].Value = "Civilian noninstitutionalized population";
                    workSheetEconomic.Cells["B40"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0095E);
                    workSheetEconomic.Cells["C40"].Value = "100%";
                    workSheetEconomic.Cells["D40"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0095E);
                    workSheetEconomic.Cells["E40"].Value = "100%";
                    workSheetEconomic.Cells["F40"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0095E - data.NewYorkCity.DP03_0095E));
                    workSheetEconomic.Cells["G40"].Value = "0.0";

                    workSheetEconomic.Cells["A41"].Value = "With health insurance coverage";
                    workSheetEconomic.Cells["B41"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0096E);
                    workSheetEconomic.Cells["C41"].Value = data.SelectedArea.DP03_0096E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0096E.Value) / data.SelectedArea.DP03_0095E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D41"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0096E);
                    workSheetEconomic.Cells["E41"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0096E.Value) / data.NewYorkCity.DP03_0095E.Value, 1) + "%";
                    workSheetEconomic.Cells["F41"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0096E - data.NewYorkCity.DP03_0096E));
                    workSheetEconomic.Cells["G41"].Value = data.SelectedArea.DP03_0096E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0096E.Value) / data.SelectedArea.DP03_0095E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0096E.Value) / data.NewYorkCity.DP03_0095E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A42"].Value = "With private health insurance";
                    workSheetEconomic.Cells["B42"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0097E);
                    workSheetEconomic.Cells["C42"].Value = data.SelectedArea.DP03_0097E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0097E.Value) / data.SelectedArea.DP03_0095E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D42"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0097E);
                    workSheetEconomic.Cells["E42"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0097E.Value) / data.NewYorkCity.DP03_0095E.Value, 1) + "%";
                    workSheetEconomic.Cells["F42"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0097E - data.NewYorkCity.DP03_0097E));
                    workSheetEconomic.Cells["G42"].Value = data.SelectedArea.DP03_0097E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0097E.Value) / data.SelectedArea.DP03_0095E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0097E.Value) / data.NewYorkCity.DP03_0095E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A43"].Value = "With public coverage";
                    workSheetEconomic.Cells["B43"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0098E);
                    workSheetEconomic.Cells["C43"].Value = data.SelectedArea.DP03_0098E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0098E.Value) / data.SelectedArea.DP03_0095E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D43"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0098E);
                    workSheetEconomic.Cells["E43"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0098E.Value) / data.NewYorkCity.DP03_0095E.Value, 1) + "%";
                    workSheetEconomic.Cells["F43"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0098E - data.NewYorkCity.DP03_0098E));
                    workSheetEconomic.Cells["G43"].Value = data.SelectedArea.DP03_0098E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0098E.Value) / data.SelectedArea.DP03_0095E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0098E.Value) / data.NewYorkCity.DP03_0095E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A44"].Value = "No health insurance coverage";
                    workSheetEconomic.Cells["B44"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0099E);
                    workSheetEconomic.Cells["C44"].Value = data.SelectedArea.DP03_0099E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0099E.Value) / data.SelectedArea.DP03_0095E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D44"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0099E);
                    workSheetEconomic.Cells["E44"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0099E.Value) / data.NewYorkCity.DP03_0095E.Value, 1) + "%";
                    workSheetEconomic.Cells["F44"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0099E - data.NewYorkCity.DP03_0099E));
                    workSheetEconomic.Cells["G44"].Value = data.SelectedArea.DP03_0099E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0099E.Value) / data.SelectedArea.DP03_0095E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0099E.Value) / data.NewYorkCity.DP03_0095E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A45"].Value = "Civilian noninstitutionalized population 19 to 64 years";
                    workSheetEconomic.Cells["B45"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0102E);
                    workSheetEconomic.Cells["C45"].Value = "";
                    workSheetEconomic.Cells["D45"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0102E);
                    workSheetEconomic.Cells["E45"].Value = "";
                    workSheetEconomic.Cells["F45"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0102E - data.NewYorkCity.DP03_0102E));
                    workSheetEconomic.Cells["G45"].Value = "";

                    workSheetEconomic.Cells["A46"].Value = "In labor force";
                    workSheetEconomic.Cells["B46"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0103E);
                    workSheetEconomic.Cells["C46"].Value = "";
                    workSheetEconomic.Cells["D46"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0103E);
                    workSheetEconomic.Cells["E46"].Value = "";
                    workSheetEconomic.Cells["F46"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0103E - data.NewYorkCity.DP03_0103E));
                    workSheetEconomic.Cells["G46"].Value = "";

                    workSheetEconomic.Cells["A47"].Value = "Employed";
                    workSheetEconomic.Cells["B47"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0104E);
                    workSheetEconomic.Cells["C47"].Value = "100%";
                    workSheetEconomic.Cells["D47"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0104E);
                    workSheetEconomic.Cells["E47"].Value = "100%";
                    workSheetEconomic.Cells["F47"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0104E - data.NewYorkCity.DP03_0104E));
                    workSheetEconomic.Cells["G47"].Value = "0.0";

                    workSheetEconomic.Cells["A48"].Value = "With health insurance coverage";
                    workSheetEconomic.Cells["B48"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0105E);
                    workSheetEconomic.Cells["C48"].Value = data.SelectedArea.DP03_0105E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0105E.Value) / data.SelectedArea.DP03_0104E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D48"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0105E);
                    workSheetEconomic.Cells["E48"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0105E.Value) / data.NewYorkCity.DP03_0104E.Value, 1) + "%";
                    workSheetEconomic.Cells["F48"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0105E - data.NewYorkCity.DP03_0105E));
                    workSheetEconomic.Cells["G48"].Value = data.SelectedArea.DP03_0105E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0105E.Value) / data.SelectedArea.DP03_0104E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0105E.Value) / data.NewYorkCity.DP03_0104E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A49"].Value = "With private health insurance";
                    workSheetEconomic.Cells["B49"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0106E);
                    workSheetEconomic.Cells["C49"].Value = data.SelectedArea.DP03_0106E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0106E.Value) / data.SelectedArea.DP03_0104E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D49"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0106E);
                    workSheetEconomic.Cells["E49"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0106E.Value) / data.NewYorkCity.DP03_0104E.Value, 1) + "%";
                    workSheetEconomic.Cells["F49"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0106E - data.NewYorkCity.DP03_0106E));
                    workSheetEconomic.Cells["G49"].Value = data.SelectedArea.DP03_0106E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0106E.Value) / data.SelectedArea.DP03_0104E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0106E.Value) / data.NewYorkCity.DP03_0104E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A50"].Value = "With public coverage";
                    workSheetEconomic.Cells["B50"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0107E);
                    workSheetEconomic.Cells["C50"].Value = data.SelectedArea.DP03_0107E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0107E.Value) / data.SelectedArea.DP03_0104E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D50"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0107E);
                    workSheetEconomic.Cells["E50"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0107E.Value) / data.NewYorkCity.DP03_0104E.Value, 1) + "%";
                    workSheetEconomic.Cells["F50"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0107E - data.NewYorkCity.DP03_0107E));
                    workSheetEconomic.Cells["G50"].Value = data.SelectedArea.DP03_0107E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0107E.Value) / data.SelectedArea.DP03_0104E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0107E.Value) / data.NewYorkCity.DP03_0104E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A51"].Value = "No health insurance coverage";
                    workSheetEconomic.Cells["B51"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0108E);
                    workSheetEconomic.Cells["C51"].Value = data.SelectedArea.DP03_0108E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0108E.Value) / data.SelectedArea.DP03_0104E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D51"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0108E);
                    workSheetEconomic.Cells["E51"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0108E.Value) / data.NewYorkCity.DP03_0104E.Value, 1) + "%";
                    workSheetEconomic.Cells["F51"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0108E - data.NewYorkCity.DP03_0108E));
                    workSheetEconomic.Cells["G51"].Value = data.SelectedArea.DP03_0108E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0108E.Value) / data.SelectedArea.DP03_0104E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0108E.Value) / data.NewYorkCity.DP03_0104E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A52"].Value = "Unemployed";
                    workSheetEconomic.Cells["B52"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0109E);
                    workSheetEconomic.Cells["C52"].Value = "100%";
                    workSheetEconomic.Cells["D52"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0109E);
                    workSheetEconomic.Cells["E52"].Value = "100%";
                    workSheetEconomic.Cells["F52"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0109E - data.NewYorkCity.DP03_0109E));
                    workSheetEconomic.Cells["G52"].Value = "0.0";

                    workSheetEconomic.Cells["A53"].Value = "With health insurance coverage";
                    workSheetEconomic.Cells["B53"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0110E);
                    workSheetEconomic.Cells["C53"].Value = data.SelectedArea.DP03_0110E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0110E.Value) / data.SelectedArea.DP03_0109E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D53"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0110E);
                    workSheetEconomic.Cells["E53"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0110E.Value) / data.NewYorkCity.DP03_0109E.Value, 1) + "%";
                    workSheetEconomic.Cells["F53"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0110E - data.NewYorkCity.DP03_0110E));
                    workSheetEconomic.Cells["G53"].Value = data.SelectedArea.DP03_0110E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0110E.Value) / data.SelectedArea.DP03_0109E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0110E.Value) / data.NewYorkCity.DP03_0109E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A54"].Value = "With private health insurance";
                    workSheetEconomic.Cells["B54"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0111E);
                    workSheetEconomic.Cells["C54"].Value = data.SelectedArea.DP03_0111E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0111E.Value) / data.SelectedArea.DP03_0109E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D54"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0111E);
                    workSheetEconomic.Cells["E54"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0111E.Value) / data.NewYorkCity.DP03_0109E.Value, 1) + "%";
                    workSheetEconomic.Cells["F54"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0111E - data.NewYorkCity.DP03_0111E));
                    workSheetEconomic.Cells["G54"].Value = data.SelectedArea.DP03_0111E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0111E.Value) / data.SelectedArea.DP03_0109E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0111E.Value) / data.NewYorkCity.DP03_0109E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A55"].Value = "With public coverage";
                    workSheetEconomic.Cells["B55"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0112E);
                    workSheetEconomic.Cells["C55"].Value = data.SelectedArea.DP03_0112E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0112E.Value) / data.SelectedArea.DP03_0109E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D55"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0112E);
                    workSheetEconomic.Cells["E55"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0112E.Value) / data.NewYorkCity.DP03_0109E.Value, 1) + "%";
                    workSheetEconomic.Cells["F55"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0112E - data.NewYorkCity.DP03_0112E));
                    workSheetEconomic.Cells["G55"].Value = data.SelectedArea.DP03_0112E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0112E.Value) / data.SelectedArea.DP03_0109E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0112E.Value) / data.NewYorkCity.DP03_0109E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells["A56"].Value = "No health insurance coverage";
                    workSheetEconomic.Cells["B56"].Value = String.Format("{0:n0}", data.SelectedArea.DP03_0113E);
                    workSheetEconomic.Cells["C56"].Value = data.SelectedArea.DP03_0113E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP03_0113E.Value) / data.SelectedArea.DP03_0109E.Value, 1) + "%" : "";
                    workSheetEconomic.Cells["D56"].Value = String.Format("{0:n0}", data.NewYorkCity.DP03_0113E);
                    workSheetEconomic.Cells["E56"].Value = Math.Round((double)(100 * data.NewYorkCity.DP03_0113E.Value) / data.NewYorkCity.DP03_0109E.Value, 1) + "%";
                    workSheetEconomic.Cells["F56"].Value = String.Format("{0:n0}", (data.SelectedArea.DP03_0113E - data.NewYorkCity.DP03_0113E));
                    workSheetEconomic.Cells["G56"].Value = data.SelectedArea.DP03_0113E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP03_0113E.Value) / data.SelectedArea.DP03_0109E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP03_0113E.Value) / data.NewYorkCity.DP03_0109E.Value, 1), 1).ToString() : "";

                    workSheetEconomic.Cells[workSheetDemographics.Dimension.Address].AutoFitColumns();
                    SetHeaderStyle(workSheetEconomic, "A1");
                    SetHeaderStyle(workSheetEconomic, "A14");
                    SetHeaderStyle(workSheetEconomic, "A37");
                    #endregion Economic
                    #region Housing
                    ExcelWorksheet workSheetHousing = xlPackage.Workbook.Worksheets.Add("Housing");

                    workSheetHousing.Cells["A1:G1"].Merge = true;
                    workSheetHousing.Cells["A1"].Value = "Units in Structure";
                    workSheetHousing.Cells["A2:A3"].Merge = true;
                    workSheetHousing.Cells["B2:C2"].Merge = true;
                    workSheetHousing.Cells["B2"].Value = "Selected Area";
                    workSheetHousing.Cells["D2:E2"].Merge = true;
                    workSheetHousing.Cells["D2"].Value = "New York City";
                    workSheetHousing.Cells["F2:G2"].Merge = true;
                    workSheetHousing.Cells["F2"].Value = "Difference";

                    workSheetHousing.Cells["B3"].Value = "Number";
                    workSheetHousing.Cells["C3"].Value = "Percent";
                    workSheetHousing.Cells["D3"].Value = "Number";
                    workSheetHousing.Cells["E3"].Value = "Percent";
                    workSheetHousing.Cells["F3"].Value = "Number";
                    workSheetHousing.Cells["G3"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A4"].Value = "Total housing units";
                    workSheetHousing.Cells["B4"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0006E);
                    workSheetHousing.Cells["C4"].Value = "100%";
                    workSheetHousing.Cells["D4"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0006E);
                    workSheetHousing.Cells["E4"].Value = "100%";
                    workSheetHousing.Cells["F4"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0006E - data.NewYorkCity.DP04_0006E));
                    workSheetHousing.Cells["G4"].Value = "0.0";

                    workSheetHousing.Cells["A5"].Value = "1-unit, detached";
                    workSheetHousing.Cells["B5"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0007E);
                    workSheetHousing.Cells["C5"].Value = data.SelectedArea.DP04_0007E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0007E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D5"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0007E);
                    workSheetHousing.Cells["E5"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0007E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F5"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0007E - data.NewYorkCity.DP04_0007E));
                    workSheetHousing.Cells["G5"].Value = data.SelectedArea.DP04_0007E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0007E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0007E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A6"].Value = "1-unit, attached";
                    workSheetHousing.Cells["B6"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0008E);
                    workSheetHousing.Cells["C6"].Value = data.SelectedArea.DP04_0008E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0008E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D6"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0008E);
                    workSheetHousing.Cells["E6"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0008E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F6"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0008E - data.NewYorkCity.DP04_0008E));
                    workSheetHousing.Cells["G6"].Value = data.SelectedArea.DP04_0008E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0008E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0008E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A7"].Value = "2 units";
                    workSheetHousing.Cells["B7"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0009E);
                    workSheetHousing.Cells["C7"].Value = data.SelectedArea.DP04_0009E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0009E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D7"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0009E);
                    workSheetHousing.Cells["E7"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0009E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F7"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0009E - data.NewYorkCity.DP04_0009E));
                    workSheetHousing.Cells["G7"].Value = data.SelectedArea.DP04_0009E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0009E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0009E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A8"].Value = "3 or 4 units";
                    workSheetHousing.Cells["B8"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0010E);
                    workSheetHousing.Cells["C8"].Value = data.SelectedArea.DP04_0010E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0010E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D8"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0010E);
                    workSheetHousing.Cells["E8"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0010E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F8"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0010E - data.NewYorkCity.DP04_0010E));
                    workSheetHousing.Cells["G8"].Value = data.SelectedArea.DP04_0010E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0010E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0010E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A9"].Value = "5 to 9 units";
                    workSheetHousing.Cells["B9"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0011E);
                    workSheetHousing.Cells["C9"].Value = data.SelectedArea.DP04_0011E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0011E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D9"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0011E);
                    workSheetHousing.Cells["E9"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0011E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F9"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0011E - data.NewYorkCity.DP04_0011E));
                    workSheetHousing.Cells["G9"].Value = data.SelectedArea.DP04_0011E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0011E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0011E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A10"].Value = "10 to 19 units";
                    workSheetHousing.Cells["B10"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0012E);
                    workSheetHousing.Cells["C10"].Value = data.SelectedArea.DP04_0012E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0012E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D10"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0012E);
                    workSheetHousing.Cells["E10"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0012E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F10"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0012E - data.NewYorkCity.DP04_0012E));
                    workSheetHousing.Cells["G10"].Value = data.SelectedArea.DP04_0012E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0012E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0012E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A11"].Value = "20 or more units";
                    workSheetHousing.Cells["B11"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0013E);
                    workSheetHousing.Cells["C11"].Value = data.SelectedArea.DP04_0013E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0013E.Value) / data.SelectedArea.DP04_0006E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D11"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0013E);
                    workSheetHousing.Cells["E11"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0013E.Value) / data.NewYorkCity.DP04_0006E.Value, 1) + "%";
                    workSheetHousing.Cells["F11"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0013E - data.NewYorkCity.DP04_0013E));
                    workSheetHousing.Cells["G11"].Value = data.SelectedArea.DP04_0013E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0013E.Value) / data.SelectedArea.DP04_0006E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0013E.Value) / data.NewYorkCity.DP04_0006E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A13:G13"].Merge = true;
                    workSheetHousing.Cells["A13"].Value = "Rooms";
                    workSheetHousing.Cells["A14:A15"].Merge = true;
                    workSheetHousing.Cells["B14:C14"].Merge = true;
                    workSheetHousing.Cells["B14"].Value = "Selected Area";
                    workSheetHousing.Cells["D14:E14"].Merge = true;
                    workSheetHousing.Cells["D14"].Value = "New York City";
                    workSheetHousing.Cells["F14:G14"].Merge = true;
                    workSheetHousing.Cells["F14"].Value = "Difference";

                    workSheetHousing.Cells["B15"].Value = "Number";
                    workSheetHousing.Cells["C15"].Value = "Percent";
                    workSheetHousing.Cells["D15"].Value = "Number";
                    workSheetHousing.Cells["E15"].Value = "Percent";
                    workSheetHousing.Cells["F15"].Value = "Number";
                    workSheetHousing.Cells["G15"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A16"].Value = "Total housing units";
                    workSheetHousing.Cells["B16"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0027E);
                    workSheetHousing.Cells["C16"].Value = "100%";
                    workSheetHousing.Cells["D16"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0027E);
                    workSheetHousing.Cells["E16"].Value = "100%";
                    workSheetHousing.Cells["F16"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0027E - data.NewYorkCity.DP04_0027E));
                    workSheetHousing.Cells["G16"].Value = "0.0";

                    workSheetHousing.Cells["A17"].Value = "1 room";
                    workSheetHousing.Cells["B17"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0028E);
                    workSheetHousing.Cells["C17"].Value = data.SelectedArea.DP04_0028E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0028E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D17"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0028E);
                    workSheetHousing.Cells["E17"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0028E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F17"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0028E - data.NewYorkCity.DP04_0028E));
                    workSheetHousing.Cells["G17"].Value = data.SelectedArea.DP04_0028E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0028E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0028E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A18"].Value = "2 rooms";
                    workSheetHousing.Cells["B18"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0029E);
                    workSheetHousing.Cells["C18"].Value = data.SelectedArea.DP04_0029E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0029E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D18"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0029E);
                    workSheetHousing.Cells["E18"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0029E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F18"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0029E - data.NewYorkCity.DP04_0029E));
                    workSheetHousing.Cells["G18"].Value = data.SelectedArea.DP04_0029E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0029E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0029E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A19"].Value = "3 rooms";
                    workSheetHousing.Cells["B19"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0030E);
                    workSheetHousing.Cells["C19"].Value = data.SelectedArea.DP04_0030E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0030E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D19"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0030E);
                    workSheetHousing.Cells["E19"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0030E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F19"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0030E - data.NewYorkCity.DP04_0030E));
                    workSheetHousing.Cells["G19"].Value = data.SelectedArea.DP04_0030E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0030E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0030E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A20"].Value = "4 rooms";
                    workSheetHousing.Cells["B20"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0031E);
                    workSheetHousing.Cells["C20"].Value = data.SelectedArea.DP04_0031E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0031E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D20"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0031E);
                    workSheetHousing.Cells["E20"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0031E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F20"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0031E - data.NewYorkCity.DP04_0031E));
                    workSheetHousing.Cells["G20"].Value = data.SelectedArea.DP04_0031E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0031E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0031E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A21"].Value = "5 rooms";
                    workSheetHousing.Cells["B21"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0032E);
                    workSheetHousing.Cells["C21"].Value = data.SelectedArea.DP04_0032E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0032E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D21"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0032E);
                    workSheetHousing.Cells["E21"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0032E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F21"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0032E - data.NewYorkCity.DP04_0032E));
                    workSheetHousing.Cells["G21"].Value = data.SelectedArea.DP04_0032E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0032E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0032E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A22"].Value = "6 rooms";
                    workSheetHousing.Cells["B22"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0033E);
                    workSheetHousing.Cells["C22"].Value = data.SelectedArea.DP04_0033E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0033E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D22"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0033E);
                    workSheetHousing.Cells["E22"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0033E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F22"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0033E - data.NewYorkCity.DP04_0033E));
                    workSheetHousing.Cells["G22"].Value = data.SelectedArea.DP04_0033E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0033E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0033E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A23"].Value = "7 rooms";
                    workSheetHousing.Cells["B23"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0034E);
                    workSheetHousing.Cells["C23"].Value = data.SelectedArea.DP04_0034E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0034E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D23"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0034E);
                    workSheetHousing.Cells["E23"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0034E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F23"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0034E - data.NewYorkCity.DP04_0034E));
                    workSheetHousing.Cells["G23"].Value = data.SelectedArea.DP04_0034E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0034E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0034E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A24"].Value = "8 rooms";
                    workSheetHousing.Cells["B24"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0035E);
                    workSheetHousing.Cells["C24"].Value = data.SelectedArea.DP04_0035E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0035E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D24"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0035E);
                    workSheetHousing.Cells["E24"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0035E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F24"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0035E - data.NewYorkCity.DP04_0035E));
                    workSheetHousing.Cells["G24"].Value = data.SelectedArea.DP04_0035E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0035E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0035E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A25"].Value = "9 rooms or more";
                    workSheetHousing.Cells["B25"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0036E);
                    workSheetHousing.Cells["C25"].Value = data.SelectedArea.DP04_0036E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0036E.Value) / data.SelectedArea.DP04_0027E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D25"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0036E);
                    workSheetHousing.Cells["E25"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0036E.Value) / data.NewYorkCity.DP04_0027E.Value, 1) + "%";
                    workSheetHousing.Cells["F25"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0036E - data.NewYorkCity.DP04_0036E));
                    workSheetHousing.Cells["G25"].Value = data.SelectedArea.DP04_0036E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0036E.Value) / data.SelectedArea.DP04_0027E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0036E.Value) / data.NewYorkCity.DP04_0027E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A26"].Value = "Median rooms";
                    workSheetHousing.Cells["B26"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0037E);
                    workSheetHousing.Cells["C26"].Value = "";
                    workSheetHousing.Cells["D26"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0037E);
                    workSheetHousing.Cells["E26"].Value = "";
                    workSheetHousing.Cells["F26"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0037E - data.NewYorkCity.DP04_0037E));
                    workSheetHousing.Cells["G26"].Value = "";

                    workSheetHousing.Cells["A28:G28"].Merge = true;
                    workSheetHousing.Cells["A28"].Value = "Bedrooms";
                    workSheetHousing.Cells["A29:A30"].Merge = true;
                    workSheetHousing.Cells["B29:C29"].Merge = true;
                    workSheetHousing.Cells["B29"].Value = "Selected Area";
                    workSheetHousing.Cells["D29:E29"].Merge = true;
                    workSheetHousing.Cells["D29"].Value = "New York City";
                    workSheetHousing.Cells["F29:G29"].Merge = true;
                    workSheetHousing.Cells["F29"].Value = "Difference";

                    workSheetHousing.Cells["B30"].Value = "Number";
                    workSheetHousing.Cells["C30"].Value = "Percent";
                    workSheetHousing.Cells["D30"].Value = "Number";
                    workSheetHousing.Cells["E30"].Value = "Percent";
                    workSheetHousing.Cells["F30"].Value = "Number";
                    workSheetHousing.Cells["G30"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A31"].Value = "Total housing units";
                    workSheetHousing.Cells["B31"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0038E);
                    workSheetHousing.Cells["C31"].Value = "100%";
                    workSheetHousing.Cells["D31"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0038E);
                    workSheetHousing.Cells["E31"].Value = "100%";
                    workSheetHousing.Cells["F31"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0038E - data.NewYorkCity.DP04_0038E));
                    workSheetHousing.Cells["G31"].Value = "0.0";

                    workSheetHousing.Cells["A32"].Value = "No bedroom";
                    workSheetHousing.Cells["B32"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0039E);
                    workSheetHousing.Cells["C32"].Value = data.SelectedArea.DP04_0039E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0039E.Value) / data.SelectedArea.DP04_0038E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D32"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0039E);
                    workSheetHousing.Cells["E32"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0039E.Value) / data.NewYorkCity.DP04_0038E.Value, 1) + "%";
                    workSheetHousing.Cells["F32"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0039E - data.NewYorkCity.DP04_0039E));
                    workSheetHousing.Cells["G32"].Value = data.SelectedArea.DP04_0039E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0039E.Value) / data.SelectedArea.DP04_0038E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0039E.Value) / data.NewYorkCity.DP04_0038E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A33"].Value = "1 bedroom";
                    workSheetHousing.Cells["B33"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0040E);
                    workSheetHousing.Cells["C33"].Value = data.SelectedArea.DP04_0040E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0040E.Value) / data.SelectedArea.DP04_0038E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D33"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0040E);
                    workSheetHousing.Cells["E33"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0040E.Value) / data.NewYorkCity.DP04_0038E.Value, 1) + "%";
                    workSheetHousing.Cells["F33"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0040E - data.NewYorkCity.DP04_0040E));
                    workSheetHousing.Cells["G33"].Value = data.SelectedArea.DP04_0040E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0040E.Value) / data.SelectedArea.DP04_0038E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0040E.Value) / data.NewYorkCity.DP04_0038E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A34"].Value = "2 bedrooms";
                    workSheetHousing.Cells["B34"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0041E);
                    workSheetHousing.Cells["C34"].Value = data.SelectedArea.DP04_0041E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0041E.Value) / data.SelectedArea.DP04_0038E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D34"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0041E);
                    workSheetHousing.Cells["E34"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0041E.Value) / data.NewYorkCity.DP04_0038E.Value, 1) + "%";
                    workSheetHousing.Cells["F34"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0041E - data.NewYorkCity.DP04_0041E));
                    workSheetHousing.Cells["G34"].Value = data.SelectedArea.DP04_0041E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0041E.Value) / data.SelectedArea.DP04_0038E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0041E.Value) / data.NewYorkCity.DP04_0038E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A35"].Value = "3 bedrooms";
                    workSheetHousing.Cells["B35"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0042E);
                    workSheetHousing.Cells["C35"].Value = data.SelectedArea.DP04_0042E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0042E.Value) / data.SelectedArea.DP04_0038E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D35"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0042E);
                    workSheetHousing.Cells["E35"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0042E.Value) / data.NewYorkCity.DP04_0038E.Value, 1) + "%";
                    workSheetHousing.Cells["F35"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0042E - data.NewYorkCity.DP04_0042E));
                    workSheetHousing.Cells["G35"].Value = data.SelectedArea.DP04_0042E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0042E.Value) / data.SelectedArea.DP04_0038E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0042E.Value) / data.NewYorkCity.DP04_0038E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A36"].Value = "4 bedrooms";
                    workSheetHousing.Cells["B36"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0043E);
                    workSheetHousing.Cells["C36"].Value = data.SelectedArea.DP04_0043E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0043E.Value) / data.SelectedArea.DP04_0038E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D36"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0043E);
                    workSheetHousing.Cells["E36"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0043E.Value) / data.NewYorkCity.DP04_0038E.Value, 1) + "%";
                    workSheetHousing.Cells["F36"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0043E - data.NewYorkCity.DP04_0043E));
                    workSheetHousing.Cells["G36"].Value = data.SelectedArea.DP04_0043E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0043E.Value) / data.SelectedArea.DP04_0038E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0043E.Value) / data.NewYorkCity.DP04_0038E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A37"].Value = "5 or more bedrooms";
                    workSheetHousing.Cells["B37"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0044E);
                    workSheetHousing.Cells["C37"].Value = data.SelectedArea.DP04_0044E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0044E.Value) / data.SelectedArea.DP04_0038E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D37"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0044E);
                    workSheetHousing.Cells["E37"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0044E.Value) / data.NewYorkCity.DP04_0038E.Value, 1) + "%";
                    workSheetHousing.Cells["F37"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0044E - data.NewYorkCity.DP04_0044E));
                    workSheetHousing.Cells["G37"].Value = data.SelectedArea.DP04_0044E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0044E.Value) / data.SelectedArea.DP04_0038E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0044E.Value) / data.NewYorkCity.DP04_0038E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A39:G39"].Merge = true;
                    workSheetHousing.Cells["A39"].Value = "Housing Tenure";
                    workSheetHousing.Cells["A40:A41"].Merge = true;
                    workSheetHousing.Cells["B40:C40"].Merge = true;
                    workSheetHousing.Cells["B40"].Value = "Selected Area";
                    workSheetHousing.Cells["D40:E40"].Merge = true;
                    workSheetHousing.Cells["D40"].Value = "New York City";
                    workSheetHousing.Cells["F40:G40"].Merge = true;
                    workSheetHousing.Cells["F40"].Value = "Difference";

                    workSheetHousing.Cells["B41"].Value = "Number";
                    workSheetHousing.Cells["C41"].Value = "Percent";
                    workSheetHousing.Cells["D41"].Value = "Number";
                    workSheetHousing.Cells["E41"].Value = "Percent";
                    workSheetHousing.Cells["F41"].Value = "Number";
                    workSheetHousing.Cells["G41"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A42"].Value = "Occupied housing units";
                    workSheetHousing.Cells["B42"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0045E);
                    workSheetHousing.Cells["C42"].Value = "100%";
                    workSheetHousing.Cells["D42"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0045E);
                    workSheetHousing.Cells["E42"].Value = "100%";
                    workSheetHousing.Cells["F42"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0045E - data.NewYorkCity.DP04_0045E));
                    workSheetHousing.Cells["G42"].Value = "0.0";

                    workSheetHousing.Cells["A43"].Value = "Owner-occupied";
                    workSheetHousing.Cells["B43"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0046E);
                    workSheetHousing.Cells["C43"].Value = data.SelectedArea.DP04_0046E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0046E.Value) / data.SelectedArea.DP04_0045E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D43"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0046E);
                    workSheetHousing.Cells["E43"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0046E.Value) / data.NewYorkCity.DP04_0045E.Value, 1) + "%";
                    workSheetHousing.Cells["F43"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0046E - data.NewYorkCity.DP04_0046E));
                    workSheetHousing.Cells["G43"].Value = data.SelectedArea.DP04_0046E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0046E.Value) / data.SelectedArea.DP04_0045E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0046E.Value) / data.NewYorkCity.DP04_0045E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A44"].Value = "Renter-occupied";
                    workSheetHousing.Cells["B44"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0047E);
                    workSheetHousing.Cells["C44"].Value = data.SelectedArea.DP04_0047E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0047E.Value) / data.SelectedArea.DP04_0045E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D44"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0047E);
                    workSheetHousing.Cells["E44"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0047E.Value) / data.NewYorkCity.DP04_0045E.Value, 1) + "%";
                    workSheetHousing.Cells["F44"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0047E - data.NewYorkCity.DP04_0047E));
                    workSheetHousing.Cells["G44"].Value = data.SelectedArea.DP04_0047E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0047E.Value) / data.SelectedArea.DP04_0045E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0047E.Value) / data.NewYorkCity.DP04_0045E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A46:G46"].Merge = true;
                    workSheetHousing.Cells["A46"].Value = "Year Householder Moved Into Unit";
                    workSheetHousing.Cells["A47:A48"].Merge = true;
                    workSheetHousing.Cells["B47:C47"].Merge = true;
                    workSheetHousing.Cells["B47"].Value = "Selected Area";
                    workSheetHousing.Cells["D47:E47"].Merge = true;
                    workSheetHousing.Cells["D47"].Value = "New York City";
                    workSheetHousing.Cells["F47:G47"].Merge = true;
                    workSheetHousing.Cells["F47"].Value = "Difference";

                    workSheetHousing.Cells["B48"].Value = "Number";
                    workSheetHousing.Cells["C48"].Value = "Percent";
                    workSheetHousing.Cells["D48"].Value = "Number";
                    workSheetHousing.Cells["E48"].Value = "Percent";
                    workSheetHousing.Cells["F48"].Value = "Number";
                    workSheetHousing.Cells["G48"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A49"].Value = "Occupied housing units";
                    workSheetHousing.Cells["B49"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0050E);
                    workSheetHousing.Cells["C49"].Value = "100%";
                    workSheetHousing.Cells["D49"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0050E);
                    workSheetHousing.Cells["E49"].Value = "100%";
                    workSheetHousing.Cells["F49"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0050E - data.NewYorkCity.DP04_0050E));
                    workSheetHousing.Cells["G49"].Value = "0.0";

                    workSheetHousing.Cells["A50"].Value = "Moved in 2017 or later";
                    workSheetHousing.Cells["B50"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0051E);
                    workSheetHousing.Cells["C50"].Value = data.SelectedArea.DP04_0051E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0051E.Value) / data.SelectedArea.DP04_0050E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D50"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0051E);
                    workSheetHousing.Cells["E50"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0051E.Value) / data.NewYorkCity.DP04_0050E.Value, 1) + "%";
                    workSheetHousing.Cells["F50"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0051E - data.NewYorkCity.DP04_0051E));
                    workSheetHousing.Cells["G50"].Value = data.SelectedArea.DP04_0051E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0051E.Value) / data.SelectedArea.DP04_0050E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0051E.Value) / data.NewYorkCity.DP04_0050E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A51"].Value = "Moved in 2015 to 2016";
                    workSheetHousing.Cells["B51"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0052E);
                    workSheetHousing.Cells["C51"].Value = data.SelectedArea.DP04_0052E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0052E.Value) / data.SelectedArea.DP04_0050E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D51"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0052E);
                    workSheetHousing.Cells["E51"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0052E.Value) / data.NewYorkCity.DP04_0050E.Value, 1) + "%";
                    workSheetHousing.Cells["F51"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0052E - data.NewYorkCity.DP04_0052E));
                    workSheetHousing.Cells["G51"].Value = data.SelectedArea.DP04_0052E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0052E.Value) / data.SelectedArea.DP04_0050E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0052E.Value) / data.NewYorkCity.DP04_0050E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A52"].Value = "Moved in 2010 to 2014";
                    workSheetHousing.Cells["B52"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0053E);
                    workSheetHousing.Cells["C52"].Value = data.SelectedArea.DP04_0053E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0053E.Value) / data.SelectedArea.DP04_0050E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D52"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0053E);
                    workSheetHousing.Cells["E52"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0053E.Value) / data.NewYorkCity.DP04_0050E.Value, 1) + "%";
                    workSheetHousing.Cells["F52"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0053E - data.NewYorkCity.DP04_0053E));
                    workSheetHousing.Cells["G52"].Value = data.SelectedArea.DP04_0053E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0053E.Value) / data.SelectedArea.DP04_0050E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0053E.Value) / data.NewYorkCity.DP04_0050E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A53"].Value = "Moved in 2000 to 2009";
                    workSheetHousing.Cells["B53"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0054E);
                    workSheetHousing.Cells["C53"].Value = data.SelectedArea.DP04_0054E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0054E.Value) / data.SelectedArea.DP04_0050E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D53"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0054E);
                    workSheetHousing.Cells["E53"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0054E.Value) / data.NewYorkCity.DP04_0050E.Value, 1) + "%";
                    workSheetHousing.Cells["F53"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0054E - data.NewYorkCity.DP04_0054E));
                    workSheetHousing.Cells["G53"].Value = data.SelectedArea.DP04_0054E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0054E.Value) / data.SelectedArea.DP04_0050E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0054E.Value) / data.NewYorkCity.DP04_0050E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A54"].Value = "Moved in 1990 to 1999";
                    workSheetHousing.Cells["B54"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0055E);
                    workSheetHousing.Cells["C54"].Value = data.SelectedArea.DP04_0055E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0055E.Value) / data.SelectedArea.DP04_0050E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D54"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0055E);
                    workSheetHousing.Cells["E54"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0055E.Value) / data.NewYorkCity.DP04_0050E.Value, 1) + "%";
                    workSheetHousing.Cells["F54"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0055E - data.NewYorkCity.DP04_0055E));
                    workSheetHousing.Cells["G54"].Value = data.SelectedArea.DP04_0055E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0055E.Value) / data.SelectedArea.DP04_0050E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0055E.Value) / data.NewYorkCity.DP04_0050E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A55"].Value = "Moved in 1989 and earlier";
                    workSheetHousing.Cells["B55"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0056E);
                    workSheetHousing.Cells["C55"].Value = data.SelectedArea.DP04_0056E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0056E.Value) / data.SelectedArea.DP04_0050E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D55"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0056E);
                    workSheetHousing.Cells["E55"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0056E.Value) / data.NewYorkCity.DP04_0050E.Value, 1) + "%";
                    workSheetHousing.Cells["F55"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0056E - data.NewYorkCity.DP04_0056E));
                    workSheetHousing.Cells["G55"].Value = data.SelectedArea.DP04_0056E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0056E.Value) / data.SelectedArea.DP04_0050E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0056E.Value) / data.NewYorkCity.DP04_0050E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A57:G57"].Merge = true;
                    workSheetHousing.Cells["A57"].Value = "Occupants per Room";
                    workSheetHousing.Cells["A58:A59"].Merge = true;
                    workSheetHousing.Cells["B58:C58"].Merge = true;
                    workSheetHousing.Cells["B58"].Value = "Selected Area";
                    workSheetHousing.Cells["D58:E58"].Merge = true;
                    workSheetHousing.Cells["D58"].Value = "New York City";
                    workSheetHousing.Cells["F58:G58"].Merge = true;
                    workSheetHousing.Cells["F58"].Value = "Difference";

                    workSheetHousing.Cells["B59"].Value = "Number";
                    workSheetHousing.Cells["C59"].Value = "Percent";
                    workSheetHousing.Cells["D59"].Value = "Number";
                    workSheetHousing.Cells["E59"].Value = "Percent";
                    workSheetHousing.Cells["F59"].Value = "Number";
                    workSheetHousing.Cells["G59"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A60"].Value = "Occupied housing units";
                    workSheetHousing.Cells["B60"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0076E);
                    workSheetHousing.Cells["C60"].Value = "100%";
                    workSheetHousing.Cells["D60"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0076E);
                    workSheetHousing.Cells["E60"].Value = "100%";
                    workSheetHousing.Cells["F60"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0076E - data.NewYorkCity.DP04_0076E));
                    workSheetHousing.Cells["G60"].Value = "0.0";

                    workSheetHousing.Cells["A61"].Value = "1.00 or less";
                    workSheetHousing.Cells["B61"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0077E);
                    workSheetHousing.Cells["C61"].Value = data.SelectedArea.DP04_0077E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0077E.Value) / data.SelectedArea.DP04_0076E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D61"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0077E);
                    workSheetHousing.Cells["E61"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0077E.Value) / data.NewYorkCity.DP04_0076E.Value, 1) + "%";
                    workSheetHousing.Cells["F61"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0077E - data.NewYorkCity.DP04_0077E));
                    workSheetHousing.Cells["G61"].Value = data.SelectedArea.DP04_0077E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0077E.Value) / data.SelectedArea.DP04_0076E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0077E.Value) / data.NewYorkCity.DP04_0076E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A62"].Value = "1.01 to 1.50";
                    workSheetHousing.Cells["B62"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0078E);
                    workSheetHousing.Cells["C62"].Value = data.SelectedArea.DP04_0078E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0078E.Value) / data.SelectedArea.DP04_0076E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D62"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0078E);
                    workSheetHousing.Cells["E62"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0078E.Value) / data.NewYorkCity.DP04_0076E.Value, 1) + "%";
                    workSheetHousing.Cells["F62"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0078E - data.NewYorkCity.DP04_0078E));
                    workSheetHousing.Cells["G62"].Value = data.SelectedArea.DP04_0078E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0078E.Value) / data.SelectedArea.DP04_0076E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0078E.Value) / data.NewYorkCity.DP04_0076E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A63"].Value = "1.51 or more";
                    workSheetHousing.Cells["B63"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0079E);
                    workSheetHousing.Cells["C63"].Value = data.SelectedArea.DP04_0079E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0079E.Value) / data.SelectedArea.DP04_0076E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D63"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0079E);
                    workSheetHousing.Cells["E63"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0079E.Value) / data.NewYorkCity.DP04_0076E.Value, 1) + "%";
                    workSheetHousing.Cells["F63"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0079E - data.NewYorkCity.DP04_0079E));
                    workSheetHousing.Cells["G63"].Value = data.SelectedArea.DP04_0079E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0079E.Value) / data.SelectedArea.DP04_0076E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0079E.Value) / data.NewYorkCity.DP04_0076E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A65:G65"].Merge = true;
                    workSheetHousing.Cells["A65"].Value = "Mortgage Status";
                    workSheetHousing.Cells["A66:A67"].Merge = true;
                    workSheetHousing.Cells["B66:C66"].Merge = true;
                    workSheetHousing.Cells["B66"].Value = "Selected Area";
                    workSheetHousing.Cells["D66:E66"].Merge = true;
                    workSheetHousing.Cells["D66"].Value = "New York City";
                    workSheetHousing.Cells["F66:G66"].Merge = true;
                    workSheetHousing.Cells["F66"].Value = "Difference";

                    workSheetHousing.Cells["B67"].Value = "Number";
                    workSheetHousing.Cells["C67"].Value = "Percent";
                    workSheetHousing.Cells["D67"].Value = "Number";
                    workSheetHousing.Cells["E67"].Value = "Percent";
                    workSheetHousing.Cells["F67"].Value = "Number";
                    workSheetHousing.Cells["G67"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A68"].Value = "Owner-occupied units";
                    workSheetHousing.Cells["B68"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0090E);
                    workSheetHousing.Cells["C68"].Value = "100%";
                    workSheetHousing.Cells["D68"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0090E);
                    workSheetHousing.Cells["E68"].Value = "100%";
                    workSheetHousing.Cells["F68"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0090E - data.NewYorkCity.DP04_0090E));
                    workSheetHousing.Cells["G68"].Value = "0.0";

                    workSheetHousing.Cells["A69"].Value = "Housing units with a mortgage";
                    workSheetHousing.Cells["B69"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0091E);
                    workSheetHousing.Cells["C69"].Value = data.SelectedArea.DP04_0091E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0091E.Value) / data.SelectedArea.DP04_0090E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D69"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0091E);
                    workSheetHousing.Cells["E69"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0091E.Value) / data.NewYorkCity.DP04_0090E.Value, 1) + "%";
                    workSheetHousing.Cells["F69"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0091E - data.NewYorkCity.DP04_0091E));
                    workSheetHousing.Cells["G69"].Value = data.SelectedArea.DP04_0091E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0091E.Value) / data.SelectedArea.DP04_0090E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0091E.Value) / data.NewYorkCity.DP04_0090E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A70"].Value = "Housing units without a mortgage";
                    workSheetHousing.Cells["B70"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0092E);
                    workSheetHousing.Cells["C70"].Value = data.SelectedArea.DP04_0092E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0092E.Value) / data.SelectedArea.DP04_0090E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D70"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0092E);
                    workSheetHousing.Cells["E70"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0092E.Value) / data.NewYorkCity.DP04_0090E.Value, 1) + "%";
                    workSheetHousing.Cells["F70"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0092E - data.NewYorkCity.DP04_0092E));
                    workSheetHousing.Cells["G70"].Value = data.SelectedArea.DP04_0092E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0092E.Value) / data.SelectedArea.DP04_0090E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0092E.Value) / data.NewYorkCity.DP04_0090E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A72:G72"].Merge = true;
                    workSheetHousing.Cells["A72"].Value = "Selected Monthly Owner Costs (SMOC)";
                    workSheetHousing.Cells["A73:A74"].Merge = true;
                    workSheetHousing.Cells["B73:C73"].Merge = true;
                    workSheetHousing.Cells["B73"].Value = "Selected Area";
                    workSheetHousing.Cells["D73:E73"].Merge = true;
                    workSheetHousing.Cells["D73"].Value = "New York City";
                    workSheetHousing.Cells["F73:G73"].Merge = true;
                    workSheetHousing.Cells["F73"].Value = "Difference";

                    workSheetHousing.Cells["B74"].Value = "Number";
                    workSheetHousing.Cells["C74"].Value = "Percent";
                    workSheetHousing.Cells["D74"].Value = "Number";
                    workSheetHousing.Cells["E74"].Value = "Percent";
                    workSheetHousing.Cells["F74"].Value = "Number";
                    workSheetHousing.Cells["G74"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A75"].Value = "Housing units with a mortgage";
                    workSheetHousing.Cells["B75"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0093E);
                    workSheetHousing.Cells["C75"].Value = "100%";
                    workSheetHousing.Cells["D75"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0093E);
                    workSheetHousing.Cells["E75"].Value = "100%";
                    workSheetHousing.Cells["F75"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0093E - data.NewYorkCity.DP04_0093E));
                    workSheetHousing.Cells["G75"].Value = "0.0";

                    workSheetHousing.Cells["A76"].Value = "Less than $500";
                    workSheetHousing.Cells["B76"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0094E);
                    workSheetHousing.Cells["C76"].Value = data.SelectedArea.DP04_0094E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0094E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D76"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0094E);
                    workSheetHousing.Cells["E76"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0094E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F76"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0094E - data.NewYorkCity.DP04_0094E));
                    workSheetHousing.Cells["G76"].Value = data.SelectedArea.DP04_0094E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0094E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0094E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A77"].Value = "$500 to $999";
                    workSheetHousing.Cells["B77"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0095E);
                    workSheetHousing.Cells["C77"].Value = data.SelectedArea.DP04_0095E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0095E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D77"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0095E);
                    workSheetHousing.Cells["E77"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0095E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F77"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0095E - data.NewYorkCity.DP04_0095E));
                    workSheetHousing.Cells["G77"].Value = data.SelectedArea.DP04_0095E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0095E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0095E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A78"].Value = "$1,000 to $1,499";
                    workSheetHousing.Cells["B78"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0096E);
                    workSheetHousing.Cells["C78"].Value = data.SelectedArea.DP04_0096E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0096E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D78"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0096E);
                    workSheetHousing.Cells["E78"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0096E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F78"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0096E - data.NewYorkCity.DP04_0096E));
                    workSheetHousing.Cells["G78"].Value = data.SelectedArea.DP04_0096E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0096E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0096E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A79"].Value = "$1,500 to $1,999";
                    workSheetHousing.Cells["B79"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0097E);
                    workSheetHousing.Cells["C79"].Value = data.SelectedArea.DP04_0097E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0097E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D79"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0097E);
                    workSheetHousing.Cells["E79"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0097E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F79"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0097E - data.NewYorkCity.DP04_0097E));
                    workSheetHousing.Cells["G79"].Value = data.SelectedArea.DP04_0097E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0097E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0097E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A80"].Value = "$2,000 to $2,499";
                    workSheetHousing.Cells["B80"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0098E);
                    workSheetHousing.Cells["C80"].Value = data.SelectedArea.DP04_0098E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0098E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D80"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0098E);
                    workSheetHousing.Cells["E80"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0098E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F80"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0098E - data.NewYorkCity.DP04_0098E));
                    workSheetHousing.Cells["G80"].Value = data.SelectedArea.DP04_0098E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0098E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0098E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A81"].Value = "$2,500 to $2,999";
                    workSheetHousing.Cells["B81"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0099E);
                    workSheetHousing.Cells["C81"].Value = data.SelectedArea.DP04_0099E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0099E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D81"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0099E);
                    workSheetHousing.Cells["E81"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0099E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F81"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0099E - data.NewYorkCity.DP04_0099E));
                    workSheetHousing.Cells["G81"].Value = data.SelectedArea.DP04_0099E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0099E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0099E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A82"].Value = "$3,000 or more";
                    workSheetHousing.Cells["B82"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0100E);
                    workSheetHousing.Cells["C82"].Value = data.SelectedArea.DP04_0100E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0100E.Value) / data.SelectedArea.DP04_0093E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D82"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0100E);
                    workSheetHousing.Cells["E82"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0100E.Value) / data.NewYorkCity.DP04_0093E.Value, 1) + "%";
                    workSheetHousing.Cells["F82"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0100E - data.NewYorkCity.DP04_0100E));
                    workSheetHousing.Cells["G82"].Value = data.SelectedArea.DP04_0100E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0100E.Value) / data.SelectedArea.DP04_0093E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0100E.Value) / data.NewYorkCity.DP04_0093E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A83"].Value = "Median (dollars)";
                    workSheetHousing.Cells["B83"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0101E);
                    workSheetHousing.Cells["C83"].Value = "";
                    workSheetHousing.Cells["D83"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0101E);
                    workSheetHousing.Cells["E83"].Value = "";
                    workSheetHousing.Cells["F83"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0101E - data.NewYorkCity.DP04_0101E));
                    workSheetHousing.Cells["G83"].Value = "";

                    workSheetHousing.Cells["A85:G85"].Merge = true;
                    workSheetHousing.Cells["A85"].Value = "Selected Monthly Owner Costs as a Percentage of Household Income (SMOCAPI)";
                    workSheetHousing.Cells["A86:A87"].Merge = true;
                    workSheetHousing.Cells["B86:C86"].Merge = true;
                    workSheetHousing.Cells["B86"].Value = "Selected Area";
                    workSheetHousing.Cells["D86:E86"].Merge = true;
                    workSheetHousing.Cells["D86"].Value = "New York City";
                    workSheetHousing.Cells["F86:G86"].Merge = true;
                    workSheetHousing.Cells["F86"].Value = "Difference";

                    workSheetHousing.Cells["B87"].Value = "Number";
                    workSheetHousing.Cells["C87"].Value = "Percent";
                    workSheetHousing.Cells["D87"].Value = "Number";
                    workSheetHousing.Cells["E87"].Value = "Percent";
                    workSheetHousing.Cells["F87"].Value = "Number";
                    workSheetHousing.Cells["G87"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A88"].Value = "Housing units with a mortgage (excluding units where SMOCAPI cannot be computed)";
                    workSheetHousing.Cells["B88"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0110E);
                    workSheetHousing.Cells["C88"].Value = "100%";
                    workSheetHousing.Cells["D88"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0110E);
                    workSheetHousing.Cells["E88"].Value = "100%";
                    workSheetHousing.Cells["F88"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0110E - data.NewYorkCity.DP04_0110E));
                    workSheetHousing.Cells["G88"].Value = "0.0";

                    workSheetHousing.Cells["A89"].Value = "Less than 20.0 percent";
                    workSheetHousing.Cells["B89"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0111E);
                    workSheetHousing.Cells["C89"].Value = data.SelectedArea.DP04_0111E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0111E.Value) / data.SelectedArea.DP04_0110E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D89"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0111E);
                    workSheetHousing.Cells["E89"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0111E.Value) / data.NewYorkCity.DP04_0110E.Value, 1) + "%";
                    workSheetHousing.Cells["F89"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0111E - data.NewYorkCity.DP04_0111E));
                    workSheetHousing.Cells["G89"].Value = data.SelectedArea.DP04_0111E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0111E.Value) / data.SelectedArea.DP04_0110E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0111E.Value) / data.NewYorkCity.DP04_0110E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A90"].Value = "20.0 to 24.9 percent";
                    workSheetHousing.Cells["B90"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0112E);
                    workSheetHousing.Cells["C90"].Value = data.SelectedArea.DP04_0112E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0112E.Value) / data.SelectedArea.DP04_0110E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D90"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0112E);
                    workSheetHousing.Cells["E90"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0112E.Value) / data.NewYorkCity.DP04_0110E.Value, 1) + "%";
                    workSheetHousing.Cells["F90"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0112E - data.NewYorkCity.DP04_0112E));
                    workSheetHousing.Cells["G90"].Value = data.SelectedArea.DP04_0112E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0112E.Value) / data.SelectedArea.DP04_0110E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0112E.Value) / data.NewYorkCity.DP04_0110E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A91"].Value = "25.0 to 29.9 percent";
                    workSheetHousing.Cells["B91"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0113E);
                    workSheetHousing.Cells["C91"].Value = data.SelectedArea.DP04_0113E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0113E.Value) / data.SelectedArea.DP04_0110E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D91"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0113E);
                    workSheetHousing.Cells["E91"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0113E.Value) / data.NewYorkCity.DP04_0110E.Value, 1) + "%";
                    workSheetHousing.Cells["F91"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0113E - data.NewYorkCity.DP04_0113E));
                    workSheetHousing.Cells["G91"].Value = data.SelectedArea.DP04_0113E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0113E.Value) / data.SelectedArea.DP04_0110E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0113E.Value) / data.NewYorkCity.DP04_0110E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A92"].Value = "30.0 to 34.9 percent";
                    workSheetHousing.Cells["B92"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0114E);
                    workSheetHousing.Cells["C92"].Value = data.SelectedArea.DP04_0114E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0114E.Value) / data.SelectedArea.DP04_0110E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D92"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0114E);
                    workSheetHousing.Cells["E92"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0114E.Value) / data.NewYorkCity.DP04_0110E.Value, 1) + "%";
                    workSheetHousing.Cells["F92"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0114E - data.NewYorkCity.DP04_0114E));
                    workSheetHousing.Cells["G92"].Value = data.SelectedArea.DP04_0114E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0114E.Value) / data.SelectedArea.DP04_0110E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0114E.Value) / data.NewYorkCity.DP04_0110E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A93"].Value = "35.0 percent or more";
                    workSheetHousing.Cells["B93"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0115E);
                    workSheetHousing.Cells["C93"].Value = data.SelectedArea.DP04_0115E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0115E.Value) / data.SelectedArea.DP04_0110E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D93"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0115E);
                    workSheetHousing.Cells["E93"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0115E.Value) / data.NewYorkCity.DP04_0110E.Value, 1) + "%";
                    workSheetHousing.Cells["F93"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0115E - data.NewYorkCity.DP04_0115E));
                    workSheetHousing.Cells["G93"].Value = data.SelectedArea.DP04_0115E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0115E.Value) / data.SelectedArea.DP04_0110E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0115E.Value) / data.NewYorkCity.DP04_0110E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A95:G95"].Merge = true;
                    workSheetHousing.Cells["A95"].Value = "Gross Rent";
                    workSheetHousing.Cells["A96:A97"].Merge = true;
                    workSheetHousing.Cells["B96:C96"].Merge = true;
                    workSheetHousing.Cells["B96"].Value = "Selected Area";
                    workSheetHousing.Cells["D96:E96"].Merge = true;
                    workSheetHousing.Cells["D96"].Value = "New York City";
                    workSheetHousing.Cells["F96:G96"].Merge = true;
                    workSheetHousing.Cells["F96"].Value = "Difference";

                    workSheetHousing.Cells["B97"].Value = "Number";
                    workSheetHousing.Cells["C97"].Value = "Percent";
                    workSheetHousing.Cells["D97"].Value = "Number";
                    workSheetHousing.Cells["E97"].Value = "Percent";
                    workSheetHousing.Cells["F97"].Value = "Number";
                    workSheetHousing.Cells["G97"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A98"].Value = "Occupied units paying rent";
                    workSheetHousing.Cells["B98"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0126E);
                    workSheetHousing.Cells["C98"].Value = "100%";
                    workSheetHousing.Cells["D98"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0126E);
                    workSheetHousing.Cells["E98"].Value = "100%";
                    workSheetHousing.Cells["F98"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0126E - data.NewYorkCity.DP04_0126E));
                    workSheetHousing.Cells["G98"].Value = "0.0";

                    workSheetHousing.Cells["A99"].Value = "Less than $500";
                    workSheetHousing.Cells["B99"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0127E);
                    workSheetHousing.Cells["C99"].Value = data.SelectedArea.DP04_0127E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0127E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D99"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0127E);
                    workSheetHousing.Cells["E99"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0127E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F99"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0127E - data.NewYorkCity.DP04_0127E));
                    workSheetHousing.Cells["G99"].Value = data.SelectedArea.DP04_0127E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0127E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0127E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A100"].Value = "$500 to $999";
                    workSheetHousing.Cells["B100"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0128E);
                    workSheetHousing.Cells["C100"].Value = data.SelectedArea.DP04_0128E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0128E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D100"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0128E);
                    workSheetHousing.Cells["E100"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0128E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F100"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0128E - data.NewYorkCity.DP04_0128E));
                    workSheetHousing.Cells["G100"].Value = data.SelectedArea.DP04_0128E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0128E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0128E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A101"].Value = "$1,000 to $1,499";
                    workSheetHousing.Cells["B101"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0129E);
                    workSheetHousing.Cells["C101"].Value = data.SelectedArea.DP04_0129E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0129E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D101"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0129E);
                    workSheetHousing.Cells["E101"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0129E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F101"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0129E - data.NewYorkCity.DP04_0129E));
                    workSheetHousing.Cells["G101"].Value = data.SelectedArea.DP04_0129E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0129E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0129E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A102"].Value = "$1,500 to $1,999";
                    workSheetHousing.Cells["B102"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0130E);
                    workSheetHousing.Cells["C102"].Value = data.SelectedArea.DP04_0130E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0130E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D102"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0130E);
                    workSheetHousing.Cells["E102"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0130E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F102"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0130E - data.NewYorkCity.DP04_0130E));
                    workSheetHousing.Cells["G102"].Value = data.SelectedArea.DP04_0130E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0130E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0130E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A103"].Value = "$2,000 to $2,499";
                    workSheetHousing.Cells["B103"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0131E);
                    workSheetHousing.Cells["C103"].Value = data.SelectedArea.DP04_0131E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0131E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D103"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0131E);
                    workSheetHousing.Cells["E103"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0131E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F103"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0131E - data.NewYorkCity.DP04_0131E));
                    workSheetHousing.Cells["G103"].Value = data.SelectedArea.DP04_0131E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0131E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0131E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A104"].Value = "$2,500 to $2,999";
                    workSheetHousing.Cells["B104"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0132E);
                    workSheetHousing.Cells["C104"].Value = data.SelectedArea.DP04_0132E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0132E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D104"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0132E);
                    workSheetHousing.Cells["E104"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0132E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F104"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0132E - data.NewYorkCity.DP04_0132E));
                    workSheetHousing.Cells["G104"].Value = data.SelectedArea.DP04_0132E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0132E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0132E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A105"].Value = "$3,000 or more";
                    workSheetHousing.Cells["B105"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0133E);
                    workSheetHousing.Cells["C105"].Value = data.SelectedArea.DP04_0133E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0133E.Value) / data.SelectedArea.DP04_0126E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D105"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0133E);
                    workSheetHousing.Cells["E105"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0133E.Value) / data.NewYorkCity.DP04_0126E.Value, 1) + "%";
                    workSheetHousing.Cells["F105"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0133E - data.NewYorkCity.DP04_0133E));
                    workSheetHousing.Cells["G105"].Value = data.SelectedArea.DP04_0133E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0133E.Value) / data.SelectedArea.DP04_0126E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0133E.Value) / data.NewYorkCity.DP04_0126E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A106"].Value = "Median (dollars)";
                    workSheetHousing.Cells["B106"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0134E);
                    workSheetHousing.Cells["C106"].Value = "";
                    workSheetHousing.Cells["D106"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0134E);
                    workSheetHousing.Cells["E106"].Value = "";
                    workSheetHousing.Cells["F106"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0134E - data.NewYorkCity.DP04_0134E));
                    workSheetHousing.Cells["G106"].Value = "";

                    workSheetHousing.Cells["A108:G108"].Merge = true;
                    workSheetHousing.Cells["A108"].Value = "Gross Rent as a Percentage of Household Income (GRAPI)";
                    workSheetHousing.Cells["A109:A110"].Merge = true;
                    workSheetHousing.Cells["B109:C109"].Merge = true;
                    workSheetHousing.Cells["B109"].Value = "Selected Area";
                    workSheetHousing.Cells["D109:E109"].Merge = true;
                    workSheetHousing.Cells["D109"].Value = "New York City";
                    workSheetHousing.Cells["F109:G109"].Merge = true;
                    workSheetHousing.Cells["F109"].Value = "Difference";

                    workSheetHousing.Cells["B110"].Value = "Number";
                    workSheetHousing.Cells["C110"].Value = "Percent";
                    workSheetHousing.Cells["D110"].Value = "Number";
                    workSheetHousing.Cells["E110"].Value = "Percent";
                    workSheetHousing.Cells["F110"].Value = "Number";
                    workSheetHousing.Cells["G110"].Value = "Pctg. Pt.";

                    workSheetHousing.Cells["A111"].Value = "Occupied units paying rent (excluding units where GRAPI cannot be computed)";
                    workSheetHousing.Cells["B111"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0136E);
                    workSheetHousing.Cells["C111"].Value = "100%";
                    workSheetHousing.Cells["D111"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0136E);
                    workSheetHousing.Cells["E111"].Value = "100%";
                    workSheetHousing.Cells["F111"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0136E - data.NewYorkCity.DP04_0136E));
                    workSheetHousing.Cells["G111"].Value = "0.0";

                    workSheetHousing.Cells["A112"].Value = "Less than 15.0 percent";
                    workSheetHousing.Cells["B112"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0137E);
                    workSheetHousing.Cells["C112"].Value = data.SelectedArea.DP04_0137E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0137E.Value) / data.SelectedArea.DP04_0136E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D112"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0137E);
                    workSheetHousing.Cells["E112"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0137E.Value) / data.NewYorkCity.DP04_0136E.Value, 1) + "%";
                    workSheetHousing.Cells["F112"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0137E - data.NewYorkCity.DP04_0137E));
                    workSheetHousing.Cells["G112"].Value = data.SelectedArea.DP04_0137E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0137E.Value) / data.SelectedArea.DP04_0136E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0137E.Value) / data.NewYorkCity.DP04_0136E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A113"].Value = "15.0 to 19.9 percent";
                    workSheetHousing.Cells["B113"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0138E);
                    workSheetHousing.Cells["C113"].Value = data.SelectedArea.DP04_0138E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0138E.Value) / data.SelectedArea.DP04_0136E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D113"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0138E);
                    workSheetHousing.Cells["E113"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0138E.Value) / data.NewYorkCity.DP04_0136E.Value, 1) + "%";
                    workSheetHousing.Cells["F113"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0138E - data.NewYorkCity.DP04_0138E));
                    workSheetHousing.Cells["G113"].Value = data.SelectedArea.DP04_0138E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0138E.Value) / data.SelectedArea.DP04_0136E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0138E.Value) / data.NewYorkCity.DP04_0136E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A114"].Value = "20.0 to 24.9 percent";
                    workSheetHousing.Cells["B114"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0139E);
                    workSheetHousing.Cells["C114"].Value = data.SelectedArea.DP04_0139E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0139E.Value) / data.SelectedArea.DP04_0136E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D114"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0139E);
                    workSheetHousing.Cells["E114"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0139E.Value) / data.NewYorkCity.DP04_0136E.Value, 1) + "%";
                    workSheetHousing.Cells["F114"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0139E - data.NewYorkCity.DP04_0139E));
                    workSheetHousing.Cells["G114"].Value = data.SelectedArea.DP04_0139E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0139E.Value) / data.SelectedArea.DP04_0136E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0139E.Value) / data.NewYorkCity.DP04_0136E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A115"].Value = "25.0 to 29.9 percent";
                    workSheetHousing.Cells["B115"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0140E);
                    workSheetHousing.Cells["C115"].Value = data.SelectedArea.DP04_0140E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0140E.Value) / data.SelectedArea.DP04_0136E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D115"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0140E);
                    workSheetHousing.Cells["E115"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0140E.Value) / data.NewYorkCity.DP04_0136E.Value, 1) + "%";
                    workSheetHousing.Cells["F115"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0140E - data.NewYorkCity.DP04_0140E));
                    workSheetHousing.Cells["G115"].Value = data.SelectedArea.DP04_0140E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0140E.Value) / data.SelectedArea.DP04_0136E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0140E.Value) / data.NewYorkCity.DP04_0136E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A116"].Value = "30.0 to 34.9 percent";
                    workSheetHousing.Cells["B116"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0141E);
                    workSheetHousing.Cells["C116"].Value = data.SelectedArea.DP04_0141E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0141E.Value) / data.SelectedArea.DP04_0136E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D116"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0141E);
                    workSheetHousing.Cells["E116"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0141E.Value) / data.NewYorkCity.DP04_0136E.Value, 1) + "%";
                    workSheetHousing.Cells["F116"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0141E - data.NewYorkCity.DP04_0141E));
                    workSheetHousing.Cells["G116"].Value = data.SelectedArea.DP04_0141E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0141E.Value) / data.SelectedArea.DP04_0136E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0141E.Value) / data.NewYorkCity.DP04_0136E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells["A117"].Value = "35.0 percent or more";
                    workSheetHousing.Cells["B117"].Value = String.Format("{0:n0}", data.SelectedArea.DP04_0142E);
                    workSheetHousing.Cells["C117"].Value = data.SelectedArea.DP04_0142E.HasValue ? Math.Round((double)(100 * data.SelectedArea.DP04_0142E.Value) / data.SelectedArea.DP04_0136E.Value, 1) + "%" : "";
                    workSheetHousing.Cells["D117"].Value = String.Format("{0:n0}", data.NewYorkCity.DP04_0142E);
                    workSheetHousing.Cells["E117"].Value = Math.Round((double)(100 * data.NewYorkCity.DP04_0142E.Value) / data.NewYorkCity.DP04_0136E.Value, 1) + "%";
                    workSheetHousing.Cells["F117"].Value = String.Format("{0:n0}", (data.SelectedArea.DP04_0142E - data.NewYorkCity.DP04_0142E));
                    workSheetHousing.Cells["G117"].Value = data.SelectedArea.DP04_0142E.HasValue ? Math.Round(Math.Round((double)(100 * data.SelectedArea.DP04_0142E.Value) / data.SelectedArea.DP04_0136E.Value, 1) - Math.Round((double)(100 * data.NewYorkCity.DP04_0142E.Value) / data.NewYorkCity.DP04_0136E.Value, 1), 1).ToString() : "";

                    workSheetHousing.Cells[workSheetDemographics.Dimension.Address].AutoFitColumns();
                    SetHeaderStyle(workSheetHousing, "A1");
                    SetHeaderStyle(workSheetHousing, "A13");
                    SetHeaderStyle(workSheetHousing, "A28");
                    SetHeaderStyle(workSheetHousing, "A39");
                    SetHeaderStyle(workSheetHousing, "A46");
                    SetHeaderStyle(workSheetHousing, "A57");
                    SetHeaderStyle(workSheetHousing, "A65");
                    SetHeaderStyle(workSheetHousing, "A72");
                    SetHeaderStyle(workSheetHousing, "A85");
                    SetHeaderStyle(workSheetHousing, "A95");
                    SetHeaderStyle(workSheetHousing, "A108");
                    #endregion Housing
                    excelBytes = xlPackage.GetAsByteArray();
                }

                string targetFolder = Server.MapPath("~/Reports");
                string fileName = ReportName + "_" + DateTime.Now.Ticks.ToString() + ".xlsx";
                string targetPath = Path.Combine(targetFolder, fileName);

                MyReport myReport = new MyReport()
                {
                    Username = User.Identity.Name,
                    ReportName = ReportName,
                    FileName = fileName
                };
                db.MyReports.Add(myReport);
                db.SaveChanges();

                System.IO.File.WriteAllBytes(targetPath, excelBytes);
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }

        }

        private void SetHeaderStyle(ExcelWorksheet workSheet, string cells)
        {
            var workSheetRange = workSheet.Cells[cells];
            workSheetRange.Style.Font.Bold = true;
            workSheetRange.Style.Border.Top.Style
                = workSheetRange.Style.Border.Left.Style
                = workSheetRange.Style.Border.Right.Style
                = workSheetRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheetRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            workSheetRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#4D7496"));
            workSheetRange.Style.Font.Color.SetColor(Color.White);
            workSheetRange.AutoFitColumns(25, 100);
        }

    }
}