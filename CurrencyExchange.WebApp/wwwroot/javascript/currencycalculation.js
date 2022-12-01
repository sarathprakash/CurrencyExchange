window.onload = function () {
    loadBaseCurency();
    loadTargetCurency();
    document.getElementById('date').valueAsDate = new Date();
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
    var xmlhttp = new XMLHttpRequest();
    var apiUrl = baseurl + "/rates/GetCurrencyCodes";
    xmlhttp.open("GET", apiUrl, true);
    xmlhttp.send();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
            var rates = JSON.parse(xmlhttp.responseText);
            var select = document.getElementById("tcode");
            rates.forEach(function (b) {
                select.innerHTML += '<option value="' + b.code + '">' + b.code + '</option>';
            })
            debugger;
            select.selectedIndex = 2;
        }
    };
}

function loadExchangeRates() {
    debugger;
    var xmlhttp = new XMLHttpRequest();
    var scode = document.getElementById('scode').value;
    var tcode = document.getElementById('tcode').value;
    var amount = document.getElementById('amount').value;
    var date = document.getElementById('date').value;


    var apiUrl = baseurl + "/rates/GetLatestCurrencyExchangeRate/" + scode + "/" + tcode + "/" + amount + "";
    if (date) {
        apiUrl = apiUrl + "?date=" + date;
    }


    xmlhttp.open("GET", apiUrl, true);
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
            debugger;
            var rates = JSON.parse(xmlhttp.responseText);
            var elem = document.getElementById('divalert');
            if (rates.length == 0) {
                elem.style.display = 'inline'
            }
            else {
                elem.style.display = 'none'
                var tbltop = "<table class='table table-bordered'> <thead class='thead-dark'><tr><th scope='col'>Recorded On</th><th scope='col'>Exchange Rate</th></tr></thead>";
                //main table content we fill from data from the rest call
                var main = "";
                Object.entries(rates.exchangeRates).forEach(function ([currency, exchangeRate]) {
                    main += "<tbody><tr><td>" + currency + "</td><td>" + exchangeRate + "</td></tr> ";
                });
                var tblbottom = "</tbody></table>";
                var tbl = tbltop + main + tblbottom;
                document.getElementById("personinfo").innerHTML = tbl;
            }
        }
    };
    xmlhttp.send();
}
function loadResource() {
    var select = document.getElementById("slanguage").value;
    var httprequest = new XMLHttpRequest();
    var apiUrl = baseurl + "/rates/Localize?res=" + select + "";
    httprequest.open("GET", apiUrl, true);
    httprequest.send();
    httprequest.onreadystatechange = function () {
        if (httprequest.readyState === 4 && httprequest.status === 200) {
            var rates = JSON.parse(httprequest.responseText);
            rates.forEach(function (b) {
                if (b.name == 'lblbaseCurrency') {
                    document.getElementById('lblbaseCurrency').innerHTML = b.value;
                }
                if (b.name == 'lbltargetCurrency') {
                    document.getElementById('lbltargetCurrency').innerHTML = b.value;
                }
                if (b.name == 'lblAmount') {
                    document.getElementById('lblAmount').innerHTML = b.value;
                }
                if (b.name == 'lblDate') {
                    document.getElementById('lblDate').innerHTML = b.value;
                }
                if (b.name == 'btnSubmit') {
                    document.getElementById('btnSubmit').innerHTML = b.value;
                }
                if (b.name == 'TitleCalculation') {
                    document.getElementById('TitleCalculation').innerHTML = b.value;
                }
                if (b.name == 'legendCalculation') {
                    document.getElementById('legendCalculation').innerHTML = b.value;
                }
                if (b.name == 'legendGraph') {
                    document.getElementById('legendGraph').innerHTML = b.value;
                }
                if (b.name == 'TitleGraph') {
                    document.getElementById('TitleGraph').innerHTML = b.value;
                }
            })
        }
    };
}


   
