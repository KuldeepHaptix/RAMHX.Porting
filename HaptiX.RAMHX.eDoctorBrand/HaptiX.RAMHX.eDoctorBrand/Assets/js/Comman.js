var Locations = [];
var Doctors = [];

$(document).ready(function () {
    LoadDoctors();
    LoadLocations();
    console.log('Comman.js');
})

function LoadDoctors() {
    $.ajax({
        type: 'GET',
        url: '/doctor/GetDoctorsForDrodown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        success: function (response) {
            $.each(response, function (index, item) {
                Doctors.push({ key: item.Id, value: item.Name });
            });
        },
        error: function (jqXHR, exception) {
            $.unblockUI();
            console.log(jqXHR);
            console.log(exception);
        }
    });
}

function LoadLocations() {
    $.ajax({
        type: 'GET',
        url: '/location/GetDoctorsForDrodown',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        success: function (response) {
            $.each(response, function (index, item) {
                Locations.push({ key: item.Id, value: item.Name });
            });

            console.log('Locations', response, Locations);
        },
        error: function (jqXHR, Locations, response) {
            $.unblockUI();
            console.log(jqXHR);
            console.log(exception);
        }
    });
}

function DateToString(date) {
    return GetTowDigitInteger(date.getDate()) + "/" + GetTowDigitInteger((date.getMonth() + 1)) + "/" + date.getFullYear();
}

function DateToTime(date) {
    return GetTowDigitInteger(date.getHours()) + ":" + GetTowDigitInteger(date.getMinutes());
}

function DateToStringWithTime(date) {
    return GetTowDigitInteger(date.getDate()) + "/" + GetTowDigitInteger((date.getMonth() + 1)) + "/" + date.getFullYear() + " " + GetTowDigitInteger(date.getHours()) + ":" + GetTowDigitInteger(date.getMinutes());
}

function GetTowDigitInteger(input) {
    return ("0" + input).slice(-2);
}

function GetLocationName(locationId) {
    if (locationId != undefined) {
        for (var i = 0; i < Locations.length; i++) {
            if (Locations[i].key.toUpperCase() === locationId.toUpperCase()) {
                return Locations[i].value;
            }
        }
    }
    return "";
}

function GetDoctorName(doctorId) {
    if (doctorId != undefined) {
        for (var i = 0; i < Doctors.length; i++) {
            if (Doctors[i].key.toUpperCase() === doctorId.toUpperCase()) {
                return Doctors[i].value;
            }
        }
    }
    return "";
}

function getParameterByName(name, url) {
    if (!url) {
        url = window.location.href;
    }
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
    if (!results)
        return null;
    if (!results[2])
        return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function LoadLocation(ddlId) {
    $('#' + ddlId).html('');
    $.each(Locations, function (key, value) {
        //            $.each(result, function () {
        $('#' + ddlId).append($("<option />").val(this.key).text(this.value));
        //            });

    });
}

function LoadDoctorDropDown(ddlId) {
    $('#' + ddlId).html('');
    $.each(Doctors, function (key, value) {
        $('#' + ddlId).append($("<option />").val(this.key).text(this.value));
    });
}