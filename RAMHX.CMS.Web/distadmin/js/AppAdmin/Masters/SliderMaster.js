var editor;
var totalRows = 0;
var listCategory;

var appSliderCategory = "/api/CoreModule_SlidersCategory";
var appSliderItems = "/api/CoreModule_SliderItems";

$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, appSliderCategory);
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, height:950, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnPopCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () { addNew(); });
    $("#popupWindow").on('open', function () {
        $("#txtFirstLine").jqxInput('selectAll');
        $("#txtSecondLine").jqxInput('selectAll');
        $("#txtThirdLine").jqxInput('selectAll');
        $("#txtFourthLine").jqxInput('selectAll');
        $("#txtFirstButton").jqxInput('selectAll');
        $("#txtSecondButton").jqxInput('selectAll');
        $("#txtFirstButtonLink").jqxInput('selectAll');
        $("#txtSecondButtonLink").jqxInput('selectAll');
        
        $("#chkActive").jqxInput('selectAll');
    });

    $("#btnSave").click(function () {
        if (validateRequiredFields()) {
            saveRow();
        }
        
    });
});

function getCategoryName(categoryid) {
    console.log(listCategory);
    var categoryName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == categoryid) {
            categoryName = item.SliderName;
        }
    });
    return categoryName;
}

function LoadCategory(response) {
    listCategory = response;
    loadAll(BindData, appSliderItems);
    LoadCategoryDroplist();
}

function LoadCategoryDroplist() {
    var dropdown = $('#ddlCategory');
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
                text: item.SliderName
            }, '</option>'))
    });
}

function BindData(response) {
    console.log('res', response);

    var source = {
        datatype: "array",
        datafields: [
            { name: 'Id', type: 'int' },
            { name: 'DisplayOrder', type: 'int' },
            { name: 'Active', type: 'bool' },
            { name: 'SliderImagePath', type: 'string' },
            { name: 'SliderId', type: 'int' },
            { name: 'FirstLine', type: 'string' },
            { name: 'SecondLine', type: 'string' },
            { name: 'ThirdLine', type: 'string' },
            { name: 'ForthLine', type: 'string' },
            { name: 'FirstButtonText', type: 'string' },
            { name: 'SecondButtonText', type: 'string' },
            { name: 'FirstButtonLink', type: 'string' },
            { name: 'SecondButtonLink', type: 'string' },
            { name: 'CategoryName', type: 'string' },

        ],
        id: 'Id'
    };

    $.each(response, function (index, item) {
        item.CategoryName = getCategoryName(item.SliderId);
    });

    totalRows = response.length;
    source.localdata = response;
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid({
        theme: jqxTheme,
        width: '100%',
        source: dataAdapter,
        columnsresize: false,
        altrows: true,
        columns: [
            { text: 'Display', datafield: 'DisplayOrder', width: "90px", cellsrenderer: linkrendererDisplay, cellsalign: "center", align: 'center', sortable: false, filterable: false },
            { text: 'Id', datafield: 'Id', width: "50px", hidden: false },
            { text: 'Category', datafield: 'CategoryName', width: "150px" },
            { text: 'Name', datafield: 'FirstLine', width: "150px" },
            { text: 'Image Path', datafield: 'SliderImagePath', width: "350px" },
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
}

function addNew() {
    editrow = -1;
    var offset = $("#jqxgrid").offset();
    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
    $("#txtFirstLine").val("");
    $("#txtSecondLine").val("");
    $("#txtThirdLine").val("");
    $("#txtFourthLine").val("");
    $("#txtFirstButton").val("");
    $("#txtSecondButton").val("");
    $("#txtFirstButtonLink").val("");
    $("#txtSecondButtonLink").val("");
    $("#imgPhoto").attr('src','');
    $("#ddlCategory").val(0);
    $("#chkActive").prop("checked", true);
    $("#popupWindow").jqxWindow('open');
}

function saveRow() {
   
    var path = '';
    var formData = new FormData("frmUpload");
    formData.append('file', $('[id=updPhoto]')[0].files[0]);
    $.ajax({
        url: '/CoreModule_general/UploadFile?dir=' + '/files/Sliders',
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
        $("#txtThumbPath").val(path);
    }

    if (editrow >= 0) {

        var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
        console.log("dataRecord-save", dataRecord);
        $("#popupWindow").jqxWindow('hide');

        var dataSave = {
            "Id": dataRecord.Id,
            "FirstLine": $("#txtFirstLine").val(),
            "SecondLine": $("#txtSecondLine").val(),
            "ThirdLine": $("#txtThirdLine").val(),
            "ForthLine": $("#txtFourthLine").val(),
            "FirstButtonText": $("#txtFirstButton").val(),
            "SecondButtonText": $("#txtSecondButton").val(),
            "FirstButtonLink": $("#txtFirstButtonLink").val(),
            "SecondButtonLink": $("#txtSecondButtonLink").val(),
            "Active": $("#chkActive").is(':checked'),
            "SliderImagePath": $("#txtThumbPath").val(),
            "SliderId": $("#ddlCategory").val(),
            "DisplayOrder": dataRecord.DisplayOrder
        };

        var request = $.ajax({
            url: appSliderItems + "?id=" + dataRecord.Id,
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
    } else {
        $("#popupWindow").jqxWindow('hide');

        var dataSave = {
            "Id": 0,
            "FirstLine": $("#txtFirstLine").val(),
            "SecondLine": $("#txtSecondLine").val(),
            "ThirdLine": $("#txtThirdLine").val(),
            "ForthLine": $("#txtFourthLine").val(),
            "FirstButtonText": $("#txtFirstButton").val(),
            "SecondButtonText": $("#txtSecondButton").val(),
            "FirstButtonLink": $("#txtFirstButtonLink").val(),
            "SecondButtonLink": $("#txtSecondButtonLink").val(),
            "Active": $("#chkActive").is(':checked'),
            "SliderImagePath": $("#txtThumbPath").val(),
            "SliderId": $("#ddlCategory").val()
        };
        var request = $.ajax({
            url: appSliderItems,
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

function onhypClick() {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
    window.location = "/appadmin/masters/sliderimages?slideritemId=" + dataRecord.Id;
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
    $("#txtFirstLine").val(dataRecord.FirstLine);
    $("#txtSecondLine").val(dataRecord.SecondLine);
    $("#txtThirdLine").val(dataRecord.ThirdLine);
    $("#txtFourthLine").val(dataRecord.ForthLine);
    $("#txtFirstButton").val(dataRecord.FirstButtonText);
    $("#txtSecondButton").val(dataRecord.SecondButtonText);
    $("#txtFirstButtonLink").val(dataRecord.FirstButtonLink);
    $("#txtSecondButtonLink").val(dataRecord.SecondButtonLink);
    $("#ddlCategory").val(dataRecord.SliderId);
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.SliderImagePath);
    $("#txtThumbPath").val(dataRecord.SliderImagePath);
    $("#popupWindow").jqxWindow('open');
    $("#chkActive").prop("checked", dataRecord.Active);
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}

function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Slider?")) {
        var request = $.ajax({
            url: appSliderItems + "?id=" + dataRecord.uid,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeSliderItemsDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, appSliderItems);
}