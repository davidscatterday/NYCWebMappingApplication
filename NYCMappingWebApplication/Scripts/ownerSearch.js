var maxPropertySearchConstructionFloorArea = 999999999;
var whereOwnerSearchClause = "";
var sqlQueryJobPermit = "select ISNULL(bbl_10_digits, 0) AS BBL, j.Job_Type AS job_type, (j.House + ' ' + j.Street_Name) AS Address, j.Borough, j.TOTAL_CONSTRUCTION_FLOOR_AREA, j.BUILDING_CLASS AS BldgClass, j.Zoning_Dist1 AS ZoneDist1, j.Proposed_Height, j.Proposed_Occupancy, (j.Owner_s_First_Name + ' ' + j.Owner_s_Last_Name) AS OwnerName, j.Owner_s_Business_Name AS BusinessName, p.permittee_s_business_name AS GeneralContractor, (j.Applicant_s_First_Name + ' ' + j.Applicant_s_Last_Name) AS Architect from dbo.DOB_Job_Application_Filings j left join dbo.Permit p on j.Job = p.job__ where ";
lstTableAttributes = [{ name: "Borough", attribute: "Borough", dataset: "OwnerSearch" }, { name: "Address", attribute: "Address", dataset: "OwnerSearch" }
    , { name: "Owner Name", attribute: "OwnerName", dataset: "OwnerSearch" }, { name: "Business Name", attribute: "BusinessName", dataset: "OwnerSearch" }
    , { name: "General Contractor", attribute: "GeneralContractor", dataset: "OwnerSearch" }, { name: "Architect", attribute: "Architect", dataset: "OwnerSearch" }
    , { name: "Job Type", attribute: "job_type", dataset: "OwnerSearch" }, { name: "Construction Floor Area", attribute: "TOTAL_CONSTRUCTION_FLOOR_AREA", dataset: "OwnerSearch" }
    , { name: "Building Class", attribute: "BldgClass", dataset: "OwnerSearch" }, { name: "Zoning District", attribute: "ZoneDist1", dataset: "OwnerSearch" }
    , { name: "Proposed Height", attribute: "Proposed_Height", dataset: "OwnerSearch" }, { name: "Proposed Occupancy", attribute: "Proposed_Occupancy", dataset: "OwnerSearch" }];
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

});

function btnOwnerSearch() {
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
            CreateDatabaseTable(data, true);
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

    $('.panel-collapse.in').collapse('toggle');
    hideBottomPanel();

    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
}