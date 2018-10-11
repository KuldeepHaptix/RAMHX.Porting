$(document).ready(function () {

    //setFocusOnNavigation();
    applyRequiredValidation();
    $('#btnSendEmail').click(function () {
        if (validateRequiredFields())
            Applyjob();

        return false;
    });
});

function Applyjob() {

    event.preventDefault();

    // Get form
    var form = $('#frmApplyJob')[0];
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
            //window.location.reload(true);
            console.log("success", result);
            alert('Thank You for contacting us. We will get back to you shortly.');
            window.location = window.location;
        },
        error: function (er) {
            console.log(er);
        }
    });
}
