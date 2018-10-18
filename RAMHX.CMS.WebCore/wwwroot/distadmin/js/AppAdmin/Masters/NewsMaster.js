var loadedCategory = 0;
var listCategory = [];
var totalRows = 0;

var editor;
var newsAPIUrl = '/api/CoreModule_News';
var categoryAPIUrl = '/api/CoreModule_NewsCategories';

$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, categoryAPIUrl);

    $('#dtpDate').datetimepicker({
        format: 'DD/MM/YYYY HH:mm',
    });

    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 850, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () {
        editrow = -1;
        var offset = $("#jqxgrid").offset();
        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

        $("#txtTitle").val("");
        $("#txtShortDesc").val("");
        $("#txtLongDesc").val("");
        $("#txtImageUrl").val("");
        $("#imgPhoto").attr('src', '');
        $("#ddlCategoryId").val("");
        $("#dtpDate").val("");

        $("#popupWindow").jqxWindow('open');
    });
});

function getCategory(CategoryId) {
    var CategoryName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == CategoryId) {
            CategoryName = item.CategoryName
        }
    });
    return CategoryName;
}

function LoadCategory(response) {
    listCategory = response;
    LoadCategoryDroplist();
    loadAll(BindData, newsAPIUrl);
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
                text: item.CategoryName
            }, '</option>'))
    });
}

function BindData(response) {

    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'NewsTitle', type: 'string' },
                { name: 'NewsShortDesc', type: 'string' },
                { name: 'LongDesc', type: 'string' },
                { name: 'Active', type: 'bool' },
                { name: 'NewsDate', type: 'date' },
                { name: 'AttachmentPath', type: 'string' },
                { name: 'NewsCategoryId', type: 'int' },
                { name: 'CategoryName', type: 'string' },
                { name: 'DisplayOrder', type: 'int' },
            ],
            id: 'Id'
        };

    $.each(response, function (index, item) {
        item.CategoryName = getCategory(item.NewsCategoryId);
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
                { text: 'Category', datafield: 'CategoryName', width: "150px" },
                { text: 'Title', datafield: 'NewsTitle', width: "150px" },
                { text: 'Date', datafield: 'NewsDate', width: "250px", cellsformat: "dd/MM/yyyy HH:mm" },
                { text: 'Image Path', datafield: 'AttachmentPath', width: "350px" },
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
        $("#txtTitle").jqxInput('selectAll');
        $("#txtShortDesc").jqxInput('selectAll');
        $("#txtLongDesc").jqxInput('selectAll');
        $("#dtpDate").jqxInput('selectAll');
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
                    "NewsTitle": $("#txtTitle").val(),
                    "NewsShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "AttachmentPath": $("#txtImageUrl").val(),
                    "NewsCategoryId": $("#ddlCategoryId").val(),
                    "DisplayOrder": dataRecord.DisplayOrder,
                    "Active": $("#chkActive").is(':checked'),
                    //"NewsDate": $("#dtpDate").val(),
                    "NewsDate": getDateFromFields("dtpDate")

                };
                var request = $.ajax({
                    url: newsAPIUrl + "?id=" + dataRecord.Id,
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
                    "NewsTitle": $("#txtTitle").val(),
                    "NewsShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "AttachmentPath": $("#txtImageUrl").val(),
                    "NewsCategoryId": $("#ddlCategoryId").val(),
                    "Active": $("#chkActive").is(':checked'),
                    //"NewsDate": $("#dtpDate").val()
                    "NewsDate": getDateFromFields("dtpDate")

                };
                var request = $.ajax({
                    url: newsAPIUrl,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeNewsMasterDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, newsAPIUrl);
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
    $("#txtTitle").val(dataRecord.NewsTitle);
    $("#txtShortDesc").val(dataRecord.NewsShortDesc);
    CKEDITOR.instances['txtLongDesc'].setData(dataRecord.LongDesc);
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.AttachmentPath);
    $("#txtImageUrl").val(dataRecord.AttachmentPath);
    $("#ddlCategoryId").val(dataRecord.NewsCategoryId);
    $("#chkActive").prop("checked", dataRecord.Active);

    var dateObj = dataRecord.NewsDate; 
    var newDate = new Date(dateObj);
    var formattedString = [newDate.getDate(), newDate.getMonth() + 1, newDate.getFullYear()].join("/");

    var st = newDate.getHours() + ":" + newDate.getMinutes();
    if (newDate.getHours() < 10) {
        st = "0" + newDate.getHours();
    }
    else {
        st = newDate.getHours();
    }
    if (newDate.getMinutes() < 10) {
        st = st + ":0" + newDate.getMinutes();
    }
    else {
        st = st + ":" + newDate.getMinutes();
    }

    $("#dtpDate").val(formattedString + " " + st);

    $("#popupWindow").jqxWindow('open');
    $("#dtpDate").focus();
    $("#txtTitle").focus();
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}

function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.NewsTitle + "' News?")) {
        var request = $.ajax({
            url: newsAPIUrl + "?id=" + dataRecord.uid,
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


