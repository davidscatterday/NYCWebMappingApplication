﻿using NYCMappingWebApp.DataAccessLayer;
using NYCMappingWebApp.Entities;
using NYCMappingWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Dynamic;
using NYCMappingWebApp.Helpers;

namespace NYCMappingWebApp.Controllers
{
    public class HomeController : Controller
    {
        MainDAL mainDAL = new MainDAL();
        OwnerAnalysisDAL ownerAnalysisDAL = new OwnerAnalysisDAL();
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();
        public ActionResult Index()
        {
            if (!(GlobalVariables.GetFromCookie("NYCUser", "IsLogged") == "True"))
            {
                return RedirectToAction("Login", "AppUsers");
            }
            IndexData data = mainDAL.GetIndexData(GlobalVariables.GetFromCookie("NYCUser", "Username"));
            ViewBag.ZoningDistricts = mainDAL.GetAllZoningDistricts();
            ViewBag.CommercialOverlays = mainDAL.GetAllCommercialOverlays();
            ViewBag.EvictionStatuses = mainDAL.GetAllEvictionStatuses();
            ViewBag.ElevatorDeviceTypes = mainDAL.GetAllElevatorDeviceTypes();
            ViewBag.FilingTypes = mainDAL.GetAllFilingTypes();
            ViewBag.FilingStatuses = mainDAL.GetAllFilingStatuses();
            ViewBag.YesNoStatuses = mainDAL.GetAllYesNoStatuses();
            ViewBag.Frequencies = mainDAL.GetAllFrequencies();
            ViewBag.CenterPoints = mainDAL.GetAllCenterPoints();
            ViewBag.CenterPointValues = mainDAL.GetCenterPointValues("1");
            return View(data);
        }

        public JsonResult Configuration()
        {
            var data = mainDAL.GetConfiguration();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoroughs(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughs(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoroughsCP(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughsCP(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoroughsTA(string term)
        {
            var BoroughsList = mainDAL.GetAllBoroughsTA(term);
            return Json(new { BoroughsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistricts(string term)
        {
            var DistrictsList = mainDAL.GetAllDistricts(term);
            return Json(new { DistrictsList }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetTopBBLs(string term)
        {
            var BBLList = mainDAL.GetTopBBLs(term);
            return Json(new { BBLList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTopAddresses(string term)
        {
            var AddressList = mainDAL.GetTopAddresses(term);
            return Json(new { AddressList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCensusTracts11Digit(string term)
        {
            var DataList = mainDAL.GetCensusTracts11Digit(term);
            return Json(new { DataList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrendAnalysisEcbViolationTypes(string term)
        {
            var EcbViolationTypesList = mainDAL.GetTrendAnalysisEcbViolationTypes(term);
            return Json(new { EcbViolationTypesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrendAnalysisPermitWorkTypes(string term)
        {
            var PermitWorkTypesList = mainDAL.GetTrendAnalysisPermitWorkTypes(term);
            return Json(new { PermitWorkTypesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTrendAnalysisPermitJobTypes(string term)
        {
            var PermitJobTypesList = mainDAL.GetTrendAnalysisPermitJobTypes(term);
            return Json(new { PermitJobTypesList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDesignationDescriptions(string term)
        {
            var DesignationDescriptionsList = mainDAL.GetDesignationDescriptions(term);
            return Json(new { DesignationDescriptionsList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchLookaLikeByBBL(string bbl)
        {
            ReturnLookaLike data = mainDAL.SearchLookaLikeByBBL(bbl);
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchLookaLikeByAddresses(string adr)
        {
            ReturnLookaLike data = mainDAL.SearchLookaLikeByAddresses(adr);
            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchDatabase(string sqlQuery)
        {
            var data = mainDAL.SearchDatabase(sqlQuery);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchConsumerProfilesDatabaseList(string sqlQuery)
        {
            var data = mainDAL.SearchConsumerProfilesDatabaseList(sqlQuery);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchDatabaseHeatMap(string StoredProcedure, string DateHmPsBasePeriod, string DateHmPsAnalysisPeriod, string DiffDaysHmPsBasePeriod
            , string DiffDaysHmPsAnalysisPeriod, string BoroughsTA, string DistrictsTA, string ZipCodeRangeTAFrom, string ZipCodeRangeTATo)
        {
            var data = mainDAL.SearchDatabaseHeatMap(StoredProcedure, DateHmPsBasePeriod, DateHmPsAnalysisPeriod, DiffDaysHmPsBasePeriod
            , DiffDaysHmPsAnalysisPeriod, BoroughsTA, DistrictsTA, ZipCodeRangeTAFrom, ZipCodeRangeTATo);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchDatabaseHeatMapViolations(string ViolationType, string StoredProcedure, string FormatedBasePeriodFrom, string FormatedBasePeriodTo, string FormatedAnalysisPeriodFrom
            , string FormatedAnalysisPeriodTo, string BoroughsTA, string DistrictsTA, string ZipCodeRangeTAFrom, string ZipCodeRangeTATo)
        {
            var data = mainDAL.SearchDatabaseHeatMapViolations(ViolationType, StoredProcedure, FormatedBasePeriodFrom, FormatedBasePeriodTo, FormatedAnalysisPeriodFrom
            , FormatedAnalysisPeriodTo, BoroughsTA, DistrictsTA, ZipCodeRangeTAFrom, ZipCodeRangeTATo);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult SearchDatabaseHeatMapPermits(string PermitJobType, string PermitWorkType, string FormatedBasePeriodFrom, string FormatedBasePeriodTo, string FormatedAnalysisPeriodFrom
            , string FormatedAnalysisPeriodTo, string BoroughsTA, string DistrictsTA, string ZipCodeRangeTAFrom, string ZipCodeRangeTATo)
        {
            var data = mainDAL.SearchDatabaseHeatMapPermits(PermitJobType, PermitWorkType, FormatedBasePeriodFrom, FormatedBasePeriodTo, FormatedAnalysisPeriodFrom
            , FormatedAnalysisPeriodTo, BoroughsTA, DistrictsTA, ZipCodeRangeTAFrom, ZipCodeRangeTATo);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult GetMyAlerts()
        {
            var data = mainDAL.GetMyAlerts(GlobalVariables.GetFromCookie("NYCUser", "Username"));

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAlert(int AlertID)
        {
            var res = mainDAL.DeleteAlert(AlertID);
            var data = mainDAL.GetMyAlerts(GlobalVariables.GetFromCookie("NYCUser", "Username"));

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyReports()
        {
            var data = mainDAL.GetMyReports(GlobalVariables.GetFromCookie("NYCUser", "Username"));

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowInfoForSelectedAlert(int AlertID)
        {
            var data = mainDAL.ShowInfoForSelectedAlert(AlertID);

            return Json(data, JsonRequestBehavior.AllowGet);
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
                    Username = GlobalVariables.GetFromCookie("NYCUser", "Username"),
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

        public JsonResult DeleteReport(int ReportID)
        {
            var res = mainDAL.DeleteReport(ReportID);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateAlert(string AlertName, string AlertFrequency, string AlertQuery, bool IsPlutoSearch, bool IsEnergySearch, bool IsPermitSearch, bool IsViolationSearch, bool IsEvictionSearch, bool IsElevatorSearch, bool IsPropertySalesSearch, string ProjectSearchAdditional)
        {
            string msg = "Alert created successfully";
            try
            {
                DatabaseMaxValues result = IsPlutoSearch ? mainDAL.GetMaxValues() : new DatabaseMaxValues();
                MyAlert myAlert = new MyAlert()
                {
                    Username = GlobalVariables.GetFromCookie("NYCUser", "Username"),
                    AlertName = AlertName,
                    AlertQuery = AlertQuery,
                    Frequency = Convert.ToInt32(AlertFrequency),
                    Last_OBJECTID = IsPlutoSearch ? result.OBJECTID : null,
                    IsEnergySearch = IsEnergySearch,
                    IsPermitSearch = IsPermitSearch,
                    IsViolationSearch = IsViolationSearch,
                    IsEvictionSearch = IsEvictionSearch,
                    IsElevatorSearch = IsElevatorSearch,
                    IsPropertySalesSearch = IsPropertySalesSearch,
                    ProjectSearchAdditional = ProjectSearchAdditional,
                    DateCreated = DateTime.Now,
                    Last_DateCheck = DateTime.Now,
                    Next_DateCheck = AlertFrequency == "7" ? DateTime.Now.AddDays(7) : DateTime.Now.AddDays(1),
                    IsUnread = false
                };
                db.MyAlerts.Add(myAlert);
                db.SaveChanges();
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(new { msg }, JsonRequestBehavior.AllowGet);
            }
        }
        public string CreateCsvLine(DatabaseAttributes elem, bool isHeader)
        {
            string borough = elem.Borough != null ? elem.Borough : "";
            string address = elem.Address != null ? elem.Address.Replace(",", string.Empty) : "";
            string csvLine = isHeader ? "Borough,Address" : borough + "," + address;
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
                csvLine += isHeader ? ",Owner Name" : "," + elem.OwnerName.Replace(",", string.Empty);
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
            if (elem.DISTRICT != null)
                csvLine += isHeader ? ",District" : "," + elem.DISTRICT;
            if (elem.OrganizationName != null)
            {
                string orgName = elem.OrganizationName.Contains(",") ? String.Format("\"{0}\"", elem.OrganizationName) : elem.OrganizationName;
                csvLine += isHeader ? ",Organization Name" : "," + orgName;
            }
            if (elem.Faith_Based_Organization != null)
                csvLine += isHeader ? ",Faith Based Organization" : "," + elem.Faith_Based_Organization;
            if (elem.Foundation != null)
                csvLine += isHeader ? ",Foundation" : "," + elem.Foundation;
            if (elem.New_York_City_Agency != null)
                csvLine += isHeader ? ",New York City Agency" : "," + elem.New_York_City_Agency;
            if (elem.Nonprofit != null)
                csvLine += isHeader ? ",Nonprofit" : "," + elem.Nonprofit;
            if (elem.elevatordevicetype != null)
                csvLine += isHeader ? ",Elevator Device Type" : "," + elem.elevatordevicetype;
            if (elem.job_number != null)
                csvLine += isHeader ? ",Job Number" : "," + elem.job_number;
            if (elem.filing_type != null)
                csvLine += isHeader ? ",Filing Type" : "," + elem.filing_type;
            if (elem.filing_status != null)
                csvLine += isHeader ? ",Filing Status" : "," + elem.filing_status;
            if (elem.filing_date != null)
                csvLine += isHeader ? ",Filing Date" : "," + elem.filing_date_string_format;
            if (elem.AssessTotPerSqFt != null)
                csvLine += isHeader ? ",Assessed Value per Square Foot" : "," + elem.AssessTotPerSqFt;
            if (elem.sale_date != null)
                csvLine += isHeader ? ",Sale Date" : "," + elem.sale_date_string_format;
            if (elem.sale_price != null)
                csvLine += isHeader ? ",Sale Price" : "," + elem.sale_price;
            if (elem.DESCRIPTION != null)
                csvLine += isHeader ? ",Description" : "," + elem.DESCRIPTION.Replace(",", string.Empty);
            if (elem.BusinessName != null)
                csvLine += isHeader ? ",Business Name" : "," + elem.BusinessName.Replace(",", string.Empty);
            if (elem.GeneralContractor != null)
                csvLine += isHeader ? ",General Contractor" : "," + elem.GeneralContractor.Replace(",", string.Empty);
            if (elem.Architect != null)
                csvLine += isHeader ? ",Architect" : "," + elem.Architect.Replace(",", string.Empty);
            if (elem.TOTAL_CONSTRUCTION_FLOOR_AREA != null)
                csvLine += isHeader ? ",Construction Floor Area" : "," + elem.TOTAL_CONSTRUCTION_FLOOR_AREA;
            if (elem.Proposed_Height != null)
                csvLine += isHeader ? ",Proposed Height" : "," + elem.Proposed_Height;
            if (elem.Proposed_Occupancy != null)
                csvLine += isHeader ? ",Proposed Occupancy" : "," + elem.Proposed_Occupancy;
            if (elem.Pre_Filing_Date != null)
                csvLine += isHeader ? ",Filing Date" : "," + elem.Pre_Filing_Date;
            if (elem.owner_name != null)
                csvLine += isHeader ? ",Owner Name" : "," + elem.owner_name.Replace(",", string.Empty);
            if (elem.owner_bus_name != null)
                csvLine += isHeader ? ",Owner Bus Name" : "," + elem.owner_bus_name.Replace(",", string.Empty);
            if (elem.qewi_name != null)
                csvLine += isHeader ? ",Qewi Name" : "," + elem.qewi_name.Replace(",", string.Empty);
            if (elem.qewi_bus_name != null)
                csvLine += isHeader ? ",Qewi Bus Name" : "," + elem.qewi_bus_name.Replace(",", string.Empty);
            if (elem.RESPONDENT_NAME != null)
                csvLine += isHeader ? ",Respondent Name" : "," + elem.RESPONDENT_NAME.Replace(",", string.Empty);
            if (elem.LandUse != null)
                csvLine += isHeader ? ",Land Use" : "," + elem.LandUse.Replace(",", string.Empty);
            if (elem.VIOLATION_DESCRIPTION != null)
                csvLine += isHeader ? ",Violation Description" : "," + elem.VIOLATION_DESCRIPTION.Replace(",", string.Empty);
            if (elem.Owner_Type != null)
                csvLine += isHeader ? ",Owner Type" : "," + elem.Owner_Type.Replace(",", string.Empty);
            if (elem.issuance_date != null)
                csvLine += isHeader ? ",Permit Issuance date" : "," + elem.issuance_date_string_format;
            if (elem.PermitteName != null)
                csvLine += isHeader ? ",Permitte Name" : "," + elem.PermitteName.Replace(",", string.Empty);
            if (elem.permittee_s_business_name != null)
                csvLine += isHeader ? ",Permittee Business Name" : "," + elem.permittee_s_business_name.Replace(",", string.Empty);
            if (elem.permittee_s_license_type != null)
                csvLine += isHeader ? ",Permittee License Type" : "," + elem.permittee_s_license_type.Replace(",", string.Empty);
            return csvLine;


        }

        public JsonResult UpdatePlutoDatatableTractIDs(string BBLs, int tractID, int OBJECTID)
        {
            var res = mainDAL.UpdatePlutoDatatableTractIDs(BBLs, tractID, OBJECTID);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonaTypeSearchOwner(string term)
        {
            var PersonaTypeSearchOwnerList = mainDAL.GetPersonaTypeSearchOwner(term);
            return Json(new { PersonaTypeSearchOwnerList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonaTypeSearchGeneralContractor(string term)
        {
            var PersonaTypeSearchGeneralContractorList = mainDAL.GetPersonaTypeSearchGeneralContractor(term);
            return Json(new { PersonaTypeSearchGeneralContractorList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonaTypeSearchArchitect(string term)
        {
            var PersonaTypeSearchArchitectList = mainDAL.GetPersonaTypeSearchArchitect(term);
            return Json(new { PersonaTypeSearchArchitectList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPropertySearchJobType(string term)
        {
            var PropertySearchJobTypeList = mainDAL.GetPropertySearchJobType(term);
            return Json(new { PropertySearchJobTypeList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertySearchBorough(string term)
        {
            var PropertySearchBoroughList = mainDAL.GetPropertySearchBorough(term);
            return Json(new { PropertySearchBoroughList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistinctBuildingClass(string term)
        {
            var PropertySearchBuildingClassList = mainDAL.GetDistinctBuildingClass(term);
            return Json(new { PropertySearchBuildingClassList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistinctZoningDistrict(string term)
        {
            var PropertySearchZoningDistrictList = mainDAL.GetDistinctZoningDistrict(term);
            return Json(new { PropertySearchZoningDistrictList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistinctProposedOccupancy(string term)
        {
            var PropertySearchProposedOccupancyList = mainDAL.GetDistinctProposedOccupancy(term);
            return Json(new { PropertySearchProposedOccupancyList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistinctLandUse(string term)
        {
            var LandUseList = mainDAL.GetDistinctLandUse(term);
            return Json(new { LandUseList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBuildingClassInclusionCriteria(string term)
        {
            var BuildingClassICList = mainDAL.GetBuildingClassInclusionCriteria(term);
            return Json(new { BuildingClassICList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBuildingClassExclusionCriteria(string term)
        {
            var BuildingClassECList = mainDAL.GetBuildingClassExclusionCriteria(term);
            return Json(new { BuildingClassECList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnsafeBuildingFacadeConditionAddress(string term)
        {
            var UnsafeBuildingFacadeConditionAddressList = mainDAL.GetUnsafeBuildingFacadeConditionAddress(term);
            return Json(new { UnsafeBuildingFacadeConditionAddressList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnsafeBuildingFacadeConditionOwnerName(string term)
        {
            var UnsafeBuildingFacadeConditionOwnerNameList = mainDAL.GetUnsafeBuildingFacadeConditionOwnerName(term);
            return Json(new { UnsafeBuildingFacadeConditionOwnerNameList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnsafeBuildingFacadeConditionOwnerBusName(string term)
        {
            var UnsafeBuildingFacadeConditionOwnerBusNameList = mainDAL.GetUnsafeBuildingFacadeConditionOwnerBusName(term);
            return Json(new { UnsafeBuildingFacadeConditionOwnerBusNameList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnsafeBuildingFacadeConditionFilingStatus(string term)
        {
            var UnsafeBuildingFacadeConditionFilingStatusList = mainDAL.GetUnsafeBuildingFacadeConditionFilingStatus(term);
            return Json(new { UnsafeBuildingFacadeConditionFilingStatusList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnsafeBuildingFacadeConditionQewiName(string term)
        {
            var UnsafeBuildingFacadeConditionQewiNameList = mainDAL.GetUnsafeBuildingFacadeConditionQewiName(term);
            return Json(new { UnsafeBuildingFacadeConditionQewiNameList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnsafeBuildingFacadeConditionQewiBusName(string term)
        {
            var UnsafeBuildingFacadeConditionQewiBusNameList = mainDAL.GetUnsafeBuildingFacadeConditionQewiBusName(term);
            return Json(new { UnsafeBuildingFacadeConditionQewiBusNameList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConstructionViolationsRespondentName(string term)
        {
            var ConstructionViolationsRespondentNameList = mainDAL.GetConstructionViolationsRespondentName(term);
            return Json(new { ConstructionViolationsRespondentNameList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDOB_Job_Application_Filings_Address(string term)
        {
            var AddressList = mainDAL.GetDOB_Job_Application_Filings_Address(term);
            return Json(new { AddressList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPermitIssuanceOwnerName(string term)
        {
            var OwnerNameList = mainDAL.GetPermitIssuanceOwnerName(term);
            return Json(new { OwnerNameList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPermitIssuancePermitteName(string term)
        {
            var PermitteNameList = mainDAL.GetPermitIssuancePermitteName(term);
            return Json(new { PermitteNameList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPermitIssuancePermitteeLicenseType(string term)
        {
            var PermitteeLicenseTypeList = mainDAL.GetPermitIssuancePermitteeLicenseType(term);
            return Json(new { PermitteeLicenseTypeList }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SearchDatabaseTopRecords(string sqlQuery, string sqlQueryTotalRecords)
        {
            var data = mainDAL.SearchDatabaseTopRecords(sqlQuery, sqlQueryTotalRecords);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult GetCenterPointValues(string centerPoint)
        {
            var data = mainDAL.GetCenterPointValues(centerPoint);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSpiderSearchByCenterPointValue(string centerPoint, string centerPointValue, string value, string whereSpiderChartAdditional)
        {
            var data = mainDAL.GetSpiderSearchByCenterPointValue(centerPoint, centerPointValue, value, whereSpiderChartAdditional);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}