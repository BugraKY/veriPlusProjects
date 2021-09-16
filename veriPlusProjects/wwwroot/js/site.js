var s=0;
setInterval(function () {
    $.ajax({
        url: '/Home/GetCurrencyExc',
        type: 'GET',
        dataType: 'json',
        success: function (curr) {
            console.log(curr);
            $("#usd").append(curr.usd);
            $("#eur").append(curr.eur);
            $("#gbp").append(curr.gbp);
        },
        error: function (e) {
            console.log(e);
        }
    });

}, 3600000);

$.ajax({
    url: '/Home/GetCurrencyExc',
    type: 'GET',
    dataType: 'json',
    success: function (curr) {

        $("#usd").empty();
        $("#eur").empty();
        $("#gbp").empty();

        $("#usd").append(curr.usd);
        $("#eur").append(curr.eur);
        $("#gbp").append(curr.gbp);
    },
    error: function (e) {
        console.log(e);
    }
});
