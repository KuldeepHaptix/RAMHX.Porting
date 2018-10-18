var APITemplate = "/api/cms_Templates";

$(document).ready(function () {
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, height: 420, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    loadAll(BindData, APITemplate)
})


$("#btnAddNewTemplate").click(function () {
    editrow = -1;
    var offset = $("#Templatejqxgrid").offset();
    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

    // get the clicked row's data and initialize the input fields.
    $("#txtTempName").val("");
    $("#txtTempCode").val("");
    $("#txtTempDescritpion").val("");

    // show the popup window.
    $("#popupWindow").jqxWindow('open');
});

function BindData(response) {
    console.log(response);
    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'TemplateId', type: 'string' },
                { name: 'TemplateName', type: 'string' },
                { name: 'TemplateCode', type: 'string' },
                { name: 'Description', type: 'string' },
            ],
            id: 'TemplateId'
        };

    source.localdata = response;
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#Templatejqxgrid").jqxGrid(
        {
            theme: jqxTheme,
            width: '100%',
            source: dataAdapter,
            columnsresize: false,
            altrows: true,
            columns: [
                { text: 'Template#', datafield: 'TemplateId', width: "300px" },
                { text: 'Name', datafield: 'TemplateName', width: "200px" },
                { text: 'Code', datafield: 'TemplateCode', width: "200px" },
                { text: 'Description', datafield: 'Description', width: "200px", },
                {
                    text: '', datafield: 'Edit', width: "75px", columntype: 'button', cellsrenderer: function () {
                        return "Edit";
                    },
                    buttonclick: function (row) {
                        if (!$(".errorDiv").hasClass('hide'))
                            $(".errorDiv").addClass('hide');
                        $('#perror').html('');
                        editrow = row;
                        var offset = $("#Templatejqxgrid").offset();
                        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
                        var dataRecord = $("#Templatejqxgrid").jqxGrid('getrowdata', editrow);
                        console.log("dataRecord", dataRecord);

                        $("#txtTempName").val(dataRecord.TemplateName);
                        $("#txtTempCode").val(dataRecord.TemplateCode);
                        $("#txtTempDescritpion").val(dataRecord.Description);
                    
                        $("#popupWindow").jqxWindow('open');
                    }
                },

                {
                    text: '', datafield: 'Delete', width: "100px", columntype: 'button', cellsrenderer: function () {
                        return "Delete";
                    }, buttonclick: function (row) {
                        var dataRecord = $("#Templatejqxgrid").jqxGrid('getrowdata', row);
                        if (confirm("Are you sure what to delete '" + dataRecord.TemplateName + "'?")) {
                            var request = $.ajax({
                                url: '/api/cms_Templates?id=' + dataRecord.TemplateId,
                                cache: false,
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                type: 'DELETE',
                                success: function () {

                                    loadAll(BindData, APITemplate);
                                },
                                error: function (jqXHR, exception) {
                                    console.log(jqXHR);
                                    console.log(exception);
                                }
                            });
                        }
                    }
                },

                {
                    text: '', datafield: 'Template Field', width: "120px", columntype: 'button', cellsalign: "center", align: 'center',
                    cellsrenderer: function () {
                        return "Template Field";
                    },
                    buttonclick: function (row) {

                        if (!$(".errorDiv").hasClass('hide'))
                            $(".errorDiv").addClass('hide');
                        $('#perror').html('');
                        editrow = row;
                        var offset = $("#Templatejqxgrid").offset();
                        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
                        var dataRecord = $("#Templatejqxgrid").jqxGrid('getrowdata', editrow);
                        console.log("dataRecord", dataRecord);
                        onhypClick();
                    }
                },
            ],
            filterable: true,
            sortable: true,
            pageable: true,
            pagermode: 'simple',
            selectionmode: 'none'
        });

    $.unblockUI();
    // initialize the popup window and buttons.
}

$("#btnSave").click(function () {

    if (!$(".errorDiv").hasClass('hide'))
        $(".errorDiv").addClass('hide');
    $('#perror').html('');

    if ($('#txtTempName').val() == "") {
        $('#perror').append('> "Template Name" is required <br/>');
        $('#txtTempName').focus();
    }
    if ($('#txtTempCode').val() == "") {
        $('#perror').append('> "Template Code" is required <br/>');
        $('#txtTempCode').focus();
    }

    if ($('#perror').html() !== '') {
        $(".errorDiv").removeClass('hide');
        return;
    }

    if (editrow >= 0) {

        var dataRecord = $("#Templatejqxgrid").jqxGrid('getrowdata', editrow);
        console.log("dataRecord-save", dataRecord);
        $("#popupWindow").jqxWindow('hide');
   

        var dataSave = {
            "TemplateId": dataRecord.TemplateId,
            "TemplateName": $("#txtTempName").val(),
            "TemplateCode": $("#txtTempCode").val(),
            "Description": $("#txtTempDescritpion").val()
        };

        $("#popUpHeader").html('Template Details - Edit');
        // Send company information to database
        var request = $.ajax({
            url: '/api/cms_Templates?id=' + dataRecord.TemplateId,
            cache: false,
            data: JSON.stringify(dataSave),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            type: 'PUT',
            success: function () {
                loadAll(BindData, APITemplate);
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR);
                console.log(exception);
            }
        });
    }
    else {
        $("#popupWindow").jqxWindow('hide');

        var dataSave = {
            "TemplateName": $("#txtTempName").val(),
            "TemplateCode": $("#txtTempCode").val(),
            "Description": $("#txtTempDescritpion").val()
        };

        var request = $.ajax({
            url: "/api/cms_Templates",
            type: 'POST',
            dataType: 'json',
            cache: false,
            data: JSON.stringify(dataSave),
            contentType: 'application/json; charset=utf-8',
            success: function () {
                loadAll(BindData, APITemplate);
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR);
                console.log(exception);
            }
        });
    }
});

function onhypClick()
{
    var dataRecord = $("#Templatejqxgrid").jqxGrid('getrowdata', editrow);
    var url = "/admin/templates/templatefields?templateId=" + dataRecord.TemplateId;
    window.open(url, '_blank');
}