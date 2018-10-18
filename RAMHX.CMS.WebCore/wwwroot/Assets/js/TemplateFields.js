var APIfieldTypes = '/Templates/GetFieldTypes';
var listFieldTypes;
var currentTemplateId = getParameterByName("templateId", window.location.href);
//var APIFieldTemplate = "/api/cms_TemplateFields?templateId=" + currentTemplateId;

var APIFieldTemplate = '/Templates/GetFieldTemplate?templateId=' + currentTemplateId;

$(document).ready(function () {
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, height: '980px', resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });
    loadAll(LoadFieldTypes, APIfieldTypes);
    loadAll(BindData, APIFieldTemplate);
})

$("#btnAddNewTemplate").click(function () {
    editrow = -1;
    var offset = $("#TemplateFieldjqxgrid").offset();
    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

    // get the clicked row's data and initialize the input fields.
    $("#txtFieldName").val("");
    $("#ddlFieldType").val(0);
    $("#txtFieldDisplay").val("");
    $("#txtDefaultValue").val("");
    $("#txtDisplayName").val("");
    $("#txtNotes").val("");

    // show the popup window.
    $("#popupWindow").jqxWindow('open');
});

function LoadFieldTypes(fieldTypes) {
    listFieldTypes = fieldTypes;
    LoadFieldDropList();
}

function LoadFieldDropList() {
    var dropdown = $('#ddlFieldType');
    dropdown.empty();
    dropdown.append(
        $('<option>', {
            value: "0",
            text: "---Select---"
        }, '</option>'));
    $.each(listFieldTypes, function (index, item) {
        dropdown.append(
            $('<option>', {
                value: item.ItemId,
                text: item.ItemName
            }, '</option>'))
    });
}

function getFieldTypeName(FieldTypeId) {
    var FieldTypeName = "";
    $.each(listFieldTypes, function (index, item) {
        if (item.ItemId == FieldTypeId) {
            FieldTypeName = item.ItemName;
        }
    });
    return FieldTypeName;
}


function BindData(response) {
    console.log(response);
    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'TemplateFieldId', type: 'string' },
                { name: 'FieldName', type: 'string' },
                { name: 'FieldTypeId', type: 'int' },
                { name: 'FieldDisplayOrder', type: 'int' },
                { name: 'DefaultValue', type: 'string' },
                { name: 'DisplayName', type: 'string' },
                { name: 'Notes', type: 'string' },
                {name: 'FieldTypeName', type: 'string'}

            ],
            id: 'TemplateFieldId'
        };

    $.each(response, function (index, item) {
        item.FieldTypeName = getFieldTypeName(item.FieldTypeId);
    });

    source.localdata = response;
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#TemplateFieldjqxgrid").jqxGrid(
        {
            theme: jqxTheme,
            width: '100%',
            source: dataAdapter,
            columnsresize: false,
            altrows: true,
            columns: [
                { text: 'Template Field #', datafield: 'TemplateFieldId', width: "300px" },
                { text: 'Name', datafield: 'FieldName', width: "200px" },
                { text: 'Type', datafield: 'FieldTypeName', width: "200px" },
                { text: 'Display', datafield: 'FieldDisplayOrder', width: "200px", },
                { text: 'Default Value', datafield: 'DefaultValue', width: "150px" },
                { text: 'Display Name', datafield: 'DisplayName', width: "150px" },
                { text: 'Notes', datafield: 'Notes', width: "200px" },
                {
                    text: '', datafield: 'Edit', width: "75px", columntype: 'button', cellsrenderer: function () {
                        return "Edit";
                    },
                    buttonclick: function (row) {
                        if (!$(".errorDiv").hasClass('hide'))
                            $(".errorDiv").addClass('hide');
                        $('#perror').html('');
                        editrow = row;
                        var offset = $("#TemplateFieldjqxgrid").offset();
                        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
                        var dataRecord = $("#TemplateFieldjqxgrid").jqxGrid('getrowdata', editrow);
                        console.log("dataRecord", dataRecord);

                        $("#txtFieldName").val(dataRecord.FieldName);
                        $("#ddlFieldType").val(dataRecord.FieldTypeId);
                        $("#txtFieldDisplay").val(dataRecord.FieldDisplayOrder);
                        $("#txtDefaultValue").val(dataRecord.DefaultValue);
                        $("#txtDisplayName").val(dataRecord.DisplayName);
                        $("#txtNotes").val(dataRecord.Notes);

                        $("#popupWindow").jqxWindow('open');
                    }
                },

                {
                    text: '', datafield: 'Delete', width: "100px", columntype: 'button', cellsrenderer: function () {
                        return "Delete";
                    }, buttonclick: function (row) {
                        var dataRecord = $("#TemplateFieldjqxgrid").jqxGrid('getrowdata', row);
                        if (confirm("Are you sure what to delete '" + dataRecord.FieldName + "'?")) {
                            var request = $.ajax({
                                url: '/api/cms_TemplateFields?id=' + dataRecord.TemplateFieldId,
                                cache: false,
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                type: 'DELETE',
                                success: function () {

                                    loadAll(BindData, APIFieldTemplate);
                                },
                                error: function (jqXHR, exception) {
                                    console.log(jqXHR);
                                    console.log(exception);
                                }
                            });
                        }
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

    if ($('#txtFieldName').val() == "") {
        $('#perror').append('> "Field Name" is required <br/>');
        $('#txtTempName').focus();
    }
    if ($('#txtFieldDisplay').val() == "") {
        $('#perror').append('> "Field Display" is required <br/>');
        $('#txtTempCode').focus();
    }

    if ($('#txtDefaultValue').val() == "") {
        $('#perror').append('> "Default Value" is required <br/>');
        $('#txtTempCode').focus();
    }

    if ($('#txtDisplayName').val() == "") {
        $('#perror').append('> "Display Name" is required <br/>');
        $('#txtTempCode').focus();
    }

    if ($('#perror').html() !== '') {
        $(".errorDiv").removeClass('hide');
        return;
    }

    if (editrow >= 0) {

        var dataRecord = $("#TemplateFieldjqxgrid").jqxGrid('getrowdata', editrow);
        console.log("dataRecord-save", dataRecord);
        $("#popupWindow").jqxWindow('hide');


        var dataSave = {
            "TemplateFieldId": dataRecord.TemplateFieldId,
            "FieldName": $("#txtFieldName").val(),
            "FieldTypeId": $("#ddlFieldType").val(),
            "FieldDisplayOrder": $("#txtFieldDisplay").val(),
            "DefaultValue": $("#txtDefaultValue").val(),
            "DisplayName": $("#txtDisplayName").val(),
            "Notes": $("#txtNotes").val(),
            "TemplateId": currentTemplateId
        };

        // Send company information to database
        var request = $.ajax({
            url: '/api/cms_TemplateFields?id=' + dataRecord.TemplateFieldId,
            cache: false,
            data: JSON.stringify(dataSave),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            type: 'PUT',
            success: function () {
                loadAll(BindData, APIFieldTemplate);
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
            "FieldName": $("#txtFieldName").val(),
            "FieldTypeId": $("#ddlFieldType").val(),
            "FieldDisplayOrder": $("#txtFieldDisplay").val(),
            "DefaultValue": $("#txtDefaultValue").val(),
            "DisplayName": $("#txtDisplayName").val(),
            "Notes": $("#txtNotes").val(),
            "TemplateId": currentTemplateId
        };

        var request = $.ajax({
            url: '/api/cms_TemplateFields',
            type: 'POST',
            dataType: 'json',
            cache: false,
            data: JSON.stringify(dataSave),
            contentType: 'application/json; charset=utf-8',
            success: function () {
                loadAll(BindData, APIFieldTemplate);
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR);
                console.log(exception);
            }
        });
    }
});