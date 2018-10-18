var loadedCategory = 0;
var listCategory = [];
var totalRows = 0;

var editor;
var projectAPIUrl = '/api/CoreModule_ProjectMasters';
var categoryAPIUrl = '/api/CoreModule_ProjectCategories';


$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, categoryAPIUrl);
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 850, height:550, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () {

        editrow = -1;
        var offset = $("#jqxgrid").offset();
        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

        $("#txtProjName").val("");
        $("#txtShortDesc").val("");
        $("#txtLongDesc").val("");
        $("#txtUsage").val("");
        $("#txtMRP").val("");
        $("#txtOfferPrice").val("");
        $("#txtImageUrl").val("");
        $("#imgPhoto").attr('src', '');
        $("#ddlCategoryId").val("");
        $("#txtDirectRefPoints").val("");
        $("#popupWindow").jqxWindow('open');
    });
});
function getCategory(CategoryId) {
    var CategoryName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == CategoryId) {
            CategoryName = item.ProjCategoryName
        }
    });
    return CategoryName;
}

function LoadCategory(response) {
    listCategory = response;
    LoadCategoryDroplist();
    loadAll(BindData, projectAPIUrl);
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
                text: item.ProjCategoryName
            }, '</option>'))
    });
}

function BindData(response) {

    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'ProjName', type: 'string' },
                { name: 'ShortDesc', type: 'string' },
                { name: 'LongDesc', type: 'string' },
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
                { text: 'Name', datafield: 'ProjName', width: "500px" },
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
        $("#txtProjName").jqxInput('selectAll');
        $("#txtShortDesc").jqxInput('selectAll');
        $("#txtLongDesc").jqxInput('selectAll');
        $("#txtUsage").jqxInput('selectAll');
        $("#txtMRP").jqxInput('selectAll');
        $("#txtOfferPrice").jqxInput('selectAll');
        $("#txtImageUrl").jqxInput('selectAll');
        $("#txtDirectRefPoints").jqxInput('selectAll');
        $("#ddlCategoryId").jqxInput('selectAll');
    });

    $("#btnSave").click(function () {

        if (validateRequiredFields()) {

            var path = '';
            var formData = new FormData("frmUpload");
            formData.append('file', $('[id=updPhoto]')[0].files[0]);
            $.ajax({
                url: '/CoreModule_general/UploadFile?dir=' + '/files/projects',
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
                    "ProjName": $("#txtProjName").val(),
                    "ShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "Usage": $("#txtUsage").val(),
                    "MRP": $("#txtMRP").val(),
                    "OfferPrice": $("#txtOfferPrice").val(),
                    "ImageUrl": $("#txtImageUrl").val(),
                    "CategoryId": $("#ddlCategoryId").val(),
                    "DirectRefPoints": $("#txtDirectRefPoints").val(),
                    "DisplayOrder": dataRecord.DisplayOrder
                };

                var request = $.ajax({
                    url: projectAPIUrl + "?id=" + dataRecord.Id,
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
                    "ProjName": $("#txtProjName").val(),
                    "ShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "Usage": $("#txtUsage").val(),
                    "MRP": $("#txtMRP").val(),
                    "OfferPrice": $("#txtOfferPrice").val(),
                    "ImageUrl": $("#txtImageUrl").val(),
                    "CategoryId": $("#ddlCategoryId").val(),
                    "DirectRefPoints": $("#txtDirectRefPoints").val(),
                };
                var request = $.ajax({
                    url: projectAPIUrl,
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

    $("#txtProjName").val(dataRecord.ProjName);
    $("#txtShortDesc").val(dataRecord.ShortDesc);
    //$("#txtLongDesc").val(dataRecord.LongDesc);
    CKEDITOR.instances['txtLongDesc'].setData(dataRecord.LongDesc);
    $("#txtUsage").val(dataRecord.Usage);
    $("#txtMRP").val(dataRecord.MRP);
    $("#txtOfferPrice").val(dataRecord.OfferPrice);
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.ImageUrl);
    $("#txtImageUrl").val(dataRecord.ImageUrl);
    $("#txtDirectRefPoints").val(dataRecord.DirectRefPoints);
    $("#ddlCategoryId").val(dataRecord.CategoryId);

    $("#popupWindow").jqxWindow('open');

}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}


function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.PackName + "' Package?")) {
        var request = $.ajax({
            url: projectAPIUrl + "?id=" + dataRecord.uid,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeProjectDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, projectAPIUrl);
}