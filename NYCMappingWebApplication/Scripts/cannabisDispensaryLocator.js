var maxDistanceCDL = 1000, currentGeometryNum = 0, currentBufferNum = 0;
$(function () {
    handle = $("#custom-handle"), handleWidth = handle.width();
    $("#slider-DistanceCDL").slider({
        min: 0,
        max: maxDistanceCDL,
        value: 100,
        create: function () {
            handle.text($(this).slider("value"));
        },
        slide: function (event, ui) {
            handle.text(ui.value);
            handle.css({
                'margin-left': 0 - ((ui.value / maxDistanceCDL) * handleWidth)
            });
        }
    });
});
function btnResetCDL() {
    resetAllSelectionToolsCDL();
    selectionToolbarCDL.deactivate();
    selectionLayer.clear();
    selectionLayerCDL.clear();
    selectionLayerExclusionCriteriaCDL.clear();
    $("#txtBuildingClassIC").select2("val", "");
    $("#txtBuildingClassEC").select2("val", "");
    document.getElementById("rbDistanceCDLFeet").checked = true;
    document.getElementById("rbDistanceCDLMeters").checked = false;
    $("#slider-DistanceCDL").slider("value", 100);
    handle.text(100);
    $('.panel-collapse.in').collapse('toggle');
    hideResultsPanel();
    map.setExtent(initExtent);

    $('#divSelectItemsTable').text('');
    $('#divSelectItemsMessage').hide();
    $('#divSelectItemsMoreInfo').hide();
    $('#divSelectItemsCount').hide();
    $('#btnOpenSaveReport').hide();
    $('#btnOpenAlerts').hide();
}

function btnSearchCDL() {
    currentGeometryNum = 0;
    currentBufferNum = 0;
    $('#loading').show();
    document.getElementById("divLoadingText").innerHTML = "";
    var BldgClassIC = document.getElementById("txtBuildingClassIC").value;
    var drawnGraphics = selectionLayerCDL.graphics;
    selectionLayerExclusionCriteriaCDL.clear();
    selectionLayer.clear();
    if (drawnGraphics.length > 0 && BldgClassIC != "") {
        var queryTask = new esri.tasks.QueryTask(MapPlutoUrl);
        var query = new esri.tasks.Query();
        query.geometry = drawnGraphics[0].geometry;
        query.where = "BldgClass in (" + BldgClassIC + ")";
        query.returnGeometry = true;
        query.outFields = ["*"];
        queryTask.execute(query, function (featureSet) {
            //Performance enhancer - assign featureSet array to a single variable.
            var resultFeatures = featureSet.features;
            if (resultFeatures.length > 0) {
                //Loop through each feature returned
                for (var i = 0, il = resultFeatures.length; i < il; i++) {
                    //Feature is a graphic
                    var graphic = resultFeatures[i];
                    switch (graphic.geometry.type) {
                        case "point":
                        case "multipoint":
                            graphic.setSymbol(selectionSymbolPoint);
                            break;
                        case "polyline":
                            graphic.setSymbol(selectionSymbolLine);
                            break;
                        default:
                            graphic.setSymbol(selectionSymbolFill);
                            break;
                    }
                    selectionLayer.add(graphic);
                }
                var BldgClassEC = document.getElementById("txtBuildingClassEC").value;
                if (BldgClassEC != "") {
                    searchExclusionCriteria(BldgClassEC);
                }
                else {
                    CallCreateTable();
                    $('#loading').hide();
                }
            }
            else {
                $('#loading').hide();
                swal("No one property meet the selected criteria");
            }
        }, function (error) {
            console.log("An error occurred from btnSearchCDL()." + error);
        });
    }
    else {
        $('#loading').hide();
        swal("Please draw searching area and choose including criteria");
    }
}

function searchExclusionCriteria(BldgClassEC) {
    var params = new esri.tasks.BufferParameters();
    params.geometries = [selectionLayerCDL.graphics[0].geometry];

    params.distances = [$("#slider-DistanceCDL").slider("value")];
    if (document.getElementById("rbDistanceCDLFeet").checked) {
        params.unit = esri.tasks.GeometryService.UNIT_FOOT;
    }
    else {
        params.unit = esri.tasks.GeometryService.UNIT_METER;
    }
    params.outSpatialReference = map.spatialReference;

    geometryService.buffer(params, function (bufferedGeometries) {
        var graphic = new esri.Graphic(bufferedGeometries[0], selectionSymbolEC_CDL_Fill);
        var queryTask = new esri.tasks.QueryTask(MapPlutoUrl);
        var query = new esri.tasks.Query();
        query.geometry = graphic.geometry;
        query.where = "BldgClass in (" + BldgClassEC + ")";
        query.returnGeometry = true;
        query.outFields = ["*"];
        queryTask.execute(query, function (featureSet) {
            //Performance enhancer - assign featureSet array to a single variable.
            var resultFeatures = featureSet.features;
            if (resultFeatures.length > 0) {
                //Loop through each feature returned
                for (var i = 0, il = resultFeatures.length; i < il; i++) {
                    //Feature is a graphic
                    var graphic = resultFeatures[i];
                    doBuffer(graphic.geometry, resultFeatures.length);
                    switch (graphic.geometry.type) {
                        case "point":
                        case "multipoint":
                            graphic.setSymbol(selectionSymbolPoint);
                            break;
                        case "polyline":
                            graphic.setSymbol(selectionSymbolLine);
                            break;
                        default:
                            graphic.setSymbol(selectionSymbolFill);
                            break;
                    }
                }
            }
            else {
                CallCreateTable();
                $('#loading').hide();
            }
        }, function (error) {
            $('#loading').hide();
            console.log("An error occurred from btnSearchCDL()." + error);
        });
    });
}

function doBuffer(myGeometry, totalGeometries) {
    var params = new esri.tasks.BufferParameters();
    params.geometries = [myGeometry];

    params.distances = [$("#slider-DistanceCDL").slider("value")];
    if (document.getElementById("rbDistanceCDLFeet").checked) {
        params.unit = esri.tasks.GeometryService.UNIT_FOOT;
    }
    else {
        params.unit = esri.tasks.GeometryService.UNIT_METER;
    }
    params.outSpatialReference = map.spatialReference;

    geometryService.buffer(params, function (bufferedGeometries) {
        currentBufferNum++;
        document.getElementById("divLoadingText").innerHTML = percentage(currentGeometryNum + currentBufferNum, totalGeometries * 2) + " %";
        var graphic = new esri.Graphic(bufferedGeometries[0], selectionSymbolEC_CDL_Fill);
        selectionLayerExclusionCriteriaCDL.add(graphic);
        for (var i = 0; i < selectionLayer.graphics.length; i++) {
            checkIntersection(selectionLayer.graphics[i], graphic.geometry, (selectionLayer.graphics.length - 1) == i, totalGeometries);
        }
    });
}

function checkIntersection(selectionLayerGraphics, excludeBufferGeometry, isLastGraphics, totalGeometries) {
    require([
        "esri/geometry/geometryEngineAsync"
    ], function (geometryEngineAsync) {
        geometryEngineAsync.intersects(selectionLayerGraphics.geometry, excludeBufferGeometry).then(
            function (res) {
                if (res) {
                    selectionLayer.remove(selectionLayerGraphics);
                }
                if (isLastGraphics) {
                    currentGeometryNum++;
                    if (totalGeometries == currentGeometryNum) {
                        CallCreateTable();
                        document.getElementById("divLoadingText").innerHTML = "";
                        $('#loading').hide();
                    }
                }
                else {
                    document.getElementById("divLoadingText").innerHTML = percentage(currentGeometryNum + currentBufferNum, totalGeometries * 2) + " %";
                }
            }
        );
    });
}

function CallCreateTable() {
    var data = [];
    for (var i = 0; i < selectionLayer.graphics.length; i++) {
        data.push({ BBL: selectionLayer.graphics[i].attributes.BBL, Borough: selectionLayer.graphics[i].attributes.Borough, Address: selectionLayer.graphics[i].attributes.Address, BldgClass: selectionLayer.graphics[i].attributes.BldgClass });
    }
    lstTableAttributes = [{ name: 'Borough', attribute: "Borough", dataset: "Pluto" }, { name: 'Address', attribute: "Address", dataset: "Pluto" }, { name: 'Building Class', attribute: "BldgClass", dataset: "Pluto" }];
    showAlerts = false;
    CreateDatabaseTable(data, false, false, data.length);
    var commaSeparatedBBLs = data.map(function (elem) {
        return "'" + elem.BBL + "'";
    }).join(",");
    sqlQuery = "SELECT Borough,Address,BldgClass FROM dbo.Pluto WHERE BBL IN (" + commaSeparatedBBLs + ")";
}

function activateSelectionToolCDL(tool, bth) {
    map.setInfoWindowOnClick(false);
    resetAllSelectionToolsCDL();
    $(bth).removeClass('classSelectButtons');
    $(bth).addClass('classSelectButtonsActive');
    selectionToolbarCDL.activate(esri.toolbars.Draw[tool.toUpperCase()]);

    dojo.on(selectionToolbarCDL, "DrawEnd", function (geometry) {
        selectionLayerCDL.clear();
        selectionToolbarCDL.deactivate();
        map.setInfoWindowOnClick(true);
        resetAllSelectionToolsCDL();
        switch (geometry.type) {
            case "point":
            case "multipoint":
                graphic = new esri.Graphic(geometry, selectionSymbolPoint);
                break;
            case "polyline":
                graphic = new esri.Graphic(geometry, selectionSymbolLine);
                break;
            default:
                graphic = new esri.Graphic(geometry, selectionSymbolFill);
                break;
        }
        selectionLayerCDL.add(graphic);
    });
}

function resetAllSelectionToolsCDL() {
    document.getElementById("btnSelectByPointCDL").className = " btn btn-small classSelectButtons col-sm-2";
    document.getElementById("btnSelectByMultiPointCDL").className = " btn btn-small classSelectButtons col-sm-2";
    document.getElementById("btnSelectByPolylineCDL").className = " btn btn-small classSelectButtons col-sm-2";
    document.getElementById("btnSelectByPolygonCDL").className = " btn btn-small classSelectButtons col-sm-2";
    document.getElementById("btnSelectByFreehandPolylineCDL").className = " btn btn-small classSelectButtons col-sm-2";
    document.getElementById("btnSelectByRectangleCDL").className = " btn btn-small classSelectButtons col-sm-2";
}

function percentage(partialValue, totalValue) {
    return ((100 * partialValue) / totalValue).toFixed(0);
} 