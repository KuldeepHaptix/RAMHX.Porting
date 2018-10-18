$(document).ready(function () {
    applyRequiredValidation();
    $("#btnLogin").on("click", function () {

        Login();
    });
    $('#txtUserName').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            Login();
        }
    });
    $('#txtPassword').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            Login();
        }
    });

});


function Login() {
    if (!validateRequiredFieldsByGroup('divLogin'))
        return false;

    event.preventDefault();
    var formData = new FormData("frmLogin");
    formData.append("email", $("#txtUsername").val());
    formData.append("password", $("#txtPassword").val());
    blockUI();
    $.ajax({
        url: "/admin/Account/PublicLogin", // Get the action URL to send AJAX to
        type: "POST",
        dataType: 'json',
        cache: false,
        processData: false,
        contentType: false,
        data: formData, // get all form variables
        success: function (result) {
            if (result.status == "fail") {
                $.unblockUI();
                $("#drefLoginError").removeClass('hide');
                $("#spnLoginError").html(result.message);
            } else {
                var timeStampInMs = window.performance && window.performance.now && window.performance.timing && window.performance.timing.navigationStart ? window.performance.now() + window.performance.timing.navigationStart : Date.now();
                window.parent.location = "/appadmin?t=" + timeStampInMs;
            }
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