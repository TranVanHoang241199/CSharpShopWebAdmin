function doSearch(page) {
    var url = $("#searchInput").prop("action");
    var input = $("#searchInput").serializeArray();
    input.push({
        "name": "page", "value": page
    });
    console.log(url);
    // gọi api để lấy dlieu
    $.ajax({
        type: "GET",
        url: url,
        data: input,
        success: function (data) {
            $("#searchResult").html(data);
        }
    });
}