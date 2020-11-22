var selectionLayer, districtLayer, geometryService, selectionSymbolPoint, selectionSymbolLine, selectionSymbolFill, heatmapLayer;
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
    geometryService = new GeometryService(GeometryServiceUrl);

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

    //xmin   //ymin    //xmax    //ymax
    //initExtent = new esri.geometry.Extent(-8276983.607858135, 4935156.731231112, -8169207.3979761815, 5005402.3602250945,
    //        new esri.SpatialReference({ wkid: 102100 }));
    //xmin   //ymin    //xmax    //ymax
    initExtent = new esri.geometry.Extent(913128.926351264, 120048.986001998, 1067335.95126992, 272811.183429763,
        new esri.SpatialReference({ wkid: 102718 }));

    //Takes a URL to a non cached map service.
    serviceFeatures = new FeatureLayer(MapPlutoUrl, {
        visible: true,
        mode: FeatureLayer.MODE_ONDEMAND,
        outFields: ["*"]
        //infoTemplate: new InfoTemplate("Tax Lot Info", "${*}")
    });
    //Takes a URL to a non cached map service.
    districtFeatures = new FeatureLayer(DistrictsUrl, {
        visible: true,
        mode: FeatureLayer.MODE_ONDEMAND,
        outFields: ["*"]
    });
    //Takes a URL to a non cached map service.
    censusTractsFeatures = new FeatureLayer(CensusTractsUrl, {
        visible: true,
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
    });

    //// create a text symbol to define the style of labels
    //var statesLabel = new TextSymbol().setColor(new Color("#666"));
    //statesLabel.font.setSize("14pt");
    //statesLabel.font.setFamily("arial");

    ////this is the very least of what should be set within the JSON  
    //var json = {
    //    "labelExpressionInfo": { "value": "{ZIPCODE}" }
    //};

    ////create instance of LabelClass (note: multiple LabelClasses can be passed in as an array)
    //var labelClass = new LabelClass(json);
    //labelClass.symbol = statesLabel; // symbol also can be set in LabelClass' json
    //zipCodeFeatures.setLabelingInfo([labelClass]);

    // create a text symbol to define the style of labels
    var myLabel = new TextSymbol().setColor(new Color("#000000"));
    myLabel.font.setSize("10pt");
    myLabel.font.setFamily("helvetica");

    myLabelRenderer = new SimpleRenderer(myLabel);
    myLabelLayer = new LabelLayer({ id: "zipCodeLabels", visible: false });
    myLabelLayer.addFeatureLayer(zipCodeFeatures, myLabelRenderer, "{ZIPCODE}");

    map.addLayers([baseMapLayer, serviceFeatures, districtLayer, censusTractsFeatures, zipCodeFeatures, myLabelLayer, selectionLayer, heatmapLayer]);

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
});