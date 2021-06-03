var heatmapColorRamp = ["rgb(16, 255, 16, 1)", "rgb(28, 242, 16, 1)", "rgb(41, 229, 16, 1)", "rgb(53, 217, 16, 1)", "rgb(66, 204, 16, 1)"
    , "rgb(78, 192, 16, 1)", "rgb(91, 179, 16, 1)", "rgb(104, 166, 16, 1)", "rgb(116, 154, 16, 1)", "rgb(129, 141, 16, 1)"
    , "rgb(141, 129, 16, 1)", "rgb(154, 116, 16, 1)", "rgb(166, 104, 16, 1)", "rgb(179, 91, 16, 1)", "rgb(192, 78, 16, 1)"
    , "rgb(204, 66, 16, 1)", "rgb(217, 53, 16, 1)", "rgb(229, 41, 16, 1)", "rgb(242, 28, 16, 1)", "rgb(255, 16, 16, 1)"];
var whereClauseHeatmapPropertySales = "";
var whereClauseHeatmapViolations = "";
var whereClauseHeatmapPermits = "";
Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}
function formatDate(originalDate) {
    var formatedYear = originalDate.getFullYear();
    var formatedMonth = originalDate.getMonth() + 1;
    formatedMonth = formatedMonth < 10 ? "0" + formatedMonth : formatedMonth;
    var formatedDay = originalDate.getDate();
    formatedDay = formatedDay < 10 ? "0" + formatedDay : formatedDay;
    var formatedDate = formatedYear + "-" + formatedMonth + "-" + formatedDay;
    return formatedDate;
}
function formatDateViolations(originalDate) {
    var formatedYear = originalDate.getFullYear();
    var formatedMonth = originalDate.getMonth() + 1;
    formatedMonth = formatedMonth < 10 ? "0" + formatedMonth : formatedMonth;
    var formatedDay = originalDate.getDate();
    formatedDay = formatedDay < 10 ? "0" + formatedDay : formatedDay;
    var formatedDate = formatedYear + "" + formatedMonth + "" + formatedDay;
    return formatedDate;
}
function btnSearchHeatMapPropertySales() {
    heatmapLayer.clear();
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
    zipCodeFeatures.setVisibility(false);
    myLabelLayer.setVisibility(false);
    hideResultsPanel();
    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();

    sqlQueryTA = "";
    var StoredProcedure = 1;
    if (document.getElementById('rbHeatMapByNumberOfPropertySales').checked) {
        StoredProcedure = 1;
    }
    else {
        StoredProcedure = 2;
    }
    sqlQueryTA = "select p.OBJECTID, p.BBL, CD, DISTRICT, p.Borough, p.Address, ZipCode, sale_date, sale_price from dbo.Pluto p inner join PropertySales ps on p.BBL = ps.bbl inner join Districts d on p.CD = d.DISTRICTCODE";
    var DateHmPsBasePeriod = document.getElementById("txtHmPsBasePeriod").value;
    var DateHmPsAnalysisPeriod = document.getElementById("txtHmPsAnalysisPeriod").value;
    var DiffDaysHmPsBasePeriod = document.getElementById("numHmPsAnalysisPeriod").value;
    var DiffDaysHmPsAnalysisPeriod = document.getElementById("numHmPsAnalysisPeriod").value;
    var BoroughsTA = "";
    var DistrictsTA = "";
    if (document.getElementById("cbBoroughTA").checked == true) {
        BoroughsTA = $(txtBoroughsTA).val();
    }
    if (document.getElementById("cbDistrictTA").checked == true) {
        DistrictsTA = $(txtDistrictsTA).val();
    }
    var ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
    var ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
    if (DateHmPsBasePeriod == "" || DateHmPsAnalysisPeriod == "" || DiffDaysHmPsBasePeriod == "" || DiffDaysHmPsAnalysisPeriod == "") {
        swal('Base and Analysis periods and +/- days are required fields');
    }
    else {
        var BasePeriod = new Date(DateHmPsBasePeriod);
        var BasePeriodFrom = BasePeriod.addDays(DiffDaysHmPsBasePeriod * (-1));
        var BasePeriodTo = BasePeriod.addDays(DiffDaysHmPsBasePeriod * (1));
        var AnalysisPeriod = new Date(DateHmPsAnalysisPeriod);
        var AnalysisPeriodFrom = AnalysisPeriod.addDays(DiffDaysHmPsAnalysisPeriod * (-1));
        var AnalysisPeriodTo = AnalysisPeriod.addDays(DiffDaysHmPsAnalysisPeriod * (1));
        var formatedBasePeriodFrom = formatDate(BasePeriodFrom);
        var formatedBasePeriodTo = formatDate(BasePeriodTo);
        var formatedAnalysisPeriodFrom = formatDate(AnalysisPeriodFrom);
        var formatedAnalysisPeriodTo = formatDate(AnalysisPeriodTo);
        whereClauseHeatmapPropertySales = " Where ((ps.sale_date >= '" + formatedBasePeriodFrom + "' AND ps.sale_date <= '" + formatedBasePeriodTo + "') OR (ps.sale_date >= '" + formatedAnalysisPeriodFrom + "' AND ps.sale_date <= '" + formatedAnalysisPeriodTo + "'))";

        if (document.getElementById('rbHeatMapByAveragePropertySalesPrice').checked) {
            whereClauseHeatmapPropertySales += " AND sale_price is not null AND sale_price <> '0' ";
        }
        lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'District', attribute: "DISTRICT", dataset: "Districts" }
            , { name: 'Address', attribute: "Address", dataset: "Pluto" }, { name: 'Zip Code', attribute: "ZipCode", dataset: "Pluto" }
            , { name: 'Sale Date', attribute: "sale_date", dataset: "PropertySales" }, { name: 'Sale Price', attribute: "sale_price", dataset: "PropertySales" }];
        if (document.getElementById("cbZipCodeRangeTA").checked == true) {
            ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
            ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
            if (ZipCodeRangeTAFrom != "" || ZipCodeRangeTATo != "") {
                if (whereClauseHeatmapPropertySales != "") {
                    whereClauseHeatmapPropertySales += " AND ";
                }
                if (ZipCodeRangeTAFrom != "" && ZipCodeRangeTATo != "") {
                    whereClauseHeatmapPropertySales += "p.ZipCode >= " + ZipCodeRangeTAFrom + " AND p.ZipCode <= " + ZipCodeRangeTATo;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE >= " + ZipCodeRangeTAFrom + " AND ZIPCODE <= " + ZipCodeRangeTATo);
                }
                else if (ZipCodeRangeTAFrom != "") {
                    whereClauseHeatmapPropertySales += "p.ZipCode >= " + ZipCodeRangeTAFrom;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE >= " + ZipCodeRangeTAFrom);
                }
                else if (ZipCodeRangeTATo != "") {
                    whereClauseHeatmapPropertySales += "p.ZipCode <= " + ZipCodeRangeTATo;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE <= " + ZipCodeRangeTATo);
                }
                zipCodeFeatures.setVisibility(true);
                myLabelLayer.setVisibility(true);
            }
        }
        else {
            ZipCodeRangeTAFrom = "";
            ZipCodeRangeTATo = "";
        }
        $.ajax({
            url: RootUrl + 'Home/SearchDatabaseHeatMap',
            type: "POST",
            data: {
                "StoredProcedure": StoredProcedure,
                "DateHmPsBasePeriod": DateHmPsBasePeriod,
                "DateHmPsAnalysisPeriod": DateHmPsAnalysisPeriod,
                "DiffDaysHmPsBasePeriod": DiffDaysHmPsBasePeriod,
                "DiffDaysHmPsAnalysisPeriod": DiffDaysHmPsAnalysisPeriod,
                "BoroughsTA": BoroughsTA,
                "DistrictsTA": DistrictsTA,
                "ZipCodeRangeTAFrom": ZipCodeRangeTAFrom,
                "ZipCodeRangeTATo": ZipCodeRangeTATo,
            }
        }).done(function (data) {
            if (data.length > 0) {
                $('#infoColorRamp').show();
                for (var i = 0; i < data.length; i++) {
                    if (data[i].diff_percentage > 100) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[0], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 100 && data[i].diff_percentage > 90) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[1], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 90 && data[i].diff_percentage > 80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[2], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 80 && data[i].diff_percentage > 70) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[3], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 70 && data[i].diff_percentage > 60) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[4], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 60 && data[i].diff_percentage > 50) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[5], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 50 && data[i].diff_percentage > 40) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[6], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 40 && data[i].diff_percentage > 30) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[7], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 30 && data[i].diff_percentage > 20) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[8], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 20 && data[i].diff_percentage > 10) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[9], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 10 && data[i].diff_percentage > 0) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[10], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 0 && data[i].diff_percentage > -10) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[11], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -10 && data[i].diff_percentage > -20) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[12], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -20 && data[i].diff_percentage > -30) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[13], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -30 && data[i].diff_percentage > -40) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[14], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -40 && data[i].diff_percentage > -50) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[15], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -50 && data[i].diff_percentage > -60) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[16], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -60 && data[i].diff_percentage > -70) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[17], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -70 && data[i].diff_percentage > -80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[18], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[19], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                }
                dojo.connect(heatmapLayer, "onMouseMove", function (evt) {
                    var g = evt.graphic;
                    var basePeriodValue = g.attributes.basePeriodValue == null ? "" : g.attributes.basePeriodValue.toLocaleString('en');
                    var analysisPeriodValue = g.attributes.analysisPeriodValue == null ? "" : g.attributes.analysisPeriodValue.toLocaleString('en');
                    var diff_value = g.attributes.diff_value == null ? "" : g.attributes.diff_value.toLocaleString('en');
                    var percentage = g.attributes.percentage == null ? "" : g.attributes.percentage.toLocaleString('en') + "%";
                    var html = "<b>Base Period Value: </b>" + basePeriodValue + "<br/>";
                    html += "<b>Analysis Period Value: </b>" + analysisPeriodValue + "<br/>";
                    html += "<b>Difference: </b>" + diff_value + "<br/>";
                    html += "<b>Difference Percentage: </b>" + percentage + "<br/>";
                    map.infoWindow.setContent(html);
                    map.infoWindow.setTitle(g.attributes.district);
                    map.infoWindow.show(evt.screenPoint, map.getInfoWindowAnchor(evt.screenPoint));
                });
                dojo.connect(heatmapLayer, "onMouseOut", function () { map.infoWindow.hide(); });
                dojo.connect(heatmapLayer, "onClick", function (evt) {
                    var g = evt.graphic;
                    sqlQuery = sqlQueryTA + whereClauseHeatmapPropertySales + " AND CD = " + g.attributes.districtcode + " order by sale_date";
                    $.ajax({
                        url: RootUrl + 'Home/SearchDatabase',
                        type: "POST",
                        data: {
                            "sqlQuery": sqlQuery
                        }
                    }).done(function (data) {
                        showAlerts = false;
                        CreateDatabaseTable(data, false, true, data.length);
                    }).fail(function (f) {
                        $('#loading').hide();
                        swal("Failed to search the query");
                    });
                });
            }
            else {
                swal("Records not found");
                $('#infoColorRamp').hide();
            }
        }).fail(function (f) {
            swal("Failed to search the query");
        });
    }
}

function MapPlutoHeatmapSearch(CD, heatmapColor, percentage, district, basePeriodValue, analysisPeriodValue, diff_value) {
    if (CD !== undefined) {
        queryTask = new esri.tasks.QueryTask(DistrictsUrl);
        //initialize query
        query = new esri.tasks.Query();
        query.returnGeometry = true;

        query.where = "DISTRICTCODE IN (" + CD + ")";

        //execute query
        queryTask.execute(query, function executeMapPlutoSearch(featureSet) {
            var resultFeatures = featureSet.features;
            //Loop through each feature returned
            for (var i = 0, il = resultFeatures.length; i < il; i++) {
                var graphic = resultFeatures[i];
                var graphicAtt = {
                    percentage: percentage,
                    district: district,
                    districtcode: CD,
                    basePeriodValue: basePeriodValue,
                    analysisPeriodValue: analysisPeriodValue,
                    diff_value: diff_value
                };

                var myColor = esri.Color.fromString(heatmapColor);
                var heatmapSymbolFill = new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color(myColor));
                var myGraphic = new esri.Graphic(graphic.geometry, heatmapSymbolFill, graphicAtt);
                heatmapLayer.add(myGraphic);
            }
        }, function (error) {
            console.log(error);
        });
    }
}

function btnResetHeatMap() {
    $('#infoColorRamp').hide();
    heatmapLayer.clear();
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
    zipCodeFeatures.setVisibility(false);
    myLabelLayer.setVisibility(false);

    document.getElementById("cbBoroughTA").checked = false;
    document.getElementById("cbDistrictTA").checked = false;
    document.getElementById("cbZipCodeRangeTA").checked = false;
    $("#txtBoroughsTA").select2("val", "");
    $("#txtDistrictsTA").select2("val", "");
    document.getElementById("txtZipCodeRangeTAFrom").value = "";
    document.getElementById("txtZipCodeRangeTATo").value = "";

    document.getElementById("rbHeatMapByNumberOfPropertySales").checked = true;
    document.getElementById("rbHeatMapByAveragePropertySalesPrice").checked = false;
    document.getElementById("txtHmPsBasePeriod").value = "";
    document.getElementById("txtHmPsAnalysisPeriod").value = "";
    document.getElementById("numHmPsBasePeriod").value = "15";
    document.getElementById("numHmPsAnalysisPeriod").value = "15";

    $("#txtEcbViolationTypes").select2("val", "");
    document.getElementById("txtHmEcbViolationsBasePeriod").value = "";
    document.getElementById("txtHmEcbViolationsAnalysisPeriod").value = "";
    document.getElementById("numHmEcbViolationsBasePeriod").value = "15";
    document.getElementById("numHmEcbViolationsAnalysisPeriod").value = "15";

    $("#txtPermitJobTypes").select2("val", "");
    $("#txtPermitWorkTypes").select2("val", "");
    document.getElementById("txtHmPermitsBasePeriod").value = "";
    document.getElementById("txtHmPermitsAnalysisPeriod").value = "";
    document.getElementById("numHmPermitsBasePeriod").value = "15";
    document.getElementById("numHmPermitsAnalysisPeriod").value = "15";

    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();

    hideResultsPanel();
}

function btnSearchHeatMapViolations(StoredProcedure) {
    heatmapLayer.clear();
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
    zipCodeFeatures.setVisibility(false);
    myLabelLayer.setVisibility(false);
    hideResultsPanel();
    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();

    sqlQueryTA = "";
    var databaseTable = "Violation";
    var selectedViolationType = "";
    var DateHmViolationBasePeriod = "";
    var DateHmViolationAnalysisPeriod = "";
    var DiffDaysHmViolationBasePeriod = "";
    var DiffDaysHmViolationAnalysisPeriod = "";
    if (StoredProcedure == 1) {
        databaseTable = "EcbDobViolations";
        selectedViolationType = $(txtEcbViolationTypes).val().replace(";", ",");
        DateHmViolationBasePeriod = document.getElementById("txtHmEcbViolationsBasePeriod").value;
        DateHmViolationAnalysisPeriod = document.getElementById("txtHmEcbViolationsAnalysisPeriod").value;
        DiffDaysHmViolationBasePeriod = document.getElementById("numHmEcbViolationsAnalysisPeriod").value;
        DiffDaysHmViolationAnalysisPeriod = document.getElementById("numHmEcbViolationsAnalysisPeriod").value;
    }
    else {
        databaseTable = "Violation";
        selectedViolationType = $(txtDobViolationTypes).val();
        DateHmViolationBasePeriod = document.getElementById("txtHmDobViolationsBasePeriod").value;
        DateHmViolationAnalysisPeriod = document.getElementById("txtHmDobViolationsAnalysisPeriod").value;
        DiffDaysHmViolationBasePeriod = document.getElementById("numHmDobViolationsAnalysisPeriod").value;
        DiffDaysHmViolationAnalysisPeriod = document.getElementById("numHmDobViolationsAnalysisPeriod").value;
    }
    sqlQueryTA = "select p.OBJECTID, p.BBL, CD, DISTRICT, p.Borough, p.Address, ZipCode, issue_date, violation_type from dbo.Pluto p inner join " + databaseTable + " v on p.BBL = v.bbl_10_digits inner join Districts d on p.CD = d.DISTRICTCODE";
    var BoroughsTA = "";
    var DistrictsTA = "";
    if (document.getElementById("cbBoroughTA").checked == true) {
        BoroughsTA = $(txtBoroughsTA).val();
    }
    if (document.getElementById("cbDistrictTA").checked == true) {
        DistrictsTA = $(txtDistrictsTA).val();
    }
    var ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
    var ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
    if (selectedViolationType == "" || DateHmViolationBasePeriod == "" || DateHmViolationAnalysisPeriod == "" || DiffDaysHmViolationBasePeriod == "" || DiffDaysHmViolationAnalysisPeriod == "") {
        swal('Violation Type, Base and Analysis periods and +/- days are required fields');
    }
    else {
        $('#loading').show();
        var BasePeriod = new Date(DateHmViolationBasePeriod);
        var BasePeriodFrom = BasePeriod.addDays(DiffDaysHmViolationBasePeriod * (-1));
        var BasePeriodTo = BasePeriod.addDays(DiffDaysHmViolationBasePeriod * (1));
        var AnalysisPeriod = new Date(DateHmViolationAnalysisPeriod);
        var AnalysisPeriodFrom = AnalysisPeriod.addDays(DiffDaysHmViolationAnalysisPeriod * (-1));
        var AnalysisPeriodTo = AnalysisPeriod.addDays(DiffDaysHmViolationAnalysisPeriod * (1));
        var formatedBasePeriodFrom = formatDateViolations(BasePeriodFrom);
        var formatedBasePeriodTo = formatDateViolations(BasePeriodTo);
        var formatedAnalysisPeriodFrom = formatDateViolations(AnalysisPeriodFrom);
        var formatedAnalysisPeriodTo = formatDateViolations(AnalysisPeriodTo);
        whereClauseHeatmapViolations = " Where (v.violation_type IN ('" + selectedViolationType.split(",").join("','") + "')) AND ((v.issue_date >= '" + formatedBasePeriodFrom + "' AND v.issue_date <= '" + formatedBasePeriodTo + "') OR (v.issue_date >= '" + formatedAnalysisPeriodFrom + "' AND v.issue_date <= '" + formatedAnalysisPeriodTo + "'))";

        lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'District', attribute: "DISTRICT", dataset: "Districts" }
            , { name: 'Address', attribute: "Address", dataset: "Pluto" }, { name: 'Zip Code', attribute: "ZipCode", dataset: "Pluto" }
            , { name: 'Date', attribute: "issue_date", dataset: databaseTable }, { name: 'Violation Type', attribute: "violation_type", dataset: databaseTable }];
        if (document.getElementById("cbZipCodeRangeTA").checked == true) {
            ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
            ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
            if (ZipCodeRangeTAFrom != "" || ZipCodeRangeTATo != "") {
                if (whereClauseHeatmapViolations != "") {
                    whereClauseHeatmapViolations += " AND ";
                }
                if (ZipCodeRangeTAFrom != "" && ZipCodeRangeTATo != "") {
                    whereClauseHeatmapViolations += "p.ZipCode >= " + ZipCodeRangeTAFrom + " AND p.ZipCode <= " + ZipCodeRangeTATo;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE >= " + ZipCodeRangeTAFrom + " AND ZIPCODE <= " + ZipCodeRangeTATo);
                }
                else if (ZipCodeRangeTAFrom != "") {
                    whereClauseHeatmapViolations += "p.ZipCode >= " + ZipCodeRangeTAFrom;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE >= " + ZipCodeRangeTAFrom);
                }
                else if (ZipCodeRangeTATo != "") {
                    whereClauseHeatmapViolations += "p.ZipCode <= " + ZipCodeRangeTATo;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE <= " + ZipCodeRangeTATo);
                }
                zipCodeFeatures.setVisibility(true);
                myLabelLayer.setVisibility(true);
            }
        }
        else {
            ZipCodeRangeTAFrom = "";
            ZipCodeRangeTATo = "";
        }
        $.ajax({
            url: RootUrl + 'Home/SearchDatabaseHeatMapViolations',
            type: "POST",
            data: {
                "ViolationType": selectedViolationType,
                "StoredProcedure": StoredProcedure,
                "FormatedBasePeriodFrom": formatedBasePeriodFrom,
                "FormatedBasePeriodTo": formatedBasePeriodTo,
                "FormatedAnalysisPeriodFrom": formatedAnalysisPeriodFrom,
                "FormatedAnalysisPeriodTo": formatedAnalysisPeriodTo,
                "BoroughsTA": BoroughsTA,
                "DistrictsTA": DistrictsTA,
                "ZipCodeRangeTAFrom": ZipCodeRangeTAFrom,
                "ZipCodeRangeTATo": ZipCodeRangeTATo,
            }
        }).done(function (data) {
            if (data.length > 0) {
                $('#infoColorRamp').show();
                for (var i = 0; i < data.length; i++) {
                    if (data[i].diff_percentage > 100) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[0], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 100 && data[i].diff_percentage > 90) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[1], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 90 && data[i].diff_percentage > 80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[2], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 80 && data[i].diff_percentage > 70) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[3], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 70 && data[i].diff_percentage > 60) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[4], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 60 && data[i].diff_percentage > 50) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[5], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 50 && data[i].diff_percentage > 40) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[6], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 40 && data[i].diff_percentage > 30) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[7], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 30 && data[i].diff_percentage > 20) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[8], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 20 && data[i].diff_percentage > 10) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[9], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 10 && data[i].diff_percentage > 0) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[10], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 0 && data[i].diff_percentage > -10) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[11], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -10 && data[i].diff_percentage > -20) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[12], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -20 && data[i].diff_percentage > -30) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[13], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -30 && data[i].diff_percentage > -40) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[14], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -40 && data[i].diff_percentage > -50) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[15], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -50 && data[i].diff_percentage > -60) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[16], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -60 && data[i].diff_percentage > -70) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[17], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -70 && data[i].diff_percentage > -80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[18], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[19], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                }
                dojo.connect(heatmapLayer, "onMouseMove", function (evt) {
                    var g = evt.graphic;
                    var basePeriodValue = g.attributes.basePeriodValue == null ? "" : g.attributes.basePeriodValue.toLocaleString('en');
                    var analysisPeriodValue = g.attributes.analysisPeriodValue == null ? "" : g.attributes.analysisPeriodValue.toLocaleString('en');
                    var diff_value = g.attributes.diff_value == null ? "" : g.attributes.diff_value.toLocaleString('en');
                    var percentage = g.attributes.percentage == null ? "" : g.attributes.percentage.toLocaleString('en') + "%";
                    var html = "<b>Base Period Value: </b>" + basePeriodValue + "<br/>";
                    html += "<b>Analysis Period Value: </b>" + analysisPeriodValue + "<br/>";
                    html += "<b>Difference: </b>" + diff_value + "<br/>";
                    html += "<b>Difference Percentage: </b>" + percentage + "<br/>";
                    map.infoWindow.setContent(html);
                    map.infoWindow.setTitle(g.attributes.district);
                    map.infoWindow.show(evt.screenPoint, map.getInfoWindowAnchor(evt.screenPoint));
                });
                dojo.connect(heatmapLayer, "onMouseOut", function () { map.infoWindow.hide(); });
                dojo.connect(heatmapLayer, "onClick", function (evt) {
                    $('#loading').show();
                    var g = evt.graphic;
                    sqlQuery = sqlQueryTA + whereClauseHeatmapViolations + " AND CD = " + g.attributes.districtcode + " order by issue_date";
                    $.ajax({
                        url: RootUrl + 'Home/SearchDatabase',
                        type: "POST",
                        data: {
                            "sqlQuery": sqlQuery
                        }
                    }).done(function (data) {
                        showAlerts = false;
                        CreateDatabaseTable(data, false, true, data.length);
                        $('#loading').hide();
                    }).fail(function (f) {
                        $('#loading').hide();
                        swal("Failed to search the query");
                    });
                });
            }
            else {
                swal("Records not found");
                $('#infoColorRamp').hide();
                $('#loading').hide();
            }
            $('#loading').hide();
        }).fail(function (f) {
            swal("Failed to search the query");
            $('#loading').hide();
        });
    }
}

function btnSearchHeatMapPermits() {
    heatmapLayer.clear();
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
    zipCodeFeatures.setVisibility(false);
    myLabelLayer.setVisibility(false);
    hideResultsPanel();
    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();

    sqlQueryTA = "";
    var selectedPermitJobType = $(txtPermitJobTypes).val();
    var selectedPermitWorkType = $(txtPermitWorkTypes).val();
    var DateHmPermitBasePeriod = document.getElementById("txtHmPermitsBasePeriod").value;
    var DateHmPermitAnalysisPeriod = document.getElementById("txtHmPermitsAnalysisPeriod").value;
    var DiffDaysHmPermitBasePeriod = document.getElementById("numHmPermitsAnalysisPeriod").value;
    var DiffDaysHmPermitAnalysisPeriod = document.getElementById("numHmPermitsAnalysisPeriod").value;
    sqlQueryTA = "select p.OBJECTID, p.BBL, CD, DISTRICT, p.Borough, p.Address, ZipCode, job_type, work_type, job_start_date from dbo.Pluto p inner join Permit pe on p.BBL = pe.bbl_10_digits inner join Districts d on p.CD = d.DISTRICTCODE";
    var BoroughsTA = "";
    var DistrictsTA = "";
    if (document.getElementById("cbBoroughTA").checked == true) {
        BoroughsTA = $(txtBoroughsTA).val();
    }
    if (document.getElementById("cbDistrictTA").checked == true) {
        DistrictsTA = $(txtDistrictsTA).val();
    }
    var ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
    var ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
    if ((selectedPermitJobType == "" && selectedPermitWorkType == "") || DateHmPermitBasePeriod == "" || DateHmPermitAnalysisPeriod == "" || DiffDaysHmPermitBasePeriod == "" || DiffDaysHmPermitAnalysisPeriod == "") {
        swal('Permit Job/Work Type, Base and Analysis periods and +/- days are required fields');
    }
    else {
        $('#loading').show();
        var BasePeriod = new Date(DateHmPermitBasePeriod);
        var BasePeriodFrom = BasePeriod.addDays(DiffDaysHmPermitBasePeriod * (-1));
        var BasePeriodTo = BasePeriod.addDays(DiffDaysHmPermitBasePeriod * (1));
        var AnalysisPeriod = new Date(DateHmPermitAnalysisPeriod);
        var AnalysisPeriodFrom = AnalysisPeriod.addDays(DiffDaysHmPermitAnalysisPeriod * (-1));
        var AnalysisPeriodTo = AnalysisPeriod.addDays(DiffDaysHmPermitAnalysisPeriod * (1));
        var formatedBasePeriodFrom = formatDate(BasePeriodFrom);
        var formatedBasePeriodTo = formatDate(BasePeriodTo);
        var formatedAnalysisPeriodFrom = formatDate(AnalysisPeriodFrom);
        var formatedAnalysisPeriodTo = formatDate(AnalysisPeriodTo);
        whereClauseHeatmapPermits = " Where (pe.work_type IN ('" + selectedPermitWorkType.split(",").join("','") + "')) AND (pe.job_type IN ('" + selectedPermitJobType.split(",").join("','") + "')) AND ((pe.job_start_date >= '" + formatedBasePeriodFrom + "' AND pe.job_start_date <= '" + formatedBasePeriodTo + "') OR (pe.job_start_date >= '" + formatedAnalysisPeriodFrom + "' AND pe.job_start_date <= '" + formatedAnalysisPeriodTo + "'))";

        lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'District', attribute: "DISTRICT", dataset: "Districts" }
            , { name: 'Address', attribute: "Address", dataset: "Pluto" }, { name: 'Zip Code', attribute: "ZipCode", dataset: "Pluto" }
            , { name: 'Job Start Date', attribute: "job_start_date", dataset: "Permit" }, { name: 'Work Type', attribute: "work_type", dataset: "Permit" }
            , { name: 'Job Type', attribute: "job_type", dataset: "Permit" }];
        if (document.getElementById("cbZipCodeRangeTA").checked == true) {
            ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
            ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
            if (ZipCodeRangeTAFrom != "" || ZipCodeRangeTATo != "") {
                if (whereClauseHeatmapPermits != "") {
                    whereClauseHeatmapPermits += " AND ";
                }
                if (ZipCodeRangeTAFrom != "" && ZipCodeRangeTATo != "") {
                    whereClauseHeatmapPermits += "p.ZipCode >= " + ZipCodeRangeTAFrom + " AND p.ZipCode <= " + ZipCodeRangeTATo;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE >= " + ZipCodeRangeTAFrom + " AND ZIPCODE <= " + ZipCodeRangeTATo);
                }
                else if (ZipCodeRangeTAFrom != "") {
                    whereClauseHeatmapPermits += "p.ZipCode >= " + ZipCodeRangeTAFrom;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE >= " + ZipCodeRangeTAFrom);
                }
                else if (ZipCodeRangeTATo != "") {
                    whereClauseHeatmapPermits += "p.ZipCode <= " + ZipCodeRangeTATo;
                    zipCodeFeatures.setDefinitionExpression("ZIPCODE <= " + ZipCodeRangeTATo);
                }
                zipCodeFeatures.setVisibility(true);
                myLabelLayer.setVisibility(true);
            }
        }
        else {
            ZipCodeRangeTAFrom = "";
            ZipCodeRangeTATo = "";
        }
        $.ajax({
            url: RootUrl + 'Home/SearchDatabaseHeatMapPermits',
            type: "POST",
            data: {
                "PermitJobType": selectedPermitJobType,
                "PermitWorkType": selectedPermitWorkType,
                "FormatedBasePeriodFrom": formatedBasePeriodFrom,
                "FormatedBasePeriodTo": formatedBasePeriodTo,
                "FormatedAnalysisPeriodFrom": formatedAnalysisPeriodFrom,
                "FormatedAnalysisPeriodTo": formatedAnalysisPeriodTo,
                "BoroughsTA": BoroughsTA,
                "DistrictsTA": DistrictsTA,
                "ZipCodeRangeTAFrom": ZipCodeRangeTAFrom,
                "ZipCodeRangeTATo": ZipCodeRangeTATo,
            }
        }).done(function (data) {
            if (data.length > 0) {
                $('#infoColorRamp').show();
                for (var i = 0; i < data.length; i++) {
                    if (data[i].diff_percentage > 100) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[0], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 100 && data[i].diff_percentage > 90) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[1], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 90 && data[i].diff_percentage > 80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[2], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 80 && data[i].diff_percentage > 70) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[3], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 70 && data[i].diff_percentage > 60) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[4], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 60 && data[i].diff_percentage > 50) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[5], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 50 && data[i].diff_percentage > 40) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[6], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 40 && data[i].diff_percentage > 30) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[7], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 30 && data[i].diff_percentage > 20) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[8], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 20 && data[i].diff_percentage > 10) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[9], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 10 && data[i].diff_percentage > 0) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[10], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= 0 && data[i].diff_percentage > -10) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[11], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -10 && data[i].diff_percentage > -20) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[12], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -20 && data[i].diff_percentage > -30) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[13], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -30 && data[i].diff_percentage > -40) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[14], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -40 && data[i].diff_percentage > -50) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[15], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -50 && data[i].diff_percentage > -60) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[16], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -60 && data[i].diff_percentage > -70) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[17], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -70 && data[i].diff_percentage > -80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[18], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                    else if (data[i].diff_percentage <= -80) {
                        MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[19], data[i].diff_percentage, data[i].DISTRICT, data[i].BasePeriodValue, data[i].AnalysisPeriodValue, data[i].diff_value);
                    }
                }
                dojo.connect(heatmapLayer, "onMouseMove", function (evt) {
                    var g = evt.graphic;
                    var basePeriodValue = g.attributes.basePeriodValue == null ? "" : g.attributes.basePeriodValue.toLocaleString('en');
                    var analysisPeriodValue = g.attributes.analysisPeriodValue == null ? "" : g.attributes.analysisPeriodValue.toLocaleString('en');
                    var diff_value = g.attributes.diff_value == null ? "" : g.attributes.diff_value.toLocaleString('en');
                    var percentage = g.attributes.percentage == null ? "" : g.attributes.percentage.toLocaleString('en') + "%";
                    var html = "<b>Base Period Value: </b>" + basePeriodValue + "<br/>";
                    html += "<b>Analysis Period Value: </b>" + analysisPeriodValue + "<br/>";
                    html += "<b>Difference: </b>" + diff_value + "<br/>";
                    html += "<b>Difference Percentage: </b>" + percentage + "<br/>";
                    map.infoWindow.setContent(html);
                    map.infoWindow.setTitle(g.attributes.district);
                    map.infoWindow.show(evt.screenPoint, map.getInfoWindowAnchor(evt.screenPoint));
                });
                dojo.connect(heatmapLayer, "onMouseOut", function () { map.infoWindow.hide(); });
                dojo.connect(heatmapLayer, "onClick", function (evt) {
                    $('#loading').show();
                    var g = evt.graphic;
                    sqlQuery = sqlQueryTA + whereClauseHeatmapPermits + " AND CD = " + g.attributes.districtcode + " order by job_start_date";
                    $.ajax({
                        url: RootUrl + 'Home/SearchDatabase',
                        type: "POST",
                        data: {
                            "sqlQuery": sqlQuery
                        }
                    }).done(function (data) {
                        showAlerts = false;
                        CreateDatabaseTable(data, false, true, data.length);
                        $('#loading').hide();
                    }).fail(function (f) {
                        $('#loading').hide();
                        swal("Failed to search the query");
                    });
                });
            }
            else {
                swal("Records not found");
                $('#infoColorRamp').hide();
                $('#loading').hide();
            }
            $('#loading').hide();
        }).fail(function (f) {
            swal("Failed to search the query");
            $('#loading').hide();
        });
    }
}
