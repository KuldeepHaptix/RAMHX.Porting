function CloseDialogUserRole() {
    $("#dialogUserRoles").dialog("close");
}

function AddNewUserRoles(title, userid) {
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


$(document).ready(function () {

    $("#popupWindow").removeClass("hide");
    $("#popupWindow").jqxWindow({
        width: 350, resizable: false, isModal: true, autoOpen: false, cancelButton: $("#btnCancel"), modalOpacity: 0.01, theme: jqxTheme
    });
    $("#chkShowPwd").on("click", function () {
        if ($(this).prop("checked")) {
            $("#txtPassword").attr("type", "text");
            $("#txtConfirmPassword").attr("type", "text");
        }
        else {
            $("#txtPassword").attr("type", "password");
            $("#txtConfirmPassword").attr("type", "password");
        }
    });

    $("#btnGenPwd").on("click", function () {
        var pwd = Math.random().toString(36).slice(-8) + "#X3";
        $("#txtPassword").val(pwd);
        $("#txtConfirmPassword").val(pwd);
    });

    $("#btnViewAll").click(function () {
        $.blockUI();

        loadData();

    });

    $("#btnAddNewuser").click(function () {
        editrow = -1;
        var offset = $("#UserSearchjqxgrid").offset();
        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);

        // get the clicked row's data and initialize the input fields.
        $("#txtUsername").val("");
        $("#txtEmail").val("");
        $("#txtPassword").val("");
        $("#txtConfirmPassword").val("");
        $("#txtFirstName").val("");
        $("#txtLastName").val("");
        $("#txtAddressName").val(""),
            $("#txtCity").val("");
        $("#txtMobile").val("");

        $("#editHide").removeClass('hide');
        $("#txtUsername").prop('readonly', false);

        // show the popup window.
        $("#popupWindow").jqxWindow('open');
    });

    $('#txtSearchCustomer').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            $("#btnViewResult").click();
        }
    });

    $("#btnViewResult").click(function () {
        checkLength();
    });
    $('#txtSearchCustomer').focus();

    $("#btnChangePassword").click(function () {
        var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', editrow);

        $.ajax({
            url: "/admin/account/AppAdminResetPassword?userid=" + dataRecord.Id + "&password=" + $("#txtRPassword").val() + "&confirmPassword=" + $("#txtRConfirmPassword").val(),
            type: "Post",
            dataType: 'json',
            cache: false,
            processData: false,
            contentType: false,
            success: function (result) {

                console.log(result);
                if (result.status == "fail") {
                    $(".errorDivR").removeClass('hide');
                    $('#ploginerrorR').html('');
                    if (result.message != undefined && result.message != '') {

                        $('#ploginerrorR').append("> " + result.message);
                        $.each(result.message, function (index, itmErr) {
                            $('#ploginerrorR').append("> " + itmErr + "<br/>");
                        });
                    } else {
                        $.each(result.result.Errors, function (index, itmErr) {
                            $('#ploginerrorR').append("> " + itmErr + "<br/>");
                        });
                    }

                } else {
                    $(".errorDivR").addClass('hide');
                    $(".successdivR").removeClass('hide');
                    $("#dForm").addClass('hide');
                }
                $.unblockUI();
            },
            error: function (jqXHR, exception) {
                $.unblockUI();
                console.log(jqXHR);
                console.log(exception);
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                alert(msg);
            }
        });
    })

});

function checkLength() {
    var textbox = document.getElementById("txtSearchCustomer");
    if (textbox.value.length < 3) {
        alert("Minimum 3 Character Require");
        return;
    }
    else {
        $.blockUI();

        loadData();
    }
}

function loadData() {

    var fltUrl = "/admin/account/GetUserList?keywords=" + $('#txtSearchCustomer').val() + "&roleid=" + getParameterByName('roleid');
    loadAll(BindData, fltUrl);
}

function BindData(response) {
    console.log(response);
    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'string' },
                { name: 'UserName', type: 'string' },
                { name: 'Email', type: 'string' },
                { name: 'FirstName', type: 'string' },
                { name: 'LastName', type: 'string' },
                { name: 'AssignedRoles', type: 'string' },
                { name: 'Address', type: 'string' },
                { name: 'City', type: 'string' },
                { name: 'Mobile', type: 'string' },
            ],
            id: 'Id'
        };
    var hideRole = false;
    if (isAdminUser == 1) {
        hideRole = false;
    }
    var sr = 1;
    $.each(response.Users, function (idxBL, itmBL) {
        itmBL.SrNo = sr++;
        itmBL.Id = itmBL.Users.Id;
        itmBL.UserName = itmBL.Users.UserName;
        itmBL.Email = itmBL.Users.Email;
        itmBL.FirstName = itmBL.Users.FirstName;
        itmBL.LastName = itmBL.Users.LastName;
        itmBL.Address = itmBL.Users.Address;
        itmBL.City = itmBL.Users.City;
        itmBL.Mobile = itmBL.Users.Mobile;
    });

    source.localdata = response.Users;
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#UserSearchjqxgrid").jqxGrid(
        {
            theme: jqxTheme,
            width: '100%',
            source: dataAdapter,
            columnsresize: false,
            altrows: true,
            columns: [
                { text: 'User#', datafield: 'Id', width: "300px" },
                { text: 'User Name', datafield: 'UserName', width: "200px" },
                { text: 'Email', datafield: 'Email', width: "200px" },
                { text: 'First Name', datafield: 'FirstName', width: "200px", },
                { text: 'Last Name', datafield: 'LastName', width: "100px" },
                { text: 'Roles', datafield: 'AssignedRoles', width: "100px", hidden:hideRole },
                {
                    text: '', datafield: 'Edit', width: "75px", columntype: 'button', cellsrenderer: function () {
                        return "Edit";
                    },
                    buttonclick: function (row) {
                        if (!$(".errorDiv").hasClass('hide'))
                            $(".errorDiv").addClass('hide');
                        $('#perror').html('');
                        editrow = row;
                        var offset = $("#UserSearchjqxgrid").offset();
                        $("#popupWindow").jqxWindow("move", $(window).width() / 2 - $("#popupWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupWindow").jqxWindow("height") / 2);
                        var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', editrow);
                        console.log("dataRecord", dataRecord);
                        $("#editHide").addClass('hide');

                        $("#txtUsername").val(dataRecord.UserName).prop('readonly', true);
                        $("#txtEmail").val(dataRecord.Email);
                        $("#txtFirstName").val(dataRecord.FirstName);
                        $("#txtLastName").val(dataRecord.LastName);
                        $("#txtAddressName").val(dataRecord.Address);
                        $("#txtCity").val(dataRecord.City);
                        $("#txtMobile").val(dataRecord.Mobile);

                        $("#popupWindow").jqxWindow('open');
                    }
                },

                {
                    text: '', datafield: 'Roles', width: "75px", columntype: 'button', hidden: hideRole, cellsrenderer: function () {
                        return 'Roles';
                    }, buttonclick: function (row) {
                        var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', row);
                        AddNewUserRoles('User Roles', dataRecord.Id);
                    },

                },

                {
                    text: '', datafield: 'Delete', width: "100px", columntype: 'button', cellsrenderer: function () {
                        return "Delete";
                    }, buttonclick: function (row) {
                        var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', row);
                        if (confirm("Are you sure what to delete '" + dataRecord.FirstName + "'?")) {
                            var request = $.ajax({
                                url: "/Admin/Account/DeleteAccount?userid=" + dataRecord.Id,
                                cache: false,
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                type: 'GET',
                                success: function () {

                                    loadData();
                                },
                                error: function (jqXHR, exception) {
                                    console.log(jqXHR);
                                    console.log(exception);
                                }
                            });
                        }
                    }
                },
                {
                    text: '', datafield: 'ResetPassword', width: "120px", columntype: 'button', cellsrenderer: function () {
                        return "Reset Password";
                    },
                    buttonclick: function (row) {
                        $("#popupResetPWDWindow").removeClass('hide');
                        if (!$(".errorDiv").hasClass('hide'))
                            $(".errorDiv").addClass('hide');
                        $('#perror').html('');
                        editrow = row;
                        var offset = $("#UserSearchjqxgrid").offset();
                        $("#popupResetPWDWindow").jqxWindow("move", $(window).width() / 2 - $("#popupResetPWDWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupResetPWDWindow").jqxWindow("height") / 2);
                        var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', editrow);
                        console.log("dataRecord", dataRecord);

                        onhypClick();
                    }
                }


            ],
            filterable: true,
            sortable: true,
            pageable: true,
            pagermode: 'simple',
            selectionmode: 'none'
        });

    $.unblockUI();
    // initialize the popup window and buttons.
}



// update the edited row when the user clicks the 'Save' button.
$("#btnSave").click(function () {

    if (!$(".errorDiv").hasClass('hide'))
        $(".errorDiv").addClass('hide');
    $('#perror').html('');

    if ($('#txtUsername').val() == "") {
        $('#perror').append('> "Username" is required <br/>');
        $('#txtUsername').focus();
    }
    if ($('#txtEmail').val() == "") {
        $('#perror').append('> "Email" is required <br/>');
        $('#txtEmail').focus();
    }

    if ($('#txtPassword').val().length < 6) {
        $('#perror').append('> "Password" you have to enter at least  MinimumLength! = 6 <br/>');
        $('#txtPassword').focus();
    }

    if ($('#txtPassword').val() != $('#txtConfirmPassword').val()) {
        $('#perror').append('> "Password" is not Match <br/>');
    }

    if ($('#txtFirstName').val() == "") {
        $('#perror').append('> "txtFirstName" is required <br/>');
        $('#txtFirstName').focus();
    }

    if ($('#txtLastName').val() == "") {
        $('#perror').append('> "txtLastName" is required <br/>');
        $('#txtLastName').focus();
    }

    if ($('#perror').html() !== '') {
        $(".errorDiv").removeClass('hide');
        return;
    }

    if (editrow >= 0) {

        var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', editrow);
        console.log("dataRecord-save", dataRecord);
        $("#popupWindow").jqxWindow('hide');

        var dataSave = {
            "Id": dataRecord.Id,
            "Username": $("#txtUsername").val(),
            "Email": $("#txtEmail").val(),
            "FirstName": $("#txtFirstName").val(),
            "LastName": $("#txtLastName").val(),
            "Address": $("#txtAddressName").val(),
            "City": $("#txtCity").val(),
            "Mobile": $("#txtMobile").val(),
        };

        // Send company information to database
        var request = $.ajax({
            url: '/Admin/Account/UpdateUser?id=' + dataRecord.Id,
            cache: false,
            data: JSON.stringify(dataSave),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            success: function () {
                loadData();
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
            "Username": $("#txtUsername").val(),
            "Email": $("#txtEmail").val(),
            "Password": $("#txtPassword").val(),
            "ConfirmPassword": $("#txtConfirmPassword").val(),
            "SendPasswordInEmail": $("#chkSendPasswordInEmail").is(':checked'),
            "FirstName": $("#txtFirstName").val(),
            "LastName": $("#txtLastName").val(),
            "Address": $("#txtAddressName").val(),
            "City": $("#txtCity").val(),
            "Mobile": $("#txtMobile").val(),
            "RoleId": getParameterByName('roleid')
        };
        var request = $.ajax({
            url: "/Admin/Account/CreateUser",
            type: 'POST',
            dataType: 'json',
            cache: false,
            data: JSON.stringify(dataSave),
            contentType: 'application/json; charset=utf-8',
            success: function () {
                loadData();
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR);
                console.log(exception);
            }
        });
    }
});


function onhypClick() {
    var dataRecord = $("#UserSearchjqxgrid").jqxGrid('getrowdata', editrow);
    //$("#ifrResetPassword").attr("src", "/account/resetpassword?userid=" + dataRecord.Id);
    $("#popupResetPWDWindow").jqxWindow("move", $(window).width() / 2 - $("#popupResetPWDWindow").jqxWindow("width") / 2, $(window).height() / 2 - $("#popupResetPWDWindow").jqxWindow("height") / 2);
    $("#popupResetPWDWindow").jqxWindow({ width: 300, maxWidth: 300 });
    $("#popupResetPWDWindow").jqxWindow({ height: 500 });
    $("#popupResetPWDWindow").jqxWindow('open');
}
