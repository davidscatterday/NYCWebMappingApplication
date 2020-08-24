using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCMappingWebApp.Controllers
{
    public class ConsumerProfilesController : Controller
    {
        MainDAL mainDAL = new MainDAL();
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
    }
}