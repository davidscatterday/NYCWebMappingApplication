var maxDP05_0001E = 28272, maxDP05_0002E = 11964, maxDP05_0003E = 16308, maxDP05_0004E = 1249, maxDP05_0009E = 2420, maxDP05_0010E = 4432, maxDP05_0011E = 4041, maxDP05_0012E = 3346, maxDP05_0013E = 1776, maxDP05_0014E = 2156, maxDP05_0015E = 3970, maxDP05_0016E = 2701
    , maxDP02_0001E = 12827, maxDP02_0002E = 6960, maxDP02_0003E = 2088, maxDP02_0006E = 1315, maxDP02_0008E = 2921, maxDP02_0010E = 5867, maxDP02_0011E = 5824, maxDP02_0012E = 3700
    , maxDP02_0024E = 9688, maxDP02_0025E = 5822, maxDP02_0026E = 3517, maxDP02_0027E = 498, maxDP02_0028E = 946, maxDP02_0029E = 1014, maxDP02_0030E = 14730, maxDP02_0031E = 5849, maxDP02_0032E = 3266, maxDP02_0033E = 632, maxDP02_0034E = 2259, maxDP02_0035E = 2876;
$(function () {
    $("#slider-range-DP05_0001E").slider({
        range: true,
        min: 0,
        max: maxDP05_0001E,
        values: [0, maxDP05_0001E],
        slide: function (event, ui) {
            $("#txtDP05_0001E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0001E").val($("#slider-range-DP05_0001E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0001E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0002E").slider({
        range: true,
        min: 0,
        max: maxDP05_0002E,
        values: [0, maxDP05_0002E],
        slide: function (event, ui) {
            $("#txtDP05_0002E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0002E").val($("#slider-range-DP05_0002E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0002E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0003E").slider({
        range: true,
        min: 0,
        max: maxDP05_0003E,
        values: [0, maxDP05_0003E],
        slide: function (event, ui) {
            $("#txtDP05_0003E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0003E").val($("#slider-range-DP05_0003E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0003E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0004E").slider({
        range: true,
        min: 0,
        max: maxDP05_0004E,
        values: [0, maxDP05_0004E],
        slide: function (event, ui) {
            $("#txtDP05_0004E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0004E").val($("#slider-range-DP05_0004E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0004E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0009E").slider({
        range: true,
        min: 0,
        max: maxDP05_0009E,
        values: [0, maxDP05_0009E],
        slide: function (event, ui) {
            $("#txtDP05_0009E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0009E").val($("#slider-range-DP05_0009E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0009E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0010E").slider({
        range: true,
        min: 0,
        max: maxDP05_0010E,
        values: [0, maxDP05_0010E],
        slide: function (event, ui) {
            $("#txtDP05_0010E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0010E").val($("#slider-range-DP05_0010E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0010E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0011E").slider({
        range: true,
        min: 0,
        max: maxDP05_0011E,
        values: [0, maxDP05_0011E],
        slide: function (event, ui) {
            $("#txtDP05_0011E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0011E").val($("#slider-range-DP05_0011E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0011E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0012E").slider({
        range: true,
        min: 0,
        max: maxDP05_0012E,
        values: [0, maxDP05_0012E],
        slide: function (event, ui) {
            $("#txtDP05_0012E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0012E").val($("#slider-range-DP05_0012E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0012E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0013E").slider({
        range: true,
        min: 0,
        max: maxDP05_0013E,
        values: [0, maxDP05_0013E],
        slide: function (event, ui) {
            $("#txtDP05_0013E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0013E").val($("#slider-range-DP05_0013E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0013E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0014E").slider({
        range: true,
        min: 0,
        max: maxDP05_0014E,
        values: [0, maxDP05_0014E],
        slide: function (event, ui) {
            $("#txtDP05_0014E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0014E").val($("#slider-range-DP05_0014E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0014E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0015E").slider({
        range: true,
        min: 0,
        max: maxDP05_0015E,
        values: [0, maxDP05_0015E],
        slide: function (event, ui) {
            $("#txtDP05_0015E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0015E").val($("#slider-range-DP05_0015E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0015E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP05_0016E").slider({
        range: true,
        min: 0,
        max: maxDP05_0016E,
        values: [0, maxDP05_0016E],
        slide: function (event, ui) {
            $("#txtDP05_0016E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP05_0016E").val($("#slider-range-DP05_0016E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP05_0016E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0001E").slider({
        range: true,
        min: 0,
        max: maxDP02_0001E,
        values: [0, maxDP02_0001E],
        slide: function (event, ui) {
            $("#txtDP02_0001E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0001E").val($("#slider-range-DP02_0001E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0001E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0002E").slider({
        range: true,
        min: 0,
        max: maxDP02_0002E,
        values: [0, maxDP02_0002E],
        slide: function (event, ui) {
            $("#txtDP02_0002E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0002E").val($("#slider-range-DP02_0002E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0002E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0003E").slider({
        range: true,
        min: 0,
        max: maxDP02_0003E,
        values: [0, maxDP02_0003E],
        slide: function (event, ui) {
            $("#txtDP02_0003E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0003E").val($("#slider-range-DP02_0003E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0003E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0006E").slider({
        range: true,
        min: 0,
        max: maxDP02_0006E,
        values: [0, maxDP02_0006E],
        slide: function (event, ui) {
            $("#txtDP02_0006E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0006E").val($("#slider-range-DP02_0006E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0006E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0008E").slider({
        range: true,
        min: 0,
        max: maxDP02_0008E,
        values: [0, maxDP02_0008E],
        slide: function (event, ui) {
            $("#txtDP02_0008E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0008E").val($("#slider-range-DP02_0008E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0008E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0010E").slider({
        range: true,
        min: 0,
        max: maxDP02_0010E,
        values: [0, maxDP02_0010E],
        slide: function (event, ui) {
            $("#txtDP02_0010E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0010E").val($("#slider-range-DP02_0010E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0010E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0011E").slider({
        range: true,
        min: 0,
        max: maxDP02_0011E,
        values: [0, maxDP02_0011E],
        slide: function (event, ui) {
            $("#txtDP02_0011E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0011E").val($("#slider-range-DP02_0011E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0011E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0012E").slider({
        range: true,
        min: 0,
        max: maxDP02_0012E,
        values: [0, maxDP02_0012E],
        slide: function (event, ui) {
            $("#txtDP02_0012E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0012E").val($("#slider-range-DP02_0012E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0012E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0024E").slider({
        range: true,
        min: 0,
        max: maxDP02_0024E,
        values: [0, maxDP02_0024E],
        slide: function (event, ui) {
            $("#txtDP02_0024E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0024E").val($("#slider-range-DP02_0024E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0024E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0025E").slider({
        range: true,
        min: 0,
        max: maxDP02_0025E,
        values: [0, maxDP02_0025E],
        slide: function (event, ui) {
            $("#txtDP02_0025E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0025E").val($("#slider-range-DP02_0025E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0025E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0026E").slider({
        range: true,
        min: 0,
        max: maxDP02_0026E,
        values: [0, maxDP02_0026E],
        slide: function (event, ui) {
            $("#txtDP02_0026E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0026E").val($("#slider-range-DP02_0026E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0026E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0027E").slider({
        range: true,
        min: 0,
        max: maxDP02_0027E,
        values: [0, maxDP02_0027E],
        slide: function (event, ui) {
            $("#txtDP02_0027E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0027E").val($("#slider-range-DP02_0027E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0027E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0028E").slider({
        range: true,
        min: 0,
        max: maxDP02_0028E,
        values: [0, maxDP02_0028E],
        slide: function (event, ui) {
            $("#txtDP02_0028E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0028E").val($("#slider-range-DP02_0028E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0028E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0029E").slider({
        range: true,
        min: 0,
        max: maxDP02_0029E,
        values: [0, maxDP02_0029E],
        slide: function (event, ui) {
            $("#txtDP02_0029E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0029E").val($("#slider-range-DP02_0029E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0029E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0030E").slider({
        range: true,
        min: 0,
        max: maxDP02_0030E,
        values: [0, maxDP02_0030E],
        slide: function (event, ui) {
            $("#txtDP02_0030E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0030E").val($("#slider-range-DP02_0030E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0030E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0031E").slider({
        range: true,
        min: 0,
        max: maxDP02_0031E,
        values: [0, maxDP02_0031E],
        slide: function (event, ui) {
            $("#txtDP02_0031E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0031E").val($("#slider-range-DP02_0031E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0031E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0032E").slider({
        range: true,
        min: 0,
        max: maxDP02_0032E,
        values: [0, maxDP02_0032E],
        slide: function (event, ui) {
            $("#txtDP02_0032E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0032E").val($("#slider-range-DP02_0032E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0032E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0033E").slider({
        range: true,
        min: 0,
        max: maxDP02_0033E,
        values: [0, maxDP02_0033E],
        slide: function (event, ui) {
            $("#txtDP02_0033E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0033E").val($("#slider-range-DP02_0033E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0033E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0034E").slider({
        range: true,
        min: 0,
        max: maxDP02_0034E,
        values: [0, maxDP02_0034E],
        slide: function (event, ui) {
            $("#txtDP02_0034E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0034E").val($("#slider-range-DP02_0034E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0034E").slider("values", 1).toLocaleString('en'));

    $("#slider-range-DP02_0035E").slider({
        range: true,
        min: 0,
        max: maxDP02_0035E,
        values: [0, maxDP02_0035E],
        slide: function (event, ui) {
            $("#txtDP02_0035E").val(ui.values[0].toLocaleString('en') + " - " + ui.values[1].toLocaleString('en'));
        }
    });
    $("#txtDP02_0035E").val($("#slider-range-DP02_0035E").slider("values", 0).toLocaleString('en') +
        " - " + $("#slider-range-DP02_0035E").slider("values", 1).toLocaleString('en'));

});

function btnResetCP() {
    document.getElementById("cbBoroughCP").checked = false;
    document.getElementById("cbDistrictCP").checked = false;
    $("#txtBoroughsCP").select2("val", "");
    $("#txtDistrictsCP").select2("val", "");

    document.getElementById("cbDP05_0001E").checked = false;
    $("#slider-range-DP05_0001E").slider("values", 0, 0);
    $("#slider-range-DP05_0001E").slider("values", 1, maxDP05_0001E);
    $("#txtDP05_0001E").val($("#slider-range-DP05_0001E").slider("values", 0) + " - " + $("#slider-range-DP05_0001E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0002E").checked = false;
    $("#slider-range-DP05_0002E").slider("values", 0, 0);
    $("#slider-range-DP05_0002E").slider("values", 1, maxDP05_0002E);
    $("#txtDP05_0002E").val($("#slider-range-DP05_0002E").slider("values", 0) + " - " + $("#slider-range-DP05_0002E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0003E").checked = false;
    $("#slider-range-DP05_0003E").slider("values", 0, 0);
    $("#slider-range-DP05_0003E").slider("values", 1, maxDP05_0003E);
    $("#txtDP05_0003E").val($("#slider-range-DP05_0003E").slider("values", 0) + " - " + $("#slider-range-DP05_0003E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0004E").checked = false;
    $("#slider-range-DP05_0004E").slider("values", 0, 0);
    $("#slider-range-DP05_0004E").slider("values", 1, maxDP05_0004E);
    $("#txtDP05_0004E").val($("#slider-range-DP05_0004E").slider("values", 0) + " - " + $("#slider-range-DP05_0004E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0009E").checked = false;
    $("#slider-range-DP05_0009E").slider("values", 0, 0);
    $("#slider-range-DP05_0009E").slider("values", 1, maxDP05_0009E);
    $("#txtDP05_0009E").val($("#slider-range-DP05_0009E").slider("values", 0) + " - " + $("#slider-range-DP05_0009E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0010E").checked = false;
    $("#slider-range-DP05_0010E").slider("values", 0, 0);
    $("#slider-range-DP05_0010E").slider("values", 1, maxDP05_0010E);
    $("#txtDP05_0010E").val($("#slider-range-DP05_0010E").slider("values", 0) + " - " + $("#slider-range-DP05_0010E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0011E").checked = false;
    $("#slider-range-DP05_0011E").slider("values", 0, 0);
    $("#slider-range-DP05_0011E").slider("values", 1, maxDP05_0011E);
    $("#txtDP05_0011E").val($("#slider-range-DP05_0011E").slider("values", 0) + " - " + $("#slider-range-DP05_0011E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0012E").checked = false;
    $("#slider-range-DP05_0012E").slider("values", 0, 0);
    $("#slider-range-DP05_0012E").slider("values", 1, maxDP05_0012E);
    $("#txtDP05_0012E").val($("#slider-range-DP05_0012E").slider("values", 0) + " - " + $("#slider-range-DP05_0012E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0013E").checked = false;
    $("#slider-range-DP05_0013E").slider("values", 0, 0);
    $("#slider-range-DP05_0013E").slider("values", 1, maxDP05_0013E);
    $("#txtDP05_0013E").val($("#slider-range-DP05_0013E").slider("values", 0) + " - " + $("#slider-range-DP05_0013E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0014E").checked = false;
    $("#slider-range-DP05_0014E").slider("values", 0, 0);
    $("#slider-range-DP05_0014E").slider("values", 1, maxDP05_0014E);
    $("#txtDP05_0014E").val($("#slider-range-DP05_0014E").slider("values", 0) + " - " + $("#slider-range-DP05_0014E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0015E").checked = false;
    $("#slider-range-DP05_0015E").slider("values", 0, 0);
    $("#slider-range-DP05_0015E").slider("values", 1, maxDP05_0015E);
    $("#txtDP05_0015E").val($("#slider-range-DP05_0015E").slider("values", 0) + " - " + $("#slider-range-DP05_0015E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP05_0016E").checked = false;
    $("#slider-range-DP05_0016E").slider("values", 0, 0);
    $("#slider-range-DP05_0016E").slider("values", 1, maxDP05_0016E);
    $("#txtDP05_0016E").val($("#slider-range-DP05_0016E").slider("values", 0) + " - " + $("#slider-range-DP05_0016E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0001E").checked = false;
    $("#slider-range-DP02_0001E").slider("values", 0, 0);
    $("#slider-range-DP02_0001E").slider("values", 1, maxDP02_0001E);
    $("#txtDP02_0001E").val($("#slider-range-DP02_0001E").slider("values", 0) + " - " + $("#slider-range-DP02_0001E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0002E").checked = false;
    $("#slider-range-DP02_0002E").slider("values", 0, 0);
    $("#slider-range-DP02_0002E").slider("values", 1, maxDP02_0002E);
    $("#txtDP02_0002E").val($("#slider-range-DP02_0002E").slider("values", 0) + " - " + $("#slider-range-DP02_0002E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0003E").checked = false;
    $("#slider-range-DP02_0003E").slider("values", 0, 0);
    $("#slider-range-DP02_0003E").slider("values", 1, maxDP02_0003E);
    $("#txtDP02_0003E").val($("#slider-range-DP02_0003E").slider("values", 0) + " - " + $("#slider-range-DP02_0003E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0006E").checked = false;
    $("#slider-range-DP02_0006E").slider("values", 0, 0);
    $("#slider-range-DP02_0006E").slider("values", 1, maxDP02_0006E);
    $("#txtDP02_0006E").val($("#slider-range-DP02_0006E").slider("values", 0) + " - " + $("#slider-range-DP02_0006E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0008E").checked = false;
    $("#slider-range-DP02_0008E").slider("values", 0, 0);
    $("#slider-range-DP02_0008E").slider("values", 1, maxDP02_0008E);
    $("#txtDP02_0008E").val($("#slider-range-DP02_0008E").slider("values", 0) + " - " + $("#slider-range-DP02_0008E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0010E").checked = false;
    $("#slider-range-DP02_0010E").slider("values", 0, 0);
    $("#slider-range-DP02_0010E").slider("values", 1, maxDP02_0010E);
    $("#txtDP02_0010E").val($("#slider-range-DP02_0010E").slider("values", 0) + " - " + $("#slider-range-DP02_0010E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0011E").checked = false;
    $("#slider-range-DP02_0011E").slider("values", 0, 0);
    $("#slider-range-DP02_0011E").slider("values", 1, maxDP02_0011E);
    $("#txtDP02_0011E").val($("#slider-range-DP02_0011E").slider("values", 0) + " - " + $("#slider-range-DP02_0011E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0012E").checked = false;
    $("#slider-range-DP02_0012E").slider("values", 0, 0);
    $("#slider-range-DP02_0012E").slider("values", 1, maxDP02_0012E);
    $("#txtDP02_0012E").val($("#slider-range-DP02_0012E").slider("values", 0) + " - " + $("#slider-range-DP02_0012E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0024E").checked = false;
    $("#slider-range-DP02_0024E").slider("values", 0, 0);
    $("#slider-range-DP02_0024E").slider("values", 1, maxDP02_0024E);
    $("#txtDP02_0024E").val($("#slider-range-DP02_0024E").slider("values", 0) + " - " + $("#slider-range-DP02_0024E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0025E").checked = false;
    $("#slider-range-DP02_0025E").slider("values", 0, 0);
    $("#slider-range-DP02_0025E").slider("values", 1, maxDP02_0025E);
    $("#txtDP02_0025E").val($("#slider-range-DP02_0025E").slider("values", 0) + " - " + $("#slider-range-DP02_0025E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0026E").checked = false;
    $("#slider-range-DP02_0026E").slider("values", 0, 0);
    $("#slider-range-DP02_0026E").slider("values", 1, maxDP02_0026E);
    $("#txtDP02_0026E").val($("#slider-range-DP02_0026E").slider("values", 0) + " - " + $("#slider-range-DP02_0026E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0027E").checked = false;
    $("#slider-range-DP02_0027E").slider("values", 0, 0);
    $("#slider-range-DP02_0027E").slider("values", 1, maxDP02_0027E);
    $("#txtDP02_0027E").val($("#slider-range-DP02_0027E").slider("values", 0) + " - " + $("#slider-range-DP02_0027E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0028E").checked = false;
    $("#slider-range-DP02_0028E").slider("values", 0, 0);
    $("#slider-range-DP02_0028E").slider("values", 1, maxDP02_0028E);
    $("#txtDP02_0028E").val($("#slider-range-DP02_0028E").slider("values", 0) + " - " + $("#slider-range-DP02_0028E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0029E").checked = false;
    $("#slider-range-DP02_0029E").slider("values", 0, 0);
    $("#slider-range-DP02_0029E").slider("values", 1, maxDP02_0029E);
    $("#txtDP02_0029E").val($("#slider-range-DP02_0029E").slider("values", 0) + " - " + $("#slider-range-DP02_0029E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0030E").checked = false;
    $("#slider-range-DP02_0030E").slider("values", 0, 0);
    $("#slider-range-DP02_0030E").slider("values", 1, maxDP02_0030E);
    $("#txtDP02_0030E").val($("#slider-range-DP02_0030E").slider("values", 0) + " - " + $("#slider-range-DP02_0030E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0031E").checked = false;
    $("#slider-range-DP02_0031E").slider("values", 0, 0);
    $("#slider-range-DP02_0031E").slider("values", 1, maxDP02_0031E);
    $("#txtDP02_0031E").val($("#slider-range-DP02_0031E").slider("values", 0) + " - " + $("#slider-range-DP02_0031E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0032E").checked = false;
    $("#slider-range-DP02_0032E").slider("values", 0, 0);
    $("#slider-range-DP02_0032E").slider("values", 1, maxDP02_0032E);
    $("#txtDP02_0032E").val($("#slider-range-DP02_0032E").slider("values", 0) + " - " + $("#slider-range-DP02_0032E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0033E").checked = false;
    $("#slider-range-DP02_0033E").slider("values", 0, 0);
    $("#slider-range-DP02_0033E").slider("values", 1, maxDP02_0033E);
    $("#txtDP02_0033E").val($("#slider-range-DP02_0033E").slider("values", 0) + " - " + $("#slider-range-DP02_0033E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0034E").checked = false;
    $("#slider-range-DP02_0034E").slider("values", 0, 0);
    $("#slider-range-DP02_0034E").slider("values", 1, maxDP02_0034E);
    $("#txtDP02_0034E").val($("#slider-range-DP02_0034E").slider("values", 0) + " - " + $("#slider-range-DP02_0034E").slider("values", 1).toLocaleString('en'));

    document.getElementById("cbDP02_0035E").checked = false;
    $("#slider-range-DP02_0035E").slider("values", 0, 0);
    $("#slider-range-DP02_0035E").slider("values", 1, maxDP02_0035E);
    $("#txtDP02_0035E").val($("#slider-range-DP02_0035E").slider("values", 0) + " - " + $("#slider-range-DP02_0035E").slider("values", 1).toLocaleString('en'));

    $('.panel-collapse.in').collapse('toggle');

    if (document.getElementById("cbCensusTracts").checked == true) {
        document.getElementById("cbCensusTracts").checked = false;
        censusTractsFeatures.setVisibility(false);
    }
    $("#txtCensusTracts11Digit").select2("val", "");
    $('#divResultButton').text('');
    map.graphics.clear();
    selectionLayer.clear();
    districtLayer.clear();
    map.setExtent(initExtent);
}

function btnSearchCP() {
    $('#loading').show();
    var BoroughCP = null, DP05_0001EStart = null, DP05_0001EEnd = null, DP05_0002EStart = null, DP05_0002EEnd = null, DP05_0003EStart = null, DP05_0003EEnd = null, DP05_0004EStart = null, DP05_0004EEnd = null
        , DP05_0009EStart = null, DP05_0009EEnd = null, DP05_0010EStart = null, DP05_0010EEnd = null, DP05_0011EStart = null, DP05_0011EEnd = null, DP05_0012EStart = null, DP05_0012EEnd = null
        , DP05_0013EStart = null, DP05_0013EEnd = null, DP05_0014EStart = null, DP05_0014EEnd = null, DP05_0015EStart = null, DP05_0015EEnd = null, DP05_0016EStart = null, DP05_0016EEnd = null
        , DP02_0001EStart = null, DP02_0001EEnd = null, DP02_0002EStart = null, DP02_0002EEnd = null, DP02_0003EStart = null, DP02_0003EEnd = null, DP02_0006EStart = null, DP02_0006EEnd = null
        , DP02_0008EStart = null, DP02_0008EEnd = null, DP02_0010EStart = null, DP02_0010EEnd = null, DP02_0011EStart = null, DP02_0011EEnd = null, DP02_0012EStart = null, DP02_0012EEnd = null
        , DP02_0024EStart = null, DP02_0024EEnd = null, DP02_0025EStart = null, DP02_0025EEnd = null, DP02_0026EStart = null, DP02_0026EEnd = null, DP02_0027EStart = null, DP02_0027EEnd = null, DP02_0028EStart = null, DP02_0028EEnd = null, DP02_0029EStart = null, DP02_0029EEnd = null
        , DP02_0030EStart = null, DP02_0030EEnd = null, DP02_0031EStart = null, DP02_0031EEnd = null, DP02_0032EStart = null, DP02_0032EEnd = null, DP02_0033EStart = null, DP02_0033EEnd = null, DP02_0034EStart = null, DP02_0034EEnd = null, DP02_0035EStart = null, DP02_0035EEnd = null;
    var whereClauseCP = "";
    if (document.getElementById("cbBoroughCP").checked == true) {
        BoroughCP = $(txtBoroughsCP).val();
        if (BoroughCP != "") {
            if (whereClauseCP == "") {
                whereClauseCP = "Borough IN (" + BoroughCP + ")";
            }
            else {
                whereClauseCP += " AND Borough IN (" + BoroughCP + ")";
            }
        }
    }
    if (document.getElementById("cbDP05_0001E").checked == true) {
        DP05_0001EStart = $("#slider-range-DP05_0001E").slider("values", 0);
        DP05_0001EEnd = $("#slider-range-DP05_0001E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0001E >= " + DP05_0001EStart + " AND DP05_0001E <= " + DP05_0001EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0001E >= " + DP05_0001EStart + " AND DP05_0001E <= " + DP05_0001EEnd;
        }
    }
    if (document.getElementById("cbDP05_0002E").checked == true) {
        DP05_0002EStart = $("#slider-range-DP05_0002E").slider("values", 0);
        DP05_0002EEnd = $("#slider-range-DP05_0002E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0002E >= " + DP05_0002EStart + " AND DP05_0002E <= " + DP05_0002EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0002E >= " + DP05_0002EStart + " AND DP05_0002E <= " + DP05_0002EEnd;
        }
    }
    if (document.getElementById("cbDP05_0003E").checked == true) {
        DP05_0003EStart = $("#slider-range-DP05_0003E").slider("values", 0);
        DP05_0003EEnd = $("#slider-range-DP05_0003E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0003E >= " + DP05_0003EStart + " AND DP05_0003E <= " + DP05_0003EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0003E >= " + DP05_0003EStart + " AND DP05_0003E <= " + DP05_0003EEnd;
        }
    }
    if (document.getElementById("cbDP05_0004E").checked == true) {
        DP05_0004EStart = $("#slider-range-DP05_0004E").slider("values", 0);
        DP05_0004EEnd = $("#slider-range-DP05_0004E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0004E >= " + DP05_0004EStart + " AND DP05_0004E <= " + DP05_0004EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0004E >= " + DP05_0004EStart + " AND DP05_0004E <= " + DP05_0004EEnd;
        }
    }
    if (document.getElementById("cbDP05_0009E").checked == true) {
        DP05_0009EStart = $("#slider-range-DP05_0009E").slider("values", 0);
        DP05_0009EEnd = $("#slider-range-DP05_0009E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0009E >= " + DP05_0009EStart + " AND DP05_0009E <= " + DP05_0009EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0009E >= " + DP05_0009EStart + " AND DP05_0009E <= " + DP05_0009EEnd;
        }
    }
    if (document.getElementById("cbDP05_0010E").checked == true) {
        DP05_0010EStart = $("#slider-range-DP05_0010E").slider("values", 0);
        DP05_0010EEnd = $("#slider-range-DP05_0010E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0010E >= " + DP05_0010EStart + " AND DP05_0010E <= " + DP05_0010EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0010E >= " + DP05_0010EStart + " AND DP05_0010E <= " + DP05_0010EEnd;
        }
    }
    if (document.getElementById("cbDP05_0011E").checked == true) {
        DP05_0011EStart = $("#slider-range-DP05_0011E").slider("values", 0);
        DP05_0011EEnd = $("#slider-range-DP05_0011E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0011E >= " + DP05_0011EStart + " AND DP05_0011E <= " + DP05_0011EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0011E >= " + DP05_0011EStart + " AND DP05_0011E <= " + DP05_0011EEnd;
        }
    }
    if (document.getElementById("cbDP05_0012E").checked == true) {
        DP05_0012EStart = $("#slider-range-DP05_0012E").slider("values", 0);
        DP05_0012EEnd = $("#slider-range-DP05_0012E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0012E >= " + DP05_0012EStart + " AND DP05_0012E <= " + DP05_0012EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0012E >= " + DP05_0012EStart + " AND DP05_0012E <= " + DP05_0012EEnd;
        }
    }
    if (document.getElementById("cbDP05_0013E").checked == true) {
        DP05_0013EStart = $("#slider-range-DP05_0013E").slider("values", 0);
        DP05_0013EEnd = $("#slider-range-DP05_0013E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0013E >= " + DP05_0013EStart + " AND DP05_0013E <= " + DP05_0013EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0013E >= " + DP05_0013EStart + " AND DP05_0013E <= " + DP05_0013EEnd;
        }
    }
    if (document.getElementById("cbDP05_0014E").checked == true) {
        DP05_0014EStart = $("#slider-range-DP05_0014E").slider("values", 0);
        DP05_0014EEnd = $("#slider-range-DP05_0014E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0014E >= " + DP05_0014EStart + " AND DP05_0014E <= " + DP05_0014EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0014E >= " + DP05_0014EStart + " AND DP05_0014E <= " + DP05_0014EEnd;
        }
    }
    if (document.getElementById("cbDP05_0015E").checked == true) {
        DP05_0015EStart = $("#slider-range-DP05_0015E").slider("values", 0);
        DP05_0015EEnd = $("#slider-range-DP05_0015E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0015E >= " + DP05_0015EStart + " AND DP05_0015E <= " + DP05_0015EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0015E >= " + DP05_0015EStart + " AND DP05_0015E <= " + DP05_0015EEnd;
        }
    }
    if (document.getElementById("cbDP05_0016E").checked == true) {
        DP05_0016EStart = $("#slider-range-DP05_0016E").slider("values", 0);
        DP05_0016EEnd = $("#slider-range-DP05_0016E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP05_0016E >= " + DP05_0016EStart + " AND DP05_0016E <= " + DP05_0016EEnd;
        }
        else {
            whereClauseCP += " AND DP05_0016E >= " + DP05_0016EStart + " AND DP05_0016E <= " + DP05_0016EEnd;
        }
    }
    if (document.getElementById("cbDP02_0001E").checked == true) {
        DP02_0001EStart = $("#slider-range-DP02_0001E").slider("values", 0);
        DP02_0001EEnd = $("#slider-range-DP02_0001E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0001E >= " + DP02_0001EStart + " AND DP02_0001E <= " + DP02_0001EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0001E >= " + DP02_0001EStart + " AND DP02_0001E <= " + DP02_0001EEnd;
        }
    }
    if (document.getElementById("cbDP02_0002E").checked == true) {
        DP02_0002EStart = $("#slider-range-DP02_0002E").slider("values", 0);
        DP02_0002EEnd = $("#slider-range-DP02_0002E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0002E >= " + DP02_0002EStart + " AND DP02_0002E <= " + DP02_0002EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0002E >= " + DP02_0002EStart + " AND DP02_0002E <= " + DP02_0002EEnd;
        }
    }
    if (document.getElementById("cbDP02_0003E").checked == true) {
        DP02_0003EStart = $("#slider-range-DP02_0003E").slider("values", 0);
        DP02_0003EEnd = $("#slider-range-DP02_0003E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0003E >= " + DP02_0003EStart + " AND DP02_0003E <= " + DP02_0003EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0003E >= " + DP02_0003EStart + " AND DP02_0003E <= " + DP02_0003EEnd;
        }
    }
    if (document.getElementById("cbDP02_0006E").checked == true) {
        DP02_0006EStart = $("#slider-range-DP02_0006E").slider("values", 0);
        DP02_0006EEnd = $("#slider-range-DP02_0006E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0006E >= " + DP02_0006EStart + " AND DP02_0006E <= " + DP02_0006EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0006E >= " + DP02_0006EStart + " AND DP02_0006E <= " + DP02_0006EEnd;
        }
    }
    if (document.getElementById("cbDP02_0008E").checked == true) {
        DP02_0008EStart = $("#slider-range-DP02_0008E").slider("values", 0);
        DP02_0008EEnd = $("#slider-range-DP02_0008E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0008E >= " + DP02_0008EStart + " AND DP02_0008E <= " + DP02_0008EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0008E >= " + DP02_0008EStart + " AND DP02_0008E <= " + DP02_0008EEnd;
        }
    }
    if (document.getElementById("cbDP02_0010E").checked == true) {
        DP02_0010EStart = $("#slider-range-DP02_0010E").slider("values", 0);
        DP02_0010EEnd = $("#slider-range-DP02_0010E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0010E >= " + DP02_0010EStart + " AND DP02_0010E <= " + DP02_0010EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0010E >= " + DP02_0010EStart + " AND DP02_0010E <= " + DP02_0010EEnd;
        }
    }
    if (document.getElementById("cbDP02_0011E").checked == true) {
        DP02_0011EStart = $("#slider-range-DP02_0011E").slider("values", 0);
        DP02_0011EEnd = $("#slider-range-DP02_0011E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0011E >= " + DP02_0011EStart + " AND DP02_0011E <= " + DP02_0011EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0011E >= " + DP02_0011EStart + " AND DP02_0011E <= " + DP02_0011EEnd;
        }
    }
    if (document.getElementById("cbDP02_0012E").checked == true) {
        DP02_0012EStart = $("#slider-range-DP02_0012E").slider("values", 0);
        DP02_0012EEnd = $("#slider-range-DP02_0012E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0012E >= " + DP02_0012EStart + " AND DP02_0012E <= " + DP02_0012EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0012E >= " + DP02_0012EStart + " AND DP02_0012E <= " + DP02_0012EEnd;
        }
    }
    if (document.getElementById("cbDP02_0024E").checked == true) {
        DP02_0024EStart = $("#slider-range-DP02_0024E").slider("values", 0);
        DP02_0024EEnd = $("#slider-range-DP02_0024E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0024E >= " + DP02_0024EStart + " AND DP02_0024E <= " + DP02_0024EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0024E >= " + DP02_0024EStart + " AND DP02_0024E <= " + DP02_0024EEnd;
        }
    }
    if (document.getElementById("cbDP02_0025E").checked == true) {
        DP02_0025EStart = $("#slider-range-DP02_0025E").slider("values", 0);
        DP02_0025EEnd = $("#slider-range-DP02_0025E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0025E >= " + DP02_0025EStart + " AND DP02_0025E <= " + DP02_0025EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0025E >= " + DP02_0025EStart + " AND DP02_0025E <= " + DP02_0025EEnd;
        }
    }
    if (document.getElementById("cbDP02_0026E").checked == true) {
        DP02_0026EStart = $("#slider-range-DP02_0026E").slider("values", 0);
        DP02_0026EEnd = $("#slider-range-DP02_0026E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0026E >= " + DP02_0026EStart + " AND DP02_0026E <= " + DP02_0026EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0026E >= " + DP02_0026EStart + " AND DP02_0026E <= " + DP02_0026EEnd;
        }
    }
    if (document.getElementById("cbDP02_0027E").checked == true) {
        DP02_0027EStart = $("#slider-range-DP02_0027E").slider("values", 0);
        DP02_0027EEnd = $("#slider-range-DP02_0027E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0027E >= " + DP02_0027EStart + " AND DP02_0027E <= " + DP02_0027EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0027E >= " + DP02_0027EStart + " AND DP02_0027E <= " + DP02_0027EEnd;
        }
    }
    if (document.getElementById("cbDP02_0028E").checked == true) {
        DP02_0028EStart = $("#slider-range-DP02_0028E").slider("values", 0);
        DP02_0028EEnd = $("#slider-range-DP02_0028E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0028E >= " + DP02_0028EStart + " AND DP02_0028E <= " + DP02_0028EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0028E >= " + DP02_0028EStart + " AND DP02_0028E <= " + DP02_0028EEnd;
        }
    }
    if (document.getElementById("cbDP02_0029E").checked == true) {
        DP02_0029EStart = $("#slider-range-DP02_0029E").slider("values", 0);
        DP02_0029EEnd = $("#slider-range-DP02_0029E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0029E >= " + DP02_0029EStart + " AND DP02_0029E <= " + DP02_0029EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0029E >= " + DP02_0029EStart + " AND DP02_0029E <= " + DP02_0029EEnd;
        }
    }
    if (document.getElementById("cbDP02_0030E").checked == true) {
        DP02_0030EStart = $("#slider-range-DP02_0030E").slider("values", 0);
        DP02_0030EEnd = $("#slider-range-DP02_0030E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0030E >= " + DP02_0030EStart + " AND DP02_0030E <= " + DP02_0030EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0030E >= " + DP02_0030EStart + " AND DP02_0030E <= " + DP02_0030EEnd;
        }
    }
    if (document.getElementById("cbDP02_0031E").checked == true) {
        DP02_0031EStart = $("#slider-range-DP02_0031E").slider("values", 0);
        DP02_0031EEnd = $("#slider-range-DP02_0031E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0031E >= " + DP02_0031EStart + " AND DP02_0031E <= " + DP02_0031EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0031E >= " + DP02_0031EStart + " AND DP02_0031E <= " + DP02_0031EEnd;
        }
    }
    if (document.getElementById("cbDP02_0032E").checked == true) {
        DP02_0032EStart = $("#slider-range-DP02_0032E").slider("values", 0);
        DP02_0032EEnd = $("#slider-range-DP02_0032E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0032E >= " + DP02_0032EStart + " AND DP02_0032E <= " + DP02_0032EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0032E >= " + DP02_0032EStart + " AND DP02_0032E <= " + DP02_0032EEnd;
        }
    }
    if (document.getElementById("cbDP02_0033E").checked == true) {
        DP02_0033EStart = $("#slider-range-DP02_0033E").slider("values", 0);
        DP02_0033EEnd = $("#slider-range-DP02_0033E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0033E >= " + DP02_0033EStart + " AND DP02_0033E <= " + DP02_0033EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0033E >= " + DP02_0033EStart + " AND DP02_0033E <= " + DP02_0033EEnd;
        }
    }
    if (document.getElementById("cbDP02_0034E").checked == true) {
        DP02_0034EStart = $("#slider-range-DP02_0034E").slider("values", 0);
        DP02_0034EEnd = $("#slider-range-DP02_0034E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0034E >= " + DP02_0034EStart + " AND DP02_0034E <= " + DP02_0034EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0034E >= " + DP02_0034EStart + " AND DP02_0034E <= " + DP02_0034EEnd;
        }
    }
    if (document.getElementById("cbDP02_0035E").checked == true) {
        DP02_0035EStart = $("#slider-range-DP02_0035E").slider("values", 0);
        DP02_0035EEnd = $("#slider-range-DP02_0035E").slider("values", 1);
        if (whereClauseCP == "") {
            whereClauseCP = "DP02_0035E >= " + DP02_0035EStart + " AND DP02_0035E <= " + DP02_0035EEnd;
        }
        else {
            whereClauseCP += " AND DP02_0035E >= " + DP02_0035EStart + " AND DP02_0035E <= " + DP02_0035EEnd;
        }
    }

    var sqlQueryCP = "SELECT County,Tract FROM dbo.ConsumerProfiles WHERE " + whereClauseCP;
    $.ajax({
        url: RootUrl + 'Home/SearchConsumerProfilesDatabaseList',
        type: "POST",
        data: {
            "sqlQuery": sqlQueryCP
        }
    }).done(function (data) {
        selectionLayer.clear();
        var commaSeparatedBoroCT2010 = "", boroughCode = "";;
        for (var i = 0, il = data.length; i < il; i++) {
            switch (data[i].County) {
                case "005": boroughCode = "2"; break
                case "047": boroughCode = "3"; break
                case "061": boroughCode = "1"; break
                case "081": boroughCode = "4"; break
                case "085": boroughCode = "5"; break
                default: return
            }
            if (commaSeparatedBoroCT2010 == "") {
                commaSeparatedBoroCT2010 += boroughCode + data[i].Tract;
            }
            else {
                commaSeparatedBoroCT2010 += "," + boroughCode + data[i].Tract;
            }
        } var queryTask = new esri.tasks.QueryTask(CensusTractsUrl);
        var query = new esri.tasks.Query();
        query.where = "BoroCT2010 IN (" + commaSeparatedBoroCT2010 + ")";
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
                var htmlDataResultButton = "<button type='button' class='btn btn-primary btn-lg btn-block' onclick='btnTractResult()'>View Profile " + resultFeatures.length + " Tracts</button>";
                $('#divResultButton').append(htmlDataResultButton);
            }
            else {
                $('#divResultButton').text('');
            }
            $('#loading').hide();
        }, function (error) {
            $('#loading').hide();
            console.log(error);
        });
    }).fail(function (f) {
        $('#loading').hide();
        swal("Failed to search the query");
    });
}

function btnTractResult() {
    var commaSeparatedTracts = "", boroughCode = "";;
    for (var i = 0, il = selectionLayer.graphics.length; i < il; i++) {
        var graphic = selectionLayer.graphics[i];
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
    window.open(
        RootUrl + "ConsumerProfiles/Preview?tracts=" + commaSeparatedTracts,
        '_blank' // <- This is what makes it open in a new window.
    );
}

function txtCensusTracts11Digit_Change(evt) {
    selectionLayer.clear();
    var state = evt.value.substring(0, 3);
    var tract = evt.value.substring(3, 9);
    var BoroCT2010;
    switch (state) {
        case "005": BoroCT2010 = 2 + tract; break
        case "047": BoroCT2010 = 3 + tract; break
        case "061": BoroCT2010 = 1 + tract; break
        case "081": BoroCT2010 = 4 + tract; break
        case "085": BoroCT2010 = 5 + tract; break
        default: return
    }
    var queryTask = new esri.tasks.QueryTask(CensusTractsUrl);
    var query = new esri.tasks.Query();
    query.where = "BoroCT2010='" + BoroCT2010 + "'";
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
            var htmlDataResultButton = "<button type='button' class='btn btn-primary btn-lg btn-block' onclick='btnTractResult()'>View Profile " + resultFeatures.length + " Tracts</button>";
            $('#divResultButton').append(htmlDataResultButton);
        }
        else {
            $('#divResultButton').text('');
        }
    }, function (error) {
        console.log(error);
    });
}

function cbCensusTracts_Change(evt) {
    if (evt.checked) {
        censusTractsFeatures.setVisibility(true);
    }
    else {
        censusTractsFeatures.setVisibility(false);
    }
}

function activateSelectionTool(tool, bth) {
    resetAllSelectionTools();
    $(bth).removeClass('classSelectButtons');
    $(bth).addClass('classSelectButtonsActive');
    selectionToolbar.activate(esri.toolbars.Draw[tool.toUpperCase()]);
    map.setInfoWindowOnClick(false);
}

function resetAllSelectionTools() {
    document.getElementById("btnSelectByPoint").className = " btn btn-small classSelectButtons";
    document.getElementById("btnSelectByMultiPoint").className = " btn btn-small classSelectButtons";
    document.getElementById("btnSelectByPolyline").className = " btn btn-small classSelectButtons";
    document.getElementById("btnSelectByPolygon").className = " btn btn-small classSelectButtons";
    document.getElementById("btnSelectByFreehandPolyline").className = " btn btn-small classSelectButtons";
    document.getElementById("btnSelectByRectangle").className = " btn btn-small classSelectButtons";
}
