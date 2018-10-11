$(document).ready(function () {
    var pid = GetParameterValues('pageid');
    var uid = GetParameterValues('userid');
    $('#tblPageRoles').DataTable();
    console.log("pid", pid);
    console.log("pid", uid);
    if (pid != 'undefined' && pid != null && pid != "" || uid != 'undefined' && uid != null && uid != "") {
        $('.thSelect').removeClass("hide");
        $('.tdChkBox').removeClass("hide");
        $('#savedetail').removeClass("hide");
        $('.thaction').addClass("hide");
        $('.tdaction').addClass("hide");

        SetPageUrlWithParams('#lnkcreatepage', "/admin/Roles/Create");
        SetAssignedRole(uid);
    }
});
var hmids = [];
var pid = GetParameterValues('pageid');
var pagetype = GetParameterValues('pagetype');
var usertype = GetParameterValues('usertype');
var uid = GetParameterValues('userid');
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
        var pagetype = GetParameterValues('pagetype');
        console.log("type", pagetype);
        if (pagetype === 'page') {
            var hms = [];
            var hmids = hms.concat(hid);
            var hmsid = hmids.join();
            console.log("pageid", pgid);
            console.log("hmids", hid);
            console.log("hmsid", hmsid);

            $.ajax({
                type: "GET",
                url: "/admin/Roles/AssignedRoles",
                data: { "pageid": pgid, "hmids": hmsid },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    window.parent.CloseDialogPageRole();
                },
                failure: function (response) {
                    console.log('error', response)
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        }
        else {
            var hid = GetCheckBoxListValue();
            var usid = GetParameterValues('userid');
            var hms = [];
            var hmids = hms.concat(hid);
            var hmsid = hmids.join();
            console.log("userid", usid);
            console.log("hmids", hid);
            console.log("hmsid", hmsid);

            $.ajax({
                type: "GET",
                url: "/admin/Roles/AssignedUserRoles",
                data: { "userid": usid, "hmids": hmsid },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    window.parent.CloseDialogUserRole();
                },
                failure: function (response) {
                    console.log('error', response)
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        }
    });
});

function SetAssignedRole(userid) {
    $.ajax({
        type: "GET",
        url: "/account/GetAssingedRoles?userid=" + userid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var reult = $('input:checkbox:not(:checked)').val();
            console.log("reult",reult);
            $("input[type='checkbox'].ckbox").each(function (index) {
                var id = $(this).attr('id');
                for (var i = 0; i < response.length; i++) {
                    if (response[i] == id) {
                        $('#'+id).prop('checked', true);
                    }
                }
            });
        },
        failure: function (response) {
            console.log(response)
            alert('Oops, Error occured while getting page data. Please try again.');
        }
    });
}