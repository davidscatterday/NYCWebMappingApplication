var maxTotalBuildingFloorArea = 67000000, maxCommercialFloorArea = 24000000, maxResidentialFloorArea = 14000000
    , maxNumberOfFloors = 210, maxResidentialUnits = 11000, maxAssessedTotalValue = 7200000000
    , maxEnergyStarScore = 100, maxSourceEUI = 29000000, maxSiteEUI = 25000000, maxAnnualMaximumDemand = 2600000, maxTotalGHGEmissions = 540000000

var lstBuildingClass = ["cbOfficeO1", "cbOfficeO2", "cbOfficeO3", "cbOfficeO4", "cbOfficeO5", "cbOfficeO6", "cbOfficeO8", "cbOfficeO9"
    , "cbIndustrialF1", "cbIndustrialF2", "cbIndustrialF4", "cbIndustrialF5", "cbIndustrialF8", "cbIndustrialF9", "cbRetailK1", "cbOfficeO4"
    , "cbRetailK2", "cbRetailK3", "cbRetailK4", "cbRetailK5", "cbRetailK6", "cbRetailK8", "cbHotelH1", "cbHotelH2"
    , "cbHotelH3", "cbHotelH4", "cbHotelH5", "cbHotelH6", "cbHotelH7", "cbCondominiumsCoopR1", "cbCondominiumsCoopR2"
    , "cbCondominiumsCoopR3", "cbCondominiumsCoopR4", "cbCondominiumsCoopR5", "cbCondominiumsCoopR6", "cbCondominiumsCoopR7", "cbCondominiumsCoopR8", "cbCondominiumsCoopR9"
    , "cbCondominiumsCoopC8", "cbCondominiumsCoopD0", "cbCondominiumsCoopD5", "cbRentalResidentialC0", "cbRentalResidentialC1", "cbRentalResidentialC2", "cbRentalResidentialC3"
    , "cbRentalResidentialC4", "cbRentalResidentialC5", "cbRentalResidentialC6", "cbRentalResidentialD1", "cbRentalResidentialD2", "cbRentalResidentialD3", "cbRentalResidentialD6"
    , "cbRentalResidentialD7", "cbRentalResidentialD8", "cbForProfitOwnedHealthcareI1", "cbForProfitOwnedHealthcareI2", "cbForProfitOwnedHealthcareI3", "cbForProfitOwnedHealthcareI4"
    , "cbForProfitOwnedHealthcareI5", "cbForProfitOwnedHealthcareI6", "cbForProfitOwnedHealthcareI7"];
var lstTableAttributes = [];
var lstAllTableAttributes = [];
lstAllTableAttributes.push({ name: 'Borough', attribute: "Borough", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'District', attribute: "CD", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Address', attribute: "Address", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Zip Code', attribute: "ZipCode", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Latitude', attribute: "Latitude", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Longitude', attribute: "Longitude", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Total Building Floor Area', attribute: "BldgArea", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Commercial Floor Area', attribute: "ComArea", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Residential Floor Area', attribute: "ResArea", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Number of Floors', attribute: "NumFloors", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Residential Units', attribute: "UnitsRes", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Zoning District', attribute: "ZoneDist1", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Commercial Overlay 1', attribute: "Overlay1", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Commercial Overlay 2', attribute: "Overlay2", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Assessed Total Value', attribute: "AssessTot", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Year Built', attribute: "YearBuilt", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Owner Name', attribute: "OwnerName", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Building Class', attribute: "BldgClass", dataset: "Pluto" });
lstAllTableAttributes.push({ name: 'Energy Star Score', attribute: "energy_star_score", dataset: "Energy" });
lstAllTableAttributes.push({ name: 'Source EUI', attribute: "source_eui_kbtu_ft", dataset: "Energy" });
lstAllTableAttributes.push({ name: 'Site EUI', attribute: "site_eui_kbtu_ft", dataset: "Energy" });
lstAllTableAttributes.push({ name: 'Annual Maximum Demand', attribute: "annual_maximum_demand_kw", dataset: "Energy" });
lstAllTableAttributes.push({ name: 'Total GHG Emissions', attribute: "total_ghg_emissions_metric", dataset: "Energy" });
lstAllTableAttributes.push({ name: 'Job Start Date', attribute: "job_start_date_string_format", dataset: "Permit" });
lstAllTableAttributes.push({ name: 'Job Type', attribute: "job_type", dataset: "Permit" });
lstAllTableAttributes.push({ name: 'Work Type', attribute: "work_type", dataset: "Permit" });
lstAllTableAttributes.push({ name: 'Issue Date', attribute: "issue_date_string_format", dataset: "Violation" });
lstAllTableAttributes.push({ name: 'Violation Type', attribute: "violation_type", dataset: "Violation" });
lstAllTableAttributes.push({ name: 'Violation Category', attribute: "violation_category", dataset: "Violation" });
lstAllTableAttributes.push({ name: 'Executed Date', attribute: "executed_date_string_format", dataset: "Evictions" });
lstAllTableAttributes.push({ name: 'District', attribute: "DISTRICT", dataset: "Districts" });
lstAllTableAttributes.push({ name: 'Organization Name', attribute: "OrganizationName", dataset: "SocialServiceOrganizations" });
//lstAllTableAttributes.push({ name: 'Address', attribute: "Address1", dataset: "SocialServiceOrganizations" });
//lstAllTableAttributes.push({ name: 'Category', attribute: "Category", dataset: "SocialServiceOrganizations" });
lstAllTableAttributes.push({ name: 'Faith Based Organization', attribute: "Faith_Based_Organization", dataset: "SocialServiceOrganizations" });
lstAllTableAttributes.push({ name: 'Foundation', attribute: "Foundation", dataset: "SocialServiceOrganizations" });
lstAllTableAttributes.push({ name: 'New York City Agency', attribute: "New_York_City_Agency", dataset: "SocialServiceOrganizations" });
lstAllTableAttributes.push({ name: 'Non-Profit', attribute: "Nonprofit", dataset: "SocialServiceOrganizations" });

var sqlQuery = "";
var IsPlutoSearch = false, IsEnergySearch = false, IsPermitSearch = false, IsViolationSearch = false, IsEvictionSearch = false, IsSocialServiceOrganizationsSearch = false;
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
    document.getElementById("cbDistrict").checked = false;
    document.getElementById("cbZipCodeRange").checked = false;
    document.getElementById("cbStreetAddress").checked = false;
    document.getElementById("cbLatLong").checked = false;
    $("#txtBoroughs").select2("val", "");
    $("#txtDistricts").select2("val", "");
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
    document.getElementById("cbRentStabilizedProperty").checked = false;

    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();
    $('#btnOpenSaveReport').hide();
    $('#btnOpenAlerts').hide();

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

    document.getElementById("cbExecutedDate").checked = false;
    document.getElementById("cbEvictionStatus").checked = false;
    document.getElementById("txtExecutedDateFrom").value = "";
    document.getElementById("txtExecutedDateTo").value = "";
    $("#ddlEvictionStatus").val($("#ddlEvictionStatus option:first").val());

    document.getElementById("cbAgingStatus").checked = false;
    $("#ddlAgingStatus").val($("#ddlAgingStatus option:first").val());
    document.getElementById("cbAnti_Discrimination_Human_RightsStatus").checked = false;
    $("#ddlAnti_Discrimination_Human_RightsStatus").val($("#ddlAnti_Discrimination_Human_RightsStatus option:first").val());
    document.getElementById("cbArts_CultureStatus").checked = false;
    $("#ddlArts_CultureStatus").val($("#ddlArts_CultureStatus option:first").val());
    document.getElementById("cbBusinessStatus").checked = false;
    $("#ddlBusinessStatus").val($("#ddlBusinessStatus option:first").val());
    document.getElementById("cbChild_Care_Parent_InformationStatus").checked = false;
    $("#ddlChild_Care_Parent_InformationStatus").val($("#ddlChild_Care_Parent_InformationStatus option:first").val());
    document.getElementById("cbCommunity_Service_VolunteerismStatus").checked = false;
    $("#ddlCommunity_Service_VolunteerismStatus").val($("#ddlCommunity_Service_VolunteerismStatus option:first").val());
    document.getElementById("cbCounseling_Support_GroupsStatus").checked = false;
    $("#ddlCounseling_Support_GroupsStatus").val($("#ddlCounseling_Support_GroupsStatus option:first").val());
    document.getElementById("cbDisabilitiesStatus").checked = false;
    $("#ddlDisabilitiesStatus").val($("#ddlDisabilitiesStatus option:first").val());
    document.getElementById("cbDomestic_ViolenceStatus").checked = false;
    $("#ddlDomestic_ViolenceStatus").val($("#ddlDomestic_ViolenceStatus option:first").val());
    document.getElementById("cbEducationStatus").checked = false;
    $("#ddlEducationStatus").val($("#ddlEducationStatus option:first").val());
    document.getElementById("cbEmployment_Job_TrainingStatus").checked = false;
    $("#ddlEmployment_Job_TrainingStatus").val($("#ddlEmployment_Job_TrainingStatus option:first").val());
    document.getElementById("cbHealthStatus").checked = false;
    $("#ddlHealthStatus").val($("#ddlHealthStatus option:first").val());
    document.getElementById("cbHomelessnessStatus").checked = false;
    $("#ddlHomelessnessStatus").val($("#ddlHomelessnessStatus option:first").val());
    document.getElementById("cbHousingStatus").checked = false;
    $("#ddlHousingStatus").val($("#ddlHousingStatus option:first").val());
    document.getElementById("cbImmigrationStatus").checked = false;
    $("#ddlImmigrationStatus").val($("#ddlImmigrationStatus option:first").val());
    document.getElementById("cbLegal_ServicesStatus").checked = false;
    $("#ddlLegal_ServicesStatus").val($("#ddlLegal_ServicesStatus option:first").val());
    document.getElementById("cbLesbian_Gay_Bisexual_and_or_TransgenderStatus").checked = false;
    $("#ddlLesbian_Gay_Bisexual_and_or_TransgenderStatus").val($("#ddlLesbian_Gay_Bisexual_and_or_TransgenderStatus option:first").val());
    document.getElementById("cbPersonal_Finance_Financial_EducationStatus").checked = false;
    $("#ddlPersonal_Finance_Financial_EducationStatus").val($("#ddlPersonal_Finance_Financial_EducationStatus option:first").val());
    document.getElementById("cbProfessional_AssociationStatus").checked = false;
    $("#ddlProfessional_AssociationStatus").val($("#ddlProfessional_AssociationStatus option:first").val());
    document.getElementById("cbVeterans_Military_FamiliesStatus").checked = false;
    $("#ddlVeterans_Military_FamiliesStatus").val($("#ddlVeterans_Military_FamiliesStatus option:first").val());
    document.getElementById("cbVictim_ServicesStatus").checked = false;
    $("#ddlVictim_ServicesStatus").val($("#ddlVictim_ServicesStatus option:first").val());
    document.getElementById("cbWomen_s_GroupsStatus").checked = false;
    $("#ddlWomen_s_GroupsStatus").val($("#ddlWomen_s_GroupsStatus option:first").val());
    document.getElementById("cbYouth_ServicesStatus").checked = false;
    $("#ddlYouth_ServicesStatus").val($("#ddlYouth_ServicesStatus option:first").val());

    document.getElementById("rbSearchByBBL").checked = true;
    document.getElementById("rbSearchByAddress").checked = false;
    $("#txtLookaLikeBbl").select2("val", "");
    $("#txtLookaLikeAddress").select2("val", "");
    $('#divLookaLikeAddress').hide();
    $('#divLookaLikeBbl').show();

    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
}

function btnSearch() {
    districtLayer.clear();
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
    var whereEvictionsClause = "";
    var whereSocialServiceOrganizationsClause = "";
    if (document.getElementById("cbBorough").checked == true) {
        Borough = $(txtBoroughs).val();
        if (Borough != "") {
            IsPlutoSearch = true;
            if (whereClause == "") {
                whereClause = "p.Borough IN (" + Borough + ")";
            }
            else {
                whereClause += " AND p.Borough IN (" + Borough + ")";
            }
        }
    }
    if (document.getElementById("cbZipCodeRange").checked == true) {
        ZipCodeRangeFrom = document.getElementById("txtZipCodeRangeFrom").value;
        ZipCodeRangeTo = document.getElementById("txtZipCodeRangeTo").value;
        if (ZipCodeRangeFrom != "" || ZipCodeRangeTo != "") {
            IsPlutoSearch = true;
            lstTableAttributes.push({ name: 'Zip Code', attribute: "ZipCode", dataset: "Pluto" });
            if (whereClause != "") {
                whereClause += " AND ";
            }
            if (ZipCodeRangeFrom != "" && ZipCodeRangeTo != "") {
                whereClause += "p.ZipCode >= " + ZipCodeRangeFrom + " AND p.ZipCode <= " + ZipCodeRangeTo;
            }
            else if (ZipCodeRangeFrom != "") {
                whereClause += "p.ZipCode >= " + ZipCodeRangeFrom;;
            }
            else if (ZipCodeRangeTo != "") {
                whereClause += "p.ZipCode <= " + ZipCodeRangeTo;;
            }
        }
    }
    if (document.getElementById("cbStreetAddress").checked == true) {
        StreetAddress = document.getElementById("txtStreetAddress").value;
        if (StreetAddress != "") {
            IsPlutoSearch = true;
            if (whereClause == "") {
                whereClause = "p.Address LIKE '%" + StreetAddress + "%'";
            }
            else {
                whereClause += " AND p.Address LIKE '%" + StreetAddress + "%'";
            }
        }
    }
    if (document.getElementById("cbLatLong").checked == true) {
        Lat = document.getElementById("txtLat").value;
        Long = document.getElementById("txtLong").value;
        if (Lat != "" || Long != "") {
            IsPlutoSearch = true;
            if (whereClause != "") {
                whereClause += " AND ";
            }
            if (Lat != "" && Long != "") {
                lstTableAttributes.push({ name: 'Latitude', attribute: "Latitude", dataset: "Pluto" });
                lstTableAttributes.push({ name: 'Longitude', attribute: "Longitude", dataset: "Pluto" });
                whereClause += "p.Latitude = " + Lat + " AND p.Longitude = " + Long;
            }
            else if (Lat != "") {
                lstTableAttributes.push({ name: 'Latitude', attribute: "Latitude", dataset: "Pluto" });
                whereClause += "p.Latitude = " + Lat;;
            }
            else if (Long != "") {
                lstTableAttributes.push({ name: 'Longitude', attribute: "Longitude", dataset: "Pluto" });
                whereClause += "p.Longitude = " + Long;;
            }
        }
    }

    if (document.getElementById("cbTotalBuildingFloorArea").checked == true) {
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Total Building Floor Area', attribute: "BldgArea", dataset: "Pluto" });
        TotalBuildingFloorAreaStart = $("#slider-range-TotalBuildingFloorArea").slider("values", 0);
        TotalBuildingFloorAreaEnd = $("#slider-range-TotalBuildingFloorArea").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.BldgArea >= " + TotalBuildingFloorAreaStart + " AND p.BldgArea <= " + TotalBuildingFloorAreaEnd;
        }
        else {
            whereClause += " AND p.BldgArea >= " + TotalBuildingFloorAreaStart + " AND p.BldgArea <= " + TotalBuildingFloorAreaEnd;
        }
    }
    if (document.getElementById("cbCommercialFloorArea").checked == true) {
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Commercial Floor Area', attribute: "ComArea", dataset: "Pluto" });
        CommercialFloorAreaStart = $("#slider-range-CommercialFloorArea").slider("values", 0);
        CommercialFloorAreaEnd = $("#slider-range-CommercialFloorArea").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.ComArea >= " + CommercialFloorAreaStart + " AND p.ComArea <= " + CommercialFloorAreaEnd;
        }
        else {
            whereClause += " AND p.ComArea >= " + CommercialFloorAreaStart + " AND p.ComArea <= " + CommercialFloorAreaEnd;
        }
    }
    if (document.getElementById("cbResidentialFloorArea").checked == true) {
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Residential Floor Area', attribute: "ResArea", dataset: "Pluto" });
        ResidentialFloorAreaStart = $("#slider-range-ResidentialFloorArea").slider("values", 0);
        ResidentialFloorAreaEnd = $("#slider-range-ResidentialFloorArea").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.ResArea >= " + ResidentialFloorAreaStart + " AND p.ResArea <= " + ResidentialFloorAreaEnd;
        }
        else {
            whereClause += " AND p.ResArea >= " + ResidentialFloorAreaStart + " AND p.ResArea <= " + ResidentialFloorAreaEnd;
        }
    }
    if (document.getElementById("cbNumberOfFloors").checked == true) {
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Number of Floors', attribute: "NumFloors", dataset: "Pluto" });
        NumberOfFloorsStart = $("#slider-range-NumberOfFloors").slider("values", 0);
        NumberOfFloorsEnd = $("#slider-range-NumberOfFloors").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.NumFloors >= " + NumberOfFloorsStart + " AND p.NumFloors <= " + NumberOfFloorsEnd;
        }
        else {
            whereClause += " AND p.NumFloors >= " + NumberOfFloorsStart + " AND p.NumFloors <= " + NumberOfFloorsEnd;
        }
    }
    if (document.getElementById("cbResidentialUnits").checked == true) {
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Residential Units', attribute: "UnitsRes", dataset: "Pluto" });
        ResidentialUnitsStart = $("#slider-range-ResidentialUnits").slider("values", 0);
        ResidentialUnitsEnd = $("#slider-range-ResidentialUnits").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.UnitsRes >= " + ResidentialUnitsStart + " AND p.UnitsRes <= " + ResidentialUnitsEnd;
        }
        else {
            whereClause += " AND p.UnitsRes >= " + ResidentialUnitsStart + " AND p.UnitsRes <= " + ResidentialUnitsEnd;
        }
    }


    if (document.getElementById("cbZoningDistrict").checked == true) {
        IsPlutoSearch = true;
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
        IsPlutoSearch = true;
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
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Assessed Total Value', attribute: "AssessTot", dataset: "Pluto" });
        AssessedTotalValueStart = $("#slider-range-AssessedTotalValue").slider("values", 0);
        AssessedTotalValueEnd = $("#slider-range-AssessedTotalValue").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.AssessTot >= " + AssessedTotalValueStart + " AND p.AssessTot <= " + AssessedTotalValueEnd;
        }
        else {
            whereClause += " AND p.AssessTot >= " + AssessedTotalValueStart + " AND p.AssessTot <= " + AssessedTotalValueEnd;
        }
    }
    if (document.getElementById("cbYearBuild").checked == true) {
        IsPlutoSearch = true;
        lstTableAttributes.push({ name: 'Year Built', attribute: "YearBuilt", dataset: "Pluto" });
        YearBuildStart = $("#slider-range-YearBuild").slider("values", 0);
        YearBuildEnd = $("#slider-range-YearBuild").slider("values", 1);
        if (whereClause == "") {
            whereClause = "p.YearBuilt >= " + YearBuildStart + " AND p.YearBuilt <= " + YearBuildEnd;
        }
        else {
            whereClause += " AND p.YearBuilt >= " + YearBuildStart + " AND p.YearBuilt <= " + YearBuildEnd;
        }
    }
    if (document.getElementById("cbOwnerName").checked == true) {
        lstTableAttributes.push({ name: 'Owner Name', attribute: "OwnerName", dataset: "Pluto" });
    }
    if (document.getElementById("cbRentStabilizedProperty").checked == true) {
        if (whereClause == "") {
            whereClause = "p.IsRentStabilizedProperty = 1";
        }
        else {
            whereClause += " AND p.IsRentStabilizedProperty = 1";
        }
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
            IsPlutoSearch = true;
            lstTableAttributes.push({ name: 'Building Class', attribute: "BldgClass", dataset: "Pluto" });
            if (whereClause == "") {
                whereClause = "p.BldgClass IN (" + buildingClassValues + ")";
            }
            else {
                whereClause += " AND p.BldgClass IN (" + buildingClassValues + ")";
            }
        }
    }

    if (document.getElementById("cbEnergyStarScore").checked == true) {
        IsEnergySearch = true;
        lstTableAttributes.push({ name: 'Energy Star Score', attribute: "energy_star_score", dataset: "Energy" });
        EnergyStarScoreStart = $("#slider-range-EnergyStarScore").slider("values", 0);
        EnergyStarScoreEnd = $("#slider-range-EnergyStarScore").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "en.energy_star_score >= " + EnergyStarScoreStart + " AND en.energy_star_score <= " + EnergyStarScoreEnd;
        }
        else {
            whereEnergyClause += " AND en.energy_star_score >= " + EnergyStarScoreStart + " AND en.energy_star_score <= " + EnergyStarScoreEnd;
        }
    }
    if (document.getElementById("cbSourceEUI").checked == true) {
        IsEnergySearch = true;
        lstTableAttributes.push({ name: 'Source EUI', attribute: "source_eui_kbtu_ft", dataset: "Energy" });
        SourceEUIStart = $("#slider-range-SourceEUI").slider("values", 0);
        SourceEUIEnd = $("#slider-range-SourceEUI").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "en.source_eui_kbtu_ft >= " + SourceEUIStart + " AND en.source_eui_kbtu_ft <= " + SourceEUIEnd;
        }
        else {
            whereEnergyClause += " AND en.source_eui_kbtu_ft >= " + SourceEUIStart + " AND en.source_eui_kbtu_ft <= " + SourceEUIEnd;
        }
    }
    if (document.getElementById("cbSiteEUI").checked == true) {
        IsEnergySearch = true;
        lstTableAttributes.push({ name: 'Site EUI', attribute: "site_eui_kbtu_ft", dataset: "Energy" });
        SiteEUIStart = $("#slider-range-SiteEUI").slider("values", 0);
        SiteEUIEnd = $("#slider-range-SiteEUI").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "en.site_eui_kbtu_ft >= " + SiteEUIStart + " AND en.site_eui_kbtu_ft <= " + SiteEUIEnd;
        }
        else {
            whereEnergyClause += " AND en.site_eui_kbtu_ft >= " + SiteEUIStart + " AND en.site_eui_kbtu_ft <= " + SiteEUIEnd;
        }
    }
    if (document.getElementById("cbAnnualMaximumDemand").checked == true) {
        IsEnergySearch = true;
        lstTableAttributes.push({ name: 'Annual Maximum Demand', attribute: "annual_maximum_demand_kw", dataset: "Energy" });
        AnnualMaximumDemandStart = $("#slider-range-AnnualMaximumDemand").slider("values", 0);
        AnnualMaximumDemandEnd = $("#slider-range-AnnualMaximumDemand").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "en.annual_maximum_demand_kw >= " + AnnualMaximumDemandStart + " AND en.annual_maximum_demand_kw <= " + AnnualMaximumDemandEnd;
        }
        else {
            whereEnergyClause += " AND en.annual_maximum_demand_kw >= " + AnnualMaximumDemandStart + " AND en.annual_maximum_demand_kw <= " + AnnualMaximumDemandEnd;
        }
    }
    if (document.getElementById("cbTotalGHGEmissions").checked == true) {
        IsEnergySearch = true;
        lstTableAttributes.push({ name: 'Total GHG Emissions', attribute: "total_ghg_emissions_metric", dataset: "Energy" });
        TotalGHGEmissionsStart = $("#slider-range-TotalGHGEmissions").slider("values", 0);
        TotalGHGEmissionsEnd = $("#slider-range-TotalGHGEmissions").slider("values", 1);
        if (whereEnergyClause == "") {
            whereEnergyClause = "en.total_ghg_emissions_metric >= " + TotalGHGEmissionsStart + " AND en.total_ghg_emissions_metric <= " + TotalGHGEmissionsEnd;
        }
        else {
            whereEnergyClause += " AND en.total_ghg_emissions_metric >= " + TotalGHGEmissionsStart + " AND en.total_ghg_emissions_metric <= " + TotalGHGEmissionsEnd;
        }
    }

    if (document.getElementById("cbJobStartDate").checked == true) {
        IsPermitSearch = true;
        JobStartDateFrom = document.getElementById("txtJobStartDateFrom").value;
        JobStartDateTo = document.getElementById("txtJobStartDateTo").value;
        if (JobStartDateFrom != "" || JobStartDateTo != "") {
            lstTableAttributes.push({ name: 'Job Start Date', attribute: "job_start_date", dataset: "Permit" });
            if (wherePermitClause != "") {
                wherePermitClause += " AND ";
            }
            if (JobStartDateFrom != "" && JobStartDateTo != "") {
                var dFrom = new Date(JobStartDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(JobStartDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                wherePermitClause += "pe.job_start_date >= '" + valueFrom + "' AND pe.job_start_date <= '" + valueTo + "'";
            }
            else if (JobStartDateFrom != "") {
                var dFrom = new Date(JobStartDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                wherePermitClause += "pe.job_start_date >= '" + valueFrom + "'";
            }
            else if (JobStartDateTo != "") {
                var dTo = new Date(JobStartDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                wherePermitClause += "pe.job_start_date <= '" + valueTo + "'";
            }
        }
    }
    if (document.getElementById("cbJobType").checked == true) {
        JobType = $(txtJobTypes).val();
        if (JobType != "") {
            IsPermitSearch = true;
            lstTableAttributes.push({ name: 'Job Type', attribute: "job_type", dataset: "Permit" });
            if (wherePermitClause == "") {
                wherePermitClause = "pe.job_type IN (" + JobType + ")";
            }
            else {
                wherePermitClause += " AND pe.job_type IN (" + JobType + ")";
            }
        }
    }
    if (document.getElementById("cbWorkType").checked == true) {
        WorkType = $(txtWorkTypes).val();
        if (WorkType != "") {
            IsPermitSearch = true;
            lstTableAttributes.push({ name: 'Work Type', attribute: "work_type", dataset: "Permit" });
            if (wherePermitClause == "") {
                wherePermitClause = "pe.work_type IN (" + WorkType + ")";
            }
            else {
                wherePermitClause += " AND pe.work_type IN (" + WorkType + ")";
            }
        }
    }

    if (document.getElementById("cbIssueDate").checked == true) {
        IssueDateFrom = document.getElementById("txtIssueDateFrom").value;
        IssueDateTo = document.getElementById("txtIssueDateTo").value;
        if (IssueDateFrom != "" || IssueDateTo != "") {
            IsViolationSearch = true;
            lstTableAttributes.push({ name: 'Issue Date', attribute: "issue_date", dataset: "Violation" });
            if (whereViolationClause != "") {
                whereViolationClause += " AND ";
            }
            if (IssueDateFrom != "" && IssueDateTo != "") {
                var dFrom = new Date(IssueDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + monthFrom + dayFrom;

                var dTo = new Date(IssueDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + monthTo + dayTo;
                whereViolationClause += "v.issue_date >= '" + valueFrom + "' AND v.issue_date <= '" + valueTo + "'";
            }
            else if (IssueDateFrom != "") {
                var dFrom = new Date(IssueDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + monthFrom + dayFrom;
                whereViolationClause += "v.issue_date >= '" + valueFrom + "'";
            }
            else if (IssueDateTo != "") {
                var dTo = new Date(IssueDateTo);
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
    if (document.getElementById("cbViolationType").checked == true) {
        ViolationType = $(txtViolationTypes).val();
        if (ViolationType != "") {
            IsViolationSearch = true;
            lstTableAttributes.push({ name: 'Violation Type', attribute: "violation_type", dataset: "Violation" });
            if (whereViolationClause == "") {
                whereViolationClause = "v.violation_type IN (" + ViolationType + ")";
            }
            else {
                whereViolationClause += " AND v.violation_type IN (" + ViolationType + ")";
            }
        }
    }
    if (document.getElementById("cbViolationCategory").checked == true) {
        ViolationCategory = $(txtViolationCategories).val();
        if (ViolationCategory != "") {
            IsViolationSearch = true;
            lstTableAttributes.push({ name: 'Violation Category', attribute: "violation_category", dataset: "Violation" });
            if (whereViolationClause == "") {
                whereViolationClause = "v.violation_category IN (" + ViolationCategory + ")";
            }
            else {
                whereViolationClause += " AND v.violation_category IN (" + ViolationCategory + ")";
            }
        }
    }

    if (document.getElementById("cbExecutedDate").checked == true) {
        ExecutedDateFrom = document.getElementById("txtExecutedDateFrom").value;
        ExecutedDateTo = document.getElementById("txtExecutedDateTo").value;
        if (ExecutedDateFrom != "" || ExecutedDateTo != "") {
            IsEvictionSearch = true;
            lstTableAttributes.push({ name: 'Executed Date', attribute: "executed_date", dataset: "Evictions" });
            if (whereEvictionsClause != "") {
                whereEvictionsClause += " AND ";
            }
            if (ExecutedDateFrom != "" && ExecutedDateTo != "") {
                var dFrom = new Date(ExecutedDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;

                var dTo = new Date(ExecutedDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereEvictionsClause += "ev.executed_date >= '" + valueFrom + "' AND ev.executed_date <= '" + valueTo + "'";
            }
            else if (ExecutedDateFrom != "") {
                var dFrom = new Date(ExecutedDateFrom);
                var yearFrom = dFrom.getFullYear();
                var monthFrom = dFrom.getMonth() + 1;
                monthFrom = monthFrom < 10 ? "0" + monthFrom : monthFrom;
                var dayFrom = dFrom.getDate();
                dayFrom = dayFrom < 10 ? "0" + dayFrom : dayFrom;
                var valueFrom = yearFrom + "-" + monthFrom + "-" + dayFrom;
                whereEvictionsClause += "ev.executed_date >= '" + valueFrom + "'";
            }
            else if (ExecutedDateTo != "") {
                var dTo = new Date(ExecutedDateTo);
                var yearTo = dTo.getFullYear();
                var monthTo = dTo.getMonth() + 1;
                monthTo = monthTo < 10 ? "0" + monthTo : monthTo;
                var dayTo = dTo.getDate();
                dayTo = dayTo < 10 ? "0" + dayTo : dayTo;
                var valueTo = yearTo + "-" + monthTo + "-" + dayTo;
                whereEvictionsClause += "ev.executed_date <= '" + valueTo + "'";
            }
        }
    }
    if (document.getElementById("cbEvictionStatus").checked == true) {
        var cbEvictionStatus = document.getElementById("cbEvictionStatus");
        var selectedEvictionStatus = ddlEvictionStatus.options[ddlEvictionStatus.selectedIndex].value;
        if (selectedEvictionStatus != "") {
            if (whereEvictionsClause == "") {
                whereEvictionsClause = selectedEvictionStatus;
            }
            else {
                whereEvictionsClause += " AND " + selectedEvictionStatus;
            }
        }
    }
    if (document.getElementById("cbDistrict").checked == true) {
        District = $(txtDistricts).val();
        if (District != "") {
            lstTableAttributes.push({ name: 'District', attribute: "DISTRICT", dataset: "Districts" });
            IsPlutoSearch = true;
            if (whereClause == "") {
                whereClause = "p.CD IN (" + District + ")";
            }
            else {
                whereClause += " AND p.CD IN (" + District + ")";
            }
            SearchGeometry("DISTRICTCODE IN ('" + District + "')");
        }
    }
    var selectedAgingStatus = ddlAgingStatus.options[ddlAgingStatus.selectedIndex].value;
    var selectedAnti_Discrimination_Human_RightsStatus = ddlAnti_Discrimination_Human_RightsStatus.options[ddlAnti_Discrimination_Human_RightsStatus.selectedIndex].value;
    var selectedArts_CultureStatus = ddlArts_CultureStatus.options[ddlArts_CultureStatus.selectedIndex].value;
    var selectedBusinessStatus = ddlBusinessStatus.options[ddlBusinessStatus.selectedIndex].value;
    var selectedChild_Care_Parent_InformationStatus = ddlChild_Care_Parent_InformationStatus.options[ddlChild_Care_Parent_InformationStatus.selectedIndex].value;
    var selectedCommunity_Service_VolunteerismStatus = ddlCommunity_Service_VolunteerismStatus.options[ddlCommunity_Service_VolunteerismStatus.selectedIndex].value;
    var selectedCounseling_Support_GroupsStatus = ddlCounseling_Support_GroupsStatus.options[ddlCounseling_Support_GroupsStatus.selectedIndex].value;
    var selectedDisabilitiesStatus = ddlDisabilitiesStatus.options[ddlDisabilitiesStatus.selectedIndex].value;
    var selectedDomestic_ViolenceStatus = ddlDomestic_ViolenceStatus.options[ddlDomestic_ViolenceStatus.selectedIndex].value;
    var selectedEducationStatus = ddlEducationStatus.options[ddlEducationStatus.selectedIndex].value;
    var selectedEmployment_Job_TrainingStatus = ddlEmployment_Job_TrainingStatus.options[ddlEmployment_Job_TrainingStatus.selectedIndex].value;
    var selectedHealthStatus = ddlHealthStatus.options[ddlHealthStatus.selectedIndex].value;
    var selectedHomelessnessStatus = ddlHomelessnessStatus.options[ddlHomelessnessStatus.selectedIndex].value;
    var selectedHousingStatus = ddlHousingStatus.options[ddlHousingStatus.selectedIndex].value;
    var selectedImmigrationStatus = ddlImmigrationStatus.options[ddlImmigrationStatus.selectedIndex].value;
    var selectedLegal_ServicesStatus = ddlLegal_ServicesStatus.options[ddlLegal_ServicesStatus.selectedIndex].value;
    var selectedLesbian_Gay_Bisexual_and_or_TransgenderStatus = ddlLesbian_Gay_Bisexual_and_or_TransgenderStatus.options[ddlLesbian_Gay_Bisexual_and_or_TransgenderStatus.selectedIndex].value;
    var selectedPersonal_Finance_Financial_EducationStatus = ddlPersonal_Finance_Financial_EducationStatus.options[ddlPersonal_Finance_Financial_EducationStatus.selectedIndex].value;
    var selectedProfessional_AssociationStatus = ddlProfessional_AssociationStatus.options[ddlProfessional_AssociationStatus.selectedIndex].value;
    var selectedVeterans_Military_FamiliesStatus = ddlVeterans_Military_FamiliesStatus.options[ddlVeterans_Military_FamiliesStatus.selectedIndex].value;
    var selectedVictim_ServicesStatus = ddlVictim_ServicesStatus.options[ddlVictim_ServicesStatus.selectedIndex].value;
    var selectedWomen_s_GroupsStatus = ddlWomen_s_GroupsStatus.options[ddlWomen_s_GroupsStatus.selectedIndex].value;
    var selectedYouth_ServicesStatus = ddlYouth_ServicesStatus.options[ddlYouth_ServicesStatus.selectedIndex].value;


    if (
        (document.getElementById("cbAgingStatus").checked == true && selectedAgingStatus != "") ||
        (document.getElementById("cbAnti_Discrimination_Human_RightsStatus").checked == true && selectedAnti_Discrimination_Human_RightsStatus != "") ||
        (document.getElementById("cbArts_CultureStatus").checked == true && selectedArts_CultureStatus != "") ||
        (document.getElementById("cbBusinessStatus").checked == true && selectedBusinessStatus != "") ||
        (document.getElementById("cbChild_Care_Parent_InformationStatus").checked == true && selectedChild_Care_Parent_InformationStatus != "") ||
        (document.getElementById("cbCommunity_Service_VolunteerismStatus").checked == true && selectedCommunity_Service_VolunteerismStatus != "") ||
        (document.getElementById("cbCounseling_Support_GroupsStatus").checked == true && selectedCounseling_Support_GroupsStatus != "") ||
        (document.getElementById("cbDisabilitiesStatus").checked == true && selectedDisabilitiesStatus != "") ||
        (document.getElementById("cbDomestic_ViolenceStatus").checked == true && selectedDomestic_ViolenceStatus != "") ||
        (document.getElementById("cbEducationStatus").checked == true && selectedEducationStatus != "") ||
        (document.getElementById("cbEmployment_Job_TrainingStatus").checked == true && selectedEmployment_Job_TrainingStatus != "") ||
        (document.getElementById("cbHealthStatus").checked == true && selectedHealthStatus != "") ||
        (document.getElementById("cbHomelessnessStatus").checked == true && selectedHomelessnessStatus != "") ||
        (document.getElementById("cbHousingStatus").checked == true && selectedHousingStatus != "") ||
        (document.getElementById("cbImmigrationStatus").checked == true && selectedImmigrationStatus != "") ||
        (document.getElementById("cbLegal_ServicesStatus").checked == true && selectedLegal_ServicesStatus != "") ||
        (document.getElementById("cbLesbian_Gay_Bisexual_and_or_TransgenderStatus").checked == true && selectedLesbian_Gay_Bisexual_and_or_TransgenderStatus != "") ||
        (document.getElementById("cbPersonal_Finance_Financial_EducationStatus").checked == true && selectedPersonal_Finance_Financial_EducationStatus != "") ||
        (document.getElementById("cbProfessional_AssociationStatus").checked == true && selectedProfessional_AssociationStatus != "") ||
        (document.getElementById("cbVeterans_Military_FamiliesStatus").checked == true && selectedVeterans_Military_FamiliesStatus != "") ||
        (document.getElementById("cbVictim_ServicesStatus").checked == true && selectedVictim_ServicesStatus != "") ||
        (document.getElementById("cbWomen_s_GroupsStatus").checked == true && selectedWomen_s_GroupsStatus != "") ||
        (document.getElementById("cbYouth_ServicesStatus").checked == true && selectedYouth_ServicesStatus != "")) {
        lstTableAttributes.push({ name: 'Organization Name', attribute: "OrganizationName", dataset: "SocialServiceOrganizations" });
        //  lstTableAttributes.push({ name: 'Address', attribute: "Address1", dataset: "SocialServiceOrganizations" });
        //lstTableAttributes.push({ name: 'Category', attribute: "Category", dataset: "SocialServiceOrganizations" });
        lstTableAttributes.push({ name: 'Faith Based Organization', attribute: "Faith_Based_Organization", dataset: "SocialServiceOrganizations" });
        lstTableAttributes.push({ name: 'Foundation', attribute: "Foundation", dataset: "SocialServiceOrganizations" });
        lstTableAttributes.push({ name: 'New York City Agency', attribute: "New_York_City_Agency", dataset: "SocialServiceOrganizations" });
        lstTableAttributes.push({ name: 'Non-Profit', attribute: "Nonprofit", dataset: "SocialServiceOrganizations" });
        if (document.getElementById("cbAgingStatus").checked == true && selectedAgingStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Aging = '" + selectedAgingStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Aging = '" + selectedAgingStatus + "'";
            }
        }
        if (document.getElementById("cbAnti_Discrimination_Human_RightsStatus").checked == true && selectedAnti_Discrimination_Human_RightsStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Anti_Discrimination_Human_Rights = '" + selectedAnti_Discrimination_Human_RightsStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Anti_Discrimination_Human_Rights = '" + selectedAnti_Discrimination_Human_RightsStatus + "'";
            }
        }
        if (document.getElementById("cbArts_CultureStatus").checked == true && selectedArts_CultureStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Arts_Culture = '" + selectedArts_CultureStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Arts_Culture = '" + selectedArts_CultureStatus + "'";
            }
        }
        if (document.getElementById("cbBusinessStatus").checked == true && selectedBusinessStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Business = '" + selectedBusinessStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Business = '" + selectedBusinessStatus + "'";
            }
        }
        if (document.getElementById("cbChild_Care_Parent_InformationStatus").checked == true && selectedChild_Care_Parent_InformationStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Child_Care_Parent_Information = '" + selectedChild_Care_Parent_InformationStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Child_Care_Parent_Information = '" + selectedChild_Care_Parent_InformationStatus + "'";
            }
        }
        if (document.getElementById("cbCommunity_Service_VolunteerismStatus").checked == true && selectedCommunity_Service_VolunteerismStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Community_Service_Volunteerism = '" + selectedCommunity_Service_VolunteerismStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Community_Service_Volunteerism = '" + selectedCommunity_Service_VolunteerismStatus + "'";
            }
        }
        if (document.getElementById("cbCounseling_Support_GroupsStatus").checked == true && selectedCounseling_Support_GroupsStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Counseling_Support_Groups = '" + selectedCounseling_Support_GroupsStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Counseling_Support_Groups = '" + selectedCounseling_Support_GroupsStatus + "'";
            }
        }
        if (document.getElementById("cbDisabilitiesStatus").checked == true && selectedDisabilitiesStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Disabilities = '" + selectedDisabilitiesStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Disabilities = '" + selectedDisabilitiesStatus + "'";
            }
        }
        if (document.getElementById("cbDomestic_ViolenceStatus").checked == true && selectedDomestic_ViolenceStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Domestic_Violence = '" + selectedDomestic_ViolenceStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Domestic_Violence = '" + selectedDomestic_ViolenceStatus + "'";
            }
        }
        if (document.getElementById("cbEducationStatus").checked == true && selectedEducationStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Education = '" + selectedEducationStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Education = '" + selectedEducationStatus + "'";
            }
        }
        if (document.getElementById("cbEmployment_Job_TrainingStatus").checked == true && selectedEmployment_Job_TrainingStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Employment_Job_Training = '" + selectedEmployment_Job_TrainingStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Employment_Job_Training = '" + selectedEmployment_Job_TrainingStatus + "'";
            }
        }
        if (document.getElementById("cbHealthStatus").checked == true && selectedHealthStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Health = '" + selectedHealthStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Health = '" + selectedHealthStatus + "'";
            }
        }
        if (document.getElementById("cbHomelessnessStatus").checked == true && selectedHomelessnessStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Homelessness = '" + selectedHomelessnessStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Homelessness = '" + selectedHomelessnessStatus + "'";
            }
        }
        if (document.getElementById("cbHousingStatus").checked == true && selectedHousingStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Housing = '" + selectedHousingStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Housing = '" + selectedHousingStatus + "'";
            }
        }
        if (document.getElementById("cbImmigrationStatus").checked == true && selectedImmigrationStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Immigration = '" + selectedImmigrationStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Immigration = '" + selectedImmigrationStatus + "'";
            }
        }
        if (document.getElementById("cbLegal_ServicesStatus").checked == true && selectedLegal_ServicesStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Legal_Services = '" + selectedLegal_ServicesStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Legal_Services = '" + selectedLegal_ServicesStatus + "'";
            }
        }
        if (document.getElementById("cbLesbian_Gay_Bisexual_and_or_TransgenderStatus").checked == true && selectedLesbian_Gay_Bisexual_and_or_TransgenderStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Lesbian_Gay_Bisexual_and_or_Transgender = '" + selectedLesbian_Gay_Bisexual_and_or_TransgenderStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " AND sso.Lesbian_Gay_Bisexual_and_or_Transgender = '" + selectedLesbian_Gay_Bisexual_and_or_TransgenderStatus + "'";
            }
        }
        if (document.getElementById("cbPersonal_Finance_Financial_EducationStatus").checked == true && selectedPersonal_Finance_Financial_EducationStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Personal_Finance_Financial_Education = '" + selectedPersonal_Finance_Financial_EducationStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Personal_Finance_Financial_Education = '" + selectedPersonal_Finance_Financial_EducationStatus + "'";
            }
        }
        if (document.getElementById("cbProfessional_AssociationStatus").checked == true && selectedProfessional_AssociationStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Professional_Association = '" + selectedProfessional_AssociationStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Professional_Association = '" + selectedProfessional_AssociationStatus + "'";
            }
        }
        if (document.getElementById("cbVeterans_Military_FamiliesStatus").checked == true && selectedVeterans_Military_FamiliesStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Veterans_Military_Families = '" + selectedVeterans_Military_FamiliesStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Veterans_Military_Families = '" + selectedVeterans_Military_FamiliesStatus + "'";
            }
        }
        if (document.getElementById("cbVictim_ServicesStatus").checked == true && selectedVictim_ServicesStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Victim_Services = '" + selectedVictim_ServicesStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Victim_Services = '" + selectedVictim_ServicesStatus + "'";
            }
        }
        if (document.getElementById("cbWomen_s_GroupsStatus").checked == true && selectedWomen_s_GroupsStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Women_s_Groups = '" + selectedWomen_s_GroupsStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Women_s_Groups = '" + selectedWomen_s_GroupsStatus + "'";
            }
        }
        if (document.getElementById("cbYouth_ServicesStatus").checked == true && selectedYouth_ServicesStatus != "") {

            if (whereSocialServiceOrganizationsClause == "") {
                whereSocialServiceOrganizationsClause = "sso.Youth_Services = '" + selectedYouth_ServicesStatus + "'";
            }
            else {
                whereSocialServiceOrganizationsClause += " OR sso.Youth_Services = '" + selectedYouth_ServicesStatus + "'";
            }
        }

    }

    if (whereClause != "" || whereEnergyClause != "" || wherePermitClause != "" || whereViolationClause != "" || whereEvictionsClause || whereEvictionsClause || whereSocialServiceOrganizationsClause) {
        $('#loading').show();
        DatabaseSearch(whereEnergyClause, wherePermitClause, whereViolationClause, whereEvictionsClause, whereSocialServiceOrganizationsClause, whereClause, lstTableAttributes);
    }
    else {
        swal("Please choose some searching criteria first");
    }

    //if (whereEnergyClause != "" || wherePermitClause != "" || whereViolationClause != "" || whereEvictionsClause) {
    //    $('#loading').show();
    //    EnergySearch(whereEnergyClause, wherePermitClause, whereViolationClause, whereEvictionsClause, whereClause);
    //}
    //else if (whereClause != "") {
    //    MapPlutoSearch(whereClause, "", "", "", "", null, null, null);
    //}
    //else {
    //    swal("Please choose some searching criteria first");
    //}
}

function DatabaseSearch(whereEnergyClause, wherePermitClause, whereViolationClause, whereEvictionsClause, whereSocialServiceOrganizationsClause, whereClause, lstTableAttributes) {
    var selectStatement = "", fromStatement = "dbo.Pluto p", whereStatement = "", selectStatementList = ["p.OBJECTID"], fromStatementList = [];
    if (whereEnergyClause != "") {
        if (whereStatement == "") {
            whereStatement += whereEnergyClause;
        }
        else {
            whereStatement += " AND " + whereEnergyClause;
        }
    }
    if (wherePermitClause != "") {
        if (whereStatement == "") {
            whereStatement += wherePermitClause;
        }
        else {
            whereStatement += " AND " + wherePermitClause;
        }
    } if (whereViolationClause != "") {
        if (whereStatement == "") {
            whereStatement += whereViolationClause;
        }
        else {
            whereStatement += " AND " + whereViolationClause;
        }
    } if (whereEvictionsClause != "") {
        if (whereStatement == "") {
            whereStatement += whereEvictionsClause;
        }
        else {
            whereStatement += " AND " + whereEvictionsClause;
        }
    } if (whereClause != "") {
        if (whereStatement == "") {
            whereStatement += whereClause;
        }
        else {
            whereStatement += " AND " + whereClause;
        }
    } if (whereSocialServiceOrganizationsClause != "") {
        if (whereStatement == "") {
            whereStatement += whereSocialServiceOrganizationsClause;
        }
        else {
            whereStatement += " AND (" + whereSocialServiceOrganizationsClause + ")";
        }
    }

    for (var i = 0; i < lstTableAttributes.length; i++) {
        switch (lstTableAttributes[i].dataset) {
            case "Pluto":
                selectStatementList.push("p." + lstTableAttributes[i].attribute);
                break;
            case "Energy":
                if (!fromStatementList.includes(lstTableAttributes[i].dataset)) {
                    fromStatement += " LEFT JOIN dbo.Energy en on p.BBL = en.bbl_10_digits";
                }
                selectStatementList.push("en." + lstTableAttributes[i].attribute);
                break;
            case "Permit":
                if (!fromStatementList.includes(lstTableAttributes[i].dataset)) {
                    fromStatement += " LEFT JOIN dbo.Permit pe on p.BBL = pe.bbl_10_digits";
                }
                selectStatementList.push("pe." + lstTableAttributes[i].attribute);
                break;
            case "Violation":
                if (!fromStatementList.includes(lstTableAttributes[i].dataset)) {
                    fromStatement += " LEFT JOIN dbo.Violation v on p.BBL = v.bbl_10_digits";
                }
                selectStatementList.push("v." + lstTableAttributes[i].attribute);
                break;
            case "Evictions":
                if (!fromStatementList.includes(lstTableAttributes[i].dataset)) {
                    fromStatement += " LEFT JOIN dbo.Evictions ev on p.Address = ev.EVICTION_ADDRESS";
                }
                selectStatementList.push("ev." + lstTableAttributes[i].attribute);
                break;
            case "Districts":
                if (!fromStatementList.includes(lstTableAttributes[i].dataset)) {
                    fromStatement += " LEFT JOIN dbo.Districts d on p.CD = d.DISTRICTCODE";
                }
                selectStatementList.push("d." + lstTableAttributes[i].attribute);
                break;
            case "SocialServiceOrganizations":
                if (!fromStatementList.includes(lstTableAttributes[i].dataset)) {
                    fromStatement += " LEFT JOIN dbo.SocialServiceOrganizations sso on p.Address = sso.Address1";
                }
                selectStatementList.push("sso." + lstTableAttributes[i].attribute);
                break;
        }
        fromStatementList.push(lstTableAttributes[i].dataset);
    }
    selectStatement = selectStatementList.join(", ");
    sqlQuery = "SELECT " + selectStatement + " FROM " + fromStatement + " WHERE " + whereStatement;
    $.ajax({
        url: RootUrl + 'Home/SearchDatabase',
        type: "POST",
        data: {
            "sqlQuery": sqlQuery
        }
    }).done(function (data) {
        CreateDatabaseTable(data);
    }).fail(function (f) {
        $('#loading').hide();
        swal("Failed to search the query");
    });
}

function CreateDatabaseTable(data) {
    map.graphics.clear();
    selectionLayer.clear();
    if ($('.slider-bottom-arrow').hasClass("showBottomPanel")) {
        $('.slider-bottom-arrow').click();
    }
    if (data.length > 0) {
        var objectIDs = "";
        var htmlQueryRecords = '<div class="table-responsive"><table id=\"tblQueryRecords\" class="tablesorter"><thead><tr class=\"clickableRow\">';
        for (var i = 0; i < lstTableAttributes.length; i++) {
            htmlQueryRecords += "<th>" + lstTableAttributes[i].name + "</th>"
        }
        htmlQueryRecords += "</tr></thead><tbody>";
        //Loop through each feature returned
        for (var i = 0; i < data.length; i++) {
            if (objectIDs == "") {
                objectIDs += data[i].OBJECTID;
            }
            else {
                objectIDs += "," + data[i].OBJECTID;
            }
            htmlQueryRecords += "<tr class=\"clickableRow\" OnClick=\"ShowInfoForSelectedRecord('" + data[i].OBJECTID + "');\">";

            for (var j = 0; j < lstTableAttributes.length; j++) {
                switch (lstTableAttributes[j].attribute) {
                    case "job_start_date":
                        value = data[i]["job_start_date_string_format"];
                        break;
                    case "issue_date":
                        value = data[i]["issue_date_string_format"];
                        break;
                    case "executed_date":
                        value = data[i]["executed_date_string_format"];
                        break;
                    default:
                        value = data[i][lstTableAttributes[j].attribute];
                        break;
                }
                htmlQueryRecords += "<td>" + value + "</td>";
            }
            htmlQueryRecords += "</tr>";
        }
        if (data.length < 2000) {
            MapPlutoGeometrySearch(objectIDs);
        }
        else {
            $('#loading').hide();
            swal("Your query has more than 2000 records and they won't be selected on the map");
        }
        htmlQueryRecords += '</tr></tbody></table></div>';
        $('#divSelectItemsTable').text('');
        $('#divSelectItemsTable').append(htmlQueryRecords);
        $("#tblQueryRecords").tablesorter({ widgets: ['zebra'] });
        $('#divSelectItemsMessage').hide();
        $('#divSelectItemsMoreInfo').show();
        $('#divSelectItemsCount').show();
        $('#btnOpenSaveReport').show();
        $('#btnOpenAlerts').show();
        if (data.length == 1) {
            $('#divSelectItemsCount').text('There is ' + data.length + ' record returned');
        }
        else {
            $('#divSelectItemsCount').text('There are ' + data.length + ' records returned');
        }
        highlightTableRow('tblQueryRecords');
    }
    else {
        $('#divSelectItemsTable').text('');
        $('#divSelectItemsMoreInfo').hide();
        $('#divSelectItemsMessage').show();
        $('#divSelectItemsCount').hide();
        $('#btnOpenSaveReport').hide();
        $('#btnOpenAlerts').hide();
        $('#loading').hide();
    }
}

function MapPlutoGeometrySearch(objectIDs) {
    queryTask = new esri.tasks.QueryTask(MapPlutoUrl);

    //initialize query
    query = new esri.tasks.Query();
    query.returnGeometry = true;

    query.where = "OBJECTID IN (" + objectIDs + ")";

    //execute query
    queryTask.execute(query, function executeMapPlutoSearch(featureSet) {
        var features = [];
        var resultFeatures = featureSet.features;
        //Loop through each feature returned
        for (var i = 0, il = resultFeatures.length; i < il; i++) {
            var graphic = resultFeatures[i];
            var myGraphic = new esri.Graphic({
                geometry: graphic.geometry
            });
            myGraphic.symbol = symbolFill;
            selectionLayer.add(myGraphic);
            features.push(graphic);
            var spatialRef = new esri.SpatialReference({ wkid: 102718 });
            var zoomExtent = new esri.geometry.Extent();
            zoomExtent.spatialReference = spatialRef;
            zoomExtent.xmin = esri.graphicsExtent(features).xmin - 1000;
            zoomExtent.ymin = esri.graphicsExtent(features).ymin - 1000;
            zoomExtent.xmax = esri.graphicsExtent(features).xmax + 1000;
            zoomExtent.ymax = esri.graphicsExtent(features).ymax + 1000;
            map.setExtent(zoomExtent);
            $('#loading').hide();
        }
    }, function (error) {
        $('#loading').hide();
        console.log(error);
    });
}

function SearchGeometry(whereClause) {
    var queryTask = new esri.tasks.QueryTask(DistrictsUrl);
    var query = new esri.tasks.Query();
    query.where = whereClause;
    query.returnGeometry = true;
    queryTask.execute(query, function (featureSet) {
        //Performance enhancer - assign featureSet array to a single variable.
        var resultFeatures = featureSet.features;
        if (resultFeatures.length > 0) {
            //Loop through each feature returned
            for (var i = 0, il = resultFeatures.length; i < il; i++) {
                //Feature is a graphic
                var graphic = resultFeatures[i];
                graphic.setSymbol(symbolDistrictFill);
                districtLayer.add(graphic);
            }
        }
    }, function (error) {
        $('#loading').hide();
        console.log(error);
    });
}

function btnSaveMyReportDatabase_Click() {
    if (document.getElementById("txtReportName").value == "") {
        swal("Please enter name");
    }
    else {
        $.ajax({
            url: RootUrl + 'Home/SaveDatabaseReport',
            type: "POST",
            data: {
                "ReportName": document.getElementById("txtReportName").value,
                "sqlQuery": sqlQuery,
                "lstTableAttributes": lstTableAttributes
            }
        }).done(function (data) {
            swal(data.msg);
        }).fail(function () {
            swal("Failed to save the report");
        });
    }
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

function btnOpenAlerts_Click() {
    swal({
        text: "For Alerts including Property Permits, Property Violations or Housing Evictions criteria, please ensure the selected end date is set in the future or empty to receieve alerts in perpetuity",
        title: "Before You Save Your Alert",
        buttons: {
            ResetSearch: "Reset Search",
            ConfirmAlert: "Confirm Alert",
        },
    })
        .then((value) => {
            switch (value) {
                case "ResetSearch":
                    break;
                case "ConfirmAlert":
                    $("#myModalCreateAlert").modal();
            }
        });
}

function btnSaveMyAlert_Click() {
    var alertName = document.getElementById("txtAlertName").value;
    var ddlAlertFrequency = document.getElementById("ddlAlertFrequency");
    var selectedAlertFrequency = ddlAlertFrequency.options[ddlAlertFrequency.selectedIndex].value;
    if (alertName == "" || ddlAlertFrequency.selectedIndex == 0) {
        swal("Please enter name and frequency");
    }
    else {
        $('#loading').show();
        $.ajax({
            url: RootUrl + 'Home/CreateAlert',
            type: "POST",
            data: {
                "AlertName": alertName,
                "AlertFrequency": selectedAlertFrequency,
                "AlertQuery": sqlQuery,
                "IsPlutoSearch": IsPlutoSearch,
                "IsEnergySearch": IsEnergySearch,
                "IsPermitSearch": IsPermitSearch,
                "IsViolationSearch": IsViolationSearch,
                "IsEvictionSearch": IsEvictionSearch
            }
        }).done(function (data) {
            $('#loading').hide();
            swal(data.msg);
        }).fail(function () {
            $('#loading').hide();
            swal("Failed to create the alert");
        });
    }
}

function btnOpenMyReports_Click() {
    openModalWindow(RootUrl + "MyReports/Preview", 800, 820, "My Reports", true, "iframeMyReports");
}

function btnOpenMyAlerts_Click() {
    $("#myModalShowAlert").modal();
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
    $("#tabs").tabs();
    $.ajax({
        url: RootUrl + 'Home/GetMyAlerts',
        type: "POST",
        success: function (data) {
            CreateHtmlMyAlerts(data);
        },
        error: function (error) {
            console.log("An error occurred from GetMyAlerts()." + error);
        }
    });
    var myTimeout = setTimeout(function () {
        $('input.select2-input').attr('autocomplete', "off");
    }, 1000);

});

function rbSearchByBBL_Click() {
    $('#divLookaLikeBbl').show();
    $('#divLookaLikeAddress').hide();
}
function rbSearchByAddress_Click() {
    $('#divLookaLikeBbl').hide();
    $('#divLookaLikeAddress').show();
};
function txtLookaLikeBbl_Change(evt) {
    if (evt.value != null) {
        lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'Address', attribute: "Address", dataset: "Pluto" }];
        lstTableAttributes.push({ name: 'Assessed Total Value', attribute: "AssessTot", dataset: "Pluto" });
        lstTableAttributes.push({ name: 'Year Built', attribute: "YearBuilt", dataset: "Pluto" });
        lstTableAttributes.push({ name: 'Number of Floors', attribute: "NumFloors", dataset: "Pluto" });
        lstTableAttributes.push({ name: 'Total Building Floor Area', attribute: "BldgArea", dataset: "Pluto" });

        $('#loading').show();
        $.ajax({
            url: RootUrl + 'Home/SearchLookaLikeByBBL',
            type: "POST",
            data: {
                "bbl": evt.value
            },
            success: function (data) {
                sqlQuery = data.sqlQuery;
                CreateDatabaseTable(data.data);
                $('#loading').hide();
            },
            error: function (error) {
                console.log("An error occurred from SearchLookaLiteByBBL()." + error);
                $('#loading').hide();
            }
        });
    }
}
function txtLookaLikeAddress_Change(evt) {
    if (evt.value != null) {
        lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'Address', attribute: "Address", dataset: "Pluto" }];
        lstTableAttributes.push({ name: 'Assessed Total Value', attribute: "AssessTot", dataset: "Pluto" });
        lstTableAttributes.push({ name: 'Year Built', attribute: "YearBuilt", dataset: "Pluto" });
        lstTableAttributes.push({ name: 'Total Building Floor Area', attribute: "BldgArea", dataset: "Pluto" });
        lstTableAttributes.push({ name: 'Number of Floors', attribute: "NumFloors", dataset: "Pluto" });

        $('#loading').show();
        $.ajax({
            url: RootUrl + 'Home/SearchLookaLikeByAddresses',
            type: "POST",
            data: {
                "adr": evt.value
            },
            success: function (data) {
                sqlQuery = data.sqlQuery;
                CreateDatabaseTable(data.data);
                $('#loading').hide();
            },
            error: function (error) {
                console.log("An error occurred from SearchLookaLiteByAddresses()." + error);
                $('#loading').hide();
            }
        });
    }
}

function ShowInfoForSelectedAlert(AlertID, rowID) {
    document.getElementById(rowID).style.fontWeight = "normal";
    $.ajax({
        url: RootUrl + 'Home/ShowInfoForSelectedAlert',
        type: "POST",
        data: {
            "AlertID": AlertID
        }
    }).done(function (myDataObject) {
        var data = myDataObject.result;
        sqlQuery = myDataObject.sqlQuery;
        if (myDataObject.unreadAlerts == 0) {
            document.getElementById("myAlertBadge").textContent = "";
        }
        else {
            document.getElementById("myAlertBadge").textContent = myDataObject.unreadAlerts;
        }
        if ($('.slider-bottom-arrow').hasClass("showBottomPanel")) {
            $('.slider-bottom-arrow').click();
        }
        if (data.length > 0) {
            lstTableAttributes = [];
            var htmlAlertData = '<div class="table-responsive"><table id=\"tblAlertData\" class="tablesorter"><thead><tr class=\"clickableRow\">';
            for (var i = 0; i < lstAllTableAttributes.length; i++) {
                if (data[0][lstAllTableAttributes[i].attribute] != null) {
                    htmlAlertData += "<th>" + lstAllTableAttributes[i].name + "</th>";
                    lstTableAttributes.push(lstAllTableAttributes[i].attribute);
                }
            }
            htmlAlertData += "</tr></thead><tbody>";
            //Loop through each feature returned
            for (var i = 0; i < data.length; i++) {
                htmlAlertData += "<tr class=\"clickableRow\" OnClick=\"ShowInfoForSelectedRecord('" + data[i].OBJECTID + "');\">";
                for (var j = 0; j < lstTableAttributes.length; j++) {
                    htmlAlertData += "<td>" + data[i][lstTableAttributes[j]] + "</td>";
                }
                htmlAlertData += "</tr>";
            }
            htmlAlertData += '</tr></tbody></table></div>';
            $('#divSelectItemsTable').text('');
            $('#divSelectItemsTable').append(htmlAlertData);
            $("#tblAlertData").tablesorter({ widgets: ['zebra'] });
            $('#divSelectItemsMessage').hide();
            $('#divSelectItemsMoreInfo').show();
            $('#divSelectItemsCount').show();
            $('#btnOpenSaveReport').show();
            $('#btnOpenAlerts').hide();
            if (data.length == 1) {
                $('#divSelectItemsCount').text('There is ' + data.length + ' record returned');
            }
            else {
                $('#divSelectItemsCount').text('There are ' + data.length + ' records returned');
            }
            highlightTableRow('tblAlertData');
        }
        else {
            $('#divSelectItemsTable').text('');
            $('#divSelectItemsMessage').show();
            $('#divSelectItemsMoreInfo').hide();
            $('#divSelectItemsCount').hide();
            $('#btnOpenSaveReport').hide();
            $('#btnOpenAlerts').hide();
        }
    }).fail(function () {
        swal("Failed to show the info for the selected alert");
    });
}

function CreateHtmlMyAlerts(data) {
    var unreadAlerts = 0;
    if (data.length > 0) {
        var htmlAlertRecords = '<div class="table-responsive"><table id=\"tblAlertRecords\" class="table table-bordered"><thead><tr class=\"clickableRow\">';
        htmlAlertRecords += "<th>Name</th>";
        htmlAlertRecords += "<th>Date</th>";
        htmlAlertRecords += "<th></th>";
        htmlAlertRecords += "</tr></thead><tbody>";
        //Loop through each feature returned
        for (var i = 0; i < data.length; i++) {
            var rowID = "changeFontWeight" + i;
            if (data[i].IsUnread) {
                unreadAlerts++;
                htmlAlertRecords += "<tr id='" + rowID + "' style='font-weight: bold' class=\"clickableRow\" OnClick=\"ShowInfoForSelectedAlert('" + data[i].ID + "','" + rowID + "');\">";
            }
            else {
                htmlAlertRecords += "<tr id='" + rowID + "' class=\"clickableRow\" OnClick=\"ShowInfoForSelectedAlert('" + data[i].ID + "','" + rowID + "');\">";
            }
            htmlAlertRecords += "<td>" + data[i].AlertName + "</td>";
            htmlAlertRecords += "<td>" + data[i].DateCreatedString + "</td>";
            htmlAlertRecords += "<td style='text-align: center;'><button type=\"button\" style=\"padding: 0; border: none;\" onclick=\"btnDeleteMyAlert_Click(" + data[i].ID + ")\"><img src=\"" + RootUrl + "Images/x-delete.png\" width=\"25\" height=\"20\" /></button></td>";
            htmlAlertRecords += "</tr>";
        }
        htmlAlertRecords += '</tr></tbody></table></div>';
        $('#divAlertItemsTable').text('');
        $('#divAlertItemsTable').append(htmlAlertRecords);
        //$("#tblAlertRecords").tablesorter({ widgets: ['zebra'] });
        $('#divAlertItemsMessage').hide();
        highlightTableRow('tblAlertRecords');
    }
    else {
        $('#divAlertItemsTable').text('');
        $('#divAlertItemsMessage').show();
    }
    if (unreadAlerts == 0) {
        document.getElementById("myAlertBadge").textContent = "";
    }
    else {
        document.getElementById("myAlertBadge").textContent = unreadAlerts;
    }
}

function btnDeleteMyAlert_Click(AlertID) {
    swal("Are you sure you want to delete this alert?", {
        buttons: {
            Yes: true,
            No: "No",
        },
    }).then(function (value) {
        switch (value) {
            case "Yes":
                $.ajax({
                    url: RootUrl + 'Home/DeleteAlert',
                    data: {
                        AlertID: AlertID
                    },
                    type: "POST",
                    success: function (data) {
                        CreateHtmlMyAlerts(data);
                    },
                    error: function (error) {
                        console.log("An error occurred from DeleteAlert()." + error);
                    }
                });
                break;
            case "No":
                break;
        }
    });
}

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
