// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.




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

$("#search").on('keyup', function (e) {
    if (e.key === 'Enter' || e.keyCode === 13) {
        var searchVal = $("#search").val();
        console.log("arama işlemi enter = ", searchVal);

        $.ajax({
            url: '/Home/MovieSearching',
            type: 'GET',
            dataSrc: "",
            data: {
                search: searchVal
            },
            success: function (data) {
                console.log("Geri dönen veri: ", data);


                $("#movieElements").empty();

                $("#movieElements").append(data);

            },
           
        });


    }
});