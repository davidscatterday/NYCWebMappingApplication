
function btnSearchHpd() {
    var bbl = document.getElementById("txtHpdAddresses").value;
    if (bbl != "") {
        window.open(RootUrl + "OwnerAnalysis/Preview?bbl=" + bbl, '_blank');
    }
    else {
        swal("Please select address first");
    }
}