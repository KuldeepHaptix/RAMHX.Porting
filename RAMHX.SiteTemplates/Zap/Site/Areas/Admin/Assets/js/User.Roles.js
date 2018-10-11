function CloseDialogUserRole() {
    $("#dialogUserRoles").dialog("close");
}

function AddNewUserRoles(title,userid) {
    $("#dialogUserRoles").dialog({
        modal: true,
        width: 1200,
        height: 650,
        title: title,
        visible: true,
        open: function (ev, ui) {
            $('#iFrmPageRoles').attr('src', '/admin/roles/index?dialog=1&userid=' + userid + '&usertype=user' + '');
        },
        close: function (ev, ui) {
        }
    });
}

$('.userrole').click(function () {
    var id = $(this).attr('id');
    AddNewUserRoles('User Roles', id);
    return false;
});