
$(document).ready(function () {
    $("#txtUserName").focus();
    //UserName Click Event Step 1
    $("#btnSubmit").click(function () {

        if (validateRequiredFieldsByGroup("dUserName")) {
            checkUserName();
            return false;
        }
      
    });

    //Send OTP Click Event Step 2
    $("#btnSendCode").click(function () {

        $("#txtMobileNumber").focus();
        if ($("#txtUserMobile").val() != $("#txtMobileNumber").val()) {
            if (!$(".UDetailserrorDiv").hasClass('hide'))
                $(".UDetailserrorDiv").addClass('hide');
            $('#pUserDetailserror').html('');
            if ($('#txtMobileNumber').val() != '') {
                $('#pUserDetailserror').append('> Enter Mobile number is not valid <br/>');
                $('#txtMobileNumber').focus();
            }
            if ($('#pUserDetailserror').html() !== '') {
                $(".UDetailserrorDiv").removeClass('hide');
                return;
            }
        }
        else {
            if (validateRequiredFieldsByGroup("divUserDetails")) {
                SendCode();
                return false;
            }
        }
    });

    //Verify OTP Click Event Step 3
    $("#btnOTPSubmit").click(function () {
        if (validateRequiredFieldsByGroup("divOTPCode")) {
            VerifiedToken();
            return false;
        }
    });

    //Reset Password Click Event Step 3
    $("#btnChangePassword").click(function () {
        if (validateRequiredFieldsByGroup("divChangePassword")) {
            blockUI();
            resetPassword();
            return false;
        }
    });
});

function checkUserName() {
    $.ajax({
        url: "/CoreModuleForgotPassword/ForgotPassword/CheckUserName?userName=" + $("#txtUserName").val(),
        type: "Post",
        dataType: 'json',
        cache: false,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result);

            if (result.result == null || result == null) {
                if (!$(".UsererrorDiv").hasClass('hide'))
                    $(".UsererrorDiv").addClass('hide');
                $('#perror').html('');
                if ($('#txtUserName').val() != '') {
                    $('#perror').append('> User not found <br/>');
                    $('#txtUserName').focus();
                }
                if ($('#perror').html() !== '') {
                    $(".UsererrorDiv").removeClass('hide');
                    return;
                }
            }
            else {
                $("#divUserDetails").removeClass('hide');
                $("#dUserName").addClass('hide');
                var mobile = result.result.Mobile;
                var lastFour = mobile.substr(mobile.length - 4);
                $("#spUserMobileNumber").html(mobile.substr(0, 2) + "****" + lastFour);
                $("#txtUserId").val(result.result.Id);
                $("#txtUserMobile").val(result.result.Mobile);
                $("#txtMobileNumber").focus();

            }
            $.unblockUI();
        }
    });
}

function SendCode() {
    blockUI();
    $.ajax({
        url: "/CoreModuleForgotPassword/ForgotPassword/SendCode?UserId=" + $("#txtUserId").val(),
        type: "Post",
        dataType: 'json',
        cache: false,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result);
            $("#divOTPCode").removeClass('hide');
            $("#divUserDetails").addClass('hide');
            $("#dUserName").addClass('hide');
            $("#txtTokenId").val(result.result.TokenId);
            $("#txtUserCode").val(result.result.CodeToken);
            $("#txtOTPCode").focus();
            $.unblockUI();
        }
    });
}

function VerifiedToken() {
    $.ajax({
        url: "/CoreModuleForgotPassword/ForgotPassword/VerifiedToken?UserId=" + $("#txtUserId").val() + "&TokenId=" + $("#txtTokenId").val() + "&codeToken=" + $("#txtOTPCode").val(),
        type: "Post",
        dataType: 'json',
        cache: false,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result);
            if (result.status == 'fail' || result == null) {
                //alert(result.message);
                if (!$(".OTPerrorDiv").hasClass('hide'))
                    $(".OTPerrorDiv").addClass('hide');
                $('#pOTPCodeerror').html('');
                if ($('#txtOTPCode').val() != '') {
                    $('#pOTPCodeerror').append(">" + result.message);
                    $('#txtOTPCode').focus();
                }
                if ($('#pOTPCodeerror').html() !== '') {
                    $(".OTPerrorDiv").removeClass('hide');
                    return;
                }
            }
            else {
                $("#divChangePassword").removeClass('hide');
                $("#divOTPCode").addClass('hide');
                $("#divUserDetails").addClass('hide');
                $("#dUserName").addClass('hide');
            }
            $.unblockUI();
        }
    });
}

function resetPassword() {

    $.ajax({
        url: "/admin/account/ForgotPWDResetPassword?userid=" + $("#txtUserId").val() + "&password=" + $("#txtNewPassword").val() + "&confirmPassword=" + $("#txtConfirmNewPassword").val(),
        type: "Post",
        dataType: 'json',
        cache: false,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log(result);
            if (result.status == "fail") {
                if (!$(".changePWDerrorDiv").hasClass('hide'))
                    $(".changePWDerrorDiv").addClass('hide');
                $('#pchangePWDerror').html('');
                if (result.message !== '') {

                    $('#pchangePWDerror').append("> " + result.message);
                    $.each(result.message, function (index, itmErr) {
                        $('#pchangePWDerror').append("> " + itmErr + "<br/>");
                    });
                } else {
                    $.each(result.result, function (index, itmErr) {
                        $('#pchangePWDerror').append("> " + itmErr + "<br/>");
                    });
                }

            } else {
                $(".changePWDerrorDiv").addClass('hide');
                $(".successdiv").removeClass('hide');
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
}