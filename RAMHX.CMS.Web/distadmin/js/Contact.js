$(document).ready(function () {

    //setFocusOnNavigation();
    applyRequiredValidation();
    $('#btnSendEmail').click(function () {
      
        if (validateRequiredFieldsByGroup("divContactForm"))
            Contact();

            return false;
    });
});

function Contact() {
    
    event.preventDefault();

    // Get form
    var form = $('#frmContactUs')[0];
    var data = new FormData(form);

    blockUI();

    $.ajax({
        url: "/ContactUs/SendRequest",
        type: "POST",
        enctype: 'multipart/form-data',
        data: data,
        processData: false,
        contentType: false,
        success: function (result) {
            console.log("success", result);
            $('#frmContactUs').addClass("hide");
            $('#divSuccessContact').removeClass("hide");
            $.unblockUI();
        },
        error: function (er) {
            console.log(er);
        }
    });
}
