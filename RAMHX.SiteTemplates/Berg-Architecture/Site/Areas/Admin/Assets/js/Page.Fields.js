$(document).ready(function () {
    $(".datepicker").datepicker();
    $(function () {
        $("#accordion").accordion({
            collapsible: true,
            heightStyle: "content"
        });
    });
    $('.timepicker').timepicki();
    $('.openrichtextbox').click(function () {
        $("#richtextbox").dialog({
            modal: true,
            width: 800,
            height: 500,
            title: title,
            visible: true,
            open: function (ev, ui) {
                //  $('#iFrmFileManager').attr('src', '/FileBrowser/FileBrowser.aspx?fn=setpath');
            },
            close: function (ev, ui) {
            }
        });
    });
    $(".numericbox").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: Ctrl+C
            (e.keyCode == 67 && e.ctrlKey === true) ||
            // Allow: Ctrl+X
            (e.keyCode == 88 && e.ctrlKey === true) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});

//File Manager
function FileMngrDialog(title) {
    $("#dialogFileMngr").dialog({
        modal: true,
        width: 1200,
        height: 700,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmFileManager').attr('src', '/FileBrowser/FileBrowser.aspx?fn=setpath');
        },
        close: function (ev, ui) {
        }
    });
}
function setpath(url) {
    $('.PageLayoutPath').val(url);
    $("#dialogFileMngr").dialog("close");
}

$('#btnSaveData').click(function () {
    $.each($('textarea'), function () {
        if ($(this).attr("name").indexOf("richtxt") > -1) {
            $(this).html(($($('#cke_' + $(this).attr('id'))[0]).find('iframe')[0]).contentDocument.body.innerHTML);
        }
        //$(this).html($(($($('#cke_' + $(this).attr('id'))[0]).find('iframe')[0]).contentDocument.body).find('pre')[0].innerHTML);
        // $(this).html(($($('#cke_' + $(this).attr('id'))[0]).find('iframe')[0]).contentDocument.body.innerHTML);
    });

    var form = $("#formTemplateData").serialize();
    $.ajax({
        type: 'POST',
        url: "/Pages/SaveFieldData",
        data: form,
        dataType: 'json',
        success: function (data) {
            if (data.result == "Error") {
                alert(data.message);
            }
            window.parent.CloseDialogPageTemplate();
        },
        error: function (err) {
            console.log("Error while saving temlpate data", err);
        }
    });
});

$('#btnCancel').click(function () {
    window.parent.CloseDialogPageTemplate();
});