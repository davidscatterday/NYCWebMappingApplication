var selectionLayer, districtLayer;
require(["esri/map", "dojo/parser", "esri/layers/FeatureLayer", "esri/config", "esri/symbols/SimpleFillSymbol", "dojo/_base/array"
    , "esri/graphic", "esri/basemaps", "esri/InfoTemplate", "esri/layers/GraphicsLayer", "esri/tasks/QueryTask", "esri/tasks/query"
], function (Map, parser, FeatureLayer, esriConfig, SimpleFillSymbol, arrayUtils, Graphic, esriBasemaps, InfoTemplate, GraphicsLayer, QueryTask, Query) {
    parser.parse();
    esriConfig.defaults.io.proxyUrl = "/proxy/proxy.ashx";
    selectionLayer = new GraphicsLayer();
    districtLayer = new GraphicsLayer();

    //create symbol polygon for districts features
    symbolDistrictFill = new SimpleFillSymbol();
    symbolDistrictFill.setColor(new dojo.Color([51, 255, 204, 0]));
    //create symbol polygon for selected features
    symbolFill = new SimpleFillSymbol();
    symbolFill.setColor(new dojo.Color([51, 255, 204, 0.5]));
    //create symbol polygon for zoomed features
    symbolZoomedFill = new SimpleFillSymbol();
    symbolZoomedFill.setColor(new dojo.Color([158, 188, 218, 0.5]));

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
        outFields: ["*"],
        infoTemplate: new InfoTemplate("Tax Lot Info", "${*}")
    });
    //Takes a URL to a non cached map service.
    districtFeatures = new FeatureLayer(DistrictsUrl, {
        visible: true,
        mode: FeatureLayer.MODE_ONDEMAND,
        outFields: ["*"]
    });
    esriBasemaps.NYCbasemap = {
        baseMapLayers: [{ url: "http://www.nycdot.info:6080/arcgis/rest/services/GISAPP_GAZETTEER/NYCDOTBaseMapPale_17A/MapServer" }
        ],
        title: "NYCbasemap"
    };

    map = new Map("mapDiv", {
        basemap: "NYCbasemap", // dark-gray, gray, hybrid, national-geographic, oceans, osm, satellite, streets, terrain, topo
        extent: initExtent
    });

    map.addLayers([serviceFeatures, districtLayer, selectionLayer]);
});