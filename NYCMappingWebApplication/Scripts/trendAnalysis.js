var heatmapColorRamp = ["rgb(16, 255, 16, 1)", "rgb(28, 242, 16, 1)", "rgb(41, 229, 16, 1)", "rgb(53, 217, 16, 1)", "rgb(66, 204, 16, 1)"
    , "rgb(78, 192, 16, 1)", "rgb(91, 179, 16, 1)", "rgb(104, 166, 16, 1)", "rgb(116, 154, 16, 1)", "rgb(129, 141, 16, 1)"
    , "rgb(141, 129, 16, 1)", "rgb(154, 116, 16, 1)", "rgb(166, 104, 16, 1)", "rgb(179, 91, 16, 1)", "rgb(192, 78, 16, 1)"
    , "rgb(204, 66, 16, 1)", "rgb(217, 53, 16, 1)", "rgb(229, 41, 16, 1)", "rgb(242, 28, 16, 1)", "rgb(255, 16, 16, 1)"];
var whereClauseHeatmapPropertySales = "";
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
function btnSearchHeatMapPropertySales() {
    heatmapLayer.clear();
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
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
    sqlQueryTA = "select p.OBJECTID, CD, DISTRICT, p.Borough, p.Address, ZipCode, sale_date, sale_price from dbo.Pluto p inner join PropertySales ps on p.BBL = ps.bbl inner join Districts d on p.CD = d.DISTRICTCODE";
    var DateHmPsBasePeriod = document.getElementById("txtHmPsBasePeriod").value;
    var DateHmPsAnalysisPeriod = document.getElementById("txtHmPsAnalysisPeriod").value;
    var DiffDaysHmPsBasePeriod = document.getElementById("numHmPsAnalysisPeriod").value;
    var DiffDaysHmPsAnalysisPeriod = document.getElementById("numHmPsAnalysisPeriod").value;
    var BoroughsTA = $(txtBoroughsTA).val();
    var DistrictsTA = $(txtDistrictsTA).val();
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
        //if (document.getElementById("cbBoroughTA").checked == true) {
        //    Borough = $(txtBoroughsTA).val();
        //    if (Borough != "") {
        //        if (whereClauseHeatmapPropertySales == "") {
        //            whereClauseHeatmapPropertySales = "p.Borough IN (" + Borough + ")";
        //        }
        //        else {
        //            whereClauseHeatmapPropertySales += " AND p.Borough IN (" + Borough + ")";
        //        }
        //    }
        //}
        //if (document.getElementById("cbDistrictTA").checked == true) {
        //    District = $(txtDistrictsTA).val();
        //    if (District != "") {
        //        if (whereClauseHeatmapPropertySales == "") {
        //            whereClauseHeatmapPropertySales = "p.CD IN (" + District + ")";
        //        }
        //        else {
        //            whereClauseHeatmapPropertySales += " AND p.CD IN (" + District + ")";
        //        }
        //        SearchGeometry("DISTRICTCODE IN (" + District + ")");
        //    }
        //}
        if (document.getElementById("cbZipCodeRangeTA").checked == true) {
            ZipCodeRangeTAFrom = document.getElementById("txtZipCodeRangeTAFrom").value;
            ZipCodeRangeTATo = document.getElementById("txtZipCodeRangeTATo").value;
            if (ZipCodeRangeTAFrom != "" || ZipCodeRangeTATo != "") {
                if (whereClauseHeatmapPropertySales != "") {
                    whereClauseHeatmapPropertySales += " AND ";
                }
                if (ZipCodeRangeTAFrom != "" && ZipCodeRangeTATo != "") {
                    whereClauseHeatmapPropertySales += "p.ZipCode >= " + ZipCodeRangeTAFrom + " AND p.ZipCode <= " + ZipCodeRangeTATo;
                }
                else if (ZipCodeRangeTAFrom != "") {
                    whereClauseHeatmapPropertySales += "p.ZipCode >= " + ZipCodeRangeTAFrom;;
                }
                else if (ZipCodeRangeTATo != "") {
                    whereClauseHeatmapPropertySales += "p.ZipCode <= " + ZipCodeRangeTATo;;
                }
            }
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
                        CreateDatabaseTable(data);
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

function btnResetHeatMapPropertySales() {
    $('#infoColorRamp').hide();
    heatmapLayer.clear();
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);

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

    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsCount').hide();

    if ($('.slider-bottom-arrow').hasClass("hideBottomPanel")) {
        $('.slider-bottom-arrow').click();
    }

}