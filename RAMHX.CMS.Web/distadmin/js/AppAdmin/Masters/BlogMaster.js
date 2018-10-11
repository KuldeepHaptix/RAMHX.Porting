var loadedCategory = 0;
var listCategory = [];
var totalRows = 0;

var editor;
var blogsAPIUrl = '/api/CoreModule_Blogs';
var categoryAPIUrl = '/api/CoreModule_BlogCategories';

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
        $("#txtAuthor").val("");
        $("#txtTags").val("");
        $("#txtShortDesc").val("");
        $("#txtLongDesc").val("");
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
    loadAll(BindData, blogsAPIUrl);
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
                { name: 'BlogTitle', type: 'string' },
                { name: 'BlogShortDesc', type: 'string' },
                { name: 'LongDesc', type: 'string' },
                { name: 'Active', type: 'bool' },
                { name: 'PublishDate', type: 'date' },
                { name: 'BlogCategoryId', type: 'int' },
                { name: 'CategoryName', type: 'string' },
                { name: 'Author', type: 'string' },
                { name: 'Tags', type: 'string' },
                { name: 'DisplayOrder', type: 'int' },
            ],
            id: 'Id'
        };

    $.each(response, function (index, item) {
        item.CategoryName = getCategory(item.BlogCategoryId);
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
                { text: 'Title', datafield: 'BlogTitle', width: "150px" },
                { text: 'Author', datafield: 'Author', width: "150px" },
                { text: 'Tags', datafield: 'Tags', width: "150px" },
                { text: 'Date', datafield: 'PublishDate', width: "250px", cellsformat: "dd/MM/yyyy HH:mm" },
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
        $("#txtAuthor").jqxInput('selectAll');
        $("#txtTags").jqxInput('selectAll');
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
                    "BlogTitle": $("#txtTitle").val(),
                    "BlogShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "BlogCategoryId": $("#ddlCategoryId").val(),
                    "DisplayOrder": dataRecord.DisplayOrder,
                    "Active": $("#chkActive").is(':checked'),
                    "Author": $("#txtAuthor").val(),
                    "Tags": $("#txtTags").val(),
                    "PublishDate": getDateFromFields("dtpDate")
                };
                console.log(getDateFromFields("dtpDate"));
                var request = $.ajax({
                    url: blogsAPIUrl + "?id=" + dataRecord.Id,
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
                    "BlogTitle": $("#txtTitle").val(),
                    "BlogShortDesc": $("#txtShortDesc").val(),
                    "LongDesc": CKEDITOR.instances['txtLongDesc'].getData(),
                    "BlogCategoryId": $("#ddlCategoryId").val(),
                    "Active": $("#chkActive").val(),
                    "Author": $("#txtAuthor").val(),
                    "Tags": $("#txtTags").is(':checked'),
                    "PublishDate": getDateFromFields("dtpDate")
                };
                var request = $.ajax({
                    url: blogsAPIUrl,
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
    //$('#txtImageUrl').val(url);
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
    loadAll(RefreshData, "/CoreModule_General/ChangeBlogMasterDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, blogsAPIUrl);
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
    $("#txtTitle").val(dataRecord.BlogTitle);
    $("#txtShortDesc").val(dataRecord.BlogShortDesc);
    $("#txtAuthor").val(dataRecord.Author);
    $("#txtTags").val(dataRecord.Tags);
    CKEDITOR.instances['txtLongDesc'].setData(dataRecord.LongDesc);
    $("#ddlCategoryId").val(dataRecord.BlogCategoryId);
    $("#chkActive").prop("checked", dataRecord.Active);

    var dateObj = dataRecord.PublishDate;
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
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}

function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.BlogTitle + "' Blog?")) {
        var request = $.ajax({
            url: blogsAPIUrl + "?id=" + dataRecord.uid,
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


