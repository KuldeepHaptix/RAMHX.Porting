﻿var loadedCategory = 0;
var listCategory = [];
var totalRows = 0;

var editor;
var productAPIUrl = '/api/CoreModule_ProductMasters';
var categoryAPIUrl = '/api/CoreModule_ProductCategories';

$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, categoryAPIUrl);
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 850, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () {
        editrow = -1;
        var offset = $("#jqxgrid").offset();
        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

        $("#txtProdName").val("");
        $("#txtShortDesc").val("");
        $("#txtLongDesc").val("");
        $("#txtUsage").val("");
        $("#txtMRP").val("");
        $("#txtOfferPrice").val("");
        $("#txtImageUrl").val("");
        $("#imgPhoto").attr('src', '');
        $("#ddlCategoryId").val("");

        $("#popupWindow").jqxWindow('open');
    });
});
function getCategory(CategoryId) {
    var CategoryName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == CategoryId) {
            CategoryName = item.ProdCategoryName
        }
    });
    return CategoryName;
}

function LoadCategory(response) {
    listCategory = response;
    LoadCategoryDroplist();
    loadAll(BindData, productAPIUrl);
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
                text: item.ProdCategoryName
            }, '</option>'))
    });
}

function BindData(response) {

    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'ProdName', type: 'string' },
                { name: 'ShortDesc', type: 'string' },
                { name: 'LongDesc', type: 'string' },
                { name: 'Usage', type: 'string' },
                { name: 'MRP', type: 'float' },
                { name: 'OfferPrice', type: 'float' },
                { name: 'ImageUrl', type: 'string' },
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
                { text: 'Id', datafield: 'Id', width: "75px" },
                { text: 'Name', datafield: 'ProdName', width: "150px" },
                { text: 'Usage', datafield: 'Usage', width: "170px", hidden: true },
                { text: 'MRP', datafield: 'MRP', width: "170px" },
                { text: 'Offer Price', datafield: 'OfferPrice', width: "170px" },
                { text: 'Category', datafield: 'CategoryName', width: "150px" },
               
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
        $("#txtProdName").jqxInput('selectAll');
        $("#txtShortDesc").jqxInput('selectAll');
        $("#txtLongDesc").jqxInput('selectAll');
        $("#txtUsage").jqxInput('selectAll');
        $("#txtMRP").jqxInput('selectAll');
        $("#txtOfferPrice").jqxInput('selectAll');
        $("#txtImageUrl").jqxInput('selectAll');
        $("#ddlCategoryId").jqxInput('selectAll');
    });

    $("#btnSave").click(function () {

        if (validateRequiredFields()) {

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
                    "ProdName": $("#txtProdName").val(),
                    "ShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "Usage": $("#txtUsage").val(),
                    "MRP": $("#txtMRP").val(),
                    "OfferPrice": $("#txtOfferPrice").val(),
                    "ImageUrl": $("#txtImageUrl").val(),
                    "CategoryId": $("#ddlCategoryId").val(),
                    "DisplayOrder": dataRecord.DisplayOrder
                
                };

                var request = $.ajax({
                    url: productAPIUrl + "?id=" + dataRecord.Id,
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
                    "ProdName": $("#txtProdName").val(),
                    "ShortDesc": $("#txtShortDesc").val(),
                    //"LongDesc": $("#txtLongDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "Usage": $("#txtUsage").val(),
                    "MRP": $("#txtMRP").val(),
                    "OfferPrice": $("#txtOfferPrice").val(),
                    "ImageUrl": $("#txtImageUrl").val(),
                    "CategoryId": $("#ddlCategoryId").val()
                };
                var request = $.ajax({
                    url: productAPIUrl,
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
    $('#txtImageUrl').val(url);
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
    loadAll(RefreshData, "/CoreModule_General/ChangeProductDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, productAPIUrl);
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
    $("#txtProdName").val(dataRecord.ProdName);
    $("#txtShortDesc").val(dataRecord.ShortDesc);
    CKEDITOR.instances['txtLongDesc'].setData(dataRecord.LongDesc);
    $("#txtUsage").val(dataRecord.Usage);
    $("#txtMRP").val(dataRecord.MRP);
    $("#txtOfferPrice").val(dataRecord.OfferPrice);
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.ImageUrl);
    $("#txtImageUrl").val(dataRecord.ImageUrl);
    $("#ddlCategoryId").val(dataRecord.CategoryId);

    $("#popupWindow").jqxWindow('open');
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}

function onhypDeleteClick(row)
{
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.ProdName + "' Product?")) {
        var request = $.ajax({
            url: productAPIUrl + "?id=" + dataRecord.uid,
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
