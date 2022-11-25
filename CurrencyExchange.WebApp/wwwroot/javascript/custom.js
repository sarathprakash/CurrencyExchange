window.onload = function () {
    loadBaseCurency();
    loadTargetCurency();
    document.getElementById('fromDate').valueAsDate = new Date();
    document.getElementById('toDate').valueAsDate = new Date();

};

var baseurl = "https://localhost:7050/api";
function loadBaseCurency() {
    debugger;
    var httprequest = new XMLHttpRequest();
    var apiUrl = baseurl + "/rates/GetCurrencyCodes";
    httprequest.open("GET", apiUrl, true);
    httprequest.send();
    //httprequest.setRequestHeader('Authorization', 'Bearer ' + token);
    httprequest.onreadystatechange = function () {
        if (httprequest.readyState === 4 && httprequest.status === 200) {
            var rates = JSON.parse(httprequest.responseText);
            debugger;
            var select = document.getElementById("scode");
            rates.forEach(function (b) {
                select.innerHTML += '<option value="' + b.code + '">' + b.code + '</option>';
            })
        }
    };

}
function loadTargetCurency() {
    debugger;
    var xmlhttp = new XMLHttpRequest();
    var apiUrl = baseurl + "/rates/GetCurrencyCodes";
    xmlhttp.open("GET", apiUrl, true);
    xmlhttp.send();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
            var rates = JSON.parse(xmlhttp.responseText);
            debugger;
            var select = document.getElementById("tcode");
            rates.forEach(function (b) {
                select.innerHTML += '<option value="' + b.code + '">' + b.code + '</option>';
            })
        }
    };
}

//function loadExchangeRates() {
//    debugger;
//    var xmlhttp = new XMLHttpRequest();
//    var scode = document.getElementById('scode').value;
//    var amount = document.getElementById('amount').value;
//    var date = document.getElementById('date').value;

//    var fromdate = '2022-11-20';
//    var todate = '2022-11-22';

//    var tcode = "";
//    var select = document.getElementById("tcode");
//    for (var i = 0; i < select.length; i++) {
//        if (select[i].selected) {
//            tcode = tcode + select[i].value + ",";
//        }
//    }
//    tcode = tcode.slice(0, -1);
//    /*var apiUrl = baseurl + "/rates/GetCurrencyExchangeRate/" + scode + "/" + tcode + "/" + amount + "";*/
//    var apiUrl = baseurl + "/rates/GetCurrencyExchangeRateByPeriod/" + scode + "/"  + fromdate + "/" + todate + "";
//    if (date) {
//        apiUrl = apiUrl + "?date=" + date;
//    }
//    xmlhttp.open("GET", apiUrl, true);
//    xmlhttp.onreadystatechange = function () {
//        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
//            var rates = JSON.parse(xmlhttp.responseText);
//            var tbltop = "<table class='table table-bordered'> <thead class='thead-dark'><tr><th scope='col'>Currency</th><th scope='col'>ExchangeRate</th></tr></thead>";
//            //main table content we fill from data from the rest call
//            var main = "";
//            Object.entries(rates.exchangeRates).forEach(function ([curr, exRate]) {
//                main += "<tbody><tr><td>" + curr + "</td><td>" + 250 + "</td></tr> ";
//            });

//            var tblbottom = "</tbody></table>";
//            var tbl = tbltop + main + tblbottom;
//            document.getElementById("personinfo").innerHTML = tbl;
//        }
//    };
//    xmlhttp.send();


    
//}


   
