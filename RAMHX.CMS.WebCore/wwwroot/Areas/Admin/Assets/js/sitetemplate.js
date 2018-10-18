$(document).ready(function () {
    $(".install").on("click", function () {
        var stname = $(this).attr("data-template-name");

        var settings = {
            "async": true,
            "url": "/Admin/SiteTemplate/Install?stname=" + stname,
            "method": "GET",
        }

        $.ajax(settings).done(function (response) {
            toastr["success"]('Site template has been uploaded successfully!');
            setTimeout(function () {
                window.open("/");
            }, 1000);
        });
    });
});