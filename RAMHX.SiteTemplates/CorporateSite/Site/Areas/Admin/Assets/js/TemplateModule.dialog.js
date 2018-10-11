
$(document).ready(function () {
    $('#tblTemplateModules').DataTable();
    console.log("pageid", pageid);
    if (pageid != 'undefined' && pageid != null && pageid != "") {
        $('.thSelect').removeClass("hide");
        $('.tdChkBox').removeClass("hide");
        $('#savedetail').removeClass("hide");
        $('.btnDelete').addClass("hide");
    }
});

var pageid = GetParameterValues('pageid');

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

function GetCheckBoxListValue() {
    var selectedvalue = $('input:checkbox:checked').map(function () {
        return this.value;
    }).get();
    hmids = selectedvalue;

    return selectedvalue;
}


$(document).ready(function () {
    $('#savedetail').click(function () {
        var tmpltid = GetCheckBoxListValue();
        var pageid = GetParameterValues('pageid');

        var tmp = [];
        var tmids = tmp.concat(tmpltid);
        var tmpid = tmids.join();

        $.ajax({
            type: "GET",
            url: "/admin/Pages/SaveTemplateAndField",
            data: { "pageid": pageid, "tmpid": tmpid },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.parent.CloseDialogTemplateModule();
            },
            failure: function (response) {
                console.log('error', response)
            },
            complete: function () {
                $.unblockUI();
            }
        });
    });
});