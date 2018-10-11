$(document).ready(function () {

    //Form validation
    $("#frmPageDetail").validate({
        rules: {
            PageOrder: {  /* PageOrder is fild name attribute */
                required: true,
                number: true
            }
        }
    });

    var pageid = 0;
    $("#PageManager").jstree({
        "core": {
            "animation": 0,
            "check_callback": true,
            "themes": { "stripes": true, 'responsive': true },
            "data": {
                "url":
                    function (node) {
                        if (node.id === "#") {
                            return '/Admin/Pages/GetRootPage';
                        }
                        else {
                            return '/Admin/Pages/GetPrimaryChildrens';
                        };
                    },
                'type': 'POST',
                'dataType': 'json',
                'contentType': 'application/json',
                'cache': false,
                'icon': 'jstree-file',
                'data':
                    function (node) {
                        if (node.id === "#") {
                            return JSON.stringify({
                                pageid: node.id === "#" || node.id === "0" ? pageid : node.id
                            });
                        }
                        else {
                            return JSON.stringify({
                                pageid: node.id
                            });
                        }
                    }
                ,
                'success': function (data) {
                }
            }
        }
        ,
        "plugins": ["contextmenu", "dnd", "search", "state", "types", "wholerow"]
    }).on('move_node.jstree', function (event, data) {
        movenode(data.node.id, data.node.parent, data.old_parent, data.position);
    }).on('cut.jstree', function (event, data) {
    }).on('copy.jstree', function (event, data) {
    }).on('paste.jstree', function (event, data) {
        paste(data);
    }).on('delete_node.jstree', function (event, data) {
        DeleteSelectedPageCilds(data);
    }).on('rename_node.jstree', function (event, data) {
        renamenode(data);
    }).on('create_node.jstree', function (event, data) {
    }).on("select_node.jstree", function (e, data) {
        $('#hdnPageID').val(data.node.id);
        $('#hdnParentID').val(data.node.parent);
        if (data.node.text == "Root" || data.node.id == "0") {
            $(".pagedetails").each(function () {
                $(this).addClass('hide');
            });
        }
        else {
            $(".pagedetails").each(function () {
                $(this).removeClass('hide');
            });
        }
        editNode(data.node.id);
    });
})

function addPage(parentId) {
    $('#PageId').val("0");
    $('#ParentPageID').val(parentId);
    $('#PageOrder').val("");
    $('#PageName').val("");
    $('#PageCode').val("");
    $('#Description').val("");
    $('#PageUrl').val("");
    $('#CreatedDate').val("");
    $('#CreatedByUserId').val("");
    $('#ModifiedDate').val("");
    $('#ModifiedByUserId').val("");
    $('#PageTitle').val("");
    $('#PageMetaKeywords').val("");
    $('#PageMetaDescription').val("");
    $('#PageLayoutPath').val("");
}

function editPage(pageid) {
    var pid = pageid;
    if (pid == NaN)
        pid = 0
    $.ajax({
        type: "GET",
        url: "/admin/Pages/getbyid?id=" + pid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log('response',response);
            if (response.Page == null) {
                $(".pagedetails").each(function () {
                    $(this).addClass('hide');
                });

                return false;
            }
            else
            {
                $(".pagedetails").each(function () {
                    $(this).removeClass('hide');
                });
            }
                

            var ul = document.getElementById("sortable");
            var li = "";
            for (var i = 0; i < response.HtmlModules.length; i++) {
                var o = response.HtmlModules[i];
                li = li + '<li class="ui-state-default" id="' + o.HTMLModuleId + '"><span class=""></span>' + o.HtmlModuleCode + '<i class="rmv" hmId="' + o.HTMLModuleId + '" style="float: right;cursor: pointer;">&nbsp;&nbsp;Remove&nbsp;&nbsp;</i><i class="edit" hmId="' + o.HTMLModuleId + '" style="float: right;cursor: pointer;">Edit&nbsp;&nbsp;|</i></li>';
            }
            ul.innerHTML = li;
            $('.rmv').on("click", function (e) {
                var id = $(this).attr("hmId");
                console.log("title", id);
                console.log("E", e);
                $('#' + id).remove();
            });

            $('.edit').on("click", function (e) {
                var currhmid = $(this).attr("hmid");
                $("#dialogHTMLModule").dialog({
                    modal: true,
                    width: 1200,
                    height: 650,
                    title: 'Edit Content',
                    visible: true,
                    open: function (ev, ui) {
                        $('#iFrmHTMLModule').attr('src', '/Admin/HtmlModules/Edit/' + currhmid + '?pd=1&dialog=1');
                    },
                    close: function (ev, ui) {
                    }
                });
            });

            var roleul = document.getElementById("sortablePageRoles");
            var roleli = "";
            for (var i = 0; i < response.PageRoles.length; i++) {
                var o = response.PageRoles[i];
                roleli = roleli + '<li class="ui-state-default" id="' + o.Id + '"><span class=""></span>' + o.Name + '<i class="rmvrole" rolehmId="' + o.Id + '" style="float: right;cursor:pointer;">&nbsp;&nbsp;Remove&nbsp;&nbsp;</i></li>';
            }
            roleul.innerHTML = roleli;
            $('.rmvrole').on("click", function (e) {
                var id = $(this).attr("rolehmId");
                console.log("title", id);
                console.log("E", e);
                $('#' + id).remove();
            });

            var roleul = document.getElementById("sortableTemplate");
            var roleli = "";
            for (var i = 0; i < response.Page.cms_Templates.length; i++) {
                var o = response.Page.cms_Templates[i];
                roleli = roleli + '<li class="ui-state-default" id="' + o.TemplateId + '"><span class=""></span>' + o.TemplateName + '<i class="rmvTemplate" tmpltId="' + o.TemplateId + '" style="float: right;cursor:pointer;">&nbsp;&nbsp;Remove&nbsp;&nbsp;</i></li>';
            }
            roleul.innerHTML = roleli;
            $('.rmvTemplate').on("click", function (e) {
                var id = $(this).attr("tmpltId");
                console.log("title", id);
                console.log("E", e);
                $('#' + id).remove();
            });

            $('#PageId').val(response.Page.PageID);
            $('#ParentPageID').val(response.Page.ParentPageID);
            $('#PageOrder').val(response.Page.PageOrder);
            $('#PageName').val(response.Page.PageName);
            $('#PageCode').val(response.Page.PageCode);
            $('#Description').val(response.Page.Description);
            $('#PageUrl').val(response.Page.PageUrl);
            $('#CreatedDate').val(response.Page.CreatedDate);
            $('#CreatedByUserId').val(response.Page.CreatedByUserId);
            $('#ModifiedDate').val(response.Page.ModifiedDate);
            $('#ModifiedByUserId').val(response.Page.ModifiedByUserId);
            $('#PageTitle').val(response.Page.PageTitle);
            $('#PageMetaKeywords').val(response.Page.PageMetaKeywords);
            $('#PageMetaDescription').val(response.Page.PageMetaDescription);
            $('#PageLayoutPath').val(response.Page.PageLayoutPath);
            $('#PageFullUrl').val(response.Page.FullPageUrl);
            $("#PageFullUrlLink").attr("href", response.Page.FullPageUrl);
        },
        failure: function (response) {
            console.log(response)
            alert('Oops, Error occured while getting page data. Please try again.');
        }
    });
}

function savePage() {
    if (!$('#frmPageDetail').valid()) {
        return;
    }
    var slvals = []
    $('input:checkbox[name=nameofyourcheckbox]:checked').each(function () {
        slvals.push($(this).val())
    });


    $.blockUI();

    var pageModel = {};

    var page = {};
    page.PageID = $('#PageId').val();
    page.ParentPageID = $('#ParentPageID').val();
    page.PageOrder = $('#PageOrder').val();
    page.PageName = $('#PageName').val();
    page.PageCode = $('#PageCode').val();
    page.Description = $('#Description').val();
    page.PageUrl = $('#PageUrl').val();
    page.CreatedDate = $('#CreatedDate').val();
    page.CreatedByUserId = $('#CreatedByUserId').val();
    page.ModifiedDate = $('#ModifiedDate').val();
    page.ModifiedByUserId = $('#ModifiedByUserId').val();
    page.PageTitle = $('#PageTitle').val();
    page.PageMetaKeywords = $('#PageMetaKeywords').val();
    page.PageMetaDescription = $('#PageMetaDescription').val();
    page.PageLayoutPath = $('#PageLayoutPath').val();
    console.log(page)

    var hms = []; //[{ 'HTMLModuleId': '' }, { 'HTMLModuleId': '' }];
    $.each($('#sortable li'), function (i, item) {
        hms.push({ 'HTMLModuleId': item.id })
    });

    var prl = [];
    $.each($('#sortablePageRoles li'), function (i, item) {
        prl.push({ 'Id': item.id })
    });


    var tmpl = [];
    $.each($('#sortableTemplate li'), function (i, item) {
        tmpl.push({ 'Id': item.id })
    });

    pageModel = { Page: page, HtmlModules: hms, PageRoles: prl, PageTemplates: tmpl };

    $.ajax({
        type: "POST",
        url: "/admin/Pages/SaveData",
        data: JSON.stringify(pageModel),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#PageManager').jstree('refresh');
            //console.log('saved', response);
            //alert('Saved');
            bootbox.alert("Page datails saved!", function () {
            });
        },
        failure: function (response) {
            console.log('error', response);
            //alert('Oops, Error occured while saving data. Please try again.');
        },
        complete: function () {
            $.unblockUI();
        }
    });
}

$('#savePagedetail').click(function () { savePage(); });

function paste(data) {
    $.ajax({
        type: 'POST',
        url: '/Admin/Pages/CutCopyPage',
        data: {
            "pageid": data.node[0].id, "parentpageid": data.parent, "mode": data.mode
        },
        success: function (folderView) {
            $('#PageManager').jstree('refresh');
        }
    });
}

function movenode(pageID, ParentPageID, old_parent, position) {
    //if (confirm('Are you sure you want to move page?')) {
    bootbox.confirm("Are you sure you want to move page?", function (result) {
        if (result) {
            $.ajax({
                type: 'POST',
                url: '/Admin/Pages/MoveNode',
                data: {
                    "pageid": pageID, "parentpageid": ParentPageID, "oldParent": old_parent, "position": position
                },
                success: function (folderView) {
                    $('#PageManager').jstree('refresh');
                }
            });
        }
    });
}

function DeleteSelectedPageCilds(data) {
    //console.log("delete", data);
    //if (confirm('Are you sure you want to delete "' + data.node.text + '" page? This will delete its subpages as well.\n\nPlease Click OK to delete or Cancel to abort the operation.')) {
    bootbox.confirm('Are you sure you want to delete "' + data.node.text + '" page? This will delete its subpages as well.\n\nPlease Click OK to delete or Cancel to abort the operation.', function (result) {
        if (result) {
            var pageID = $('#hdnPageID').val();
            $.ajax({
                type: 'POST',
                url: '/Admin/Pages/DeleteChildren',
                data: {
                    "pageid": data.node.id
                },
                success: function (folderView) {
                    $('#PageManager').jstree('refresh');
                }
            });
        }
        else {
            $('#PageManager').jstree('refresh');
        }
    });
}
function editNode() {
    var pageID = $('#hdnPageID').val();
    editPage(pageID);
}



function renamenode(data) {
    console.log("renamenode", data);
    $.ajax({
        type: 'POST',
        url: '/Admin/Pages/RenamePage',
        data: {
            "pageid": data.node.id, "newPageName": data.node.text, "parentpageid": data.node.parent, "position": data.node.position
        },
        success: function (folderView) {
            $('#PageManager').jstree('refresh');
        }
    });

}

function createnode(data) {
    console.log("createnode", data);
    $.ajax({
        type: 'POST',
        url: '/Admin/Pages/CreatePage',
        data: {
            "parentpageid": data.node.parent, "position": data.node.position
        },
        success: function (folderView) {
            $('#PageManager').jstree('refresh');
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
    $('#PageLayoutPath').val(url);
    $("#dialogFileMngr").dialog("close");
}

//Shortable 
$("#sortable").sortable();
$("#sortable").disableSelection();

$("#sortablePageRoles").sortable();
$("#sortablePageRoles").disableSelection();

$("#sortableTemplate").sortable();
$("#sortableTemplate").disableSelection();

function AddNewHtmlModule(title) {
    $("#dialogHTMLModule").dialog({
        modal: true,
        width: 1200,
        height: 650,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmHTMLModule').attr('src', '/admin/HtmlModules/Index?dialog=1&pageid=' + $('#hdnPageID').val() + '');
        },
        close: function (ev, ui) {
        }
    });
}

function CloseDialogHTMLModule() {
    $("#dialogHTMLModule").dialog("close");
    editNode();
}

function CloseDialogPageRole() {
    $("#dialogPageRoles").dialog("close");
    editNode();
}

function CloseDialogTemplateModule() {
    $("#dialogTemplateModule").dialog("close");
    editNode();
}
function CloseDialogPageTemplate() {
    $("#dialogPageTemplate").dialog("close");
    editNode();
}


function AddNewRoles(title) {
    $("#dialogPageRoles").dialog({
        modal: true,
        width: 1200,
        height: 650,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmPageRoles').attr('src', '/admin/roles/index?dialog=1&pageid=' + $('#hdnPageID').val() + '&pagetype=page' + '');
        },
        close: function (ev, ui) {
        }
    });
}

function PageTemplate(title) {
    $("#dialogPageTemplate").dialog({
        modal: true,
        width: 1400,
        height: 650,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmPageTemplate').attr('src', '/admin/Pages/PageFieldValues?dialog=1&pageid=' + $('#hdnPageID').val() + '&pagetype=page' + '');
        },
        close: function (ev, ui) {
        }
    });
}

function TemplateModuleDialog(title) {
    $("#dialogTemplateModule").dialog({
        modal: true,
        width: 1200,
        height: 650,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmTemplateModule').attr('src', '/admin/Templates/TemplateList?dialog=1&pageid=' + $('#hdnPageID').val() + '');
        },
        close: function (ev, ui) {
        }
    });
}

function CloseDialogUserRole() {
    $("#dialogPageRoles").dialog("close");
    editNode();
}

$(document).ready(function () {
    $("#rolecreate").validate({
    });
});