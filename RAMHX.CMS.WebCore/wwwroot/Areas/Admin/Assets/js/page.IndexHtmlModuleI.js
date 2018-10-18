var hmids = [];
var pid = GetParameterValues('pageid');

$(document).ready(function () {
    $('#tblHTMlModules').DataTable({ pageSize: 50 });
});



function GetCheckBoxListValue() {
    var selectedvalue = $('input:checkbox:checked').map(function () {
        return this.value;
    }).get();
    hmids = selectedvalue;

    return selectedvalue;
}

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

console.log("pageid", GetParameterValues('pageid'));
console.log("hmids", hmids);

$(document).ready(function () {
    $('#savedetail').click(function () {
        var hid = GetCheckBoxListValue();
        var pgid = GetParameterValues('pageid');

        var hms = [];
        var hmids = hms.concat(hid);
        var hmsid = hmids.join();
        console.log("pageid", pgid);
        console.log("hmids", hid);
        console.log("hmsid", hmsid);

        $.ajax({
            type: "GET",
            url: "/admin/HtmlModules/addNewModule",
            data: { "pageid": pgid, "hmids": hmsid },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.parent.CloseDialogHTMLModule();
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

