var selectionLayer, districtLayer, geometryService, selectionSymbolPoint, selectionSymbolLine, selectionSymbolFill, heatmapLayer, selectionLayerCDL;
$(document).ready(function () {
    dojo.require("dojo.on");
    dojo.require("esri.tasks.geometry");
    require(["esri/map", "dojo/parser", "esri/layers/FeatureLayer", "esri/config", "esri/symbols/SimpleMarkerSymbol", "esri/symbols/SimpleLineSymbol", "esri/symbols/SimpleFillSymbol", "dojo/_base/array"
        , "esri/graphic", "esri/basemaps", "esri/InfoTemplate", "esri/layers/GraphicsLayer", "esri/tasks/QueryTask", "esri/tasks/query", "esri/toolbars/draw"
        , "esri/tasks/GeometryService", "esri/Color", "esri/symbols/TextSymbol", "esri/renderers/SimpleRenderer"
        , "esri/layers/LabelLayer"
    ], function (Map, parser, FeatureLayer, esriConfig, SimpleMarkerSymbol, SimpleLineSymbol, SimpleFillSymbol, arrayUtils
        , Graphic, esriBasemaps, InfoTemplate, GraphicsLayer, QueryTask, Query, Draw
        , GeometryService, Color, TextSymbol, SimpleRenderer
        , LabelLayer) {
        parser.parse();
        esriConfig.defaults.io.proxyUrl = "/proxy/proxy.ashx";
        selectionLayer = new GraphicsLayer();
        districtLayer = new GraphicsLayer();
        heatmapLayer = new GraphicsLayer();
        selectionLayerCDL = new GraphicsLayer();
        selectionLayerExclusionCriteriaCDL = new GraphicsLayer();
        geometryService = new GeometryService(GeometryServiceUrl);

        selectionLayer.on("click", selectionLayerClickHandler);

        //create symbol polygon for districts features
        symbolDistrictFill = new SimpleFillSymbol();
        symbolDistrictFill.setColor(new dojo.Color([51, 255, 204, 0]));
        //create symbol polygon for selected features
        symbolFill = new SimpleFillSymbol();
        symbolFill.setColor(new dojo.Color([51, 255, 204, 0.5]));
        //create symbol polygon for zoomed features
        symbolZoomedFill = new SimpleFillSymbol();
        symbolZoomedFill.setColor(new dojo.Color([158, 188, 218, 0.5]));

        //create symbols for selected features
        selectionSymbolPoint = new SimpleMarkerSymbol().setColor(new dojo.Color([51, 255, 204, 0.5])).setSize(8);
        selectionSymbolLine = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new dojo.Color([51, 255, 204, 0.5]), 3);
        selectionSymbolFill = new SimpleFillSymbol().setColor(new dojo.Color([51, 255, 204, 0.5]));

        //create symbols for excluded features in CDL
        selectionSymbolEC_CDL_Fill = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([255, 0, 0, 0.65]), 2), new Color([255, 0, 0, 0.35]));
        //xmin   //ymin    //xmax    //ymax
        //initExtent = new esri.geometry.Extent(-8276983.607858135, 4935156.731231112, -8169207.3979761815, 5005402.3602250945,
        //        new esri.SpatialReference({ wkid: 102100 }));
        //xmin   //ymin    //xmax    //ymax
        initExtent = new esri.geometry.Extent(913128.926351264, 120048.986001998, 1067335.95126992, 272811.183429763,
            new esri.SpatialReference({ wkid: 102718 }));
        var arrayAttributes = ["OBJECTID", "Borough", "Block", "Lot", "CD", "CT2010", "CB2010", "SchoolDist", "Council", "ZipCode", "FireComp", "PolicePrct", "HealthCenterDistrict", "HealthArea", "Sanitboro", "SanitDistrict", "SanitSub", "Address", "ZoneDist1", "ZoneDist2", "ZoneDist3", "ZoneDist4", "Overlay1", "Overlay2", "SPDist1", "SPDist2", "SPDist3", "LtdHeight", "SplitZone", "BldgClass", "LandUse", "Easements", "OwnerType", "OwnerName", "LotArea", "BldgArea", "ComArea", "ResArea", "OfficeArea", "RetailArea", "GarageArea", "StrgeArea", "FactryArea", "OtherArea", "AreaSource", "NumBldgs", "NumFloors", "UnitsRes", "UnitsTotal", "LotFront", "LotDepth", "BldgFront", "BldgDepth", "Ext", "ProxCode", "IrrLotCode", "LotType", "BsmtCode", "AssessLand", "AssessTot", "ExemptTot", "YearBuilt", "YearAlter1", "YearAlter2", "HistDist", "Landmark", "BuiltFAR", "ResidFAR", "CommFAR", "FacilFAR", "BoroCode", "BBL", "CondoNo", "Tract2010", "XCoord", "YCoord", "ZoneMap", "ZMCode", "Sanborn", "TaxMap", "EDesigNum", "APPBBL", "APPDate", "PLUTOMapID", "FIRM07_FLAG", "PFIRM15_FLAG", "Version", "DCPEdited", "Latitude", "Longitude", "Notes"];
        infoTemplateContent = "<img src ='https://maps.googleapis.com/maps/api/streetview?size=235x145&location=${Latitude},${Longitude}&key=AIzaSyCuf0Ca1EvxogvbZQKOBl_40y0UWm4Fk30'>";
        for (var i = 0; i < arrayAttributes.length; i++) {
            infoTemplateContent += "<br><b>" + arrayAttributes[i] + "</b>: ${" + arrayAttributes[i] + "}";
        }

        //Takes a URL to a non cached map service.
        serviceFeatures = new FeatureLayer(MapPlutoUrl, {
            visible: true,
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ["*"],
            infoTemplate: new InfoTemplate("Tax Lot Info", infoTemplateContent)
        });
        //Takes a URL to a non cached map service.
        districtFeatures = new FeatureLayer(DistrictsUrl, {
            visible: true,
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ["*"]
        });
        //districtFeatures.setOpacity(0.1);//color districts

        //Takes a URL to a non cached map service.
        censusTractsFeatures = new FeatureLayer(CensusTractsUrl, {
            visible: false,
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ["*"]
        });
        //Takes a URL to a non cached map service.
        zipCodeFeatures = new FeatureLayer(NYCzipCodeUrl, {
            id: "nycZipCodes",
            visible: false,
            mode: FeatureLayer.MODE_ONDEMAND,
            outFields: ["*"]
        });
        zipCodeFeatures.setRenderer(new SimpleRenderer(new SimpleFillSymbol(SimpleFillSymbol.STYLE_NULL, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([0, 0, 0]), 2), new Color([255, 255, 0, 0]))));
        esriBasemaps.NYCbasemap = {
            baseMapLayers: [{ url: NYCbasemapUrl }
            ],
            title: "NYCbasemap"
        };

        var baseMapLayer = new esri.layers.ArcGISTiledMapServiceLayer(NYCbasemapUrl);
        map = new Map("mapDiv", {
            //basemap: NYCbasemap, // dark-gray, gray, hybrid, national-geographic, oceans, osm, satellite, streets, terrain, topo
            extent: initExtent
        });
        map.on("load", function MapLoaded() {
            selectionToolbar = new Draw(map, { showTooltips: false });
            selectionToolbar.on("draw-end", addSelectionToMap);
            selectionToolbarCDL = new Draw(map, { showTooltips: false });
            //ColorDistricts();//color districts
            //getAllTracts(2165);
        });

        // create a text symbol to define the style of labels
        var myLabel = new TextSymbol().setColor(new Color("#000000"));
        myLabel.font.setSize("10pt");
        myLabel.font.setFamily("Arial");

        myLabelRenderer = new SimpleRenderer(myLabel);
        myLabelLayer = new LabelLayer({ id: "districtLabels", visible: true });
        myLabelLayer.addFeatureLayer(districtFeatures, myLabelRenderer, "{DISTRICT}");

        map.addLayers([baseMapLayer, serviceFeatures, districtLayer, censusTractsFeatures, selectionLayer, heatmapLayer, zipCodeFeatures, selectionLayerCDL, selectionLayerExclusionCriteriaCDL]);

        function ColorDistricts() {
            var queryTask = new esri.tasks.QueryTask(DistrictsUrl);
            var query = new esri.tasks.Query();
            query.where = "1=1";
            query.returnGeometry = true;
            queryTask.execute(query, function (featureSet) {
                //Performance enhancer - assign featureSet array to a single variable.
                var resultFeatures = featureSet.features;
                if (resultFeatures.length > 0) {
                    //Loop through each feature returned
                    for (var i = 0, il = resultFeatures.length; i < il; i++) {
                        //Feature is a graphic
                        var graphic = resultFeatures[i];
                        var districtCode = graphic.attributes["DISTRICT"];
                        symbolDistrictFillDarkGreen = new SimpleFillSymbol();
                        symbolDistrictFillDarkGreen.setOutline(new SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new Color([0, 0, 0]), 1));
                        symbolDistrictFillDarkGreen.setColor(new dojo.Color([0, 100, 0, 1]));
                        symbolDistrictFillMediumGreen = new SimpleFillSymbol();
                        symbolDistrictFillMediumGreen.setOutline(new SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new Color([0, 0, 0]), 1));
                        symbolDistrictFillMediumGreen.setColor(new dojo.Color([51, 255, 204, 1]));
                        symbolDistrictFillLightGreen = new SimpleFillSymbol();
                        symbolDistrictFillLightGreen.setOutline(new SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new Color([0, 0, 0]), 1));
                        symbolDistrictFillLightGreen.setColor(new dojo.Color([0, 255, 0, 1]));
                        symbolDistrictFillHollow = new SimpleFillSymbol();
                        symbolDistrictFillHollow.setOutline(new SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new Color([0, 0, 0]), 1));
                        symbolDistrictFillHollow.setColor(new dojo.Color([255, 255, 255, 0]));
                        if (['QW01', 'BKN01'].indexOf(districtCode) >= 0) {
                            graphic.setSymbol(symbolDistrictFillDarkGreen);
                            districtLayer.add(graphic);
                        }
                        else if (['QW06', 'BKS07'].indexOf(districtCode) >= 0) {
                            graphic.setSymbol(symbolDistrictFillMediumGreen);
                            districtLayer.add(graphic);
                        }
                        else if (['QE13', 'BKS12', 'QW02', 'QE14', 'MN10', 'BX09', 'BX03', 'BX01', 'BKS06', 'BKN04', 'BKN02', 'BKN16', 'BKS14', 'BKS13', 'BKS10', 'QE07'].indexOf(districtCode) >= 0) {
                            graphic.setSymbol(symbolDistrictFillLightGreen);
                            districtLayer.add(graphic);
                        }
                        else {
                            graphic.setSymbol(symbolDistrictFillHollow);
                            districtLayer.add(graphic);
                        }
                    }
                }
            }, function (error) {
                $('#loading').hide();
                console.log(error);
            });
        }
        function selectionLayerClickHandler(evt) {
            highlightResultRowById(evt.graphic.attributes.BBL);
        }
        function addSelectionToMap(evt) {
            localStorage.setItem('ConsumerProfileSearchedDemographics', null);
            localStorage.setItem('ConsumerProfileSearchedSocial', null);
            localStorage.setItem('ConsumerProfileSearchedEconomic', null);
            localStorage.setItem('ConsumerProfileSearchedHousing', null);
            selectionLayer.clear();
            selectionToolbar.deactivate();
            map.setInfoWindowOnClick(true);
            resetAllSelectionTools();
            var queryTask = new QueryTask(CensusTractsUrl);
            var query = new Query();
            query.geometry = evt.geometry;
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
                    var spatialRef = new esri.SpatialReference({ wkid: 102718 });
                    var zoomExtent = new esri.geometry.Extent();
                    zoomExtent.spatialReference = spatialRef;
                    zoomExtent.xmin = esri.graphicsExtent(selectionLayer.graphics).xmin - 1000;
                    zoomExtent.ymin = esri.graphicsExtent(selectionLayer.graphics).ymin - 1000;
                    zoomExtent.xmax = esri.graphicsExtent(selectionLayer.graphics).xmax + 1000;
                    zoomExtent.ymax = esri.graphicsExtent(selectionLayer.graphics).ymax + 1000;
                    map.setExtent(zoomExtent);
                    $('#divResultButton').text('');
                    var htmlDataResultButton = "<button type='button' class='btn btn-primary btn-lg btn-block' onclick='btnTractResult()'>View Profile: " + resultFeatures.length + " Tracts</button>";
                    $('#divResultButton').append(htmlDataResultButton);
                }
                else {
                    $('#divResultButton').text('');
                }
            }, function (error) {
                console.log(error);
            });
        }
        function getAllTracts(ObjectID) {
            var queryTask = new QueryTask(CensusTractsUrl);
            var query = new Query();
            query.where = "OBJECTID=" + ObjectID;
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
                        updatePlutoTractIDs(graphic.geometry, graphic.attributes.BoroCT2010, ObjectID);
                    }
                }
                else {
                    getAllTracts(--ObjectID);
                }
            }, function (error) {
                console.log(error);
            });
        }
        function updatePlutoTractIDs(tractGeometry, tractID, ObjectID) {
            var queryTask = new QueryTask(MapPlutoUrl);
            var query = new Query();
            query.geometry = tractGeometry;
            query.returnGeometry = true;
            query.outFields = ["*"];
            queryTask.execute(query, function (featureSet) {
                //Performance enhancer - assign featureSet array to a single variable.
                var resultFeatures = featureSet.features;
                if (resultFeatures.length > 0) {
                    //Loop through each feature returned
                    var BBLs = "";
                    for (var i = 0, il = resultFeatures.length; i < il; i++) {
                        //Feature is a graphic
                        var graphic = resultFeatures[i];
                        if (BBLs == "") {
                            BBLs = graphic.attributes.BBL;
                        }
                        else {
                            BBLs += "," + graphic.attributes.BBL;
                            //if (BBLs.length > 100) {
                            //    updatePlutoDatatableTractIDs(BBLs, tractID);
                            //    BBLs = "";
                            //}
                        }
                    }
                    updatePlutoDatatableTractIDs(BBLs, tractID, ObjectID);
                }
                else {
                    getAllTracts(--ObjectID);
                }
            }, function (error) {
                console.log(error);
            });
        }
        function updatePlutoDatatableTractIDs(BBLs, tractID, OBJECTID) {
            $.ajax({
                url: RootUrl + 'Home/UpdatePlutoDatatableTractIDs',
                type: "POST",
                data: {
                    "BBLs": BBLs,
                    "tractID": tractID,
                    "OBJECTID": OBJECTID
                }
            }).done(function (data) {
                if (data > 0) {
                    getAllTracts(--data);
                }
            }).fail(function (error) {
                console.log(error);
            });
        }
        function jsWaitFunc(i) {
            if (i <= 0) return;
            setTimeout(function () {
                getAllTracts(i);
                jsWaitFunc(--i);
            }, 10000);
        }
    });
});