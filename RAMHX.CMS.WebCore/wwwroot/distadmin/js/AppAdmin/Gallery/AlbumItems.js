var editor;
var totalRows = 0;
var listAlbum;
var appGalleryAlbum = "api/CoreModuleGalleryAlbums?albumid=" + getParameterByName("albumid", window.location.href);
var appGalleryAlbumItems = "/api/CoreModuleGalleryAlbumItems";

$(document).ready(function () {
    blockUI();
    loadAll(LoadAlbum, appGalleryAlbum);

    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, height: 400, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnPopCancel"), modalOpacity: 0.01, theme: jqxTheme
    });

    $("#btnAddNew").click(function () { addNew(); });
    $("#popupWindow").on('open', function () {
        $("#txtName").jqxInput('selectAll');
        $("#chkActive").jqxInput('selectAll');
    });
    $("#btnSave").click(function () { saveRow(); });

    $('input[name=uploadRadio]').on("click", function () {
        $("#divyoutube").hide();
        $("#divBrowsePhoto").show();

        if ($('input[name=uploadRadio]:checked').val() == "2") {
            $("#divyoutube").show();
            $("#divBrowsePhoto").hide();
        }
    });
    $("#divyoutube").hide();
});

function addNew() {
    editrow = -1;
    var offset = $("#jqxgrid").offset();
    $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
    $("#txtName").val("");
    $("#imgPhoto").attr('src', '');
    $("#txtItemPath").val("");
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
//    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.ItemPath);
//    $("#txtItemPath").val(dataRecord.ItemPath);
//    $("#popupWindow").jqxWindow('open');
//    $("#chkActive").prop("checked", dataRecord.IsActive);
//    $("#ddlAccess").val(dataRecord.AccessType);
//}

//function deleteRow(row) {
//    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
//    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Album Items?")) {
//        var request = $.ajax({
//            url: appGalleryAlbumItems + "?id=" + dataRecord.uid,
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

function getAlbumName(albumid) {
    var albumName = "";
    $.each(listAlbum, function (index, item) {
        if (item.Id == albumid) {
            albumName = item.Name;
        }
    });
    return albumName;
}

function LoadAlbum(response) {
    listAlbum = response;
    loadAll(BindData, appGalleryAlbumItems + "?albumid=" + getParameterByName("albumid", window.location.href));
    LoadAlbumDroplist();
}

function LoadAlbumDroplist() {
    var dropdown = $('#ddlAlbum');
    dropdown.empty();
    $.each(listAlbum, function (index, item) {
        dropdown.append(
            $('<option>', {
                value: item.Id,
                text: item.Name
            }, '</option>'))
    });
}

function BindData(response) {
    var source = {
        datatype: "array",
        datafields: [
            { name: 'Id', type: 'int' },
            { name: 'DisplayOrder', type: 'int' },
            { name: 'Name', type: 'string' },
            { name: 'IsActive', type: 'bool' },
            { name: 'ItemPath', type: 'string' },
            { name: 'AlbumId', type: 'int' },
            { name: 'AlbumName', type: 'string' },
            { name: 'AccessType', type: 'int' },
            { name: 'UploadType', type: 'int' } 

        ],
        id: 'Id'
    };

    $.each(response, function (index, item) {
        item.AlbumName = getAlbumName(item.AlbumId);
    });
    totalRows = response.length;
    source.localdata = response;
    console.log(response);

    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#jqxgrid").jqxGrid({
        theme: jqxTheme,
        width: '100%',
        source: dataAdapter,
        columnsresize: false,
        altrows: true,
        columns: [
            { text: 'Display', datafield: 'DisplayOrder', width: "90px", cellsrenderer: linkrendererDisplay, cellsalign: "center", align: 'center', sortable: false, filterable: false },
            { text: 'Id', datafield: 'Id', width: "40px", hidden: true },
            { text: 'Album Items', datafield: 'Name', width: "150px" },
            { text: 'Album', datafield: 'AlbumName', width: "180px", hidden: true },

            { text: 'Item Path', datafield: 'ItemPath', width: "400px" },
            {
                text: 'Access Type', datafield: 'AccessType', width: "100px", hidden: true, cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
                    console.log(dataRecord.AccessType);
                    if (dataRecord.AccessType == 1) {
                        return '<div style="text-align: center;">' + "Public" + '</div>';
                    }
                    else {
                        return '<div style="text-align: center;">' + "Private" + '</div>';
                    }
                }
            },

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
        $('#perror').append('> "Album Items" is required <br/>');
        $('#txtName').focus();
    }
    if ($('#perror').html() !== '') {
        $(".errorDiv").removeClass('hide');
        return;
    }
    var uploadType = 1;
    if ($("#hdnAlbumType").val() == "2") {
        if ($('input[name=uploadRadio]:checked').val() == "2") {
            uploadType = 2;
        }
    }
    var path = '';
    if (uploadType == 1) {
        var formData = new FormData("frmUpload");
        formData.append('file', $('[id=updPhoto]')[0].files[0]);
        $.ajax({
            url: '/CoreModule_general/UploadFile?dir=' + '/files/AlbumItemsPhotos',
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
    }
    else if ($('input[name=uploadRadio]:checked').val() == "2") {
        path = $("#txtYouTube").val();
    }
    if (path != '') {
        $("#txtItemPath").val(path);
    }

   
    if (editrow >= 0) {
        
        var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
        console.log("dataRecord-save", dataRecord);
        $("#popupWindow").jqxWindow('hide');

        var dataSave = {
            "Id": dataRecord.Id,
            "Name": $("#txtName").val(),
            "IsActive": $("#chkActive").is(':checked'),
            "ItemPath": $("#txtItemPath").val(),
            "UploadType": uploadType,
            "AlbumId": getParameterByName("albumid", window.location.href),
            "AccessType": $("#ddlAccess").val(),
            "DisplayOrder": dataRecord.DisplayOrder
        };

        var request = $.ajax({
            url: appGalleryAlbumItems + "?id=" + dataRecord.Id,
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
            "ItemPath": path,
            "UploadType": uploadType,
            "AlbumId": getParameterByName("albumid", window.location.href),
            "AccessType": $("#ddlAccess").val()
        };
        var request = $.ajax({
            url: appGalleryAlbumItems,
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
    $('#txtItemPath').val(url);
    $("#dialogFileMngr").dialog("close");
}

function onhypClick() {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
    $("#popupPermissionWindow").removeClass('hide');
    $("#ifrPermissions").attr("src", "/appadmin/gallery/albumpermission?albumitemId=" + dataRecord.Id);
    $("#popupPermissionWindow").jqxWindow("move", $(window).width() / 2 - $("#popupPermissionWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupPermissionWindow").jqxWindow("height") / 2);
    $("#popupPermissionWindow").jqxWindow({ width: 1000, maxWidth: 1050 });
    $("#popupPermissionWindow").jqxWindow({ height: 450 });
    $("#popupPermissionWindow").jqxWindow('open');
}

function CloseDialog() {
    $("#popupPermissionWindow").jqxWindow('close');
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
    $("#imgPhoto").attr('src', '/ImageThumbHandler.ashx?s=150&f=' + dataRecord.ItemPath);
    if (dataRecord.UploadType == 1) {
        $("#uploadMyCmp").prop('checked', true);
        $("#divyoutube").hide();
        $("#divBrowsePhoto").show();
    } else {
        $("#uploadYoutube").prop('checked', true);
        $("#divyoutube").show();
        $("#divBrowsePhoto").hide();
    }
    $("#txtYouTube").val(dataRecord.ItemPath);
    $("#txtItemPath").val(dataRecord.ItemPath);
    $("#popupWindow").jqxWindow('open');
    $("#chkActive").prop("checked", dataRecord.IsActive);
    $("#ddlAccess").val(dataRecord.AccessType);
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}


function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Album Items?")) {
        var request = $.ajax({
            url: appGalleryAlbumItems + "?id=" + dataRecord.uid,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeGalleryAlbumItemsDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, appGalleryAlbumItems + "?albumid=" + getParameterByName("albumid", window.location.href));
}