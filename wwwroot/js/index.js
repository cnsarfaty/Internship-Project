$("#submit").click(function () {
    $("#ipResult").html("");
    $("#city").html("");
    $("#region").html("");
    $("#country").html("");
    $("#error").html("");
    var ipAddress = $("#ipAddress").val();
    $.ajax({
        url: "/api/Location?ipAddress=" + ipAddress,
        method: "POST",
        contentType: "application/json",
        //data: JSON.stringify ({ ipAddress: ipAddress }),
        //data: ipAddress,
        success: function (result) {
            if (result == null)
                $("#error").html("Invalid IP Address. Please reenter.");
            else {
                $("#ipResult").html(result.ipAddress);
                $("#city").html(result.city);
                $("#region").html(result.region);
                $("#country").html(result.country);
                initMap(result);
                console.log(result);
            }
        }
    });
});

    var map;
    var marker;
    function initMap(symbol) {
        console.log(symbol);
        map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: symbol.latitude, lng: symbol.longitude },
            zoom: 8
        });
     marker = new google.maps.Marker({ position: { lat: symbol.latitude, lng: symbol.longitude }, map: map });
}
