// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function UpdateData()
{
    setInterval(function ()
    {
        GetData();
    }, 30000);
}

function CallAsyncRequest(callback, link) {
    $.ajax({
        type: "GET",
        url: link,
        dataType: "JSON",
        success: function (data) {
            callback(data);
        },
        error: function (status) {
            console.log(status);
        }
    });
}

function UpdateTable(i, data) {
    $("#side-" + i).text(data.side);
    $("#ticker-name-" + i).text(data.tickerName);
    $("#base-name-" + i).text(data.baseName);
    $("#quote-name-" + i).text(data.quoteName);
    $("#amount-label-" + i).text("Current" + data.baseName + "amount: ");
    $("#coin-amount-" + i).text(data.balance.free);
    $("#amount-for-order-" + i).text(data.amountForOrder);
    $("#estimate-label-" + i).text("Estimate" + data.quoteName + "amount: ");
    $("#estimate-amount-" + i).text(data.estimateAmount);
    $("#previous-label-" + i).text("Previous Amount" + data.quoteName + "amount: ");
    $("#previous-amount-" + i).text(data.historyAsset.amount);
    if (data.profit > 0) {
        $("#profit-container-" + i).addClass("table-success").removeClass("table-active");
    }
    else {
        $("#profit-container-" + i).addClass("table-danger").removeClass("table-active");
    }
    $("#profit-label-" + i).text("Profit" + data.quoteName + "amount: ");
    $("#profit-" + i).text(data.profit);
    $("#market-price-" + i).text(data.tickerAmount);
    $("#base-kline-label-" + i).text(data.baseName + "/USDT Change: ");
    $("#base-kline-change-" + i).text(data.bKline);
    $("#quote-kline-label-" + i).text(data.quoteName + "/USDT Change: ");
    $("#quote-kline-change-" + i).text(data.qKline);
    $("#kline-range-" + i).text(data.range);
}

function RemoveProp(i) {
    $("#table-" + i).removeAttr('style');
}
function AddProp(i) {
    var style = { "position": "relative", "animation": "animateright 1.2s" };
    $("#table-" + i).css(style);
}

function GetData() {
    CallAsyncRequest(function (result) {
        var counter = 0;
        var name = "";
        var max_value = 0;
        $.each(result, function (i, obj) {
            CallAsyncRequest(function (data) {
                if (data.profit >= max_value) {
                    max_value = data.profit;
                    name = obj.replace('"', '');
                };
                RemoveProp(i);
                UpdateTable(i, data);
                setTimeout(function () {
                    AddProp(i);
                }, 0.1);
                counter += 1;
                if (result.length - 1 == counter) {
                    CreateRequest(name);
                }
            }, "https://localhost:52142/DashBoard/GetJsonModel?id=" + obj.replace('"', ''));
        });
    }, "https://localhost:52142/DashBoard/GetJsonTickers");
}

function CreateRequest(name) {
    CallAsyncRequest(function (response) {
        DeleteDiv();
        $('#request').addClass("alert alert-dismissible alert-danger");
        $('#request').append('<div class="time-r">Time: ' + response.transactTime + '</div>');
        $('#request').append('<div class="filter-r">Filter: ' + response.filter + '</div>');
        $('#request').append('<div class="profit-r">Profit: ' + response.profit + '</div>');
        $('#request').append('<div class="range-r">Range: ' + response.range + '</div>');
    }, "https://localhost:52142/DashBoard/CreateRequest?name=" + name);
}

function DeleteDiv() {
    $(".time-r").remove();
    $(".filter-r").remove();
    $(".profit-r").remove();
    $(".range-r").remove();
}