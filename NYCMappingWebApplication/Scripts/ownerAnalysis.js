function btnSearchHpd() {
    var bbl = document.getElementById("txtHpdAddresses").value;
    if (bbl != "") {
        window.open(RootUrl + "OwnerAnalysis/Preview?bbl=" + bbl, '_blank');
    }
    else {
        swal("Please select address first");
    }
}

function txtHpdAddresses_Change() {
    selectionLayer.clear();
    var bbl = document.getElementById("txtHpdAddresses").value;
    if (bbl != "") {
        $('#loading').show();
        $.ajax({
            url: RootUrl + 'OwnerAnalysis/GetHpdRegistrationsByBBL',
            type: "POST",
            data: {
                "bbl": bbl
            }
        }).done(function (data) {
            var commaSeparatedBBLs = "";
            for (var i = 0; i < data.length; i++) {
                if (commaSeparatedBBLs == "") {
                    commaSeparatedBBLs += data[i].bbl;
                }
                else {
                    commaSeparatedBBLs += "," + data[i].bbl;
                }
            }
            SelectBBLsOnMap(commaSeparatedBBLs, bbl);
            $('#loading').hide();
        }).fail(function (f) {
            $('#loading').hide();
            swal("Failed to search GetHpdRegistrationsByBBL");
        });
    }
}

function SelectBBLsOnMap(BBLs, originalBBl) {
    queryTask = new esri.tasks.QueryTask(MapPlutoUrl);

    //initialize query
    query = new esri.tasks.Query();
    query.returnGeometry = true;
    query.outFields = ["*"];

    query.where = "BBL IN (" + BBLs + ")";

    //execute query
    queryTask.execute(query, function executeMapPlutoSearch(featureSet) {
        var features = [];
        var resultFeatures = featureSet.features;
        //Loop through each feature returned
        for (var i = 0, il = resultFeatures.length; i < il; i++) {
            var graphic = resultFeatures[i];
            //create circle inside polygon
            var pt = graphic.geometry.getCentroid();
            var myColor = new dojo.Color([255, 51, 0, 0.5]);
            if (graphic.attributes.BBL == originalBBl) {
                myColor = new dojo.Color([51, 51, 255, 0.5]);
            }
            var sms = new esri.symbol.SimpleMarkerSymbol().setStyle(esri.symbol.SimpleMarkerSymbol.STYLE_CIRCLE).setColor(myColor);
            var attr = { "Latitude": graphic.attributes.Latitude, "Longitude": graphic.attributes.Longitude, "BBL": graphic.attributes.BBL };
            var infoTemplate = new esri.InfoTemplate(graphic.attributes.Address, "<b>Latitude:</b> ${Latitude} <br/><b>Longitude:</b> ${Longitude} <br/><b>BBL:</b> ${BBL} <br/><a href=" + RootUrl + "OwnerAnalysis/Preview?bbl=" + graphic.attributes.BBL + " target='_blank'>Search</a>");
            var myGraphic = new esri.Graphic(pt, sms, attr, infoTemplate);
            selectionLayer.add(myGraphic);
            features.push(graphic);
            var spatialRef = new esri.SpatialReference({ wkid: 102718 });
            var zoomExtent = new esri.geometry.Extent();
            zoomExtent.spatialReference = spatialRef;
            zoomExtent.xmin = esri.graphicsExtent(features).xmin - 10000;
            zoomExtent.ymin = esri.graphicsExtent(features).ymin - 10000;
            zoomExtent.xmax = esri.graphicsExtent(features).xmax + 10000;
            zoomExtent.ymax = esri.graphicsExtent(features).ymax + 10000;
            map.setExtent(zoomExtent);
            $('#loading').hide();
        }
    }, function (error) {
        $('#loading').hide();
        console.log(error);
    });
}
