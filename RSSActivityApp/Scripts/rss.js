function GetRssData(numOfDays) {
    if (numOfDays == undefined || numOfDays == "")
    {
        numOfDays = "0";
    }

    $.ajax({
        type: "GET",
        url: "/RSS/GetRssFeeds",
        data: "numOfDays=" + numOfDays,
        success: function (result) {
            $("#feeds").html(result);
            $("#numOfDaysMsg").append(numOfDays);
        },
        error: function (e, x, error) {
            alert("Something went wrong...Please enter a valid integer.");
        }
    });  
}

function CopyToClipboard(link) {
    //window.clipboardData.setData("Text", link.href);
    var msg = window.prompt("Copy this link", location.href);
    //link.href.value.select();
    //link.href.execCommand("copy");
    alert("Copied the URL: " + copyText.href.value);
}