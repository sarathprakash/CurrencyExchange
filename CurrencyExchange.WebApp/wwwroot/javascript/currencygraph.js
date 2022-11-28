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
            select.selectedIndex = 2;
        }
    };
}

function loadExchangeRatesChart() {
    debugger;
    var xmlhttp = new XMLHttpRequest();
    var scode = document.getElementById('scode').value;
    var tcode = document.getElementById('tcode').value;
    var fromdate = document.getElementById('fromDate').value;
    var todate = document.getElementById('toDate').value;

    var apiUrl = baseurl + "/rates/GetCurrencyExchangeRateByPeriod/" + scode + "/" + tcode + "/" + fromdate + "/" + todate + "";

    xmlhttp.open("GET", apiUrl, true);
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
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
                var arrayRecordDate = [];
                var arrayExchangeRates = [];
                for (var i = 0; i < rates.length; i++) {
                    var exchange = rates[i];
                    var recordDate = exchange.recordedON;
                    recordDate = new Date(recordDate).toLocaleDateString('en-us', { month: "short", day: "numeric" });
                    main += "<tbody><tr><td>" + recordDate + "</td><td>" + exchange.exchangeRates + "</td></tr> ";
                    arrayRecordDate.unshift(recordDate);
                    arrayExchangeRates.unshift(exchange.exchangeRates);
                }

                const highest = Math.max.apply(Math, arrayExchangeRates);
                const lowest = Math.min.apply(Math, arrayExchangeRates);
                debugger;
                var tblbottom = "</tbody></table>";
                var tbl = tbltop + main + tblbottom;
                document.getElementById("tableinfo").innerHTML = tbl;
                const ctx = document.getElementById("currencyChart");
                new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: arrayRecordDate,
                        datasets: [{
                            label: 'Exchange Rates',
                            data: arrayExchangeRates,
                            borderWidth: 2
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                ticks: { color: 'red' }
                            },
                            x: {
                                title: {
                                    display: true,
                                    text: scode + "/" + tcode + " - High : " + highest + " " + "Low : " + lowest,
                                    color: 'Green'
                                },
                                ticks: { color: 'red'}
                            }
                        }
                    }
                });
            }
        }
    };
    xmlhttp.send();
}



   
