var appointment = new Object();

$(document).ready(function () {

    $('#bookAppointmentGetOTP').click(function () {
        if ($("#drpDoctor").val() == '0') {
            toastr.error("Please select valid Doctor");
            $("#drpDoctor").focus();
            return;
        } else if ($("#drpLocation").val() == '0') {
            toastr.error("Please select valid Location");
            $("#app_email").focus();
            return;
        } else if ($("#app_fullname").val() == '') {
            toastr.error("Please enter valid name");
            $("#app_fullname").focus();
            return;
        } else if ($("#app_email").val() == '') {
            toastr.error("Please enter valid email");
            $("#app_email").focus();
            return;
        } else if ($("#app_BirthDate").val() == '') {
            toastr.error("Please enter valid Birth Date");
            $("#app_BirthDate").focus();
            return;
        } else if (!$('#app_GenderMale').is(':checked') && !$('#app_GenderFemale').is(':checked')) {
            toastr.error("Please select valid Gender");
            return;
        } else if ($("#app_phone").val() == '') {
            toastr.error("Please enter valid Phone");
            $("#app_phone").focus();
            return;
        } else if ($("#app_date").val() == '') {
            toastr.error("Please enter valid Appontment Date");
            $("#app_date").focus();
            return;
        }

        appointment.DoctorId = $("#drpDoctor").val();
        appointment.LocationId = $("#drpLocation").val()
        appointment.FullName = $("#app_fullname").val();
        appointment.Email = $("#app_email").val();
        appointment.BirthDate = ConvertToDateObject($("#app_BirthDate").val());
        if ($('#app_GenderMale').is(':checked')) {
            appointment.Gender = "Male";
        } else {
            appointment.Gender = "Female";
        }
        appointment.Mobile = $("#app_phone").val();
        appointment.ApplicationOn = ConvertToDateObject($("#bkDate").html(), $('#btTime').html());
        appointment.Mobile = $("#app_phone").val();
        appointment.Status = 0;

        $.blockUI({
            css: {
                width: 30 + "px",
                height: 30 + "px",
                top: ($(window).height() - 40) / 2 + 'px',
                left: ($(window).width() - 40) / 2 + 'px',
            },
            message: $('#loader'),
            baseZ: 2000,
            theme: false,
        });
        //$.blockUI({ baseZ: 2000, message: $('#loader') });
        $.ajax({
            type: 'POST',
            url: '/Appointment/SendOTP',
            data: JSON.stringify(appointment),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (otp) {
                $('#loader').hide();
                $.unblockUI();
                $("#card").flip('toggle');
            },
            error: function (jqXHR, exception) {
                $.unblockUI();
                toastr.error("Error occured during generating OTP for appointment");
                console.log(jqXHR);
                console.log(exception);
            }
        });
    });

    $('#bookAppointment').click(function () {
        if ($("#app_OTP").val() == '') {
            toastr.error("Please enter valid OTP");
            $("#app_OTP").focus();
            return;
        }

        $.blockUI({
            css: {
                width: 30 + "px",
                height: 30 + "px",
                top: ($(window).height() - 40) / 2 + 'px',
                left: ($(window).width() - 40) / 2 + 'px',
            },
            message: $('#loader'),
            baseZ: 2000,
            theme: false,
        });

        //$.blockUI({
        //    baseZ: 2000, message: $('#loader')
        //});
        $.ajax({
            type: 'POST',
            url: '/Appointment/BookAppointment',
            data: JSON.stringify({ 'appo': appointment, 'OTP': $("#app_OTP").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (responce) {
                if (responce) {
                    $("#app_OTP").val('');
                    toastr.success("Appointment has been booked successfully");
                    setTimeout(function () { location.reload(); }, 3000);
                    $.unblockUI();
                } else {
                    $.unblockUI();
                    toastr.error("Your OTP is incorrecot or Error occured while performing booking");
                }
            },
            error: function (jqXHR, exception) {
                $.unblockUI();
                toastr.error("Error occured while performing booking");
                console.log(jqXHR);
                console.log(exception);
            }
        });
    });

});

$(document).on("click", ".bookAppo", function () {

    $('#drpDoctor').val($(this).attr('docid'));
    $('#drpLocation').val($(this).attr('locid'));

    //$('#docid').html($.grep(Doctors, function (e) { return e.docid == $(this).attr('docid'); }));
    //$('#locid').html($.grep(Locations, function (e) { return e.locid == $(this).attr('locid'); }));
    $('#bkDate').html($(this).attr('date'));
    $('#btTime').html($(this).html());
});

function ConvertToDateObject(dateString, timestring) {
    var dateParts = dateString.split("/");
    if (timestring != null && timestring != undefined) {
        var timeParts = timestring.split(":");
        return new Date(dateParts[2], dateParts[1] - 1, dateParts[0], timeParts[0], timeParts[1]); // month is 0-based
    }
    else {
        return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // month is 0-based
    }
}

