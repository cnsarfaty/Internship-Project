$("#submit").click(function () {
    var date = $("#date").val();
    var country = $("#country").val();
    if (country == "All countries") { country = "" }
    $.ajax({
        url: "/api/Location?date=" + date + "&country=" + country,
        method: "GET",
        contentType: "application/json",
        success: function (result) {
            $("#historyTable").html(`<tr>
                         <th>IP ADDRESS</th>
                         <th>CITY</th>
                         <th>REGION</th>
                         <th>COUNTRY</th>
                         </tr>`);
            if (result.length == 0) {
                $("#historyTable").html(`<p> No Results Found</p>`);
            } else {
                for (i = 0; i < result.length; i++) {
                    var symbol = result[i];
                    $("#historyTable").append(`<tr>
                             <td>${symbol.ip}</td>
                             <td>${symbol.city}</td>
                             <td>${symbol.region}</td>
                             <td>${symbol.country}</td>
                             </tr>`);
                }
            }
        }
    });
});