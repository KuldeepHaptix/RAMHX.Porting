var loadedCategory = 0;
var listCategory = [];
var totalRows = 0;
var editor;

var jobAPIUrl = '/api/CoreModule_JobMasters';
var categoryAPIUrl = '/api/CoreModule_JobCategories';


$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, categoryAPIUrl);
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, height: 560, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () {

        editrow = -1;
        var offset = $("#jqxgrid").offset();
        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

        $("#txtTitle").val("");
        $("#txtQualification").val("");
        $("#txtExperience").val("");
        $("#txtSpecialization").val("");
        $("#ddlCategoryId").val(0);
        $("#popupWindow").jqxWindow('open');
    });
});

function getCategory(CategoryId) {
    var CategoryName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == CategoryId) {
            CategoryName = item.JobCategoryName
        }
    });
    return CategoryName;
}


function LoadCategory(response) {
    listCategory = response;
    LoadCategoryDroplist();
    loadAll(BindData, jobAPIUrl);
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
                text: item.JobCategoryName
            }, '</option>'))
    });
}

function BindData(response) {

    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'JobTitle', type: 'string' },
                { name: 'Qualification', type: 'string' },
                { name: 'EXPERIENCE', type: 'string' },
                { name: 'SPECIALIZATION', type: 'string' },
                { name: 'CategoryId', type: 'int' },
                { name: 'CategoryName', type: 'string' },
                { name: 'DisplayOrder', type: 'int' },

            ],
            id: 'Id'

        };
    $.each(response, function (index, item) {
        item.CategoryName = getCategory(item.CategoryId);
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
                { text: 'Job #', datafield: 'Id', width: "75px" },
                { text: 'Title', datafield: 'JobTitle', width: "200px" },
                { text: 'Qualification', datafield: 'Qualification', width: "170px"},
                { text: 'Experience', datafield: 'EXPERIENCE', width: "170px" },
                { text: 'Specialization', datafield: 'SPECIALIZATION', width: "170px" },
                { text: 'Category', datafield: 'CategoryName', width: "150px" },
                { text: 'Edit', datafield: 'Edit', width: "75px", cellsrenderer: linkrendererEdit, cellsalign: "center", align: 'center' },
                { text: 'Delete', datafield: 'Delete', width: "100px", cellsrenderer: linkrendererDelete, cellsalign: "center", align: 'center' }
            ],
            filterable: true,
            sortable: true,
            pageable: false,
            pagermode: 'simple',
            selectionmode: 'singlerow'
        });
    $.unblockUI();



    $("#popupWindow").on('open', function () {
        $("#txtTitle").jqxInput('selectAll');
        $("#txtQualification").jqxInput('selectAll');
        $("#txtExperience").jqxInput('selectAll');
        $("#txtSpecialization").jqxInput('selectAll');
        $("#ddlCategoryId").jqxInput('selectAll');
    });

    $("#btnSave").click(function () {

        if (validateRequiredFields()) {

            if (editrow >= 0) {

                var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
                console.log("dataRecord-save", dataRecord);
                $("#popupWindow").jqxWindow('hide');

                var dataSave = {
                    "Id": dataRecord.Id,
                    "JobTitle": $("#txtTitle").val(),
                    "Qualification": $("#txtQualification").val(),
                    "EXPERIENCE": $("#txtExperience").val(),
                    "SPECIALIZATION": $("#txtSpecialization").val(),
                    "CategoryId": $("#ddlCategoryId").val(),
                    "DisplayOrder": dataRecord.DisplayOrder
                };

                var request = $.ajax({
                    url: jobAPIUrl + "?id=" + dataRecord.Id,
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
                    "JobTitle": $("#txtTitle").val(),
                    "Qualification": $("#txtQualification").val(),
                    "EXPERIENCE": $("#txtExperience").val(),
                    "SPECIALIZATION": $("#txtSpecialization").val(),
                    "CategoryId": $("#ddlCategoryId").val()
                };
                var request = $.ajax({
                    url: jobAPIUrl,
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

    $("#txtTitle").val(dataRecord.JobTitle);
    $("#txtQualification").val(dataRecord.Qualification);
    $("#txtExperience").val(dataRecord.EXPERIENCE);
    $("#txtSpecialization").val(dataRecord.SPECIALIZATION);
    $("#ddlCategoryId").val(dataRecord.CategoryId);

    $("#popupWindow").jqxWindow('open');

}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}


function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.JobTitle + "' Title?")) {
        var request = $.ajax({
            url: jobAPIUrl + "?id=" + dataRecord.uid,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeJobDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, jobAPIUrl);
}