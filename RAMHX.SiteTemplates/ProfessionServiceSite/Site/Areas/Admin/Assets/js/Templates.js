$(document).ready(function () {
    $("#divTemplateFieldsDetail").hide();
    //Form validation
    $("#frmPageDetail").validate({
        rules: {
            PageOrder: {  /* PageOrder is fild name attribute */
                required: true,
                number: true
            }
        }
    });

    var templateid = 0;
    $("#templates").jstree({
        "core": {
            "animation": 0,
            "check_callback": true,
            "themes": { "stripes": true, 'responsive': true },
            "data": {
                "url":
                    function (node) {
                        console.log('node', node);
                        if (node.id === "#") {
                            return '/Admin/Templates/GetRootTemplate';
                        }
                        else if (node.id === "00000000-0000-0000-0000-000000000000") {
                            return '/Admin/Templates/GetAllTemplates';
                        }
                        else {
                            return '/Admin/Templates/GetTemplatesField';
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
                                templateid: node.id === "#" || node.id === "00000000-0000-0000-0000-000000000000" ? templateid : node.id
                            });
                        }
                        else {
                            return JSON.stringify({
                                templateid: node.id
                            });
                        }
                    }
                ,
                'success': function (data) {
                }
            }
        }
        ,
        //'contextmenu': {
        //    'items': customMenu
        //},
        "plugins": ["contextmenu", "dnd", "search", "state", "types", "wholerow"]
    }).on('move_node.jstree', function (event, data) {
        //movenode(data.node.id, data.node.parent, data.old_parent, data.position);
    }).on('cut.jstree', function (event, data) {
    }).on('copy.jstree', function (event, data) {
    }).on('paste.jstree', function (event, data) {
        paste(data);
    }).on('delete_node.jstree', function (event, data) {
        DeleteSelectedPageCilds(data);
    }).on('rename_node.jstree', function (event, data) {
        renamenode(data);
        if (data.node.text == "Templates" || data.node.parent == "00000000-0000-0000-0000-000000000000") {
            $('#TemplateName').val(data.node.text);
            $('#TemplateCode').val(data.node.text);
            $('#Description').val(data.node.text);
            $("#divTemplateDetail").show();
            $("#divTemplateFieldsDetail").hide();
        }
        else {
            $("#divTemplateDetail").hide();
            $("#divTemplateFieldsDetail").show();
        }
    }).on('create_node.jstree', function (event, data) {
    }).on("select_node.jstree", function (e, data) {
        console.log("data.node.id", data.node.id);
        console.log("data.node.parent", data.node.parent);
        $('#hdnTemplateId').val(data.node.id);
        $('#hdnParentID').val(data.node.parent);

        if (data.node.parent == "00000000-0000-0000-0000-000000000000") {
            $("#divTemplateDetail").show();
            $("#divTemplateFieldsDetail").hide();
        }
        else if (data.node.parent == "#") {
            $("#divTemplateDetail").hide();
            $("#divTemplateFieldsDetail").hide();
        } else {
            $("#divTemplateDetail").hide();
            $("#divTemplateFieldsDetail").show();
        }

        if (data.node.parent == "00000000-0000-0000-0000-000000000000" && data.node.id != "00000000-0000-0000-0000-000000000000") {
            editNode(data.node.id);
        } else {
            editChildNode(data.node.parent, data.node.id)
        }

        //if (data.node.parent > 0 && data.node.id > 0) {
        //    editChildNode(data.node.parent, data.node.id)
        //}
    });
})

function customMenu(node) {
    console.log('node', node);
    var tree = $("#templates").jstree(true);
    return {
        "Create": {
            "separator_before": false,
            "separator_after": false,
            "label": "Create",
            "action": function (obj) {
                $node = tree.jstree('create_node', $node);
                tree.jstree('edit', $node);
            }
        },
        "Rename": {
            "separator_before": false,
            "separator_after": false,
            "label": "Rename",
            "action": function (obj) {
                tree.jstree('edit', $node);
            }
        },
        "Remove": {
            "separator_before": false,
            "separator_after": false,
            "label": "Remove",
            "action": function (obj) {
                tree.jstree('delete_node', $node);
            }
        }
    };
}

function editNode() {
    var tempId = $('#hdnTemplateId').val();
    editPage(tempId);
}

function editPage(tempid) {
    console.log(tempid);
    var tid = tempid;
    if (tid == NaN)
        tid = 0
    $.ajax({
        type: "GET",
        url: "/admin/Templates/GetTemplatesById?id=" + tid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#TemplateName').val(response.TemplateName);
            $('#TemplateCode').val(response.TemplateCode);
            $('#Description').val(response.Description);
            $('#hdnTemplateId').val(response.TemplateId);
        },
        error: function (response) {
            console.log(response)
            // alert(response.responseText);
        }
    });
}

function editChildNode(tid, ftid) {
    editFieldTemplate(tid, ftid);
}

function editFieldTemplate(templateid, tempFieldid) {
    console.log(templateid);
    console.log(tempFieldid);
    var tid = templateid;
    var ftid = tempFieldid;
    if (tid == NaN)
        tid = 0

    if (ftid == NaN)
        ftid = 0
    $.ajax({
        type: "GET",
        url: "/admin/Templates/GetTemplatesFieldById?templateid=" + tid + "&fieldTemplateId=" + ftid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            $('#FieldName').val(response.FieldName);
            $('#FieldTypeId').val(response.FieldTypeId);
            $('#FieldDisplayOrder').val(response.FieldDisplayOrder);
            $('#DefaultValue').val(response.DefaultValue);
            $('#hdnTemplateFieldId').val(response.TemplateFieldId);
            $('#hdnTemplateId').val(response.TemplateId);
        },
        failure: function (response) {
            console.log(response)
            alert('Oops, Error occured while getting page data. Please try again.');
        }
    });
}

function savePage() {
    if (!$('#frmTemplateDetail').valid()) {
        return;
    }
    console.log("hdnTemplateId", hdnTemplateId);
    $.blockUI();

    var template = {};
    template.TemplateId = $('#hdnTemplateId').val();
    template.TemplateName = $('#TemplateName').val();
    template.TemplateCode = $('#TemplateCode').val();
    template.Description = $('#Description').val();
    template.CreatedDate = $('#CreatedDate').val();
    template.CreatedByUserId = $('#CreatedByUserId').val();
    template.ModifiedDate = $('#ModifiedDate').val();
    template.ModifiedByUserId = $('#ModifiedByUserId').val();
    console.log(template)

    $.ajax({
        type: "POST",
        url: "/admin/Templates/SaveTemplate",
        data: JSON.stringify(template),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#templates').jstree('refresh');
            bootbox.alert("Templates datails saved!", function () {
            });
        },
        failure: function (response) {
            console.log('error', response)
        },
        complete: function () {
            $.unblockUI();
        }
    });

}

$('#saveTemplatedetail').click(function () {
    savePage();
});

$('#saveTemplateFieldsdetail').click(function () {

    if (!$('#frmTemplateFieldsDetail').valid()) {
        return;
    }
    $.blockUI();

    var fieldTemplate = {};
    fieldTemplate.TemplateId = $('#hdnTemplateId').val();
    fieldTemplate.TemplateFieldId = $('#hdnTemplateFieldId').val();
    fieldTemplate.FieldName = $('#FieldName').val();
    fieldTemplate.FieldTypeId = $('#FieldTypeId').val();
    fieldTemplate.FieldDisplayOrder = $('#FieldDisplayOrder').val();
    fieldTemplate.DefaultValue = $('#DefaultValue').val();
    fieldTemplate.TemplateId = $('#hdnTemplateId').val();
    fieldTemplate.CreatedDate = $('#CreatedDate').val();
    fieldTemplate.CreatedByUserId = $('#CreatedByUserId').val();
    fieldTemplate.ModifiedDate = $('#ModifiedDate').val();
    fieldTemplate.ModifiedByUserId = $('#ModifiedByUserId').val();
    console.log(fieldTemplate)

    $.ajax({
        type: "POST",
        url: "/admin/Templates/SaveFieldTemplate",
        data: JSON.stringify(fieldTemplate),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#templates').jstree('refresh');
            bootbox.alert("FieldTemplate datails saved!");
        },
        failure: function (response) {
            console.log('error', response)
        },
        complete: function () {
            $.unblockUI();
        }
    });

});

function DeleteSelectedPageCilds(data) {
    bootbox.confirm('Are you sure you want to delete "' + data.node.text + '" ? This will delete its subpages as well.\n\nPlease Click OK to delete or Cancel to abort the operation.', function (result) {
        if (result) {
            var templateId = $('#hdnTemplateId').val();
            $.ajax({
                type: 'POST',
                url: '/Admin/Templates/DeleteChildren',
                data: {
                    "tempid": templateId
                },
                success: function (folderView) {
                    $('#' + data.instance.element[0].id).jstree('refresh');
                }
            });
        }
        else {
            $('#' + data.instance.element[0].id).jstree('refresh');
        }
    });
}

function TemplateModuleDialog(title) {
    $("#dialogHTMLModule").dialog({
        modal: true,
        width: 1200,
        height: 650,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmHTMLModule').attr('src', '/admin/Templates/TemplateList?dialog=1&templatefieldid=' + $('#hdnTemplateFieldId').val() + '');
        },
        close: function (ev, ui) {
        }
    });
}

function renamenode(data) {
    console.log("renamenode", data);
    $.ajax({
        type: 'POST',
        url: '/Admin/Templates/RenameTemplate',
        data: {
            "templateDetails": data.node.text, "templatefieldid": data.node.id, "templateid": data.node.parent
        },
        success: function (folderView) {
            $('#templates').jstree('refresh');
        }
    });

}

function paste(data) {
    $.ajax({
        type: 'POST',
        url: '/Admin/Templates/CutCopyTemplate',
        data: {
            "pageid": data.node[0].id, "parentpageid": data.parent, "mode": data.mode
        },
        success: function (folderView) {
            $('#templates').jstree('refresh');
        }
    });
}