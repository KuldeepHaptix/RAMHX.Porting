var editor;
var totalRows = 0;
var listCategory;

var appGalleryCategory = "/api/CoreModuleGalleryCategories";
var appGalleryAlbum = "/api/CoreModuleGalleryAlbums";

$(document).ready(function () {
    blockUI();
    loadAll(LoadCategory, appGalleryCategory);
    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, height: 450, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnPopCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () { addNew(); });
    $("#popupWindow").on('open', function () {
        $("#txtName").jqxInput('selectAll');
        $("#chkActive").jqxInput('selectAll');
    });

    $("#btnSave").click(function () { saveRow(); });
});

function addNew() {
    editrow = -1;
    var offset = $("#jqxgrid").offset();
    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
    $("#txtName").val("");
    $("#txtThumbPath").val("");
    $("#ddlAlbumType").val("1");
    $("#ddlCategory").val(listCategory[0].Id);
    $("#chkActive").prop("checked", true);
    $("#popupWindow").jqxWindow('open');
}

//function edit(row) {
//    if (!$(".errorDiv").hasClass('hide'))
//        $(".errorDiv").addClass('hide');
//    $('#perror').html('');
//    editrow = row;
//    var offset = $("#jqxgrid").offset();
//    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
//    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
//    console.log("dataRecord", dataRecord);
//    $("#txtName").val(dataRecord.Name);
//    $("#ddlCategory").val(dataRecord.CategoryId);
//    $("#ddlAlbumType").val(dataRecord.AlbumType);
//    $("#txtThumbPath").val(dataRecord.ThumbnailPath);
//    $("#imgPhoto").attr('src','/ImageThumbHandler.ashx?s=150&f='+ dataRecord.ThumbnailPath);
//    $("#popupWindow").jqxWindow('open');
//    $("#chkActive").prop("checked", dataRecord.IsActive);
//}

//function deleteRow(row) {
//    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
//    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Album?")) {
//        var request = $.ajax({
//            url: appGalleryAlbum + "?id=" + dataRecord.uid,
//            cache: false,
//            dataType: 'json',
//            contentType: 'application/json; charset=utf-8',
//            type: 'DELETE',
//            success: function () {
//                window.location = window.location;
//            },
//            error: function (jqXHR, exception) {
//                console.log(jqXHR);
//                console.log(exception);
//            }
//        });
//    }
//}

function getCategoryName(categoryid) {
    var categoryName = "";
    $.each(listCategory, function (index, item) {
        if (item.Id == categoryid) {
            categoryName = item.Name;
        }
    });
    return categoryName;
}

function LoadCategory(response) {
    listCategory = response;
    loadAll(BindData, appGalleryAlbum);
    LoadCategoryDroplist();
}

function LoadCategoryDroplist() {
    var dropdown = $('#ddlCategory');
    dropdown.empty();
    $.each(listCategory, function (index, item) {
        dropdown.append(
            $('<option>', {
                value: item.Id,
                text: item.Name
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
            { name: 'Name', type: 'string' },
            { name: 'IsActive', type: 'bool' },
            { name: 'ThumbnailPath', type: 'string' },
            { name: 'CategoryId', type: 'int' },
            { name: 'AlbumType', type: 'int' },
            { name: 'AlbumTypeName', type: 'string' },
            { name: 'CategoryName', type: 'string' },
            


        ],
        id: 'Id'
    };

    $.each(response, function (index, item) {
        item.CategoryName = getCategoryName(item.CategoryId);
        item.AlbumTypeName = "Photos";
        if (item.AlbumType == 2) {
            item.AlbumTypeName = "Video";
        }
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
            { text: 'Type', datafield: 'AlbumTypeName', width: "150px" },
            { text: 'Album', datafield: 'Name', width: "150px" },
            { text: 'Thumbnail Path', datafield: 'ThumbnailPath', width: "350px" },
            {
                text: 'Active', datafield: 'IsActive', width: "120px", cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
                    console.log(dataRecord.IsActive);
                    if (dataRecord.IsActive == true) {
                        return '<div style="text-align: center;">' + "Yes" + '</div>';
                    }
                    else {
                        return '<div style="text-align: center;">' + "No" + '</div>';
                    }
                }
            },
            { text: 'Edit', datafield: 'Edit', width: "55px", cellsrenderer: linkrendererEdit, cellsalign: "center", align: 'center' },
            { text: 'Delete', datafield: 'Delete', width: "55px", cellsrenderer: linkrendererDelete, cellsalign: "center", align: 'center' },
            {
                text: '', datafield: 'Photos', width: "75px", columntype: 'button', cellsalign: "Photos", align: 'Photos',
                cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
                    if (dataRecord.AlbumType == 1) {
                        return "Photos";
                    } else {
                        return "Video";
                    }
                },
                buttonclick: function (row) {

                    if (!$(".errorDiv").hasClass('hide'))
                        $(".errorDiv").addClass('hide');
                    $('#perror').html('');
                    editrow = row;
                    var offset = $("#jqxgrid").offset();
                    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
                    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);

                    console.log("dataRecord", dataRecord);
                    onhypClick();
                }
            },
        ],
        filterable: true,
        sortable: true,
        pageable: false,
        pagermode: 'simple',
        selectionmode: 'singlerow'
    });
    $.unblockUI();
}
function saveRow() {
    if (!$(".errorDiv").hasClass('hide'))
        $(".errorDiv").addClass('hide');

    $('#perror').html('');

    if (!$('#txtName').val()) {
        $('#perror').append('> "Album" is required <br/>');
        $('#txtName').focus();
    }
    if ($('#perror').html() !== '') {
        $(".errorDiv").removeClass('hide');
        return;
    }

    var path = '';
    var formData = new FormData("frmUpload");
    formData.append('file', $('[id=updPhoto]')[0].files[0]);
    $.ajax({
        url: '/CoreModule_general/UploadFile?dir=' + '/files/albums',
        data: formData,
        type: 'POST',
        async: false,
        contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
        processData: false, // NEEDED, DON'T OMIT THIS
        success: function (result) {
            path= result.path;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR);
            console.log(exception);
            path =  "";
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
            "Name": $("#txtName").val(),
            "IsActive": $("#chkActive").is(':checked'),
            "AlbumType": $("#ddlAlbumType").val(),
            "ThumbnailPath": $("#txtThumbPath").val(),
            "CategoryId": $("#ddlCategory").val(),
            "DisplayOrder": dataRecord.DisplayOrder
        };

        var request = $.ajax({
            url: appGalleryAlbum + "?id=" + dataRecord.Id,
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
            "Name": $("#txtName").val(),
            "IsActive": $("#chkActive").is(':checked'),
            "AlbumType": $("#ddlAlbumType").val(),
            "ThumbnailPath": $("#txtThumbPath").val(),
            "CategoryId": $("#ddlCategory").val()
        };
        var request = $.ajax({
            url: appGalleryAlbum,
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
    $('#txtThumbPath').val(url);
    $("#dialogFileMngr").dialog("close");
}

function onhypClick() {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
    window.location = "/appadmin/gallery/albumitems?albumid=" + dataRecord.Id;
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
    $("#ddlCategory").val(dataRecord.CategoryId);
    $("#ddlAlbumType").val(dataRecord.AlbumType);
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.ThumbnailPath);
    $("#txtThumbPath").val(dataRecord.ThumbnailPath);
    $("#popupWindow").jqxWindow('open');
    $("#chkActive").prop("checked", dataRecord.IsActive);
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}


function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Album?")) {
        var request = $.ajax({
            url: appGalleryAlbum + "?id=" + dataRecord.uid,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeGalleryAlbumDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, appGalleryAlbum);
}