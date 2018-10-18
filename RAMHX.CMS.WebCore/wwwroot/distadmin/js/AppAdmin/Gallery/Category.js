var editor;
var totalRows = 0;

var appGalleryCategory = "/api/CoreModule_GalleryCategories";

$(document).ready(function () {
    blockUI();
    loadAll(BindData, appGalleryCategory);

    $("#btnAddNew").click(function () {
        editrow = -1;
        var offset = $("#jqxgrid").offset();
        $("#categoryPopupWindow").jqxWindow("move", $(window).width() / 2 - $("#categoryPopupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#categoryPopupWindow").jqxWindow("height") / 2);

        $("#txtName").val("");
        $("#chkActive").prop("checked", true);
        $("#categoryPopupWindow").jqxWindow('open');

    });
});

function BindData(response) {
    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'Name', type: 'string' },
                { name: 'IsActive', type: 'bool' },
                { name: 'DisplayOrder', type: 'string' }
            ],
            id: 'Id'
        };

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
                { text: 'Id', datafield: 'Id', width: "0%", hidden: true },
                { text: 'Display', datafield: 'DisplayOrder', width: "90px", cellsrenderer: linkrendererDisplay, cellsalign: "center", align: 'center', sortable: false, filterable: false },
                { text: 'Name', datafield: 'Name', width: "200px" },
                // { text: 'Active', datafield: 'IsActive', width: "10%" },
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

    $("#categoryPopupWindow").removeClass("hide");
    $("#categoryPopupWindow").jqxWindow({
        width: 350, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });
    $("#categoryPopupWindow").on('open', function () {
        $("#txtName").jqxInput('selectAll');
    });

    $("#btnSave").click(function () {

        if (!$(".errorDiv").hasClass('hide'))
            $(".errorDiv").addClass('hide');
        $('#perror').html('');
        if (!$('#txtName').val()) {
            $('#perror').append('> "Category Name" is required <br/>');
            $('#txtName').focus();
        }

        if ($('#perror').html() !== '') {
            $(".errorDiv").removeClass('hide');
            return;
        }

        if (editrow >= 0) {

            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
            console.log("dataRecord-save", dataRecord);
            $("#categoryPopupWindow").jqxWindow('hide');

            var dataSave = {
                "Id": dataRecord.Id,
                "Name": $("#txtName").val(),
                "IsActive": $('#chkActive').is(':checked'),
                "DisplayOrder": dataRecord.DisplayOrder
            };

            var request = $.ajax({
                url: appGalleryCategory + "?id=" + dataRecord.Id,
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
            $("#categoryPopupWindow").jqxWindow('hide');

            var dataSave = {
                "Id": 0,
                "Name": $("#txtName").val(),
                "IsActive": $('#chkActive').is(':checked')
            };

            var request = $.ajax({
                url: appGalleryCategory,
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
    $("#categoryPopupWindow").jqxWindow("move", $(window).width() / 2 - $("#categoryPopupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#categoryPopupWindow").jqxWindow("height") / 2);
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', editrow);
    console.log("dataRecord", dataRecord);
    $("#txtName").val(dataRecord.Name);
    $("#categoryPopupWindow").jqxWindow('open');
    $("#chkActive").prop("checked", dataRecord.IsActive);
}

var linkrendererDelete = function (row, column, value) {

    var html = "<div class='jqx-grid-cell-middle-align' style='margin-top: 5px;'><a class='' onclick='onhypDeleteClick(" + row + ");' href='javascript:;' >" + '<i class="fa fa-fw fa-trash bg-purple btn-flat" style="padding: 0px;"></i>' + "</a></div>";
    return html;
}


function onhypDeleteClick(row) {
    var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
    if (confirm("Are you sure what to delete '" + dataRecord.Name + "' Category?")) {
        blockUI();
        var request = $.ajax({
            url: appGalleryCategory + "?id=" + dataRecord.uid,
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
    loadAll(RefreshData, "/CoreModule_General/ChangeGalleryCategoryDisplayOrder?id=" + id + "&upDown=" + upDown);
}

function RefreshData(result) {
    loadAll(BindData, appGalleryCategory);
}