$("#btnSendSMS").click(function () {
    SendSMS();
    window.location = window.location;
})

function SendSMS() {
    event.preventDefault();

    var data = {
        numbers: $("#txtNumber").val(),
        message: $("#txtMessage").val(),
    };

    console.log("data", data);

    blockUI();
    $.ajax({
        url: "/CoreModule_SMS/SendSMS",
        type: "Post",
        dataType: "Json",
        data: data,
        success: function (sms) {
            alert("Message Sent Successfully");
        }
    });
    $.unblockUI();
}