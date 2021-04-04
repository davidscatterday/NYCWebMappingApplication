var maxPropertySearchConstructionFloorArea = 999999999, maxConstructionViolationsBldgArea = 24000000;
var whereOwnerSearchClause = "";
var sqlQueryJobPermit = "select ISNULL(bbl_10_digits, 0) AS BBL, j.Job_Type AS job_type, (j.House + ' ' + j.Street_Name) AS Address, j.Borough, j.TOTAL_CONSTRUCTION_FLOOR_AREA, ISNULL(j.BUILDING_CLASS, '') AS BldgClass, ISNULL(j.Zoning_Dist1, '') AS ZoneDist1, ISNULL(j.Proposed_Height, '') AS Proposed_Height, ISNULL(j.Proposed_Occupancy, '') AS Proposed_Occupancy, (j.Owner_s_First_Name + ' ' + j.Owner_s_Last_Name) AS OwnerName, j.Owner_s_Business_Name AS BusinessName, ISNULL(p.permittee_s_business_name, '') AS GeneralContractor, (j.Applicant_s_First_Name + ' ' + j.Applicant_s_Last_Name) AS Architect, j.Pre_Filing_Date from dbo.DOB_Job_Application_Filings j left join dbo.Permit p on j.Job = p.job__ where ";
var sqlQueryLandSaleDemolition = "select p.bbl_10_digits AS BBL, ps.address AS Address, p.borough AS Borough, building_class_at_present AS BldgClass, ISNULL(year_built, 0) AS YearBuilt, sale_price, sale_date from dbo.Permit p inner join dbo.PropertySales ps on p.bbl_10_digits = ps.bbl where ";
var sqlQueryUnsafeBuildingFacadeCondition = "select s.bbl_10_digits AS BBL, s.borough, s.address, ISNULL(p.ZoneDist1, '') AS ZoneDist1, ISNULL(p.BldgClass, '') AS BldgClass, ISNULL(p.NumFloors, '') AS NumFloors, ISNULL(p.YearBuilt, '') AS YearBuilt, s.owner_name, s.owner_bus_name, s.filing_date, s.filing_status, s.qewi_name, s.qewi_bus_name from dbo.SafetyFacadesComplianceFilings s left join dbo.Pluto p on s.bbl_10_digits = p.BBL where ";
var sqlQueryConstructionViolations = "select p.BBL, p.Borough, p.Address, v.RESPONDENT_NAME, v.issue_date, p.BldgClass, p.LandUse, p.BldgArea, p.AssessTot, v.VIOLATION_DESCRIPTION from dbo.EcbViolations v inner join dbo.Pluto p on v.bbl_10_digits = p.BBL where ";
var sqlQueryPlanApprovalWithNoPermitIssuance = "select j.BBL, j.Borough, j.Address, j.Pre_Filing_Date, j.Zoning_Dist1 AS ZoneDist1, j.Proposed_Occupancy, j.TOTAL_CONSTRUCTION_FLOOR_AREA, j.Owner_Type, (j.Owner_s_First_Name + ' ' + j.Owner_s_Last_Name) AS OwnerName, j.Owner_s_Business_Name AS BusinessName from dbo.DOB_Job_Application_Filings j left join dbo.Permit p on j.BBL = p.bbl_10_digits where ";
var sqlQueryPermitIssuance = "select p.BBL, p.Address, p.Borough, j.issuance_date, p.ZoneDist1, ISNULL(p.BldgArea, 0) AS BldgArea, ISNULL(p.AssessTot, 0) AS AssessTot, ISNULL(p.OwnerName, '') AS OwnerName, (j.permittee_s_first_name + ' ' + j.permittee_s_last_name) AS PermitteName, j.permittee_s_business_name, j.permittee_s_license_type from dbo.Permit j inner join dbo.Pluto p on j.bbl_10_digits = p.BBL where ";
$(function () {
    $("#slider-range-PropertySearchConstructionFloorArea").slider({
        range: true,
        min: 0,
        max: maxPropertySearchConstructionFloorArea,
        values: [0, maxPropertySearchConstructionFloorArea],
        slide: function (event, ui) {
            $("#txtPropertySearchConstructionFloorArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtPropertySearchConstructionFloorArea").val($("#slider-range-PropertySearchConstructionFloorArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-PropertySearchConstructionFloorArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider({
        range: true,
        min: 0,
        max: maxNumberOfFloors,
        values: [0, maxNumberOfFloors],
        slide: function (event, ui) {
            $("#txtUnsafeBuildingFacadeConditionNumFloors").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtUnsafeBuildingFacadeConditionNumFloors").val($("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 1).toLocaleString('en'));

    $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider({
        range: true,
        min: 1650,
        max: new Date().getFullYear(),
        values: [1650, new Date().getFullYear()],
        slide: function (event, ui) {
            $("#txtUnsafeBuildingFacadeConditionYearBuilt").val(ui.values[0] + " - " + ui.values[1]);
        }
    });
    $("#txtUnsafeBuildingFacadeConditionYearBuilt").val($("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 0) +
        " - " + $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 1));

    $("#slider-range-ConstructionViolationsBldgArea").slider({
        range: true,
        min: 0,
        max: maxConstructionViolationsBldgArea,
        values: [0, maxConstructionViolationsBldgArea],
        slide: function (event, ui) {
            $("#txtConstructionViolationsBldgArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtConstructionViolationsBldgArea").val($("#slider-range-ConstructionViolationsBldgArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-ConstructionViolationsBldgArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-ConstructionViolationsAssessTot").slider({
        range: true,
        min: 0,
        max: maxAssessedTotalValue,
        values: [0, maxAssessedTotalValue],
        slide: function (event, ui) {
            $("#txtConstructionViolationsAssessTot").val("$" + ui.values[0].toLocaleString('en') + " - $" + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtConstructionViolationsAssessTot").val("$" + $("#slider-range-ConstructionViolationsAssessTot").slider("values", 0).toLocaleString('en') +
        " - $" + $("#slider-range-ConstructionViolationsAssessTot").slider("values", 1).toLocaleString('en'));

    $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider({
        range: true,
        min: 0,
        max: maxPropertySearchConstructionFloorArea,
        values: [0, maxPropertySearchConstructionFloorArea],
        slide: function (event, ui) {
            $("#txtPlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtPlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").val($("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider({
        range: true,
        min: 0,
        max: maxConstructionViolationsBldgArea,
        values: [0, maxConstructionViolationsBldgArea],
        slide: function (event, ui) {
            $("#txtNewConstructionPermitIssuanceBldgArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtNewConstructionPermitIssuanceBldgArea").val($("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider({
        range: true,
        min: 0,
        max: maxAssessedTotalValue,
        values: [0, maxAssessedTotalValue],
        slide: function (event, ui) {
            $("#txtNewConstructionPermitIssuanceAssessTot").val("$" + ui.values[0].toLocaleString('en') + " - $" + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtNewConstructionPermitIssuanceAssessTot").val("$" + $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 0).toLocaleString('en') +
        " - $" + $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 1).toLocaleString('en'));

    $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider({
        range: true,
        min: 0,
        max: maxConstructionViolationsBldgArea,
        values: [0, maxConstructionViolationsBldgArea],
        slide: function (event, ui) {
            $("#txtMajorAlterationPermitIssuanceBldgArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtMajorAlterationPermitIssuanceBldgArea").val($("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider({
        range: true,
        min: 0,
        max: maxAssessedTotalValue,
        values: [0, maxAssessedTotalValue],
        slide: function (event, ui) {
            $("#txtMajorAlterationPermitIssuanceAssessTot").val("$" + ui.values[0].toLocaleString('en') + " - $" + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtMajorAlterationPermitIssuanceAssessTot").val("$" + $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 0).toLocaleString('en') +
        " - $" + $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 1).toLocaleString('en'));

});

function btnOwnerSearch() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Owner Name", attribute: "OwnerName", dataset: "OwnerSearch" }, { name: "Business Name", attribute: "BusinessName", dataset: "OwnerSearch" }
        , { name: "General Contractor", attribute: "GeneralContractor", dataset: "OwnerSearch" }, { name: "Architect", attribute: "Architect", dataset: "OwnerSearch" }
        , { name: "Job Type", attribute: "job_type", dataset: "OwnerSearch" }, { name: "Construction Floor Area", attribute: "TOTAL_CONSTRUCTION_FLOOR_AREA", dataset: "OwnerSearch" }
        , { name: "Building Class", attribute: "BldgClass", dataset: "OwnerSearch" }, { name: "Zoning District", attribute: "ZoneDist1", dataset: "OwnerSearch" }
        , { name: "Proposed Height", attribute: "Proposed_Height", dataset: "OwnerSearch" }, { name: "Proposed Occupancy", attribute: "Proposed_Occupancy", dataset: "OwnerSearch" }
        , { name: "Filing Date", attribute: "Pre_Filing_Date", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "";
    var PropertySearchConstructionFloorAreaStart = null, PropertySearchConstructionFloorAreaEnd = null;
    var PersonaTypeSearchOwner = document.getElementById("txtPersonaTypeSearchOwner").value;
    var PersonaTypeSearchGeneralContractor = document.getElementById("txtPersonaTypeSearchGeneralContractor").value;
    var PersonaTypeSearchArchitect = document.getElementById("txtPersonaTypeSearchArchitect").value;

    var PropertySearchJobType = $(txtPropertySearchJobType).val();
    var PropertySearchBorough = $(txtPropertySearchBorough).val();
    var PropertySearchBuildingClass = $(txtPropertySearchBuildingClass).val();
    var PropertySearchZoningDistrict = $(txtPropertySearchZoningDistrict).val();
    var PropertySearchProposedOccupancy = $(txtPropertySearchProposedOccupancy).val();
    if (PersonaTypeSearchOwner != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "(j.Owner_s_First_Name + ' ' + j.Owner_s_Last_Name IN (" + PersonaTypeSearchOwner + ") OR j.Owner_s_Business_Name IN (" + PersonaTypeSearchOwner + "))";
        }
        else {
            whereOwnerSearchClause += " AND (j.Owner_s_First_Name + ' ' + j.Owner_s_Last_Name IN (" + PersonaTypeSearchOwner + ") OR j.Owner_s_Business_Name IN (" + PersonaTypeSearchOwner + "))";
        }
    }
    if (PersonaTypeSearchGeneralContractor != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "(p.permittee_s_business_name IN (" + PersonaTypeSearchGeneralContractor + "))";
        }
        else {
            whereOwnerSearchClause += " AND (p.permittee_s_business_name IN (" + PersonaTypeSearchGeneralContractor + "))";
        }
    }
    if (PersonaTypeSearchArchitect != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "(j.Applicant_s_First_Name + ' ' + j.Applicant_s_Last_Name IN (" + PersonaTypeSearchArchitect + "))";
        }
        else {
            whereOwnerSearchClause += " AND (j.Applicant_s_First_Name + ' ' + j.Applicant_s_Last_Name IN (" + PersonaTypeSearchArchitect + "))";
        }
    }
    if (PropertySearchJobType != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.job_type IN (" + PropertySearchJobType + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.job_type IN (" + PropertySearchJobType + ")";
        }
    }
    if (PropertySearchBorough != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Borough IN (" + PropertySearchBorough + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Borough IN (" + PropertySearchBorough + ")";
        }
    }
    if (document.getElementById("cbPropertySearchConstructionFloorArea").checked == true) {
        PropertySearchConstructionFloorAreaStart = $("#slider-range-PropertySearchConstructionFloorArea").slider("values", 0);
        PropertySearchConstructionFloorAreaEnd = $("#slider-range-PropertySearchConstructionFloorArea").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "Cast(j.TOTAL_CONSTRUCTION_FLOOR_AREA AS int) >= " + PropertySearchConstructionFloorAreaStart + " AND Cast(j.TOTAL_CONSTRUCTION_FLOOR_AREA AS int) <= " + PropertySearchConstructionFloorAreaEnd;
        }
        else {
            whereOwnerSearchClause += " AND Cast(j.TOTAL_CONSTRUCTION_FLOOR_AREA AS int) >= " + PropertySearchConstructionFloorAreaStart + " AND Cast(j.TOTAL_CONSTRUCTION_FLOOR_AREA AS int) <= " + PropertySearchConstructionFloorAreaEnd;
        }
    }
    if (PropertySearchBuildingClass != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.BUILDING_CLASS IN (" + PropertySearchBuildingClass + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.BUILDING_CLASS IN (" + PropertySearchBuildingClass + ")";
        }
    }
    if (PropertySearchZoningDistrict != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Zoning_Dist1 IN (" + PropertySearchZoningDistrict + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Zoning_Dist1 IN (" + PropertySearchZoningDistrict + ")";
        }
    }
    if (PropertySearchProposedOccupancy != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Proposed_Occupancy IN (" + PropertySearchProposedOccupancy + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Proposed_Occupancy IN (" + PropertySearchProposedOccupancy + ")";
        }
    }
    if (document.getElementById("cbFilingDate").checked == true) {
        FilingDateFrom = document.getElementById("txtFilingDateFrom").value;
        FilingDateTo = document.getElementById("txtFilingDateTo").value;
        if (FilingDateFrom != "" || FilingDateTo != "") {
            if (whereOwnerSearchClause != "") {
                whereOwnerSearchClause += " AND ";
            }
            if (FilingDateFrom != "" && FilingDateTo != "") {
                var dFrom = new Date(FilingDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(FilingDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "CAST(j.Pre_Filing_Date AS datetime) >= '" + valueFrom + "' AND CAST(j.Pre_Filing_Date AS datetime) <= '" + valueTo + "'";
            }
            else if (FilingDateFrom != "") {
                var dFrom = new Date(FilingDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereOwnerSearchClause += "CAST(j.Pre_Filing_Date AS datetime) >= '" + valueFrom + "'";
            }
            else if (FilingDateTo != "") {
                var dTo = new Date(FilingDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "CAST(j.Pre_Filing_Date AS datetime) <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        sqlQuery = sqlQueryJobPermit + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}

function btnResetOwnerSearch() {
    $("#txtPersonaTypeSearchOwner").select2("val", "");
    $("#txtPersonaTypeSearchGeneralContractor").select2("val", "");
    $("#txtPersonaTypeSearchArchitect").select2("val", "");

    $("#txtPropertySearchJobType").select2("val", "");
    $("#txtPropertySearchBorough").select2("val", "");
    $("#txtPropertySearchBuildingClass").select2("val", "");
    $("#txtPropertySearchZoningDistrict").select2("val", "");
    $("#txtPropertySearchProposedOccupancy").select2("val", "");
    document.getElementById("cbPropertySearchConstructionFloorArea").checked = false;
    $("#slider-range-PropertySearchConstructionFloorArea").slider("values", 0, 0);
    $("#slider-range-PropertySearchConstructionFloorArea").slider("values", 1, maxPropertySearchConstructionFloorArea);
    $("#txtPropertySearchConstructionFloorArea").val($("#slider-range-PropertySearchConstructionFloorArea").slider("values", 0) + " - " + $("#slider-range-PropertySearchConstructionFloorArea").slider("values", 1).toLocaleString('en'));
    document.getElementById("cbFilingDate").checked = false;
    document.getElementById("txtFilingDateFrom").value = "";
    document.getElementById("txtFilingDateTo").value = "";

    document.getElementById("cbSaleDateLSD").checked = false;
    document.getElementById("txtSaleDateLSDFrom").value = "";
    document.getElementById("txtSaleDateLSDTo").value = "";

    $("#txtUnsafeBuildingFacadeConditionAddress").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionBorough").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionZoningDistrict").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionBuildingClass").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionOwnerName").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionOwnerBusName").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionFilingStatus").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionQewiName").select2("val", "");
    $("#txtUnsafeBuildingFacadeConditionQewiBusName").select2("val", "");
    document.getElementById("cbFilingDateUbfc").checked = false;
    document.getElementById("txtFilingDateUbfcFrom").value = "";
    document.getElementById("txtFilingDateUbfcTo").value = "";
    document.getElementById("cbUnsafeBuildingFacadeConditionNumFloors").checked = false;
    $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 0, 0);
    $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 1, maxNumberOfFloors);
    $("#txtUnsafeBuildingFacadeConditionNumFloors").val($("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 0) + " - " + $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 1).toLocaleString('en'));
    document.getElementById("cbUnsafeBuildingFacadeConditionYearBuilt").checked = false;
    $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 0, 1650);
    $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 1, new Date().getFullYear());
    $("#txtUnsafeBuildingFacadeConditionYearBuilt").val($("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 0) + " - " + $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 1).toLocaleString('en'));

    $("#txtConstructionViolationsAddress").select2("val", "");
    $("#txtConstructionViolationsRespondentName").select2("val", "");
    $("#txtConstructionViolationsBuildingClass").select2("val", "");
    $("#txtConstructionViolationsLandUse").select2("val", "");
    document.getElementById("cbIssueDateCV").checked = false;
    document.getElementById("txtIssueDateCVFrom").value = "";
    document.getElementById("txtIssueDateCVTo").value = "";
    document.getElementById("cbConstructionViolationsBldgArea").checked = false;
    $("#slider-range-ConstructionViolationsBldgArea").slider("values", 0, 0);
    $("#slider-range-ConstructionViolationsBldgArea").slider("values", 1, maxConstructionViolationsBldgArea);
    $("#txtConstructionViolationsBldgArea").val($("#slider-range-ConstructionViolationsBldgArea").slider("values", 0) + " - " + $("#slider-range-ConstructionViolationsBldgArea").slider("values", 1).toLocaleString('en'));
    document.getElementById("cbConstructionViolationsAssessTot").checked = false;
    $("#slider-range-ConstructionViolationsAssessTot").slider("values", 0, 0);
    $("#slider-range-ConstructionViolationsAssessTot").slider("values", 1, maxAssessedTotalValue);
    $("#txtConstructionViolationsAssessTot").val($("#slider-range-ConstructionViolationsAssessTot").slider("values", 0) + " - " + $("#slider-range-ConstructionViolationsAssessTot").slider("values", 1).toLocaleString('en'));

    $("#txtPlanApprovalWithNoPermitIssuanceAddress").select2("val", "");
    $("#txtPlanApprovalWithNoPermitIssuanceBorough").select2("val", "");
    $("#txtPlanApprovalWithNoPermitIssuanceZoningDistrict").select2("val", "");
    $("#txtPlanApprovalWithNoPermitIssuanceProposedOccupancy").select2("val", "");
    document.getElementById("cbFilingDatePAWNPI").checked = false;
    document.getElementById("txtFilingDatePAWNPIFrom").value = "";
    document.getElementById("txtFilingDatePAWNPITo").value = "";
    document.getElementById("cbPlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").checked = false;
    $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 0, 0);
    $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 1, maxPropertySearchConstructionFloorArea);
    $("#txtPlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").val($("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 0) + " - " + $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 1).toLocaleString('en'));

    $("#txtNewConstructionPermitIssuanceAddress").select2("val", "");
    $("#txtNewConstructionPermitIssuanceBorough").select2("val", "");
    $("#txtNewConstructionPermitIssuanceZoningDistrict").select2("val", "");
    $("#txtNewConstructionPermitIssuanceOwnerName").select2("val", "");
    $("#txtNewConstructionPermitIssuancePermitteName").select2("val", "");
    $("#txtNewConstructionPermitIssuancePermitteeBusinessName").select2("val", "");
    $("#txtNewConstructionPermitIssuancePermitteeLicenseType").select2("val", "");
    document.getElementById("cbIssuanceDateNCPI").checked = false;
    document.getElementById("txtIssuanceDateNCPIFrom").value = "";
    document.getElementById("txtIssuanceDateNCPITo").value = "";
    document.getElementById("cbNewConstructionPermitIssuanceBldgArea").checked = false;
    $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 0, 0);
    $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 1, maxConstructionViolationsBldgArea);
    $("#txtNewConstructionPermitIssuanceBldgArea").val($("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 0) + " - " + $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 1).toLocaleString('en'));
    document.getElementById("cbNewConstructionPermitIssuanceAssessTot").checked = false;
    $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 0, 0);
    $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 1, maxAssessedTotalValue);
    $("#txtNewConstructionPermitIssuanceAssessTot").val($("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 0) + " - " + $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 1).toLocaleString('en'));

    $("#txtMajorAlterationPermitIssuanceAddress").select2("val", "");
    $("#txtMajorAlterationPermitIssuanceBorough").select2("val", "");
    $("#txtMajorAlterationPermitIssuanceZoningDistrict").select2("val", "");
    $("#txtMajorAlterationPermitIssuanceOwnerName").select2("val", "");
    $("#txtMajorAlterationPermitIssuancePermitteName").select2("val", "");
    $("#txtMajorAlterationPermitIssuancePermitteeBusinessName").select2("val", "");
    $("#txtMajorAlterationPermitIssuancePermitteeLicenseType").select2("val", "");
    document.getElementById("cbIssuanceDateMAPI").checked = false;
    document.getElementById("txtIssuanceDateMAPIFrom").value = "";
    document.getElementById("txtIssuanceDateMAPITo").value = "";
    document.getElementById("cbMajorAlterationPermitIssuanceBldgArea").checked = false;
    $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 0, 0);
    $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 1, maxConstructionViolationsBldgArea);
    $("#txtMajorAlterationPermitIssuanceBldgArea").val($("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 0) + " - " + $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 1).toLocaleString('en'));
    document.getElementById("cbMajorAlterationPermitIssuanceAssessTot").checked = false;
    $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 0, 0);
    $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 1, maxAssessedTotalValue);
    $("#txtMajorAlterationPermitIssuanceAssessTot").val($("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 0) + " - " + $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 1).toLocaleString('en'));

    $('.panel-collapse.in').collapse('toggle');
    hideBottomPanel();

    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
}

function btnOwnerSearchLandSaleDemolition() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Building Class", attribute: "BldgClass", dataset: "OwnerSearch" }, { name: "Year Built", attribute: "YearBuilt", dataset: "OwnerSearch" }
        , { name: "Sale Price", attribute: "sale_price", dataset: "OwnerSearch" }, { name: "Sale Date", attribute: "sale_date", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "p.job_type = 'DM'";
    if (document.getElementById("cbSaleDateLSD").checked == true) {
        SaleDateLSDFrom = document.getElementById("txtSaleDateLSDFrom").value;
        SaleDateLSDTo = document.getElementById("txtSaleDateLSDTo").value;
        if (SaleDateLSDFrom != "" || SaleDateLSDTo != "") {
            if (whereOwnerSearchClause != "") {
                whereOwnerSearchClause += " AND ";
            }
            if (SaleDateLSDFrom != "" && SaleDateLSDTo != "") {
                var dFrom = new Date(SaleDateLSDFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(SaleDateLSDTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "ps.sale_date >= '" + valueFrom + "' AND ps.sale_date <= '" + valueTo + "'";
            }
            else if (SaleDateLSDFrom != "") {
                var dFrom = new Date(SaleDateLSDFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereOwnerSearchClause += "ps.sale_date >= '" + valueFrom + "'";
            }
            else if (SaleDateLSDTo != "") {
                var dTo = new Date(SaleDateLSDTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "ps.sale_date <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        sqlQuery = sqlQueryLandSaleDemolition + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}

function btnOwnerSearchUnsafeBuildingFacadeCondition() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Zone District", attribute: "ZoneDist1", dataset: "OwnerSearch" }, { name: "Building Class", attribute: "BldgClass", dataset: "OwnerSearch" }
        , { name: "Number of Floors", attribute: "NumFloors", dataset: "OwnerSearch" }, { name: "Year Built", attribute: "YearBuilt", dataset: "OwnerSearch" }
        , { name: "Owner Name", attribute: "owner_name", dataset: "OwnerSearch" }, { name: "Owner Bus Name", attribute: "owner_bus_name", dataset: "OwnerSearch" }
        , { name: "Filing Date", attribute: "filing_date", dataset: "OwnerSearch" }, { name: "Filing Status", attribute: "filing_status", dataset: "OwnerSearch" }
        , { name: "Qewi Name", attribute: "qewi_name", dataset: "OwnerSearch" }, { name: "Qewi Bus Name", attribute: "qewi_bus_name", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "";
    var UnsafeBuildingFacadeConditionNumFloorsStart = null, UnsafeBuildingFacadeConditionNumFloorsEnd = null, UnsafeBuildingFacadeConditionYearBuiltStart = null, UnsafeBuildingFacadeConditionYearBuiltEnd = null;
    var UnsafeBuildingFacadeConditionAddress = document.getElementById("txtUnsafeBuildingFacadeConditionAddress").value;
    var UnsafeBuildingFacadeConditionBorough = document.getElementById("txtUnsafeBuildingFacadeConditionBorough").value;
    var UnsafeBuildingFacadeConditionZoningDistrict = document.getElementById("txtUnsafeBuildingFacadeConditionZoningDistrict").value;
    var UnsafeBuildingFacadeConditionBuildingClass = document.getElementById("txtUnsafeBuildingFacadeConditionBuildingClass").value;
    var UnsafeBuildingFacadeConditionOwnerName = document.getElementById("txtUnsafeBuildingFacadeConditionOwnerName").value;
    var UnsafeBuildingFacadeConditionOwnerBusName = document.getElementById("txtUnsafeBuildingFacadeConditionOwnerBusName").value;
    var UnsafeBuildingFacadeConditionFilingStatus = document.getElementById("txtUnsafeBuildingFacadeConditionFilingStatus").value;
    var UnsafeBuildingFacadeConditionQewiName = document.getElementById("txtUnsafeBuildingFacadeConditionQewiName").value;
    var UnsafeBuildingFacadeConditionQewiBusName = document.getElementById("txtUnsafeBuildingFacadeConditionQewiBusName").value;

    if (document.getElementById("cbUnsafeBuildingFacadeConditionNumFloors").checked == true) {
        UnsafeBuildingFacadeConditionNumFloorsStart = $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 0);
        UnsafeBuildingFacadeConditionNumFloorsEnd = $("#slider-range-UnsafeBuildingFacadeConditionNumFloors").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.NumFloors >= " + UnsafeBuildingFacadeConditionNumFloorsStart + " AND p.NumFloors <= " + UnsafeBuildingFacadeConditionNumFloorsEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.NumFloors >= " + UnsafeBuildingFacadeConditionNumFloorsStart + " AND p.NumFloors <= " + UnsafeBuildingFacadeConditionNumFloorsEnd;
        }
    }
    if (document.getElementById("cbUnsafeBuildingFacadeConditionYearBuilt").checked == true) {
        UnsafeBuildingFacadeConditionYearBuiltStart = $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 0);
        UnsafeBuildingFacadeConditionYearBuiltEnd = $("#slider-range-UnsafeBuildingFacadeConditionYearBuilt").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.YearBuilt >= " + UnsafeBuildingFacadeConditionYearBuiltStart + " AND p.YearBuilt <= " + UnsafeBuildingFacadeConditionYearBuiltEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.YearBuilt >= " + UnsafeBuildingFacadeConditionYearBuiltStart + " AND p.YearBuilt <= " + UnsafeBuildingFacadeConditionYearBuiltEnd;
        }
    }
    if (UnsafeBuildingFacadeConditionAddress != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "s.address IN (" + UnsafeBuildingFacadeConditionAddress + ")";
        }
        else {
            whereOwnerSearchClause += " AND s.address IN (" + UnsafeBuildingFacadeConditionAddress + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionBorough != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.borough IN (" + UnsafeBuildingFacadeConditionBorough + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.borough IN (" + UnsafeBuildingFacadeConditionBorough + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionZoningDistrict != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.ZoneDist1 IN (" + UnsafeBuildingFacadeConditionZoningDistrict + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.ZoneDist1 IN (" + UnsafeBuildingFacadeConditionZoningDistrict + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionBuildingClass != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.BldgClass IN (" + UnsafeBuildingFacadeConditionBuildingClass + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.BldgClass IN (" + UnsafeBuildingFacadeConditionBuildingClass + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionOwnerName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "s.owner_name IN (" + UnsafeBuildingFacadeConditionOwnerName + ")";
        }
        else {
            whereOwnerSearchClause += " AND s.owner_name IN (" + UnsafeBuildingFacadeConditionOwnerName + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionOwnerBusName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "s.owner_bus_name IN (" + UnsafeBuildingFacadeConditionOwnerBusName + ")";
        }
        else {
            whereOwnerSearchClause += " AND s.owner_bus_name IN (" + UnsafeBuildingFacadeConditionOwnerBusName + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionFilingStatus != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "s.filing_status IN (" + UnsafeBuildingFacadeConditionFilingStatus + ")";
        }
        else {
            whereOwnerSearchClause += " AND s.filing_status IN (" + UnsafeBuildingFacadeConditionFilingStatus + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionQewiName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "s.qewi_name IN (" + UnsafeBuildingFacadeConditionQewiName + ")";
        }
        else {
            whereOwnerSearchClause += " AND s.qewi_name IN (" + UnsafeBuildingFacadeConditionQewiName + ")";
        }
    }
    if (UnsafeBuildingFacadeConditionQewiBusName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "s.qewi_bus_name IN (" + UnsafeBuildingFacadeConditionQewiBusName + ")";
        }
        else {
            whereOwnerSearchClause += " AND s.qewi_bus_name IN (" + UnsafeBuildingFacadeConditionQewiBusName + ")";
        }
    }
    if (document.getElementById("cbFilingDateUbfc").checked == true) {
        FilingDateUbfcFrom = document.getElementById("txtFilingDateUbfcFrom").value;
        FilingDateUbfcTo = document.getElementById("txtFilingDateUbfcTo").value;
        if (FilingDateUbfcFrom != "" || FilingDateUbfcTo != "") {
            if (whereOwnerSearchClause != "") {
                whereOwnerSearchClause += " AND ";
            }
            if (FilingDateUbfcFrom != "" && FilingDateUbfcTo != "") {
                var dFrom = new Date(FilingDateUbfcFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(FilingDateUbfcTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "s.filing_date >= '" + valueFrom + "' AND s.filing_date <= '" + valueTo + "'";
            }
            else if (FilingDateUbfcFrom != "") {
                var dFrom = new Date(FilingDateUbfcFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereOwnerSearchClause += "s.filing_date >= '" + valueFrom + "'";
            }
            else if (FilingDateUbfcTo != "") {
                var dTo = new Date(FilingDateUbfcTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "s.filing_date <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        if (whereOwnerSearchClause != "") {
            whereOwnerSearchClause = " where " + whereOwnerSearchClause;
        }
        sqlQuery = sqlQueryUnsafeBuildingFacadeCondition + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}

function btnOwnerSearchConstructionViolations() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Respondent Name", attribute: "RESPONDENT_NAME", dataset: "OwnerSearch" }, { name: "Issue Date", attribute: "issue_date", dataset: "OwnerSearch" }
        , { name: "Building Class", attribute: "BldgClass", dataset: "OwnerSearch" }, { name: "Land Use", attribute: "LandUse", dataset: "OwnerSearch" }
        , { name: "Building Area", attribute: "BldgArea", dataset: "OwnerSearch" }, { name: "Assessed Total Value", attribute: "AssessTot", dataset: "OwnerSearch" }
        , { name: "Violation Description", attribute: "VIOLATION_DESCRIPTION", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "";
    var ConstructionViolationsBldgAreaStart = null, ConstructionViolationsBldgAreaEnd = null, ConstructionViolationsAssessTotStart = null, ConstructionViolationsAssessTotEnd = null;
    var ConstructionViolationsAddress = document.getElementById("txtConstructionViolationsAddress").value;
    var ConstructionViolationsRespondentName = document.getElementById("txtConstructionViolationsRespondentName").value;
    var ConstructionViolationsBuildingClass = document.getElementById("txtConstructionViolationsBuildingClass").value;
    var ConstructionViolationsLandUse = document.getElementById("txtConstructionViolationsLandUse").value;

    if (document.getElementById("cbConstructionViolationsBldgArea").checked == true) {
        ConstructionViolationsBldgAreaStart = $("#slider-range-ConstructionViolationsBldgArea").slider("values", 0);
        ConstructionViolationsBldgAreaEnd = $("#slider-range-ConstructionViolationsBldgArea").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.BldgArea >= " + ConstructionViolationsBldgAreaStart + " AND p.BldgArea <= " + ConstructionViolationsBldgAreaEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.BldgArea >= " + ConstructionViolationsBldgAreaStart + " AND p.BldgArea <= " + ConstructionViolationsBldgAreaEnd;
        }
    }
    if (document.getElementById("cbConstructionViolationsAssessTot").checked == true) {
        ConstructionViolationsAssessTotStart = $("#slider-range-ConstructionViolationsAssessTot").slider("values", 0);
        ConstructionViolationsAssessTotEnd = $("#slider-range-ConstructionViolationsAssessTot").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.AssessTot >= " + ConstructionViolationsAssessTotStart + " AND p.AssessTot <= " + ConstructionViolationsAssessTotEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.AssessTot >= " + ConstructionViolationsAssessTotStart + " AND p.AssessTot <= " + ConstructionViolationsAssessTotEnd;
        }
    }
    if (ConstructionViolationsAddress != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.Address IN (" + ConstructionViolationsAddress + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.Address IN (" + ConstructionViolationsAddress + ")";
        }
    }
    if (ConstructionViolationsRespondentName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "v.RESPONDENT_NAME IN (" + ConstructionViolationsRespondentName + ")";
        }
        else {
            whereOwnerSearchClause += " AND v.RESPONDENT_NAME IN (" + ConstructionViolationsRespondentName + ")";
        }
    }
    if (ConstructionViolationsBuildingClass != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.BldgClass IN (" + ConstructionViolationsBuildingClass + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.BldgClass IN (" + ConstructionViolationsBuildingClass + ")";
        }
    }
    if (ConstructionViolationsLandUse != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.LandUse IN (" + ConstructionViolationsLandUse + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.LandUse IN (" + ConstructionViolationsLandUse + ")";
        }
    }
    if (document.getElementById("cbIssueDateCV").checked == true) {
        IssueDateCVFrom = document.getElementById("txtIssueDateCVFrom").value;
        IssueDateCVTo = document.getElementById("txtIssueDateCVTo").value;
        if (IssueDateCVFrom != "" || IssueDateCVTo != "") {
            IsViolationSearch = true;
            if (whereViolationClause != "") {
                whereViolationClause += " AND ";
            }
            if (IssueDateCVFrom != "" && IssueDateCVTo != "") {
                var dFrom = new Date(IssueDateCVFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + monthFrom + dayFrom;

                var dTo = new Date(IssueDateCVTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + monthTo + dayTo;
                whereViolationClause += "v.issue_date >= '" + valueFrom + "' AND v.issue_date <= '" + valueTo + "'";
            }
            else if (IssueDateCVFrom != "") {
                var dFrom = new Date(IssueDateCVFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + monthFrom + dayFrom;
                whereViolationClause += "v.issue_date >= '" + valueFrom + "'";
            }
            else if (IssueDateCVTo != "") {
                var dTo = new Date(IssueDateCVTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + monthTo + dayTo;
                whereViolationClause += "v.issue_date <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        sqlQuery = sqlQueryConstructionViolations + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}

function btnOwnerSearchPlanApprovalWithNoPermitIssuance() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Filing Date", attribute: "Pre_Filing_Date", dataset: "OwnerSearch" }, { name: "Zoning District", attribute: "ZoneDist1", dataset: "OwnerSearch" }
        , { name: "Proposed Occupancy", attribute: "Proposed_Occupancy", dataset: "OwnerSearch" }, { name: "Total Construction Floor Area", attribute: "TOTAL_CONSTRUCTION_FLOOR_AREA", dataset: "OwnerSearch" }
        , { name: "Owner Type", attribute: "Owner_Type", dataset: "OwnerSearch" }, { name: "Owner Name", attribute: "OwnerName", dataset: "OwnerSearch" }
        , { name: "Owner Business Name", attribute: "BusinessName", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "Cast(j.Pre_Filing_Date as datetime) > '01-01-2020' AND p.bbl_10_digits IS NULL";
    var PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaStart = null, PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaEnd = null;
    var PlanApprovalWithNoPermitIssuanceAddress = document.getElementById("txtPlanApprovalWithNoPermitIssuanceAddress").value;
    var PlanApprovalWithNoPermitIssuanceBorough = document.getElementById("txtPlanApprovalWithNoPermitIssuanceBorough").value;
    var PlanApprovalWithNoPermitIssuanceZoningDistrict = document.getElementById("txtPlanApprovalWithNoPermitIssuanceZoningDistrict").value;
    var PlanApprovalWithNoPermitIssuanceProposedOccupancy = document.getElementById("txtPlanApprovalWithNoPermitIssuanceProposedOccupancy").value;
    
    if (document.getElementById("cbPlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").checked == true) {
        PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaStart = $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 0);
        PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaEnd = $("#slider-range-PlanApprovalWithNoPermitIssuanceTotalConstructionFloorArea").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.TOTAL_CONSTRUCTION_FLOOR_AREA >= " + PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaStart + " AND j.TOTAL_CONSTRUCTION_FLOOR_AREA <= " + PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaEnd;
        }
        else {
            whereOwnerSearchClause += " AND j.TOTAL_CONSTRUCTION_FLOOR_AREA >= " + PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaStart + " AND j.TOTAL_CONSTRUCTION_FLOOR_AREA <= " + PlanApprovalWithNoPermitIssuanceTotalConstructionFloorAreaEnd;
        }
    }
    if (PlanApprovalWithNoPermitIssuanceAddress != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Address IN (" + PlanApprovalWithNoPermitIssuanceAddress + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Address IN (" + PlanApprovalWithNoPermitIssuanceAddress + ")";
        }
    }
    if (PlanApprovalWithNoPermitIssuanceBorough != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Borough IN (" + PlanApprovalWithNoPermitIssuanceBorough + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Borough IN (" + PlanApprovalWithNoPermitIssuanceBorough + ")";
        }
    }
    if (PlanApprovalWithNoPermitIssuanceZoningDistrict != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Zoning_Dist1 IN (" + PlanApprovalWithNoPermitIssuanceZoningDistrict + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Zoning_Dist1 IN (" + PlanApprovalWithNoPermitIssuanceZoningDistrict + ")";
        }
    }
    if (PlanApprovalWithNoPermitIssuanceProposedOccupancy != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.Proposed_Occupancy IN (" + PlanApprovalWithNoPermitIssuanceProposedOccupancy + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.Proposed_Occupancy IN (" + PlanApprovalWithNoPermitIssuanceProposedOccupancy + ")";
        }
    }
    if (document.getElementById("cbFilingDatePAWNPI").checked == true) {
        FilingDatePAWNPIFrom = document.getElementById("txtFilingDatePAWNPIFrom").value;
        FilingDatePAWNPITo = document.getElementById("txtFilingDatePAWNPITo").value;
        if (FilingDatePAWNPIFrom != "" || FilingDatePAWNPITo != "") {
            if (whereOwnerSearchClause != "") {
                whereOwnerSearchClause += " AND ";
            }
            if (FilingDatePAWNPIFrom != "" && FilingDatePAWNPITo != "") {
                var dFrom = new Date(FilingDatePAWNPIFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(FilingDatePAWNPITo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "CAST(j.Pre_Filing_Date AS datetime) >= '" + valueFrom + "' AND CAST(j.Pre_Filing_Date AS datetime) <= '" + valueTo + "'";
            }
            else if (FilingDatePAWNPIFrom != "") {
                var dFrom = new Date(FilingDatePAWNPIFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereOwnerSearchClause += "CAST(j.Pre_Filing_Date AS datetime) >= '" + valueFrom + "'";
            }
            else if (FilingDatePAWNPITo != "") {
                var dTo = new Date(FilingDatePAWNPITo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "CAST(j.Pre_Filing_Date AS datetime) <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        sqlQuery = sqlQueryPlanApprovalWithNoPermitIssuance + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}

function btnOwnerSearchNewConstructionPermitIssuance() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Permit Issuance date", attribute: "issuance_date", dataset: "OwnerSearch" }, { name: "Zoning District", attribute: "ZoneDist1", dataset: "OwnerSearch" }
        , { name: "Building Area", attribute: "BldgArea", dataset: "OwnerSearch" }, { name: "Assessed Property Value", attribute: "AssessTot", dataset: "OwnerSearch" }
        , { name: "Owner Name", attribute: "OwnerName", dataset: "OwnerSearch" }, { name: "Permitte Name", attribute: "PermitteName", dataset: "OwnerSearch" }
        , { name: "Permittee Business Name", attribute: "permittee_s_business_name", dataset: "OwnerSearch" }, { name: "Permittee License Type", attribute: "permittee_s_license_type", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "j.job_type = 'NB'";
    var NewConstructionPermitIssuanceBldgAreaStart = null, NewConstructionPermitIssuanceBldgAreaEnd = null, NewConstructionPermitIssuanceAssessTotStart = null, NewConstructionPermitIssuanceAssessTotEnd = null;
    var NewConstructionPermitIssuanceAddress = document.getElementById("txtNewConstructionPermitIssuanceAddress").value;
    var NewConstructionPermitIssuanceBorough = document.getElementById("txtNewConstructionPermitIssuanceBorough").value;
    var NewConstructionPermitIssuanceZoningDistrict = document.getElementById("txtNewConstructionPermitIssuanceZoningDistrict").value;
    var NewConstructionPermitIssuanceOwnerName = document.getElementById("txtNewConstructionPermitIssuanceOwnerName").value;
    var NewConstructionPermitIssuancePermitteName = document.getElementById("txtNewConstructionPermitIssuancePermitteName").value;
    var NewConstructionPermitIssuancePermitteeBusinessName = document.getElementById("txtNewConstructionPermitIssuancePermitteeBusinessName").value;
    var NewConstructionPermitIssuancePermitteeLicenseType = document.getElementById("txtNewConstructionPermitIssuancePermitteeLicenseType").value;

    if (document.getElementById("cbNewConstructionPermitIssuanceBldgArea").checked == true) {
        NewConstructionPermitIssuanceBldgAreaStart = $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 0);
        NewConstructionPermitIssuanceBldgAreaEnd = $("#slider-range-NewConstructionPermitIssuanceBldgArea").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.BldgArea >= " + NewConstructionPermitIssuanceBldgAreaStart + " AND p.BldgArea <= " + NewConstructionPermitIssuanceBldgAreaEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.BldgArea >= " + NewConstructionPermitIssuanceBldgAreaStart + " AND p.BldgArea <= " + NewConstructionPermitIssuanceBldgAreaEnd;
        }
    }
    if (document.getElementById("cbNewConstructionPermitIssuanceAssessTot").checked == true) {
        NewConstructionPermitIssuanceAssessTotStart = $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 0);
        NewConstructionPermitIssuanceAssessTotEnd = $("#slider-range-NewConstructionPermitIssuanceAssessTot").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.AssessTot >= " + NewConstructionPermitIssuanceAssessTotStart + " AND p.AssessTot <= " + NewConstructionPermitIssuanceAssessTotEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.AssessTot >= " + NewConstructionPermitIssuanceAssessTotStart + " AND p.AssessTot <= " + NewConstructionPermitIssuanceAssessTotEnd;
        }
    }
    if (NewConstructionPermitIssuanceAddress != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.Address IN (" + NewConstructionPermitIssuanceAddress + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.Address IN (" + NewConstructionPermitIssuanceAddress + ")";
        }
    }
    if (NewConstructionPermitIssuanceBorough != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.Borough IN (" + NewConstructionPermitIssuanceBorough + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.Borough IN (" + NewConstructionPermitIssuanceBorough + ")";
        }
    }
    if (NewConstructionPermitIssuanceZoningDistrict != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.ZoneDist1 IN (" + NewConstructionPermitIssuanceZoningDistrict + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.ZoneDist1 IN (" + NewConstructionPermitIssuanceZoningDistrict + ")";
        }
    }
    if (NewConstructionPermitIssuanceOwnerName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.OwnerName IN (" + NewConstructionPermitIssuanceOwnerName + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.OwnerName IN (" + NewConstructionPermitIssuanceOwnerName + ")";
        }
    }
    if (NewConstructionPermitIssuancePermitteName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.permittee_s_first_name + ' ' + j.permittee_s_last_name IN (" + NewConstructionPermitIssuancePermitteName + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.permittee_s_first_name + ' ' + j.permittee_s_last_name IN (" + NewConstructionPermitIssuancePermitteName + ")";
        }
    }
    if (NewConstructionPermitIssuancePermitteeBusinessName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.permittee_s_business_name IN (" + NewConstructionPermitIssuancePermitteeBusinessName + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.permittee_s_business_name IN (" + NewConstructionPermitIssuancePermitteeBusinessName + ")";
        }
    }
    if (NewConstructionPermitIssuancePermitteeLicenseType != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.permittee_s_license_type IN (" + NewConstructionPermitIssuancePermitteeLicenseType + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.permittee_s_license_type IN (" + NewConstructionPermitIssuancePermitteeLicenseType + ")";
        }
    }
    if (document.getElementById("cbIssuanceDateNCPI").checked == true) {
        IssuanceDateNCPIFrom = document.getElementById("txtIssuanceDateNCPIFrom").value;
        IssuanceDateNCPITo = document.getElementById("txtIssuanceDateNCPITo").value;
        if (IssuanceDateNCPIFrom != "" || IssuanceDateNCPITo != "") {
            if (whereOwnerSearchClause != "") {
                whereOwnerSearchClause += " AND ";
            }
            if (IssuanceDateNCPIFrom != "" && IssuanceDateNCPITo != "") {
                var dFrom = new Date(IssuanceDateNCPIFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(IssuanceDateNCPITo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "j.issuance_date >= '" + valueFrom + "' AND j.issuance_date <= '" + valueTo + "'";
            }
            else if (IssuanceDateNCPIFrom != "") {
                var dFrom = new Date(IssuanceDateNCPIFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereOwnerSearchClause += "j.issuance_date >= '" + valueFrom + "'";
            }
            else if (IssuanceDateNCPITo != "") {
                var dTo = new Date(IssuanceDateNCPITo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "j.issuance_date <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        sqlQuery = sqlQueryPermitIssuance + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}

function btnOwnerSearchMajorAlterationPermitIssuance() {
    lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
        , { name: "Permit Issuance date", attribute: "issuance_date", dataset: "OwnerSearch" }, { name: "Zoning District", attribute: "ZoneDist1", dataset: "OwnerSearch" }
        , { name: "Building Area", attribute: "BldgArea", dataset: "OwnerSearch" }, { name: "Assessed Property Value", attribute: "AssessTot", dataset: "OwnerSearch" }
        , { name: "Owner Name", attribute: "OwnerName", dataset: "OwnerSearch" }, { name: "Permitte Name", attribute: "PermitteName", dataset: "OwnerSearch" }
        , { name: "Permittee Business Name", attribute: "permittee_s_business_name", dataset: "OwnerSearch" }, { name: "Permittee License Type", attribute: "permittee_s_license_type", dataset: "OwnerSearch" }];
    whereOwnerSearchClause = "j.job_type = 'A1'";
    var MajorAlterationPermitIssuanceBldgAreaStart = null, MajorAlterationPermitIssuanceBldgAreaEnd = null, MajorAlterationPermitIssuanceAssessTotStart = null, MajorAlterationPermitIssuanceAssessTotEnd = null;
    var MajorAlterationPermitIssuanceAddress = document.getElementById("txtMajorAlterationPermitIssuanceAddress").value;
    var MajorAlterationPermitIssuanceBorough = document.getElementById("txtMajorAlterationPermitIssuanceBorough").value;
    var MajorAlterationPermitIssuanceZoningDistrict = document.getElementById("txtMajorAlterationPermitIssuanceZoningDistrict").value;
    var MajorAlterationPermitIssuanceOwnerName = document.getElementById("txtMajorAlterationPermitIssuanceOwnerName").value;
    var MajorAlterationPermitIssuancePermitteName = document.getElementById("txtMajorAlterationPermitIssuancePermitteName").value;
    var MajorAlterationPermitIssuancePermitteeBusinessName = document.getElementById("txtMajorAlterationPermitIssuancePermitteeBusinessName").value;
    var MajorAlterationPermitIssuancePermitteeLicenseType = document.getElementById("txtMajorAlterationPermitIssuancePermitteeLicenseType").value;

    if (document.getElementById("cbMajorAlterationPermitIssuanceBldgArea").checked == true) {
        MajorAlterationPermitIssuanceBldgAreaStart = $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 0);
        MajorAlterationPermitIssuanceBldgAreaEnd = $("#slider-range-MajorAlterationPermitIssuanceBldgArea").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.BldgArea >= " + MajorAlterationPermitIssuanceBldgAreaStart + " AND p.BldgArea <= " + MajorAlterationPermitIssuanceBldgAreaEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.BldgArea >= " + MajorAlterationPermitIssuanceBldgAreaStart + " AND p.BldgArea <= " + MajorAlterationPermitIssuanceBldgAreaEnd;
        }
    }
    if (document.getElementById("cbMajorAlterationPermitIssuanceAssessTot").checked == true) {
        MajorAlterationPermitIssuanceAssessTotStart = $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 0);
        MajorAlterationPermitIssuanceAssessTotEnd = $("#slider-range-MajorAlterationPermitIssuanceAssessTot").slider("values", 1);
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.AssessTot >= " + MajorAlterationPermitIssuanceAssessTotStart + " AND p.AssessTot <= " + MajorAlterationPermitIssuanceAssessTotEnd;
        }
        else {
            whereOwnerSearchClause += " AND p.AssessTot >= " + MajorAlterationPermitIssuanceAssessTotStart + " AND p.AssessTot <= " + MajorAlterationPermitIssuanceAssessTotEnd;
        }
    }
    if (MajorAlterationPermitIssuanceAddress != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.Address IN (" + MajorAlterationPermitIssuanceAddress + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.Address IN (" + MajorAlterationPermitIssuanceAddress + ")";
        }
    }
    if (MajorAlterationPermitIssuanceBorough != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.Borough IN (" + MajorAlterationPermitIssuanceBorough + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.Borough IN (" + MajorAlterationPermitIssuanceBorough + ")";
        }
    }
    if (MajorAlterationPermitIssuanceZoningDistrict != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.ZoneDist1 IN (" + MajorAlterationPermitIssuanceZoningDistrict + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.ZoneDist1 IN (" + MajorAlterationPermitIssuanceZoningDistrict + ")";
        }
    }
    if (MajorAlterationPermitIssuanceOwnerName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "p.OwnerName IN (" + MajorAlterationPermitIssuanceOwnerName + ")";
        }
        else {
            whereOwnerSearchClause += " AND p.OwnerName IN (" + MajorAlterationPermitIssuanceOwnerName + ")";
        }
    }
    if (MajorAlterationPermitIssuancePermitteName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.permittee_s_first_name + ' ' + j.permittee_s_last_name IN (" + MajorAlterationPermitIssuancePermitteName + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.permittee_s_first_name + ' ' + j.permittee_s_last_name IN (" + MajorAlterationPermitIssuancePermitteName + ")";
        }
    }
    if (MajorAlterationPermitIssuancePermitteeBusinessName != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.permittee_s_business_name IN (" + MajorAlterationPermitIssuancePermitteeBusinessName + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.permittee_s_business_name IN (" + MajorAlterationPermitIssuancePermitteeBusinessName + ")";
        }
    }
    if (MajorAlterationPermitIssuancePermitteeLicenseType != "") {
        if (whereOwnerSearchClause == "") {
            whereOwnerSearchClause = "j.permittee_s_license_type IN (" + MajorAlterationPermitIssuancePermitteeLicenseType + ")";
        }
        else {
            whereOwnerSearchClause += " AND j.permittee_s_license_type IN (" + MajorAlterationPermitIssuancePermitteeLicenseType + ")";
        }
    }
    if (document.getElementById("cbIssuanceDateMAPI").checked == true) {
        IssuanceDateMAPIFrom = document.getElementById("txtIssuanceDateMAPIFrom").value;
        IssuanceDateMAPITo = document.getElementById("txtIssuanceDateMAPITo").value;
        if (IssuanceDateMAPIFrom != "" || IssuanceDateMAPITo != "") {
            if (whereOwnerSearchClause != "") {
                whereOwnerSearchClause += " AND ";
            }
            if (IssuanceDateMAPIFrom != "" && IssuanceDateMAPITo != "") {
                var dFrom = new Date(IssuanceDateMAPIFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(IssuanceDateMAPITo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "j.issuance_date >= '" + valueFrom + "' AND j.issuance_date <= '" + valueTo + "'";
            }
            else if (IssuanceDateMAPIFrom != "") {
                var dFrom = new Date(IssuanceDateMAPIFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereOwnerSearchClause += "j.issuance_date >= '" + valueFrom + "'";
            }
            else if (IssuanceDateMAPITo != "") {
                var dTo = new Date(IssuanceDateMAPITo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereOwnerSearchClause += "j.issuance_date <= '" + valueTo + "'";
            }
        }
    }
    if (whereOwnerSearchClause == "") {
        swal("Please choose any search criteria");
    }
    else {
        $('#loading').show();
        sqlQuery = sqlQueryPermitIssuance + whereOwnerSearchClause;
        $.ajax({
            url: RootUrl + 'Home/SearchDatabase',
            type: "POST",
            data: {
                "sqlQuery": sqlQuery
            }
        }).done(function (data) {
            CreateDatabaseTable(data, true, true);
            $('#btnOpenAlerts').hide();
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search the query");
        });
    }
}
