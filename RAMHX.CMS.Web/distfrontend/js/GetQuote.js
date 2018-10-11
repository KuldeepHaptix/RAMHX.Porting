$(document).ready(function () {
    $('#btn-search').click(function () {
        $('#frmGetquoat').removeClass("hide");
        $('#divSuccess').addClass("hide");

        $('#txtName').val("");
        $('#txtMobile').val("");
        $('#txtFromEmail').val("");
        $('#txtProject').val("");
        $('#dPrjLocation').val("");
        $('#txtName2').val("");
        $('#txtMobile2').val("");
        $('#txtRelation').val("");
        $('#txtFrom2Email').val("");
    });
    //setFocusOnNavigation();
    applyRequiredValidation();
    $('#btnGetQuote').click(function () {

        if (validateRequiredFields())
            GetQuote(); 

        return false;
    });
});

function GetQuote() {

    event.preventDefault();

    // Get form
    var form = $('#frmGetquoat')[0];
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
            $('#frmGetquoat').addClass("hide");
            $('#divSuccess').removeClass("hide");
            $.unblockUI();
        },
        error: function (er) {
            console.log(er);
        }
    });
}
