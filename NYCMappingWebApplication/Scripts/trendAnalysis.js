﻿var heatmapColorRamp = ["rgb(16, 255, 16, 1)", "rgb(28, 242, 16, 1)", "rgb(41, 229, 16, 1)", "rgb(53, 217, 16, 1)", "rgb(66, 204, 16, 1)"
    , "rgb(78, 192, 16, 1)", "rgb(91, 179, 16, 1)", "rgb(104, 166, 16, 1)", "rgb(116, 154, 16, 1)", "rgb(129, 141, 16, 1)"
    , "rgb(141, 129, 16, 1)", "rgb(154, 116, 16, 1)", "rgb(166, 104, 16, 1)", "rgb(179, 91, 16, 1)", "rgb(192, 78, 16, 1)"
    , "rgb(204, 66, 16, 1)", "rgb(217, 53, 16, 1)", "rgb(229, 41, 16, 1)", "rgb(242, 28, 16, 1)", "rgb(255, 16, 16, 1)"];
var heatmapPercentgeRamp = ["100%-95%", "95%-90%", "90%-85%", "85%-80%", "80%-75%"
    , "75%-70%", "70%-65%", "65%-60%", "60%-55%", "55%-50%"
    , "50%-45%", "45%-40%", "40%-35%", "35%-30%", "30%-25%"
    , "25%-20%", "20%-15%", "15%-10%", "10%-5%", "5%-0%"];
function btnSearchHeatMapPropertySales() {
    heatmapLayer.clear();
    var sqlQuery = "";
    var groupOrder = " group by CD order by recordValue desc";
    if (document.getElementById('rbHeatMapByNumberOfPropertySales').checked) {
        sqlQuery = "select CD, try_convert(bigint, count(CD)) as recordValue from dbo.Pluto p inner join PropertySales ps on p.BBL = ps.bbl";
    }
    else {
        sqlQuery = "select CD, avg(try_convert(bigint, sale_price)) as recordValue from dbo.Pluto p inner join PropertySales ps on p.BBL = ps.bbl";
    }
    var whereClauseHeatmapPropertySales = "";
    var DateHmPsBasePeriod = document.getElementById("txtHmPsBasePeriod").value;
    var DateHmPsAnalysisPeriod = document.getElementById("txtHmPsAnalysisPeriod").value;
    if (document.getElementById("cbHmPsBasePeriod").checked == true && DateHmPsBasePeriod != "") {
        if (whereClauseHeatmapPropertySales == "") {
            whereClauseHeatmapPropertySales = " Where ps.sale_date >= '" + DateHmPsBasePeriod + "'";
        }
        else {
            whereClauseHeatmapPropertySales += " AND ps.sale_date >= '" + DateHmPsBasePeriod + "'";
        }
    }
    if (document.getElementById("cbHmPsAnalysisPeriod").checked == true && DateHmPsAnalysisPeriod != "") {
        if (whereClauseHeatmapPropertySales == "") {
            whereClauseHeatmapPropertySales = " Where ps.sale_date <= '" + DateHmPsAnalysisPeriod + "'";
        }
        else {
            whereClauseHeatmapPropertySales += " AND ps.sale_date <= '" + DateHmPsAnalysisPeriod + "'";
        }
    }
    sqlQuery += whereClauseHeatmapPropertySales + groupOrder;
    $.ajax({
        url: RootUrl + 'Home/SearchDatabaseHeatMap',
        type: "POST",
        data: {
            "sqlQuery": sqlQuery
        }
    }).done(function (data) {
        if (data.length > 0) {
            $('#infoColorRamp').show();
            var objectIDs = [];
            var maxValue = data[0].recordValue;
            for (var i = 0; i < data.length; i++) {
                if ((data[i].recordValue * 100 / maxValue) > 95) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[0], data[i].recordValue * 100 / maxValue);
                    //objectIDs[0] = objectIDs[0] == null ? data[i].CD : objectIDs[0] + "," + data[i].CD;
                    //if (objectIDs[0].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[0], heatmapColorRamp[0]);
                    //    objectIDs[0] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 95 && (data[i].recordValue * 100 / maxValue) > 90) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[1], data[i].recordValue * 100 / maxValue);
                    //objectIDs[1] = objectIDs[1] == null ? data[i].CD : objectIDs[1] + "," + data[i].CD;
                    //if (objectIDs[1].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[1], heatmapColorRamp[1]);
                    //    objectIDs[1] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 90 && (data[i].recordValue * 100 / maxValue) > 85) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[2], data[i].recordValue * 100 / maxValue);
                    //objectIDs[2] = objectIDs[2] == null ? data[i].CD : objectIDs[2] + "," + data[i].CD;
                    //if (objectIDs[2].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[2], heatmapColorRamp[2]);
                    //    objectIDs[2] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 85 && (data[i].recordValue * 100 / maxValue) > 80) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[3], data[i].recordValue * 100 / maxValue);
                    //objectIDs[3] = objectIDs[3] == null ? data[i].CD : objectIDs[3] + "," + data[i].CD;
                    //if (objectIDs[3].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[3], heatmapColorRamp[3]);
                    //    objectIDs[3] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 80 && (data[i].recordValue * 100 / maxValue) > 75) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[4], data[i].recordValue * 100 / maxValue);
                    //objectIDs[4] = objectIDs[4] == null ? data[i].CD : objectIDs[4] + "," + data[i].CD;
                    //if (objectIDs[4].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[4], heatmapColorRamp[4]);
                    //    objectIDs[4] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 75 && (data[i].recordValue * 100 / maxValue) > 70) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[5], data[i].recordValue * 100 / maxValue);
                    //objectIDs[5] = objectIDs[5] == null ? data[i].CD : objectIDs[5] + "," + data[i].CD;
                    //if (objectIDs[5].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[5], heatmapColorRamp[5]);
                    //    objectIDs[5] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 70 && (data[i].recordValue * 100 / maxValue) > 65) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[6], data[i].recordValue * 100 / maxValue);
                    //objectIDs[6] = objectIDs[6] == null ? data[i].CD : objectIDs[6] + "," + data[i].CD;
                    //if (objectIDs[6].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[6], heatmapColorRamp[6]);
                    //    objectIDs[6] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 65 && (data[i].recordValue * 100 / maxValue) > 60) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[7], data[i].recordValue * 100 / maxValue);
                    //objectIDs[7] = objectIDs[7] == null ? data[i].CD : objectIDs[7] + "," + data[i].CD;
                    //if (objectIDs[7].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[7], heatmapColorRamp[7]);
                    //    objectIDs[7] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 60 && (data[i].recordValue * 100 / maxValue) > 55) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[8], data[i].recordValue * 100 / maxValue);
                    //objectIDs[8] = objectIDs[8] == null ? data[i].CD : objectIDs[8] + "," + data[i].CD;
                    //if (objectIDs[8].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[8], heatmapColorRamp[8]);
                    //    objectIDs[8] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 55 && (data[i].recordValue * 100 / maxValue) > 50) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[9], data[i].recordValue * 100 / maxValue);
                    //objectIDs[9] = objectIDs[9] == null ? data[i].CD : objectIDs[9] + "," + data[i].CD;
                    //if (objectIDs[9].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[9], heatmapColorRamp[9]);
                    //    objectIDs[9] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 50 && (data[i].recordValue * 100 / maxValue) > 45) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[10], data[i].recordValue * 100 / maxValue);
                    //objectIDs[10] = objectIDs[10] == null ? data[i].CD : objectIDs[10] + "," + data[i].CD;
                    //if (objectIDs[10].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[10], heatmapColorRamp[10]);
                    //    objectIDs[10] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 45 && (data[i].recordValue * 100 / maxValue) > 40) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[11], data[i].recordValue * 100 / maxValue);
                    //objectIDs[11] = objectIDs[11] == null ? data[i].CD : objectIDs[11] + "," + data[i].CD;
                    //if (objectIDs[11].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[11], heatmapColorRamp[11]);
                    //    objectIDs[11] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 40 && (data[i].recordValue * 100 / maxValue) > 35) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[12], data[i].recordValue * 100 / maxValue);
                    //objectIDs[12] = objectIDs[12] == null ? data[i].CD : objectIDs[12] + "," + data[i].CD;
                    //if (objectIDs[12].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[12], heatmapColorRamp[12]);
                    //    objectIDs[12] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 35 && (data[i].recordValue * 100 / maxValue) > 30) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[13], data[i].recordValue * 100 / maxValue);
                    //objectIDs[13] = objectIDs[13] == null ? data[i].CD : objectIDs[13] + "," + data[i].CD;
                    //if (objectIDs[13].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[13], heatmapColorRamp[13]);
                    //    objectIDs[13] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 30 && (data[i].recordValue * 100 / maxValue) > 25) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[14], data[i].recordValue * 100 / maxValue);
                    //objectIDs[14] = objectIDs[14] == null ? data[i].CD : objectIDs[14] + "," + data[i].CD;
                    //if (objectIDs[14].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[14], heatmapColorRamp[14]);
                    //    objectIDs[14] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 25 && (data[i].recordValue * 100 / maxValue) > 20) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[15], data[i].recordValue * 100 / maxValue);
                    //objectIDs[15] = objectIDs[15] == null ? data[i].CD : objectIDs[15] + "," + data[i].CD;
                    //if (objectIDs[15].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[15], heatmapColorRamp[15]);
                    //    objectIDs[15] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 20 && (data[i].recordValue * 100 / maxValue) > 15) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[16], data[i].recordValue * 100 / maxValue);
                    //objectIDs[16] = objectIDs[16] == null ? data[i].CD : objectIDs[16] + "," + data[i].CD;
                    //if (objectIDs[16].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[16], heatmapColorRamp[16]);
                    //    objectIDs[16] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 15 && (data[i].recordValue * 100 / maxValue) > 10) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[17], data[i].recordValue * 100 / maxValue);
                    //objectIDs[17] = objectIDs[17] == null ? data[i].CD : objectIDs[17] + "," + data[i].CD;
                    //if (objectIDs[17].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[17], heatmapColorRamp[17]);
                    //    objectIDs[17] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 10 && (data[i].recordValue * 100 / maxValue) > 5) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[18], data[i].recordValue * 100 / maxValue);
                    //objectIDs[18] = objectIDs[18] == null ? data[i].CD : objectIDs[18] + "," + data[i].CD;
                    //if (objectIDs[18].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[18], heatmapColorRamp[18]);
                    //    objectIDs[18] = null;
                    //}
                }
                else if ((data[i].recordValue * 100 / maxValue) <= 5) {
                    MapPlutoHeatmapSearch(data[i].CD, heatmapColorRamp[19], data[i].recordValue * 100 / maxValue);
                    //objectIDs[19] = objectIDs[19] == null ? data[i].CD : objectIDs[19] + "," + data[i].CD;
                    //if (objectIDs[19].length > 10000) {
                    //    MapPlutoHeatmapSearch(objectIDs[19], heatmapColorRamp[19]);
                    //    objectIDs[19] = null;
                    //}
                }
            }
            //for (var i = 0; i < 20; i++) {
            //    MapPlutoHeatmapSearch(objectIDs[i], heatmapColorRamp[i], heatmapPercentgeRamp[i]);
            //}
            //MapPlutoHeatmapSearch("206,208,209,210,211,212,226,305,309,310,312,313,315,317,403,404,405,406,407,408,409,410,411,412,413,414,484,501,502,503", heatmapColorRamp[5]);
        }
        else {
            swal("Records not found");
            $('#infoColorRamp').hide();
        }
    }).fail(function (f) {
        swal("Failed to search the query");
    });
}

function MapPlutoHeatmapSearch(objectIDs, heatmapColor, percentage) {
    if (objectIDs !== undefined) {
        queryTask = new esri.tasks.QueryTask(DistrictsUrl);
        //initialize query
        query = new esri.tasks.Query();
        query.returnGeometry = true;

        query.where = "DISTRICTCODE IN (" + objectIDs + ")";

        //execute query
        queryTask.execute(query, function executeMapPlutoSearch(featureSet) {
            var resultFeatures = featureSet.features;
            //Loop through each feature returned
            for (var i = 0, il = resultFeatures.length; i < il; i++) {
                var graphic = resultFeatures[i];
                var graphicAtt = {
                    percentage: percentage
                };

                var myColor = esri.Color.fromString(heatmapColor);
                var heatmapSymbolFill = new esri.symbol.SimpleFillSymbol().setColor(new dojo.Color(myColor));
                var myGraphic = new esri.Graphic(graphic.geometry, heatmapSymbolFill, graphicAtt);
                heatmapLayer.add(myGraphic);
            }
            dojo.connect(heatmapLayer, "onMouseMove", function (evt) {
                var g = evt.graphic;
                map.infoWindow.setContent(g.attributes.percentage.toFixed(2) + "%");
                //map.infoWindow.setTitle(g.getTitle());
                map.infoWindow.show(evt.screenPoint, map.getInfoWindowAnchor(evt.screenPoint));
            });
            dojo.connect(heatmapLayer, "onMouseOut", function () { map.infoWindow.hide(); });
        }, function (error) {
            console.log(error);
        });
    }
}

function btnResetHeatMapPropertySales() {
    $('#infoColorRamp').hide();
    heatmapLayer.clear();
    document.getElementById("rbHeatMapByNumberOfPropertySales").checked = true;
    document.getElementById("rbHeatMapByAveragePropertySalesPrice").checked = false;
    document.getElementById("cbHmPsBasePeriod").checked = false;
    document.getElementById("cbHmPsAnalysisPeriod").checked = false;
    document.getElementById("txtHmPsBasePeriod").value = "";
    document.getElementById("txtHmPsAnalysisPeriod").value = "";
}