var loadedCategory = 0;
var listCategory = [];
var totalRows = 0;

var editor;
var eventAPIUrl = '/api/CoreModule_Events';
var categoryAPIUrl = '/api/CoreModule_EventCategories';

$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, categoryAPIUrl);

    $('#dtpEventDateTime').datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
    });


    $('#dtpEventRegStartOnDateTime').datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
    });


    $('#dtpEventRegEndOnDateTime').datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
    });

    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 850,height: 1050, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () {
        editrow = -1;
        var offset = $("#jqxgrid").offset();
        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

        $("#txtName").val("");
        $("#txtShortDesc").val("");
        $("#dtpEventDateTime").val("");
        $("#dtpEventRegStartOnDateTime").val("");
        $("#dtpEventRegEndOnDateTime").val("");
        $("#txtLocation").val("");
        $("#txtLongDesc").val("");
        $("#txtImageUrl").val("");
        $("#imgPhoto").attr('src', '');
        $("#ddlCategoryId").val("");

        $("#popupWindow").jqxWindow('open');
    });
});



function getCategory(CategoryId) {
    var EventName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == CategoryId) {
            EventName = item.EventName
        }
    });
    return EventName;
}

function LoadCategory(response) {
    listCategory = response;
    LoadCategoryDroplist();
    loadAll(BindData, eventAPIUrl);
}

function LoadCategoryDroplist() {
    var dropdown = $('#ddlCategoryId');
    dropdown.empty();
    dropdown.append(
        $('<option>', {
            value: "0",
            text: "--Select--"
        }, '</option>'));
    $.each(listCategory, function (index, item) {
        dropdown.append(
            $('<option>', {
                value: item.Id,
                text: item.EventName
            }, '</option>'))
    });
}

function BindData(response) {

    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'Name', type: 'string' },
                { name: 'EventCategoryId', type: 'int' },
                { name: 'ShortDesc', type: 'string' },
                { name: 'LongDesc', type: 'string' },
                { name: 'Active', type: 'bool' },
                { name: 'EventDateTime', type: 'date' },
                { name: 'EventRegistrationStartsOn', type: 'date' },
                { name: 'RegistrationEndOn', type: 'date' },
                { name: 'Location', type: 'string' },
                { name: 'PhotoUrl', type: 'string' },
                { name: 'EventName', type: 'string' },
                { name: 'DisplayOrder', type: 'int' },
            ],
            id: 'Id'
        };

    $.each(response, function (index, item) {
        item.EventName = getCategory(item.EventCategoryId);
    });
    totalRows = response.length;
    source.localdata = response;
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid(
        {
            theme: jqxTheme,
            width: '100%',
            source: dataAdapter,
            columnsresize: false,
            altrows: true,
            columns: [
                { text: 'Display', datafield: 'DisplayOrder', width: "90px", cellsrenderer: linkrendererDisplay, cellsalign: "center", align: 'center', sortable: false, filterable: false },
                { text: 'Id', datafield: 'Id', width: "75px" },
                { text: 'Category', datafield: 'EventName', width: "150px" },
                { text: 'Event', datafield: 'Name', width: "150px" },
                { text: 'Event On', datafield: 'EventDateTime', width: "200px", cellsformat: "dd/MM/yyyy HH:mm" },
                { text: 'Registration StartsOn', datafield: 'EventRegistrationStartsOn', width: "200px", cellsformat: "dd/MM/yyyy HH:mm" },
                { text: 'Registration EndOn', datafield: 'RegistrationEndOn', width: "200px", cellsformat: "dd/MM/yyyy HH:mm" },
                { text: 'Image Path', datafield: 'PhotoUrl', width: "200px" },
                { text: 'Location', datafield: 'Location', width: "150px" },
                {
                    text: 'Active', datafield: 'Active', width: "120px", cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                        var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
                        console.log(dataRecord.Active);
                        if (dataRecord.Active == true) {
                            return '<div style="text-align: center;">' + "Yes" + '</div>';
                        }
                        else {
                            return '<div style="text-align: center;">' + "No" + '</div>';
                        }
                    }
                },
                { text: 'Edit', datafield: 'Edit', width: "55px", cellsrenderer: linkrendererEdit, cellsalign: "center", align: 'center' },
                { text: 'Delete', datafield: 'Delete', width: "55px", cellsrenderer: linkrendererDelete, cellsalign: "center", align: 'center' },
            ],
            filterable: true,
            sortable: true,
            pageable: false,
            pagermode: 'simple',
            selectionmode: 'singlerow'
        });
    $.unblockUI();

    $("#popupWindow").on('open', function () {
        $("#txtName").jqxInput('selectAll');
        $("#txtShortDesc").jqxInput('selectAll');
        $("#txtLongDesc").jqxInput('selectAll');
        $("#dtpEventDateTime").jqxInput('selectAll');
        $("#dtpEventRegStartOnDateTime").jqxInput('selectAll');
        $("#dtpEventRegEndOnDateTime").jqxInput('selectAll');
        $("#txtLocation").jqxInput('selectAll');
        $("#txtImageUrl").jqxInput('selectAll');
        $("#ddlCategoryId").jqxInput('selectAll');
    });

    $("#btnSave").click(function () {

        if (validateRequiredFields()) {
            blockUI();
            var path = '';
            var formData = new FormData("frmUpload");
            formData.append('file', $('[id=updPhoto]')[0].files[0]);
            $.ajax({
                url: '/CoreModule_general/UploadFile?dir=' + '/files/products',
                data: formData,
                type: 'POST',
                async: false,
                contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
                processData: false, // NEEDED, DON'T OMIT THIS
                success: function (result) {
                    path = result.path;
                },
                error: function (jqXHR, exception) {
                    console.log(jqXHR);
                    console.log(exception);
                    path = "";
                }
            });

            if (path != '') {
                $("#txtImageUrl").val(path);
            }


            if (editrow >= 0) {

                var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
                console.log("dataRecord-save", dataRecord);
                $("#popupWindow").jqxWindow('hide');
            
                var dataSave = {
                    "Id": dataRecord.Id,
                    "Name": $("#txtName").val(),
                    "ShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "PhotoUrl": $("#txtImageUrl").val(),
                    "EventCategoryId": $("#ddlCategoryId").val(),
                    "Location": $("#txtLocation").val(),
                    "DisplayOrder": dataRecord.DisplayOrder,
                    "Active": $("#chkActive").is(':checked'),
                    "EventDateTime": getDateFromFields("dtpEventDateTime"),
                    "EventRegistrationStartsOn": getDateFromFields("dtpEventRegStartOnDateTime"),
                    "RegistrationEndOn": getDateFromFields("dtpEventRegEndOnDateTime"),

                    "DisplayOrder": dataRecord.DisplayOrder
                };
                var request = $.ajax({
                    url: eventAPIUrl + "?id=" + dataRecord.Id,
                    cache: false,
                    data: JSON.stringify(dataSave),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    type: 'PUT',
                    success: function () {
                        window.location = window.location;
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
                    "Id": 0,
                    "Name": $("#txtName").val(),
                    "ShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "PhotoUrl": $("#txtImageUrl").val(),
                    "EventCategoryId": $("#ddlCategoryId").val(),
                    "Location": $("#txtLocation").val(),
                    "Active": $("#chkActive").is(':checked'),
                    "EventDateTime": getDateFromFields("dtpEventDateTime"),
                    "EventRegistrationStartsOn": getDateFromFields("dtpEventRegStartOnDateTime"),
                    "RegistrationEndOn": getDateFromFields("dtpEventRegEndOnDateTime"),
                };
                var request = $.ajax({
                    url: eventAPIUrl,
                    cache: false,
                    data: JSON.stringify(dataSave),
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
    });
}

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
    $('#txtPhotoUrl').val(url);
    $("#dialogFileMngr").dialog("close");
}

var linkrendererDisplay = function (row, column, value) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='hypCoupons' onclick='onhypDisplayOrderClick(" + dataRecord.Id + ",1);' href='javascript:;' >" + '<i class="fa fa-fw fa-arrow-up bg-purple btn-flat" style="padding: 0px;"></i>' + "</a> <a class='hypCoupons' onclick='onhypDisplayOrderClick(" + dataRecord.Id + ", 0);' href='javascript:;' >" + '<i class="fa fa-fw fa-arrow-down bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    if (totalRows - 1 == row) {
        var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='hypCoupons' onclick='onhypDisplayOrderClick(" + dataRecord.Id + ",1);' href='javascript:;' >" + '<i class="fa fa-fw fa-arrow-up bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    }
    else if (row == 0) {
        var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='hypCoupons' onclick='onhypDisplayOrderClick(" + dataRecord.Id + ", 0);' href='javascript:;' >" + '<i class="fa fa-fw fa-arrow-down bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    }

    return html;
}

function onhypDisplayOrderClick(id, upDown) {
    loadAll(RefreshData, "/CoreModule_General/ChangeEventMasterDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, eventAPIUrl);
}


var linkrendererEdit = function (row, column, value) {
    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypEditClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-edit bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}

function onhypEditClick(row) {

    if (!$(".errorDiv").hasClass('hide'))
        $(".errorDiv").addClass('hide');
    $('#perror').html('');
    editrow = row;

    var offset = $("#jqxgrid").offset();
    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
    console.log("dataRecord", dataRecord);
    $("#txtName").val(dataRecord.Name);
    $("#txtShortDesc").val(dataRecord.ShortDesc);
    CKEDITOR.instances['txtLongDesc'].setData(dataRecord.LongDesc);
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.PhotoUrl);
    $("#txtImageUrl").val(dataRecord.PhotoUrl);
    $("#txtLocation").val(dataRecord.Location);

    $("#ddlCategoryId").val(dataRecord.EventCategoryId);
    $("#chkActive").prop("checked", dataRecord.Active);

    var dateObj = dataRecord.EventDateTime; 
    var newEventDate = new Date(dateObj);
    var formattedEventString = [newEventDate.getDate(), newEventDate.getMonth() + 1, newEventDate.getFullYear()].join("/");

    var st = newEventDate.getHours() + ":" + newEventDate.getMinutes();
    if (newEventDate.getHours() < 10) {
        st = "0" + newEventDate.getHours();
    }
    else {
        st = newEventDate.getHours();
    }
    if (newEventDate.getMinutes() < 10) {
        st = st + ":0" + newEventDate.getMinutes();
    }
    else {
        st = st + ":" + newEventDate.getMinutes();
    }

    $("#dtpEventDateTime").val(formattedEventString + " " + st);

    var dateEventRegStartObj = dataRecord.EventRegistrationStartsOn; 
    var newEventRegStartDate = new Date(dateEventRegStartObj);
    var formattedEventRegStartString = [newEventRegStartDate.getDate(), newEventRegStartDate.getMonth() + 1, newEventRegStartDate.getFullYear()].join("/");

    var st = newEventRegStartDate.getHours() + ":" + newEventRegStartDate.getMinutes();
    if (newEventRegStartDate.getHours() < 10) {
        st = "0" + newEventRegStartDate.getHours();
    }
    else {
        st = newEventRegStartDate.getHours();
    }
    if (newEventRegStartDate.getMinutes() < 10) {
        st = st + ":0" + newEventRegStartDate.getMinutes();
    }
    else {
        st = st + ":" + newEventRegStartDate.getMinutes();
    }

    $("#dtpEventRegStartOnDateTime").val(formattedEventRegStartString + " " + st);

    var dateRegEndObj = dataRecord.RegistrationEndOn; 
    var newRegEndDate = new Date(dateRegEndObj);
    var formattedRegEndString = [newRegEndDate.getDate(), newRegEndDate.getMonth() + 1, newRegEndDate.getFullYear()].join("/");

    var st = newRegEndDate.getHours() + ":" + newRegEndDate.getMinutes();
    if (newRegEndDate.getHours() < 10) {
        st = "0" + newRegEndDate.getHours();
    }
    else {
        st = newRegEndDate.getHours();
    }
    if (newRegEndDate.getMinutes() < 10) {
        st = st + ":0" + newRegEndDate.getMinutes();
    }
    else {
        st = st + ":" + newRegEndDate.getMinutes();
    }

    $("#dtpEventRegEndOnDateTime").val(formattedRegEndString + " " + st);

    $("#popupWindow").jqxWindow('open');
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}

function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Event?")) {
        var request = $.ajax({
            url: eventAPIUrl + "?id=" + dataRecord.uid,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            type: 'DELETE',
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


