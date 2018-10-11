$(document).ready(function () {
    //$('#tblPages').DataTable();
    //$('#tblContent').DataTable();
    //$('#tblTemplates').DataTable();
    $('#lnkDownloadPkg').hide();

    $('#btnExport').click(function () {

        $.blockUI();

        var pages = [];
        var content = [];
        var templates = [];

        $($('#tblPages').find('input[type="checkbox"]:not(.selectAll):checked').each(function () {
            pages.push($(this).val());
        }))

        $($('#tblContent').find('input[type="checkbox"]:not(.selectAll):checked').each(function () {
            content.push($(this).val());
        }));

        $($('#tblTemplates').find('input[type="checkbox"]:not(.selectAll):checked').each(function () {
            templates.push($(this).val());
        }));

        // console.log("/Admin/Packages/ExportXML?contents=" + content.join() + "&pages=" + pages.join() + "&templates=" + templates.join());
        //  window.location = "/Admin/Packages/ExportXML?contents=" + content.join() + "&pages=" + pages.join() + "&templates=" + templates.join();

        var data = { pages: pages.join(), contents: content.join(), templates: templates.join() };

        // console.log(JSON.stringify(data));

        $.ajax({
            type: "POST",
            url: "/Admin/Packages/ExportXML",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (response) {
                console.log('success', response);
                toastr["success"]('Package has been Created successfully! Click on "Download Package" to download it.');
                //bootbox.alert('Package has been Created successfully! Click on "Download Package" to download it.');
                $('#lnkDownloadPkg').show();
                $('#lnkDownloadPkg').attr('href', response);
                $.unblockUI();
            },
            failure: function (response) {
                console.log('Error', response)
                $.unblockUI();
            }
        });

        
    });

    $('#btnPublish').click(function () {
        //var datastring = $("#frmPackageInstall").serialize();
        var form = new FormData();
        form.append("apikey", $('#apikey').val());
        form.append("currentusername", $('#currentusername').val());
        form.append("package", $('#package')[0].files[0]);

        var settings = {
            "async": true,
            "crossDomain": true,
            //"url": "/api/RamhxApi",
            "url": "Packages/InstallRequest",
            "method": "POST",
            "processData": false,
            "contentType": false,
            "mimeType": "multipart/form-data",
            "data": form
        }

        $.ajax(settings).done(function (response) {
            toastr["success"]('Package has been uploaded successfully!');
            setTimeout(function () {
                window.location = "/Admin/Packages?status=&t=3"
            }, 1000);
        });

    });

    $('.selectAll').click(function () {
        var isSelectedOrNot = $(this).is(':checked')
        $(this).parents('table').find('input[type="checkbox"]').each(function () {
            $(this).prop("checked", isSelectedOrNot);
        });
    });


    if (getUrlParameter('t') == '3') {
        $('a[href="#tabStatus"]').click();
        $('#StatusID').val(getUrlParameter('status'));
    }

    if (getUrlParameter('t') == '2') {
        $('a[href="#tabInstall"]').click();
    }

    //alert($('#pt').val());
    $('#frmPackageInstall').attr('action', $('#pt').val() + $('#frmPackageInstall').attr('action'));
});


//File Manager
function FileMngrDialog() {
    $("#dialogFileMngr").dialog({
        modal: true,
        width: 1200,
        height: 700,
        title: "Media Library",
        visible: true,
        open: function (ev, ui) {
            $('#iFrmFileManager').attr('src', '/FileBrowser/FileBrowser.aspx?fn=setpath');
        },
        close: function (ev, ui) {
        }
    });
}

function setpath(url) {
    $('#selectPackage').val(url);
    var splitedUrl = url.split('.');

    if (splitedUrl.length > 1 && (splitedUrl[splitedUrl.length - 1]).toLowerCase() == "zip") {
        $("#dialogFileMngr").dialog("close");
    }
    else {
        alert("Please select .zip File");
        $('#selectPackage').val('');
    }

}

function FilterStatus() {
    window.location = "/Admin/Packages?status=" + $("#StatusID").val() + "&t=3";
}

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function deletePackage(hyp) {
    if (confirm("Are you sure what to delete this package?")) {
        var request = $.ajax({
            url: "/Admin/Packages/Delete?Id=" + $(hyp).attr("PackageId"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            success: function () {
                window.location = window.location;
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR);
                console.log(exception);
            }
        });
    }
}