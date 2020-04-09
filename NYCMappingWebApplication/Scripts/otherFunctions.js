var maxTotalBuildingFloorArea = 67000000, maxCommercialFloorArea = 24000000, maxResidentialFloorArea = 14000000
    , maxNumberOfFloors = 210, maxResidentialUnits = 11000, maxAssessedTotalValue = 7200000000
    , maxEnergyStarScore = 100, maxSourceEUI = 29000000, maxSiteEUI = 25000000, maxAnnualMaximumDemand = 2600000, maxTotalGHGEmissions = 540000000

var lstBuildingClass = ["cbOfficeO1", "cbOfficeO2", "cbOfficeO3", "cbOfficeO4"
    , "cbIndustrialF1", "cbIndustrialF2", "cbIndustrialF4", "cbIndustrialF5", "cbIndustrialF8", "cbIndustrialF9", "cbRetailK1", "cbOfficeO4"
    , "cbRetailK2", "cbRetailK3", "cbRetailK4", "cbRetailK5", "cbRetailK6", "cbRetailK8", "cbHotelH1", "cbHotelH2"
    , "cbHotelH3", "cbHotelH4", "cbHotelH5", "cbHotelH6", "cbHotelH7", "cbCondominiumsCoopR1", "cbCondominiumsCoopR2"
    , "cbCondominiumsCoopR3", "cbCondominiumsCoopR4", "cbCondominiumsCoopR5", "cbCondominiumsCoopR6", "cbCondominiumsCoopR7", "cbCondominiumsCoopR8", "cbCondominiumsCoopR9"
    , "cbCondominiumsCoopC8", "cbCondominiumsCoopD0", "cbCondominiumsCoopD5", "cbRentalResidentialC0", "cbRentalResidentialC1", "cbRentalResidentialC2", "cbRentalResidentialC3"
    , "cbRentalResidentialC4", "cbRentalResidentialC5", "cbRentalResidentialC6", "cbRentalResidentialD1", "cbRentalResidentialD2", "cbRentalResidentialD3", "cbRentalResidentialD6"
    , "cbRentalResidentialD7", "cbRentalResidentialD8", "cbForProfitOwnedHealthcareI1", "cbForProfitOwnedHealthcareI2", "cbForProfitOwnedHealthcareI3", "cbForProfitOwnedHealthcareI4"
    , "cbForProfitOwnedHealthcareI5", "cbForProfitOwnedHealthcareI6", "cbForProfitOwnedHealthcareI7"];
var lstTableAttributes = [];
$(function () {
    $("#slider-range-TotalBuildingFloorArea").slider({
        range: true,
        min: 0,
        max: maxTotalBuildingFloorArea,
        values: [0, maxTotalBuildingFloorArea],
        slide: function (event, ui) {
            $("#txtTotalBuildingFloorArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtTotalBuildingFloorArea").val($("#slider-range-TotalBuildingFloorArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-TotalBuildingFloorArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-CommercialFloorArea").slider({
        range: true,
        min: 0,
        max: maxCommercialFloorArea,
        values: [0, maxCommercialFloorArea],
        slide: function (event, ui) {
            $("#txtCommercialFloorArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtCommercialFloorArea").val($("#slider-range-CommercialFloorArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-CommercialFloorArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-ResidentialFloorArea").slider({
        range: true,
        min: 0,
        max: maxResidentialFloorArea,
        values: [0, maxResidentialFloorArea],
        slide: function (event, ui) {
            $("#txtResidentialFloorArea").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtResidentialFloorArea").val($("#slider-range-ResidentialFloorArea").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-ResidentialFloorArea").slider("values", 1).toLocaleString('en'));

    $("#slider-range-NumberOfFloors").slider({
        range: true,
        min: 0,
        max: maxNumberOfFloors,
        values: [0, maxNumberOfFloors],
        slide: function (event, ui) {
            $("#txtNumberOfFloors").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtNumberOfFloors").val($("#slider-range-NumberOfFloors").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-NumberOfFloors").slider("values", 1).toLocaleString('en'));

    $("#slider-range-ResidentialUnits").slider({
        range: true,
        min: 0,
        max: maxResidentialUnits,
        values: [0, maxResidentialUnits],
        slide: function (event, ui) {
            $("#txtResidentialUnits").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtResidentialUnits").val($("#slider-range-ResidentialUnits").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-ResidentialUnits").slider("values", 1).toLocaleString('en'));

    $("#slider-range-AssessedTotalValue").slider({
        range: true,
        min: 0,
        max: maxAssessedTotalValue,
        values: [0, maxAssessedTotalValue],
        slide: function (event, ui) {
            $("#txtAssessedTotalValue").val("$" + ui.values[0].toLocaleString('en') + " - $" + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtAssessedTotalValue").val("$" + $("#slider-range-AssessedTotalValue").slider("values", 0).toLocaleString('en') +
        " - $" + $("#slider-range-AssessedTotalValue").slider("values", 1).toLocaleString('en'));

    $("#slider-range-YearBuild").slider({
        range: true,
        min: 1650,
        max: new Date().getFullYear(),
        values: [1650, new Date().getFullYear()],
        slide: function (event, ui) {
            $("#txtYearBuild").val(ui.values[0] + " - " + ui.values[1]);
        }
    });
    $("#txtYearBuild").val($("#slider-range-YearBuild").slider("values", 0) +
        " - " + $("#slider-range-YearBuild").slider("values", 1));

    $("#slider-range-EnergyStarScore").slider({
        range: true,
        min: 0,
        max: maxEnergyStarScore,
        values: [0, maxEnergyStarScore],
        slide: function (event, ui) {
            $("#txtEnergyStarScore").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtEnergyStarScore").val($("#slider-range-EnergyStarScore").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-EnergyStarScore").slider("values", 1).toLocaleString('en'));

    $("#slider-range-SourceEUI").slider({
        range: true,
        min: 0,
        max: maxSourceEUI,
        values: [0, maxSourceEUI],
        slide: function (event, ui) {
            $("#txtSourceEUI").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtSourceEUI").val($("#slider-range-SourceEUI").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-SourceEUI").slider("values", 1).toLocaleString('en'));

    $("#slider-range-SiteEUI").slider({
        range: true,
        min: 0,
        max: maxSiteEUI,
        values: [0, maxSiteEUI],
        slide: function (event, ui) {
            $("#txtSiteEUI").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtSiteEUI").val($("#slider-range-SiteEUI").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-SiteEUI").slider("values", 1).toLocaleString('en'));

    $("#slider-range-AnnualMaximumDemand").slider({
        range: true,
        min: 0,
        max: maxAnnualMaximumDemand,
        values: [0, maxAnnualMaximumDemand],
        slide: function (event, ui) {
            $("#txtAnnualMaximumDemand").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtAnnualMaximumDemand").val($("#slider-range-AnnualMaximumDemand").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-AnnualMaximumDemand").slider("values", 1).toLocaleString('en'));

    $("#slider-range-TotalGHGEmissions").slider({
        range: true,
        min: 0,
        max: maxTotalGHGEmissions,
        values: [0, maxTotalGHGEmissions],
        slide: function (event, ui) {
            $("#txtTotalGHGEmissions").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtTotalGHGEmissions").val($("#slider-range-TotalGHGEmissions").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-TotalGHGEmissions").slider("values", 1).toLocaleString('en'));

});

function btnReset() {
    document.getElementById("cbBorough").checked = false;
    document.getElementById("cbZipCodeRange").checked = false;
    document.getElementById("cbStreetAddress").checked = false;
    document.getElementById("cbLatLong").checked = false;
    $("#txtBoroughs").select2("val", "");
    document.getElementById("txtZipCodeRangeFrom").value = "";
    document.getElementById("txtZipCodeRangeTo").value = "";
    document.getElementById("txtStreetAddress").value = "";
    document.getElementById("txtLat").value = "";
    document.getElementById("txtLong").value = "";

    document.getElementById("cbTotalBuildingFloorArea").checked = false;
    $("#slider-range-TotalBuildingFloorArea").slider("values", 0, 0);
    $("#slider-range-TotalBuildingFloorArea").slider("values", 1, maxTotalBuildingFloorArea);
    $("#txtTotalBuildingFloorArea").val($("#slider-range-TotalBuildingFloorArea").slider("values", 0) + " - " + $("#slider-range-TotalBuildingFloorArea").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbCommercialFloorArea").checked = false;
    $("#slider-range-CommercialFloorArea").slider("values", 0, 0);
    $("#slider-range-CommercialFloorArea").slider("values", 1, maxCommercialFloorArea);
    $("#txtCommercialFloorArea").val($("#slider-range-CommercialFloorArea").slider("values", 0) + " - " + $("#slider-range-CommercialFloorArea").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbResidentialFloorArea").checked = false;
    $("#slider-range-ResidentialFloorArea").slider("values", 0, 0);
    $("#slider-range-ResidentialFloorArea").slider("values", 1, maxResidentialFloorArea);
    $("#txtResidentialFloorArea").val($("#slider-range-ResidentialFloorArea").slider("values", 0) + " - " + $("#slider-range-ResidentialFloorArea").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbNumberOfFloors").checked = false;
    $("#slider-range-NumberOfFloors").slider("values", 0, 0);
    $("#slider-range-NumberOfFloors").slider("values", 1, maxNumberOfFloors);
    $("#txtNumberOfFloors").val($("#slider-range-NumberOfFloors").slider("values", 0) + " - " + $("#slider-range-NumberOfFloors").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbResidentialUnits").checked = false;
    $("#slider-range-ResidentialUnits").slider("values", 0, 0);
    $("#slider-range-ResidentialUnits").slider("values", 1, maxResidentialUnits);
    $("#txtResidentialUnits").val($("#slider-range-ResidentialUnits").slider("values", 0) + " - " + $("#slider-range-ResidentialUnits").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbZoningDistrict").checked = false;
    document.getElementById("cbCommercialOverlay").checked = false;
    document.getElementById("cbAssessedTotalValue").checked = false;
    document.getElementById("cbYearBuild").checked = false;
    document.getElementById("cbBuildingClass").checked = false;
    $("#ddlZoningDistrict").val($("#ddlZoningDistrict option:first").val());
    $("#ddlCommercialOverlay").val($("#ddlCommercialOverlay option:first").val());
    document.getElementById("cbCommercialOverlay").disabled = true;
    document.getElementById("ddlCommercialOverlay").disabled = true;

    document.getElementById("cbAssessedTotalValue").checked = false;
    $("#slider-range-AssessedTotalValue").slider("values", 0, 0);
    $("#slider-range-AssessedTotalValue").slider("values", 1, maxAssessedTotalValue);
    $("#txtAssessedTotalValue").val("$" + $("#slider-range-AssessedTotalValue").slider("values", 0) + " - $" + $("#slider-range-AssessedTotalValue").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbYearBuild").checked = false;
    $("#slider-range-YearBuild").slider("values", 0, 1650);
    $("#slider-range-YearBuild").slider("values", 1, new Date().getFullYear());
    $("#txtYearBuild").val($("#slider-range-YearBuild").slider("values", 0) + " - " + $("#slider-range-YearBuild").slider("values", 1));

    document.getElementById("cbOwnerName").checked = false;

    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();

    uncheckBuildingClass();
    $('.panel-collapse.in').collapse('toggle');

    document.getElementById("cbEnergyStarScore").checked = false;
    $("#slider-range-EnergyStarScore").slider("values", 0, 0);
    $("#slider-range-EnergyStarScore").slider("values", 1, maxEnergyStarScore);
    $("#txtEnergyStarScore").val($("#slider-range-EnergyStarScore").slider("values", 0) + " - " + $("#slider-range-EnergyStarScore").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbSourceEUI").checked = false;
    $("#slider-range-SourceEUI").slider("values", 0, 0);
    $("#slider-range-SourceEUI").slider("values", 1, maxSourceEUI);
    $("#txtSourceEUI").val($("#slider-range-SourceEUI").slider("values", 0) + " - " + $("#slider-range-SourceEUI").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbSiteEUI").checked = false;
    $("#slider-range-SiteEUI").slider("values", 0, 0);
    $("#slider-range-SiteEUI").slider("values", 1, maxSiteEUI);
    $("#txtSiteEUI").val($("#slider-range-SiteEUI").slider("values", 0) + " - " + $("#slider-range-SiteEUI").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbAnnualMaximumDemand").checked = false;
    $("#slider-range-AnnualMaximumDemand").slider("values", 0, 0);
    $("#slider-range-AnnualMaximumDemand").slider("values", 1, maxAnnualMaximumDemand);
    $("#txtAnnualMaximumDemand").val($("#slider-range-AnnualMaximumDemand").slider("values", 0) + " - " + $("#slider-range-AnnualMaximumDemand").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbTotalGHGEmissions").checked = false;
    $("#slider-range-TotalGHGEmissions").slider("values", 0, 0);
    $("#slider-range-TotalGHGEmissions").slider("values", 1, maxTotalGHGEmissions);
    $("#txtTotalGHGEmissions").val($("#slider-range-TotalGHGEmissions").slider("values", 0) + " - " + $("#slider-range-TotalGHGEmissions").slider("values", 1).toLocaleString('en'));

    if ($('.slider-bottom-arrow').hasClass("hideBottomPanel")) {
        $('.slider-bottom-arrow').click();
    }

    document.getElementById("cbJobStartDate").checked = false;
    document.getElementById("cbJobType").checked = false;
    document.getElementById("cbWorkType").checked = false;
    document.getElementById("txtJobStartDateFrom").value = "";
    document.getElementById("txtJobStartDateTo").value = "";
    $("#ddlJobType").val($("#ddlJobType option:first").val());
    $("#ddlWorkType").val($("#ddlWorkType option:first").val());

    document.getElementById("cbIssueDate").checked = false;
    document.getElementById("cbViolationType").checked = false;
    document.getElementById("cbViolationCategory").checked = false;
    document.getElementById("txtIssueDateFrom").value = "";
    document.getElementById("txtIssueDateTo").value = "";
    $("#ddlViolationType").val($("#ddlViolationType option:first").val());
    $("#ddlViolationCategory").val($("#ddlViolationCategory option:first").val());

    map.graphics.clear();
    selectionLayer.clear();
    map.setExtent(initExtent);
}

function btnSearch() {
    lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'Address', attribute: "Address", dataset: "Pluto" }];
    var Borough = null, ZipCodeRangeFrom = null, ZipCodeRangeTo = null, StreetAddress = null, Lat = null, Long = null
        , TotalBuildingFloorAreaStart = null, TotalBuildingFloorAreaEnd = null, CommercialFloorAreaStart = null, CommercialFloorAreaEnd = null
        , ResidentialFloorAreaStart = null, ResidentialFloorAreaEnd = null, NumberOfFloorsStart = null, NumberOfFloorsEnd = null
        , ResidentialUnitsStart = null, ResidentialUnitsEnd = null
        , ZoningDistrict, CommercialOverlay, AssessedTotalValueStart, AssessedTotalValueEnd, YearBuildStart, YearBuildEnd
        , EnergyStarScoreStart = null, EnergyStarScoreEnd = null, SourceEUIStart = null, SourceEUIEnd = null
        , SiteEUIStart = null, SiteEUIEnd = null, AnnualMaximumDemandStart = null, AnnualMaximumDemandEnd = null
        , TotalGHGEmissionsStart = null, TotalGHGEmissionsEnd = null;
    var whereClause = "";
    var whereEnergyClause = "";
    var wherePermitClause = "";
    var whereViolationClause = "";
    if (document.getElementById("cbBorough").checked == true) {
        Borough = $(txtBoroughs).val();
        if (Borough != "") {
            if (whereClause == "") {
                whereClause = "Borough IN (" + Borough + ")";
            }
            else {
                whereClause += " AND Borough IN (" + Borough + ")";
            }
        }
    }
    if (document.getElementById("cbZipCodeRange").checked == true) {
        ZipCodeRangeFrom = document.getElementById("txtZipCodeRangeFrom").value;
        ZipCodeRangeTo = document.getElementById("txtZipCodeRangeTo").value;
        if (ZipCodeRangeFrom != "" || ZipCodeRangeTo != "") {
            lstTableAttributes.push({ name: 'Zip Code', attribute: "ZipCode", dataset: "Pluto" });
            if (whereClause != "") {
                whereClause += " AND ";
            }
            if (ZipCodeRangeFrom != "" && ZipCodeRangeTo != "") {
                whereClause += "ZipCode >= " + ZipCodeRangeFrom + " AND ZipCode <= " + ZipCodeRangeTo;
            }
            else if (ZipCodeRangeFrom != "") {
                whereClause += "ZipCode >= " + ZipCodeRangeFrom;;
            }
            else if (ZipCodeRangeTo != "") {
                whereClause += "ZipCode <= " + ZipCodeRangeTo;;
            }
        }
    }
    if (document.getElementById("cbStreetAddress").checked == true) {
        StreetAddress = document.getElementById("txtStreetAddress").value;
        if (StreetAddress != "") {
            if (whereClause == "") {
                whereClause = "Address LIKE '%" + StreetAddress + "%'";
            }
            else {
                whereClause += " AND Address LIKE '%" + StreetAddress + "%'";
            }
        }
    }
    if (document.getElementById("cbLatLong").checked == true) {
        Lat = document.getElementById("txtLat").value;
        Long = document.getElementById("txtLong").value;
        if (Lat != "" || Long != "") {
            if (whereClause != "") {
                whereClause += " AND ";
            }
            if (Lat != "" && Long != "") {
                lstTableAttributes.push({ name: 'Latitude', attribute: "Latitude", dataset: "Pluto" });
                lstTableAttributes.push({ name: 'Longitude', attribute: "Longitude", dataset: "Pluto" });
                whereClause += "Latitude = " + Lat + " AND Longitude = " + Long;
            }
            else if (Lat != "") {
                lstTableAttributes.push({ name: 'Latitude', attribute: "Latitude", dataset: "Pluto" });
                whereClause += "Latitude = " + Lat;;
            }
            else if (Long != "") {
                lstTableAttributes.push({ name: 'Longitude', attribute: "Longitude", dataset: "Pluto" });
                whereClause += "Longitude = " + Long;;
            }
        }
    }

    if (document.getElementById("cbTotalBuildingFloorArea").checked == true) {
        lstTableAttributes.push({ name: 'Total Building Floor Area', attribute: "BldgArea", dataset: "Pluto" });
        TotalBuildingFloorAreaStart = $("#slider-range-TotalBuildingFloorArea").slider("values", 0);
        TotalBuildingFloorAreaEnd = $("#slider-range-TotalBuildingFloorArea").slider("values", 1);
        if (whereClause == "") {
            whereClause = "BldgArea >= " + TotalBuildingFloorAreaStart + " AND BldgArea <= " + TotalBuildingFloorAreaEnd;
        }
        else {
            whereClause += " AND BldgArea >= " + TotalBuildingFloorAreaStart + " AND BldgArea <= " + TotalBuildingFloorAreaEnd;
        }
    }
    if (document.getElementById("cbCommercialFloorArea").checked == true) {
        lstTableAttributes.push({ name: 'Commercial Floor Area', attribute: "ComArea", dataset: "Pluto" });
        CommercialFloorAreaStart = $("#slider-range-CommercialFloorArea").slider("values", 0);
        CommercialFloorAreaEnd = $("#slider-range-CommercialFloorArea").slider("values", 1);
        if (whereClause == "") {
            whereClause = "ComArea >= " + CommercialFloorAreaStart + " AND ComArea <= " + CommercialFloorAreaEnd;
        }
        else {
            whereClause += " AND ComArea >= " + CommercialFloorAreaStart + " AND ComArea <= " + CommercialFloorAreaEnd;
        }
    }
    if (document.getElementById("cbResidentialFloorArea").checked == true) {
        lstTableAttributes.push({ name: 'Residential Floor Area', attribute: "ResArea", dataset: "Pluto" });
        ResidentialFloorAreaStart = $("#slider-range-ResidentialFloorArea").slider("values", 0);
        ResidentialFloorAreaEnd = $("#slider-range-ResidentialFloorArea").slider("values", 1);
        if (whereClause == "") {
            whereClause = "ResArea >= " + ResidentialFloorAreaStart + " AND ResArea <= " + ResidentialFloorAreaEnd;
        }
        else {
            whereClause += " AND ResArea >= " + ResidentialFloorAreaStart + " AND ResArea <= " + ResidentialFloorAreaEnd;
        }
    }
    if (document.getElementById("cbNumberOfFloors").checked == true) {
        lstTableAttributes.push({ name: 'Number of Floors', attribute: "NumFloors", dataset: "Pluto" });
        NumberOfFloorsStart = $("#slider-range-NumberOfFloors").slider("values", 0);
        NumberOfFloorsEnd = $("#slider-range-NumberOfFloors").slider("values", 1);
        if (whereClause == "") {
            whereClause = "NumFloors >= " + NumberOfFloorsStart + " AND NumFloors <= " + NumberOfFloorsEnd;
        }
        else {
            whereClause += " AND NumFloors >= " + NumberOfFloorsStart + " AND NumFloors <= " + NumberOfFloorsEnd;
        }
    }
    if (document.getElementById("cbResidentialUnits").checked == true) {
        lstTableAttributes.push({ name: 'Residential Units', attribute: "UnitsRes", dataset: "Pluto" });
        ResidentialUnitsStart = $("#slider-range-ResidentialUnits").slider("values", 0);
        ResidentialUnitsEnd = $("#slider-range-ResidentialUnits").slider("values", 1);
        if (whereClause == "") {
            whereClause = "UnitsRes >= " + ResidentialUnitsStart + " AND UnitsRes <= " + ResidentialUnitsEnd;
        }
        else {
            whereClause += " AND UnitsRes >= " + ResidentialUnitsStart + " AND UnitsRes <= " + ResidentialUnitsEnd;
        }
    }


    if (document.getElementById("cbZoningDistrict").checked == true) {
        var ddlZoningDistrict = document.getElementById("ddlZoningDistrict");
        var selectedZoningDistrict = ddlZoningDistrict.options[ddlZoningDistrict.selectedIndex].value;
        if (selectedZoningDistrict != "") {
            lstTableAttributes.push({ name: 'Zoning District', attribute: "ZoneDist1", dataset: "Pluto" });
            if (whereClause == "") {
                whereClause = selectedZoningDistrict;
            }
            else {
                whereClause += " AND " + selectedZoningDistrict;
            }
        }
    }
    if (document.getElementById("cbCommercialOverlay").checked == true) {
        var ddlCommercialOverlay = document.getElementById("ddlCommercialOverlay");
        var selectedCommercialOverlay = ddlCommercialOverlay.options[ddlCommercialOverlay.selectedIndex].value;
        if (selectedCommercialOverlay != "") {
            lstTableAttributes.push({ name: 'Commercial Overlay 1', attribute: "Overlay1", dataset: "Pluto" });
            lstTableAttributes.push({ name: 'Commercial Overlay 2', attribute: "Overlay2", dataset: "Pluto" });
            if (whereClause == "") {
                whereClause = selectedCommercialOverlay;
            }
            else {
                whereClause += " AND " + selectedCommercialOverlay;
            }
        }
    }
    if (document.getElementById("cbAssessedTotalValue").checked == true) {
        lstTableAttributes.push({ name: 'Assessed Total Value', attribute: "AssessTot", dataset: "Pluto" });
        AssessedTotalValueStart = $("#slider-range-AssessedTotalValue").slider("values", 0);
        AssessedTotalValueEnd = $("#slider-range-AssessedTotalValue").slider("values", 1);
        if (whereClause == "") {
            whereClause = "AssessTot >= " + AssessedTotalValueStart + " AND AssessTot <= " + AssessedTotalValueEnd;
        }
        else {
            whereClause += " AND AssessTot >= " + AssessedTotalValueStart + " AND AssessTot <= " + AssessedTotalValueEnd;
        }
    }
    if (document.getElementById("cbYearBuild").checked == true) {
        lstTableAttributes.push({ name: 'Year Built', attribute: "YearBuilt", dataset: "Pluto" });
        YearBuildStart = $("#slider-range-YearBuild").slider("values", 0);
        YearBuildEnd = $("#slider-range-YearBuild").slider("values", 1);
        if (whereClause == "") {
            whereClause = "YearBuilt >= " + YearBuildStart + " AND YearBuilt <= " + YearBuildEnd;
        }
        else {
            whereClause += " AND YearBuilt >= " + YearBuildStart + " AND YearBuilt <= " + YearBuildEnd;
        }
    }
    if (document.getElementById("cbOwnerName").checked == true) {
        lstTableAttributes.push({ name: 'Owner Name', attribute: "OwnerName", dataset: "Pluto" });
    }
    if (document.getElementById("cbBuildingClass").checked == true) {
        var buildingClassValues = "";
        for (var i = 0; i < lstBuildingClass.length; i++) {
            var name = lstBuildingClass[i];
            if (document.getElementById(name).checked == true) {
                if (buildingClassValues == "") {
                    buildingClassValues = document.getElementById(name).value;
                }
                else {
                    buildingClassValues += "," + document.getElementById(name).value;
                }
            }
        }

        if (buildingClassValues != "") {
            lstTableAttributes.push({ name: 'Building Class', attribute: "BldgClass", dataset: "Pluto" });
            if (whereClause == "") {
                whereClause = "BldgClass IN (" + buildingClassValues + ")";
            }
            else {
                whereClause += " AND BldgClass IN (" + buildingClassValues + ")";
            }
        }
    }

    if (document.getElementById("cbEnergyStarScore").checked == true) {
        lstTableAttributes.push({ name: 'Energy Star Score', attribute: "energy_star_score", dataset: "Energy" });
        EnergyStarScoreStart = $("#slider-range-EnergyStarScore").slider("values", 0);
        EnergyStarScoreEnd = $("#slider-range-EnergyStarScore").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "energy_star_score >= " + EnergyStarScoreStart + " AND energy_star_score <= " + EnergyStarScoreEnd;
        }
        else {
            whereEnergyClause += " AND energy_star_score >= " + EnergyStarScoreStart + " AND energy_star_score <= " + EnergyStarScoreEnd;
        }
    }
    if (document.getElementById("cbSourceEUI").checked == true) {
        lstTableAttributes.push({ name: 'Source EUI', attribute: "source_eui_kbtu_ft", dataset: "Energy" });
        SourceEUIStart = $("#slider-range-SourceEUI").slider("values", 0);
        SourceEUIEnd = $("#slider-range-SourceEUI").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "source_eui_kbtu_ft >= " + SourceEUIStart + " AND source_eui_kbtu_ft <= " + SourceEUIEnd;
        }
        else {
            whereEnergyClause += " AND source_eui_kbtu_ft >= " + SourceEUIStart + " AND source_eui_kbtu_ft <= " + SourceEUIEnd;
        }
    }
    if (document.getElementById("cbSiteEUI").checked == true) {
        lstTableAttributes.push({ name: 'Site EUI', attribute: "site_eui_kbtu_ft", dataset: "Energy" });
        SiteEUIStart = $("#slider-range-SiteEUI").slider("values", 0);
        SiteEUIEnd = $("#slider-range-SiteEUI").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "site_eui_kbtu_ft >= " + SiteEUIStart + " AND site_eui_kbtu_ft <= " + SiteEUIEnd;
        }
        else {
            whereEnergyClause += " AND site_eui_kbtu_ft >= " + SiteEUIStart + " AND site_eui_kbtu_ft <= " + SiteEUIEnd;
        }
    }
    if (document.getElementById("cbAnnualMaximumDemand").checked == true) {
        lstTableAttributes.push({ name: 'Annual Maximum Demand', attribute: "annual_maximum_demand_kw", dataset: "Energy" });
        AnnualMaximumDemandStart = $("#slider-range-AnnualMaximumDemand").slider("values", 0);
        AnnualMaximumDemandEnd = $("#slider-range-AnnualMaximumDemand").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "annual_maximum_demand_kw >= " + AnnualMaximumDemandStart + " AND annual_maximum_demand_kw <= " + AnnualMaximumDemandEnd;
        }
        else {
            whereEnergyClause += " AND annual_maximum_demand_kw >= " + AnnualMaximumDemandStart + " AND annual_maximum_demand_kw <= " + AnnualMaximumDemandEnd;
        }
    }
    if (document.getElementById("cbTotalGHGEmissions").checked == true) {
        lstTableAttributes.push({ name: 'Total GHG Emissions', attribute: "total_ghg_emissions_metric", dataset: "Energy" });
        TotalGHGEmissionsStart = $("#slider-range-TotalGHGEmissions").slider("values", 0);
        TotalGHGEmissionsEnd = $("#slider-range-TotalGHGEmissions").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "total_ghg_emissions_metric >= " + TotalGHGEmissionsStart + " AND total_ghg_emissions_metric <= " + TotalGHGEmissionsEnd;
        }
        else {
            whereEnergyClause += " AND total_ghg_emissions_metric >= " + TotalGHGEmissionsStart + " AND total_ghg_emissions_metric <= " + TotalGHGEmissionsEnd;
        }
    }

    if (document.getElementById("cbJobStartDate").checked == true) {
        JobStartDateFrom = document.getElementById("txtJobStartDateFrom").value;
        JobStartDateTo = document.getElementById("txtJobStartDateTo").value;
        if (JobStartDateFrom != "" || JobStartDateTo != "") {
            lstTableAttributes.push({ name: 'Job Start Date', attribute: "job_start_date", dataset: "Permit" });
            if (wherePermitClause != "") {
                wherePermitClause += " AND ";
            }
            if (JobStartDateFrom != "" && JobStartDateTo != "") {
                wherePermitClause += "job_start_date >= " + JobStartDateFrom + " AND job_start_date <= " + JobStartDateTo;
            }
            else if (JobStartDateFrom != "") {
                wherePermitClause += "job_start_date >= " + JobStartDateFrom;
            }
            else if (JobStartDateTo != "") {
                wherePermitClause += "job_start_date <= " + JobStartDateTo;
            }
        }
    }
    if (document.getElementById("cbJobType").checked == true) {
        var ddlJobType = document.getElementById("ddlJobType");
        var selectedJobType = ddlJobType.options[ddlJobType.selectedIndex].value;
        if (selectedJobType != "") {
            lstTableAttributes.push({ name: 'Job Type', attribute: "job_type", dataset: "Permit" });
            if (wherePermitClause == "") {
                wherePermitClause = "job_type = '" + selectedJobType + "'";
            }
            else {
                wherePermitClause += " AND job_type = '" + selectedJobType + "'";
            }
        }
    }
    if (document.getElementById("cbWorkType").checked == true) {
        var ddlWorkType = document.getElementById("ddlWorkType");
        var selectedWorkType = ddlWorkType.options[ddlWorkType.selectedIndex].value;
        if (selectedWorkType != "") {
            lstTableAttributes.push({ name: 'Work Type', attribute: "work_type", dataset: "Permit" });
            if (wherePermitClause == "") {
                wherePermitClause = "work_type = '" + selectedWorkType + "'";
            }
            else {
                wherePermitClause += " AND work_type = '" + selectedWorkType + "'";
            }
        }
    }

    if (document.getElementById("cbIssueDate").checked == true) {
        IssueDateFrom = document.getElementById("txtIssueDateFrom").value;
        IssueDateTo = document.getElementById("txtIssueDateTo").value;
        if (IssueDateFrom != "" || IssueDateTo != "") {
            lstTableAttributes.push({ name: 'Issue Date', attribute: "issue_date", dataset: "Violation" });
            if (whereViolationClause != "") {
                whereViolationClause += " AND ";
            }
            if (IssueDateFrom != "" && IssueDateTo != "") {
                whereViolationClause += "issue_date >= " + IssueDateFrom + " AND issue_date <= " + IssueDateTo;
            }
            else if (IssueDateFrom != "") {
                whereViolationClause += "issue_date >= " + IssueDateFrom;
            }
            else if (IssueDateTo != "") {
                whereViolationClause += "issue_date <= " + IssueDateTo;
            }
        }
    }
    if (document.getElementById("cbViolationType").checked == true) {
        var ddlViolationType = document.getElementById("ddlViolationType");
        var selectedViolationType = ddlViolationType.options[ddlViolationType.selectedIndex].value;
        if (selectedViolationType != "") {
            lstTableAttributes.push({ name: 'Violation Type', attribute: "violation_type", dataset: "Violation" });
            if (whereViolationClause == "") {
                whereViolationClause = "violation_type = '" + selectedViolationType + "'";
            }
            else {
                whereViolationClause += " AND violation_type = '" + selectedViolationType + "'";
            }
        }
    }
    if (document.getElementById("cbViolationCategory").checked == true) {
        var ddlViolationCategory = document.getElementById("ddlViolationCategory");
        var selectedViolationCategory = ddlViolationCategory.options[ddlViolationCategory.selectedIndex].value;
        if (selectedViolationCategory != "") {
            lstTableAttributes.push({ name: 'Violation Category', attribute: "violation_category", dataset: "Violation" });
            if (whereViolationClause == "") {
                whereViolationClause = "violation_category = '" + selectedViolationCategory + "'";
            }
            else {
                whereViolationClause += " AND violation_category = '" + selectedViolationCategory + "'";
            }
        }
    }

    if (whereEnergyClause != "" || wherePermitClause != "" || whereViolationClause != "") {
        $('#loading').show();
        EnergySearch(whereEnergyClause, wherePermitClause, whereViolationClause, whereClause);
    }
    else if (whereClause != "") {
        MapPlutoSearch(whereClause, "", "", "", null, null, null);
    }
    else {
        swal("Please choose some searching criteria first");
    }
}

function EnergySearch(whereEnergyClause, wherePermitClause, whereViolationClause, whereClause) {
    if (whereEnergyClause != "") {
        $.ajax({
            url: "https://data.cityofnewyork.us/resource/n2mv-q2ia.json",
            type: "GET",
            data: {
                "$limit": 20000,
                "$$app_token": "CNInYnGGBkU5zuF516GolLbHQ",
                "$where": whereEnergyClause
            }
        }).done(function (dataEnergy) {
            try {
                var bblEnergyList = "";
                dataEnergy.forEach((field) => {
                    if (!isNaN(field.bbl_10_digits)) {
                        if (bblEnergyList == "") {
                            bblEnergyList += field.bbl_10_digits;
                        }
                        else {
                            bblEnergyList += "," + field.bbl_10_digits;
                        }
                    }
                });
                PermitSearch(wherePermitClause, whereViolationClause, whereClause, bblEnergyList, dataEnergy);
            }
            catch (e) {
                $('#loading').hide();
            }
        }).fail(function () {
            $('#loading').hide();
        });
    }
    else {
        PermitSearch(wherePermitClause, whereViolationClause, whereClause, "", null);
    }
}

function PermitSearch(wherePermitClause, whereViolationClause, whereClause, bblEnergyList, dataEnergy) {
    if (wherePermitClause != "") {
        $.ajax({
            url: "https://data.cityofnewyork.us/resource/ipu4-2q9a.json",
            type: "GET",
            data: {
                "$limit": 20000,
                "$$app_token": "CNInYnGGBkU5zuF516GolLbHQ",
                "$where": wherePermitClause
            }
        }).done(function (dataPermit) {
            try {
                var bblPermitList = "";
                dataPermit.forEach((field) => {
                    var boroNum = "";
                    switch (field.borough) {
                        case "MANHATTAN": boroNum = "1"; break
                        case "BRONX": boroNum = "2"; break
                        case "BROOKLYN": boroNum = "3"; break
                        case "QUEENS": boroNum = "4"; break
                        case "STATEN ISLAND": boroNum = "5"; break
                        default: return
                    }
                    if (!isNaN(boroNum) && !isNaN(field.block) && !isNaN(field.lot)) {
                        var bbl = boroNum + field.block + field.lot.substring(1);
                        if (!isNaN(bbl)) {
                            if (bblPermitList == "") {
                                bblPermitList += bbl;
                            }
                            else {
                                bblPermitList += "," + bbl;
                            }
                        }
                    }
                });
                ViolationSearch(whereViolationClause, whereClause, bblEnergyList, bblPermitList, dataEnergy, dataPermit);
            }
            catch (e) {
                $('#loading').hide();
            }
        }).fail(function () {
            $('#loading').hide();
        });
    }
    else {
        ViolationSearch(whereViolationClause, whereClause, bblEnergyList, "", dataEnergy, null);
    }
}

function ViolationSearch(whereViolationClause, whereClause, bblEnergyList, bblPermitList, dataEnergy, dataPermit) {
    if (whereViolationClause != "") {
        $.ajax({
            url: "https://data.cityofnewyork.us/resource/3h2n-5cm9.json",
            type: "GET",
            data: {
                "$limit": 20000,
                "$$app_token": "CNInYnGGBkU5zuF516GolLbHQ",
                "$where": whereViolationClause
            }
        }).done(function (dataViolation) {
            try {
                var bblViolationList = "";
                dataViolation.forEach((field) => {
                    if (!isNaN(field.boro) && !isNaN(field.block) && !isNaN(field.lot)) {
                        var bbl = field.boro + field.block + field.lot.substring(1);
                        if (!isNaN(bbl)) {
                            if (bblViolationList == "") {
                                bblViolationList += bbl;
                            }
                            else {
                                bblViolationList += "," + bbl;
                            }
                        }
                    }
                });
                MapPlutoSearch(whereClause, bblEnergyList, bblPermitList, bblViolationList, dataEnergy, dataPermit, dataViolation);
            }
            catch (e) {
                $('#loading').hide();
            }
        }).fail(function () {
            $('#loading').hide();
        });
    }
    else {
        MapPlutoSearch(whereClause, bblEnergyList, bblPermitList, "", dataEnergy, dataPermit, null);
    }
}

function MapPlutoSearch(whereClause, bblEnergyList, bblPermitList, bblViolationList, dataEnergy, dataPermit, dataViolation) {
    $('#loading').show();
    if (bblEnergyList != "") {
        if (whereClause == "") {
            whereClause = "BBL IN (" + bblEnergyList + ")";
        }
        else {
            whereClause += " AND BBL IN (" + bblEnergyList + ")";
        }
    }
    if (bblPermitList != "") {
        if (whereClause == "") {
            whereClause = "BBL IN (" + bblPermitList + ")";
        }
        else {
            whereClause += " AND BBL IN (" + bblPermitList + ")";
        }
    }
    if (bblViolationList != "") {
        if (whereClause == "") {
            whereClause = "BBL IN (" + bblViolationList + ")";
        }
        else {
            whereClause += " AND BBL IN (" + bblViolationList + ")";
        }
    }
    if (whereClause != "") {
        map.graphics.clear();
        selectionLayer.clear();
        queryTask = new esri.tasks.QueryTask(MapPlutoUrl);

        //initialize query
        query = new esri.tasks.Query();
        query.returnGeometry = true;
        query.outFields = ["*"];

        query.where = whereClause;

        //execute query
        queryTask.execute(query, function executeMapPlutoSearch(featureSet) {
            if ($('.slider-bottom-arrow').hasClass("showBottomPanel")) {
                $('.slider-bottom-arrow').click();
            }
            var resultFeatures = featureSet.features;
            if (resultFeatures.length > 0) {
                CreateResultTable(resultFeatures, dataEnergy, dataPermit, dataViolation);
            }
            else {
                $('#divSelectItemsTable').text('');
                $('#divSelectItemsMoreInfo').hide();
                $('#divSelectItemsMessage').show();
                $('#divSelectItemsCount').hide();
                $('#loading').hide();
            }
        }, function (error) {
            $('#loading').hide();
            swal(error.message);
            console.log(error);
        });
    }
    else {
        $('#divSelectItemsTable').text('');
        $('#divSelectItemsMoreInfo').hide();
        $('#divSelectItemsMessage').show();
        $('#divSelectItemsCount').hide();
        $('#loading').hide();
    }
}

function CreateResultTable(resultFeatures, dataEnergy, dataPermit, dataViolation) {
    var features = [];
    var htmlQueryRecords = '<div class="table-responsive"><table id=\"tblQueryRecords\" class="tablesorter"><thead><tr class=\"clickableRow\">';
    for (var i = 0; i < lstTableAttributes.length; i++) {
        htmlQueryRecords += "<th>" + lstTableAttributes[i].name + "</th>"
    }
    htmlQueryRecords += "</tr></thead><tbody>";
    var dataEnergyItem = [];
    var dataPermitItem = [];
    var dataViolationItem = [];
    //Loop through each feature returned
    for (var i = 0, il = resultFeatures.length; i < il; i++) {
        var graphic = resultFeatures[i];
        if (dataEnergy != null) {
            dataEnergyItem = dataEnergy.filter(function (obj) {
                return obj.bbl_10_digits == graphic.attributes.BBL;
            });
        }
        if (dataPermit != null) {
            dataPermitItem = dataPermit.filter(function (obj) {
                var boroNum = "";
                switch (obj.borough) {
                    case "MANHATTAN": boroNum = "1"; break
                    case "BRONX": boroNum = "2"; break
                    case "BROOKLYN": boroNum = "3"; break
                    case "QUEENS": boroNum = "4"; break
                    case "STATEN ISLAND": boroNum = "5"; break
                    default: return
                }
                var bbl = "";
                if (!isNaN(boroNum) && !isNaN(obj.block) && !isNaN(obj.lot)) {
                    bbl = boroNum + obj.block + obj.lot.substring(1);
                }
                return bbl != "" && bbl == graphic.attributes.BBL;
            });
        }
        if (dataViolation != null) {
            dataViolationItem = dataViolation.filter(function (obj) {
                var bbl = "";
                if (!isNaN(obj.boro) && !isNaN(obj.block) && !isNaN(obj.lot)) {
                    bbl = obj.boro + obj.block + obj.lot.substring(1);
                }
                return bbl != "" && bbl == graphic.attributes.BBL;
            });
        }
        var myGraphic = new esri.Graphic({
            geometry: graphic.geometry
        });
        var jsonData = {};
        for (var j = 0; j < lstTableAttributes.length; j++) {
            var name = lstTableAttributes[j].name;
            if (lstTableAttributes[j].dataset == "Pluto") {
                value = graphic.attributes[lstTableAttributes[j].attribute];
            }
            else if (lstTableAttributes[j].dataset == "Energy") {
                value = dataEnergyItem.length == 0 ? "" : dataEnergyItem[0][lstTableAttributes[j].attribute];
            }
            else if (lstTableAttributes[j].dataset == "Permit") {
                value = dataPermitItem.length == 0 ? "" : dataPermitItem[0][lstTableAttributes[j].attribute];
            }
            else if (lstTableAttributes[j].dataset == "Violation") {
                value = dataViolationItem.length == 0 ? "" : dataViolationItem[0][lstTableAttributes[j].attribute];
            }
            jsonData[lstTableAttributes[j].attribute] = value;
        }
        myGraphic.setAttributes(jsonData);
        myGraphic.setSymbol(symbolFill);
        selectionLayer.add(myGraphic);
        features.push(graphic);
        htmlQueryRecords += "<tr class=\"clickableRow\" OnClick=\"ShowInfoForSelectedRecord('" + graphic.attributes.OBJECTID + "');\">";
        for (var key in myGraphic.attributes) {
            if (myGraphic.attributes.hasOwnProperty(key)) {
                htmlQueryRecords += "<td>" + myGraphic.attributes[key] + "</td>";
            }
        }
        htmlQueryRecords += "</tr>";
    }
    var spatialRef = new esri.SpatialReference({ wkid: 102718 });
    var zoomExtent = new esri.geometry.Extent();
    zoomExtent.spatialReference = spatialRef;
    zoomExtent.xmin = esri.graphicsExtent(features).xmin - 10000;
    zoomExtent.ymin = esri.graphicsExtent(features).ymin - 10000;
    zoomExtent.xmax = esri.graphicsExtent(features).xmax + 10000;
    zoomExtent.ymax = esri.graphicsExtent(features).ymax + 10000;
    map.setExtent(zoomExtent);
    htmlQueryRecords += '</tr></tbody></table></div>';
    $('#divSelectItemsTable').text('');
    $('#divSelectItemsTable').append(htmlQueryRecords);
    $("#tblQueryRecords").tablesorter({ widgets: ['zebra'] });
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsMoreInfo').show();
    $('#divSelectItemsCount').show();
    $('#divSelectItemsCount').text('There are ' + resultFeatures.length + ' records returned');
    highlightTableRow('tblQueryRecords');
    $('#loading').hide();
}

function ShowInfoForSelectedRecord(OBJECTID) {
    map.graphics.clear();
    queryTask = new esri.tasks.QueryTask(MapPlutoUrl);

    //initialize query
    query = new esri.tasks.Query();
    query.returnGeometry = true;
    query.outFields = ["*"];

    query.where = "OBJECTID = " + OBJECTID;

    //execute query
    queryTask.execute(query, function executeMapPlutoSelect(featureSet) {
        var resultFeatures = featureSet.features;
        if (resultFeatures.length > 0) {
            var features = [];
            //Loop through each feature returned
            for (var i = 0, il = resultFeatures.length; i < il; i++) {
                var graphic = resultFeatures[i];
                graphic.setSymbol(symbolZoomedFill);
                map.graphics.add(graphic);
                features.push(graphic);
            }
            var spatialRef = new esri.SpatialReference({ wkid: 102718 });
            var zoomExtent = new esri.geometry.Extent();
            zoomExtent.spatialReference = spatialRef;
            zoomExtent.xmin = esri.graphicsExtent(features).xmin - 1000;
            zoomExtent.ymin = esri.graphicsExtent(features).ymin - 1000;
            zoomExtent.xmax = esri.graphicsExtent(features).xmax + 1000;
            zoomExtent.ymax = esri.graphicsExtent(features).ymax + 1000;
            map.setExtent(zoomExtent);
        }
    });
}

function ddlZoningDistrict_SelectionChange() {
    var selectedText = $("#ddlZoningDistrict option:selected").text();
    if (selectedText == "Residence") {
        document.getElementById("cbCommercialOverlay").disabled = false;
        document.getElementById("ddlCommercialOverlay").disabled = false;
    }
    else {
        document.getElementById("cbCommercialOverlay").checked = false;
        document.getElementById("cbCommercialOverlay").disabled = true;
        $("#ddlCommercialOverlay").val($("#ddlCommercialOverlay option:first").val());
        document.getElementById("ddlCommercialOverlay").disabled = true;
    }
}

function btnOpenSaveReport_Click() {
    $("#myModalSaveReport").modal();
}

function btnSaveMyReport_Click() {
    if (document.getElementById("txtReportName").value == "") {
        swal("Please enter name");
    }
    else {
        var tableFeatures = [];
        var tblTableItems = selectionLayer.graphics;
        tblTableItems.forEach((item) => {
            var obj = {};
            for (var j = 0; j < lstTableAttributes.length; j++) {
                var name = lstTableAttributes[j].name;
                var value = item.attributes[lstTableAttributes[j].attribute];
                obj[lstTableAttributes[j].attribute] = value;
            }
            tableFeatures.push(obj);
        });
        var tableFeatures = JSON.stringify(tableFeatures);
        $.ajax({
            url: RootUrl + 'Home/SaveReport',
            type: "POST",
            data: {
                "ReportName": document.getElementById("txtReportName").value,
                "TableFeatures": tableFeatures
            }
        }).done(function (data) {
            swal(data.msg);
        }).fail(function () {
            swal("Failed to save the report");
        });
    }
}

function btnOpenMyReports_Click() {
    openModalWindow(RootUrl + "MyReports/Preview", 800, 820, "My Reports", true, "iframeMyReports");
}

function highlightTableRow(tableName) {
    var table = document.getElementById(tableName);
    var cells = table.getElementsByTagName('td');

    for (var i = 0; i < cells.length; i++) {
        // Take each cell
        var cell = cells[i];
        // do something on onclick event for cell
        cell.onclick = function () {
            // Get the row id where the cell exists
            var rowId = this.parentNode.rowIndex;

            var rowsNotSelected = table.getElementsByTagName('tr');
            for (var row = 0; row < rowsNotSelected.length; row++) {
                rowsNotSelected[row].style.backgroundColor = "";
                rowsNotSelected[row].classList.remove('selected');
            }
            var rowSelected = table.getElementsByTagName('tr')[rowId];
            rowSelected.style.backgroundColor = "#99FFFF";
            rowSelected.className += " selected";
        }
    }
}

$(function () {
    $('.slider-arrow').click(function () {
        var anchor = this;
        var removeClass = "showPanel";
        var addClass = "hidePanel";
        var diff = "+=260px";
        var arrows = "&laquo;Close Panel";
        if ($(anchor).hasClass("hidePanel")) {
            diff = "-=260px";
            removeClass = "hidePanel";
            addClass = "showPanel";
            arrows = "&raquo;Search Options";
            $("#divSearch").show();
        }
        else {
            $("#divSearch").hide();
        }
        $(".slider-arrow, .floatingPanel").animate({
            left: diff
        }, 700, function () {
            // Animation complete.
            $(anchor).html(arrows).removeClass(removeClass).addClass(addClass);
        });
    });

    $('.slider-bottom-arrow').click(function () {
        var anchor = this;
        var removeClass = "showBottomPanel";
        var addClass = "hideBottomPanel";
        var diff = "+=260px";
        var arrows = "&ReverseUpEquilibrium;";
        if ($(anchor).hasClass("hideBottomPanel")) {
            diff = "-=260px";
            removeClass = "hideBottomPanel";
            addClass = "showBottomPanel";
        }
        $(".slider-bottom-arrow, .bottomPanel").animate({
            bottom: diff
        }, 700, function () {
            // Animation complete.
            $(anchor).html(arrows).removeClass(removeClass).addClass(addClass);
        });
    });
});

$(document).ready(function () {
    var myTimeout = setTimeout(function () {
        $('input.select2-input').attr('autocomplete', "xxxxxxxxxxx");
    }, 1000);
});

function openModalWindow(url, w, h, title, isModal, iframeId) {
    if (document.documentElement.clientHeight < h)
        height = document.documentElement.clientHeight;
    else
        height = h;

    var $dialog = $('<div></div>')
        .css({ overflow: "hidden" })
        .html('<iframe id=' + iframeId + ' style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>')
        .dialog({
            autoOpen: false,
            modal: isModal,
            height: height,
            width: w,
            title: title,
            buttons: {
                Close: function () {
                    $(this).dialog("close");
                }
            },
            closeOnEscape: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
            }
        });
    $dialog.dialog('open');
}
if (!$.ui.dialog.prototype._makeDraggableBase) {
    $.ui.dialog.prototype._makeDraggableBase = $.ui.dialog.prototype._makeDraggable;
    $.ui.dialog.prototype._makeDraggable = function () {
        this._makeDraggableBase();
        this.uiDialog.draggable("option", "containment", false);
    };
}

function txtSlider_KeyUp(x, name) {
    var maxValueTotal;
    switch (name) {
        case "TotalBuildingFloorArea": maxValueTotal = maxTotalBuildingFloorArea; break
        case "CommercialFloorArea": maxValueTotal = maxCommercialFloorArea; break
        case "ResidentialFloorArea": maxValueTotal = maxResidentialFloorArea; break
        case "ResidentialUnits": maxValueTotal = maxResidentialUnits; break
        case "SourceEUI": maxValueTotal = maxSourceEUI; break
        case "SiteEUI": maxValueTotal = maxSiteEUI; break
        case "AnnualMaximumDemand": maxValueTotal = maxAnnualMaximumDemand; break
        case "TotalGHGEmissions": maxValueTotal = maxTotalGHGEmissions; break
        case "AssessedTotalValue": maxValueTotal = maxAssessedTotalValue; break
        default: return
    }
    try {
        var array = x.value.split('-');
        var minValue = array[0].trim().startsWith("$") ? Number(array[0].trim().substr(1).replace(/,/g, '')) : Number(array[0].trim().replace(/,/g, ''));
        var maxValue = array[1].trim().startsWith("$") ? Number(array[1].trim().substr(1).replace(/,/g, '')) : Number(array[1].trim().replace(/,/g, ''));
        var cond1 = array.length == 2;
        var cond2 = !isNaN(minValue);
        var cond3 = !isNaN(maxValue);
        var minValue = minValue < 0 ? 0 : minValue;
        var maxValue = maxValue > maxValueTotal ? maxValueTotal : maxValue;
        if (cond1 && cond2 && cond3) {
            $("#slider-range-" + name).slider("values", 0, minValue);
            $("#slider-range-" + name).slider("values", 1, maxValue);
        }
        else {
            $("#slider-range-" + name).slider("values", 0, 0);
            $("#slider-range-" + name).slider("values", 1, maxValueTotal);
            if (name == "AssessedTotalValue") {
                $("#txt" + name).val("$0 - $" + maxValueTotal.toLocaleString('en'));
            }
            else {
                $("#txt" + name).val("0 - " + maxValueTotal.toLocaleString('en'));
            }
        }
    }
    catch (e) {
        $("#slider-range-" + name).slider("values", 0, 0);
        $("#slider-range-" + name).slider("values", 1, maxValueTotal);
        if (name == "AssessedTotalValue") {
            $("#txt" + name).val("$0 - $" + maxValueTotal.toLocaleString('en'));
        }
        else {
            $("#txt" + name).val("0 - " + maxValueTotal.toLocaleString('en'));
        }
    }
}

function uncheckBuildingClass() {
    for (var i = 0; i < lstBuildingClass.length; i++) {
        var name = lstBuildingClass[i];
        document.getElementById(name).checked = false;
    }
}