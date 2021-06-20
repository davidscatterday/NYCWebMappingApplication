var maxDistanceCDL = 1000, currentGeometryNum = 0, currentBufferNum = 0;
var onPremiseLiquorLicenseBBLs = "1000070027 ,1000110001 ,1000167516 ,1000200012 ,1000210004 ,1000220013 ,1000230019 ,1000250027 ,1000257502 ,1000290024 ,1000420001 ,1000477501 ,1000500001 ,1000527502 ,1000530003 ,1000530039 ,1000627501 ,1000630020 ,1000640012 ,1000650016 ,1000680011 ,1000680028 ,1000690018 ,1000757502 ,1000790015 ,1000800001 ,1000880018 ,1000930025 ,1000970030 ,1000970032 ,1000977503 ,1001070026 ,1001237501 ,1001330014 ,1001347502 ,1001360019 ,1001360027 ,1001400029 ,1001410002 ,1001410019 ,1001430014 ,1001447503 ,1001460023 ,1001470009 ,1001507503 ,1001520025 ,1001620008 ,1001620030 ,1001630033 ,1001640006 ,1001640047 ,1001730031 ,1001740037 ,1001750015 ,1001780007 ,1001870016 ,1001877501 ,1001930017 ,1001950007 ,1001960003 ,1001990005 ,1001990006 ,1002020023 ,1002020025 ,1002020028 ,1002050011 ,1002050019 ,1002100002 ,1002110016 ,1002240029 ,1002280024 ,1002310037 ,1002360021 ,1002360024 ,1002360025 ,1002370013 ,1002390034 ,1002777502 ,1002800037 ,1002880043 ,1002970036 ,1002970037 ,1003000003 ,1003040005 ,1003040006 ,1003040019 ,1003480075 ,1003540038 ,1003540108 ,1003740005 ,1003760004 ,1003760005 ,1003760007 ,1003780033 ,1003850004 ,1003850005 ,1003860005 ,1003890001 ,1003900036 ,1003910038 ,1003920007 ,1003920032 ,1003920035 ,1003920037 ,1003940001 ,1003960003 ,1003970006 ,1003980004 ,1003980029 ,1003990036 ,1003990037 ,1004010041 ,1004020034 ,1004020041 ,1004040028 ,1004040031 ,1004050007 ,1004050008 ,1004050033 ,1004050034 ,1004050035 ,1004060035 ,1004070005 ,1004070032 ,1004070034 ,1004080009 ,1004100051 ,1004110070 ,1004120002 ,1004177501 ,1004180058 ,1004230029 ,1004237501 ,1004250021 ,1004260002 ,1004260015 ,1004260033 ,1004290033 ,1004290041 ,1004290042 ,1004320034 ,1004327501 ,1004340033 ,1004340036 ,1004347501 ,1004350034 ,1004350037 ,1004360029 ,1004360034 ,1004360036 ,1004360050 ,1004380027 ,1004380029 ,1004380030 ,1004400035 ,1004410033 ,1004410035 ,1004500033 ,1004500036 ,1004570008 ,1004580001 ,1004587502 ,1004590005 ,1004630015 ,1004630027 ,1004640045 ,1004640047 ,1004640055 ,1004640057 ,1004640152 ,1004700050 ,1004700061 ,1004710016 ,1004710028 ,1004710029 ,1004740020 ,1004780032 ,1004790019 ,1004790021 ,1004800023 ,1004820025 ,1004820027 ,1004820031 ,1004880015 ,1004890017 ,1004917503 ,1004920004 ,1004920025 ,1004960001 ,1004960018 ,1004960036 ,1004970015 ,1004980009 ,1004990020 ,1005020041 ,1005030001 ,1005030023 ,1005060047 ,1005090021 ,1005110006 ,1005170009 ,1005180001 ,1005180036 ,1005210063 ,1005210077 ,1005217503 ,1005227502 ,1005230046 ,1005250035 ,1005250059 ,1005257504 ,1005270069 ,1005290043 ,1005300012 ,1005300022 ,1005300065 ,1005300132 ,1005307506 ,1005370022 ,1005370034 ,1005370035 ,1005370036 ,1005390012 ,1005440045 ,1005450039 ,1005470014 ,1005480040 ,1005520053 ,1005537501 ,1005620028 ,1005690025 ,1005790002 ,1005880006 ,1005900053 ,1005910060 ,1005920040 ,1005940034 ,1005950072 ,1005970032 ,1005970055 ,1006060021 ,1006060029 ,1006100027 ,1006120065 ,1006127504 ,1006190038 ,1006197501 ,1006240021 ,1006250009 ,1006260024 ,1006290001 ,1006300001 ,1006340060 ,1006340063 ,1006357502 ,1008110021 ,1008120022 ,1008120049 ,1008130055 ,1008140015 ,1008150014 ,1008150049 ,1008270049 ,1008280033 ,1008300024 ,1008300054 ,1008310019 ,1008330078 ,1008427502 ,1008460023 ,1008470016 ,1008480061 ,1008487502 ,1008490068 ,1008500012 ,1008770018 ,1008830071 ,1008950001 ,1008950019 ,1008950020 ,1009930011 ,1009930029 ,1010117501 ,1010140033 ,1010160036 ,1010177501 ,1010180128 ,1010200046 ,1010210043 ,1010220026 ,1010240046 ,1010250043 ,1010250044 ,1010267501 ,1010270020 ,1010300001 ,1010300058 ,1010307501 ,1011137502 ,1011147503 ,1011397501 ,1011400032 ,1011410013 ,1011460103 ,1011680029 ,1012130061 ,1012140003 ,1012280034 ,1012297503 ,1012310031 ,1012320034 ,1012380046 ,1012390034 ,1012420040 ,1012420042 ,1012420055 ,1012430035 ,1012940046 ,1013000001 ,1013020021 ,1013110052 ,1013407501 ,1013800058 ,1013900017 ,1013930056 ,1013970018 ,1013990020 ,1013990122 ,1014050021 ,1014050120 ,1014090052 ,1014620005 ,1014710024 ,1014920017 ,1014960017 ,1015040055 ,1015100041 ,1015130156 ,1015640029 ,1015830001 ,1016370021 ,1016380022 ,1018220024 ,1018610004 ,1018610062 ,1018620062 ,1018690013 ,1018720010 ,1018720019 ,1018720054 ,1018760012 ,1018770052 ,1018790061 ,1018940011 ,1018940049 ,1018940050 ,1019770036 ,1019930076 ,1019930079 ,1019930082 ,1019930086 ,1019930088 ,1020020033 ,1020780061 ,1020800003 ,1020800059 ,1020910032 ,1020960034 ,1020980036 ,1021210056 ,1021220069 ,1021270046 ,1021370001 ,1021370141 ,1021420224 ,1021450012 ,1021640038 ,1021700066 ,1021750045 ,1021800507 ,1022290005 ,1022320018 ,1022350005 ,1022360001 ,1022370052 ,1022420055 ,1022430241 ,2023010028 ,2023570035 ,2024430400 ,2024760061 ,2024840009 ,2025060104 ,2027170070 ,2028530001 ,2029500058 ,2030060013 ,2030650065 ,2030650070 ,2030660028 ,2030660047 ,2030730032 ,2030740011 ,2030770043 ,2031950092 ,2031990098 ,2032670084 ,2032690026 ,2032710189 ,2033110024 ,2033200005 ,2033210001 ,2033770059 ,2037640032 ,2037640033 ,2039480001 ,2040810021 ,2040940036 ,2041080004 ,2041700026 ,2042350034 ,2042630015 ,2042870005 ,2046550054 ,2046680054 ,2047430008 ,2052870001 ,2054730039 ,2054790098 ,2055290425 ,2056240130 ,2056250037 ,2056330137 ,2056340047 ,2056350001 ,2056360163 ,2056380011 ,2056440172 ,2057000095 ,2057040042 ,2057650728 ,2058021301 ,2058141204 ,2058481761 ,2058552257 ,2058630109 ,2058881542 ,2058881556 ,3000350011 ,3000357501 ,3000400001 ,3000410001 ,3001450032 ,3001810021 ,3001830018 ,3001870027 ,3001930020 ,3001950032 ,3002160033 ,3002370041 ,3002420014 ,3002440013 ,3002490040 ,3002680050 ,3002750017 ,3002750021 ,3002850001 ,3002970029 ,3003070030 ,3003120031 ,3003300010 ,3003360037 ,3003740033 ,3003790036 ,3003790038 ,3003900004 ,3003900043 ,3003910007 ,3003960007 ,3004020039 ,3004020044 ,3004210043 ,3004300078 ,3004350009 ,3004360001 ,3004450011 ,3004470050 ,3004580001 ,3004700001 ,3005560003 ,3005870010 ,3005980007 ,3008760051 ,3009330041 ,3009560045 ,3011090038 ,3011100002 ,3011100009 ,3011340073 ,3011380001 ,3011430005 ,3011450004 ,3011500014 ,3011510008 ,3011570006 ,3011640011 ,3011660051 ,3011680060 ,3011720040 ,3011740001 ,3012240011 ,3012520010 ,3012570035 ,3012960047 ,3014780003 ,3014790017 ,3014790021 ,3014860030 ,3014860034 ,3014860035 ,3014990009 ,3015820014 ,3015820021 ,3015820023 ,3015840021 ,3015840022 ,3015900012 ,3015900017 ,3016000007 ,3016080027 ,3016230044 ,3017320024 ,3017680010 ,3018060035 ,3018260038 ,3018350001 ,3018870093 ,3018910077 ,3018990067 ,3019260077 ,3019670041 ,3019760027 ,3019820043 ,3019840010 ,3019890039 ,3019910011 ,3020140017 ,3020590001 ,3020920066 ,3021040029 ,3021050031 ,3021050033 ,3021110011 ,3021130035 ,3021150010 ,3021310018 ,3022280027 ,3022970013 ,3023190022 ,3023200019 ,3023380021 ,3023460028 ,3023460029 ,3023580018 ,3023680022 ,3023840017 ,3023870020 ,3023930013 ,3023930023 ,3023950011 ,3023960001 ,3024060005 ,3024300022 ,3024570039 ,3024570043 ,3024570044 ,3024600025 ,3024620022 ,3024620024 ,3024630038 ,3024680027 ,3024690001 ,3024700020 ,3024707501 ,3024830001 ,3024830002 ,3025210028 ,3025220007 ,3025220008 ,3025580074 ,3025630044 ,3025660046 ,3025890016 ,3026200030 ,3026450028 ,3026490037 ,3026510003 ,3026800024 ,3027310025 ,3027350003 ,3027410013 ,3027550004 ,3027560003 ,3027560029 ,3027600007 ,3027610035 ,3027720018 ,3027830042 ,3027860014 ,3027880012 ,3027880017 ,3029260015 ,3030120029 ,3030790025 ,3031020012 ,3031360001 ,3031500002 ,3031770014 ,3031940004 ,3032090033 ,3032140047 ,3032190029 ,3032220005 ,3032270030 ,3032290043 ,3032290044 ,3032410076 ,3032830009 ,3032910004 ,3033180005 ,3033280001 ,3033370033 ,3033800003 ,3033800016 ,3034080005 ,3034380006 ,3037200038 ,3041260001 ,3041520029 ,3041980014 ,3044520540 ,3045420003 ,3046140010 ,3047220064 ,3048690025 ,3050220048 ,3051100007 ,3051260044 ,3051400021 ,3051710059 ,3051860031 ,3052160001 ,3053310001 ,3053700006 ,3053780045 ,3054290030 ,3055720038 ,3055800041 ,3057620040 ,3058590164 ,3063590036 ,3065970003 ,3066470003 ,3066640020 ,3067450009 ,3067460002 ,3067750039 ,3067920037 ,3070220003 ,3070220044 ,3070920001 ,3070960045 ,3070980042 ,3071020044 ,3071210005 ,3071220013 ,3071230048 ,3071320004 ,3071340002 ,3071350007 ,3071730042 ,3072820062 ,3073170040 ,3073240048 ,3073280044 ,3073620006 ,3073770054 ,3074050232 ,3074220302 ,3075767501 ,3077030006 ,3077480041 ,3077480044 ,3077630080 ,3078830006 ,3079160047 ,3079730009 ,3084150006 ,3084700050 ,3085010007 ,3086850004 ,3086850008 ,3086880070 ,3087720104 ,3087750041 ,3087780052 ,3087930063 ,3088370025 ,3089460024 ,4005800029 ,4005880001 ,4005880103 ,4006110040 ,4006140005 ,4006210033 ,4006210035 ,4006220041 ,4006220049 ,4006220147 ,4006230004 ,4006230005 ,4006230008 ,4006237501 ,4006480001 ,4006480003 ,4006480007 ,4006480102 ,4006570003 ,4006580008 ,4006780001 ,4006780002 ,4006840006 ,4006900039 ,4007240004 ,4012900037 ,4013010028 ,4014830045 ,4014860051 ,4014930035 ,4015800010 ,4015800103 ,4015800105 ,4019270001 ,4030820061 ,4032530062 ,4034370042 ,4034390012 ,4034610053 ,4034670013 ,4035670003 ,4040760022 ,4040830022 ,4040830032 ,4040850057 ,4041280019 ,4049410037 ,4049620004 ,4050650026 ,4050650027 ,4055800001 ,4056200001 ,4059660070 ,4060740034 ,4060740035 ,4076100008 ,4080310001 ,4089050010 ,4101500048 ,4105390011 ,4161090037 ,4161230099 ,4161340019 ,4161660005 ,4161660050 ,4161740023 ,5000220120 ,5000690001 ,5001690026 ,5001780087 ,5079050104 ,5080280063";
var selectedOnPremiseLiquorLicenseBBLs = [];
var censusTractVariables = ["DP05_0001E", "DP05_0002E", "DP05_0003E", "DP05_0004E", "DP05_0009E", "DP05_0010E", "DP05_0011E", "DP05_0012E", "DP05_0013E", "DP05_0014E", "DP05_0015E", "DP05_0016E"
    , "DP02_0001E", "DP02_0002E", "DP02_0003E", "DP02_0006E", "DP02_0008E", "DP02_0010E", "DP02_0011E", "DP02_0012E", "DP02_0024E", "DP02_0025E", "DP02_0026E", "DP02_0027E", "DP02_0028E", "DP02_0029E", "DP02_0030E", "DP02_0031E", "DP02_0032E", "DP02_0033E", "DP02_0034E", "DP02_0035E", "DP02_0052E", "DP02_0053E", "DP02_0054E", "DP02_0055E", "DP02_0056E", "DP02_0057E", "DP02_0058E", "DP02_0059E", "DP02_0060E", "DP02_0061E", "DP02_0062E", "DP02_0063E", "DP02_0064E", "DP02_0070E", "DP02_0071E", "DP02_0074E", "DP02_0075E", "DP02_0076E", "DP02_0078E", "DP02_0079E", "DP02_0080E", "DP02_0081E", "DP02_0082E", "DP02_0083E", "DP02_0084E", "DP02_0150E", "DP02_0151E", "DP02_0152E"
    , "DP03_0001E", "DP03_0003E", "DP03_0004E", "DP03_0005E", "DP03_0007E", "DP03_0010E", "DP03_0012E", "DP03_0013E", "DP03_0051E", "DP03_0052E", "DP03_0053E", "DP03_0054E", "DP03_0055E", "DP03_0056E", "DP03_0057E", "DP03_0058E", "DP03_0059E", "DP03_0060E", "DP03_0061E", "DP03_0063E", "DP03_0066E", "DP03_0068E", "DP03_0069E", "DP03_0070E", "DP03_0071E", "DP03_0072E", "DP03_0073E", "DP03_0095E", "DP03_0096E", "DP03_0097E", "DP03_0098E", "DP03_0099E", "DP03_0102E", "DP03_0103E", "DP03_0104E", "DP03_0105E", "DP03_0106E", "DP03_0107E", "DP03_0108E", "DP03_0109E", "DP03_0110E", "DP03_0111E", "DP03_0112E", "DP03_0113E"
    , "DP04_0006E", "DP04_0007E", "DP04_0008E", "DP04_0009E", "DP04_0010E", "DP04_0011E", "DP04_0012E", "DP04_0013E", "DP04_0027E", "DP04_0028E", "DP04_0029E", "DP04_0030E", "DP04_0031E", "DP04_0032E", "DP04_0033E", "DP04_0034E", "DP04_0035E", "DP04_0036E", "DP04_0037E", "DP04_0038E", "DP04_0039E", "DP04_0040E", "DP04_0041E", "DP04_0042E", "DP04_0043E", "DP04_0044E", "DP04_0045E", "DP04_0046E", "DP04_0047E", "DP04_0050E", "DP04_0051E", "DP04_0052E", "DP04_0053E", "DP04_0054E", "DP04_0055E", "DP04_0056E", "DP04_0076E", "DP04_0077E", "DP04_0078E", "DP04_0079E", "DP04_0090E", "DP04_0091E", "DP04_0092E", "DP04_0093E", "DP04_0094E", "DP04_0095E", "DP04_0096E", "DP04_0097E", "DP04_0098E", "DP04_0099E", "DP04_0100E", "DP04_0101E", "DP04_0110E", "DP04_0111E", "DP04_0112E", "DP04_0113E", "DP04_0114E", "DP04_0115E", "DP04_0126E", "DP04_0127E", "DP04_0128E", "DP04_0129E", "DP04_0130E", "DP04_0131E", "DP04_0132E", "DP04_0133E", "DP04_0134E", "DP04_0136E", "DP04_0137E", "DP04_0138E", "DP04_0139E", "DP04_0140E", "DP04_0141E", "DP04_0142E"];
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
    handleonPremiseLLs = $("#onPremiseLLs-handle"), handleWidth = handle.width();
    $("#slider-DistanceCDLonPremiseLLs").slider({
        min: 0,
        max: maxDistanceCDL,
        value: 100,
        create: function () {
            handleonPremiseLLs.text($(this).slider("value"));
        },
        slide: function (event, ui) {
            handleonPremiseLLs.text(ui.value);
            handleonPremiseLLs.css({
                'margin-left': 0 - ((ui.value / maxDistanceCDL) * handleWidth)
            });
        }
    });
});
function btnResetCDL() {
    resetAllCensusTractToolsCDL();
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
    $("#slider-DistanceCDLonPremiseLLs").slider("value", 100);
    handleonPremiseLLs.text(100);
    document.getElementById("cbDistanceCDLonPremiseLLs").checked = false;
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
    resetAllCensusTractToolsCDL();
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
                if (BldgClassEC != "" || document.getElementById("cbDistanceCDLonPremiseLLs").checked == true) {
                    searchExclusionCriteria(BldgClassEC);
                }
                else {
                    CallCreateTable();
                    $('#loading').hide();
                }
                CallTractSearch();
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
    var whereBuffer = "";
    if (BldgClassEC != "") {
        whereBuffer = "BldgClass in (" + BldgClassEC + ")";
    }
    if (document.getElementById("cbDistanceCDLonPremiseLLs").checked == true && selectedOnPremiseLiquorLicenseBBLs.length > 0) {
        if (whereBuffer == "") {
            whereBuffer = "BBL in (" + selectedOnPremiseLiquorLicenseBBLs.join(", ") + ")";
        }
        else {
            whereBuffer += " OR BBL in (" + selectedOnPremiseLiquorLicenseBBLs.join(", ") + ")";
        }
    }
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
        query.where = whereBuffer;
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
                    doBuffer(graphic.geometry, resultFeatures.length, resultFeatures[i].attributes["BBL"]);
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

function doBuffer(myGeometry, totalGeometries, BBL) {
    var myDistance = "";
    if (selectedOnPremiseLiquorLicenseBBLs.includes(BBL)) {
        myDistance = $("#slider-DistanceCDLonPremiseLLs").slider("value");
    }
    else {
        myDistance = $("#slider-DistanceCDL").slider("value");
    }
    var params = new esri.tasks.BufferParameters();
    params.geometries = [myGeometry];

    params.distances = [myDistance];
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

function CallTractSearch() {
    selectionLayerCensusTracts.clear();
    var drawnGraphics = selectionLayerCDL.graphics;
    var queryTask = new esri.tasks.QueryTask(CensusTractsUrl);
    var query = new esri.tasks.Query();
    query.geometry = drawnGraphics[0].geometry;
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
                selectionLayerCensusTracts.add(graphic);
            }
            $('#divCensusTractResultButton').text('');
            var htmlDataResultButton = "<button type='button' class='btn btn-primary btn-lg btn-block' onclick='btnTractResultCDL()'>View Profile: " + resultFeatures.length + " Tracts</button>";
            $('#divCensusTractResultButton').append(htmlDataResultButton);
            $('#accordionCDLcensus').show();
        }
        else {
            $('#divCensusTractResultButton').text('');
            $('#accordionCDLcensus').hide();
        }
    }, function (error) {
        console.log("An error occurred from CallTractSearch()." + error);
    });
}

function btnTractResultCDL() {
    var commaSeparatedTracts = "", boroughCode = "";;
    for (var i = 0, il = selectionLayerCensusTracts.graphics.length; i < il; i++) {
        var graphic = selectionLayerCensusTracts.graphics[i];
        switch (graphic.attributes.BoroName) {
            case "Bronx": boroughCode = "005"; break
            case "Brooklyn": boroughCode = "047"; break
            case "Manhattan": boroughCode = "061"; break
            case "Queens": boroughCode = "081"; break
            case "Staten Island": boroughCode = "085"; break
            default: return
        }
        if (commaSeparatedTracts == "") {
            commaSeparatedTracts += graphic.attributes.CT2010 + ":" + boroughCode;
        }
        else {
            commaSeparatedTracts += "," + graphic.attributes.CT2010 + ":" + boroughCode;
        }
    }
    var variablesList = "&variables=";
    for (var i = 0; i < censusTractVariables.length; i++) {
        var checkBoxName = "cb" + censusTractVariables[i] + "_CDL";
        if (document.getElementById(checkBoxName).checked) {
            variablesList += censusTractVariables[i] + ",";
        }
    }
    if (variablesList != "&variables=") {
        variablesList = variablesList.slice(0, -1);
    }
    window.open(
        RootUrl + "ConsumerProfiles/Preview?tracts=" + commaSeparatedTracts + variablesList,
        '_blank' // <- This is what makes it open in a new window.
    );
}

function activateSelectionToolCDL(tool, bth) {
    map.setInfoWindowOnClick(false);
    resetAllSelectionToolsCDL();
    $(bth).removeClass('classSelectButtons');
    $(bth).addClass('classSelectButtonsActive');
    selectionToolbarCDL.activate(esri.toolbars.Draw[tool.toUpperCase()]);
}

function addSelectionCDLToMap(evt) {
    var geometry = evt.geometry;
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
    var queryTask = new esri.tasks.QueryTask(MapPlutoUrl);
    var query = new esri.tasks.Query();
    query.geometry = graphic.geometry;
    query.where = "BBL in (" + onPremiseLiquorLicenseBBLs + ")";
    query.returnGeometry = true;
    query.outFields = ["*"];
    selectedOnPremiseLiquorLicenseBBLs = [];
    queryTask.execute(query, function (featureSet) {
        //Performance enhancer - assign featureSet array to a single variable.
        var resultFeatures = featureSet.features;
        if (resultFeatures.length > 0) {
            for (var i = 0, il = resultFeatures.length; i < il; i++) {
                //Feature is a graphic
                var graphic = resultFeatures[i];
                selectedOnPremiseLiquorLicenseBBLs.push(graphic.attributes["BBL"]);
            }
        }
        else {
            //$('#loading').hide();
        }
    }, function (error) {
        //$('#loading').hide();
        console.log("An error occurred from btnSearchCDL()." + error);
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

function resetAllCensusTractToolsCDL() {
    selectionLayerCensusTracts.clear();
    $('#accordionCDLcensus').hide();
    $('#divCensusTractResultButton').text('');
    document.getElementById("cbCensusTractSelections").checked = true;
    for (var i = 0; i < censusTractVariables.length; i++) {
        var checkBoxName = "cb" + censusTractVariables[i] + "_CDL";
        document.getElementById(checkBoxName).checked = false;
    }
}

function percentage(partialValue, totalValue) {
    return ((100 * partialValue) / totalValue).toFixed(0);
} 