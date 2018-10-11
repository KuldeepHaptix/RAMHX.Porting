var sortableElement = {};
var clientDetailSubmited = false;

$(document).ready(function () {
    $('#txtBirthDate, #txtAppDate').datepicker();
    $('.timepicker').timepicker({
        showInputs: false,
        showMeridian: false
    });

    LoadDoctorDropDown('ddlDoctor');
    LoadLocation('ddlLocation');

    sortableElement = $("#sortable1, #sortable2").sortable({
        connectWith: ".connectedSortable",
        stop: function (event, ui) {
            //console.log(event, ui);
            //console.log($(ui.item[0]).parents('ul').attr('status'))
            $.ajax({
                url: '/Appointment/GetAppoiment?appointmentid=' + ui.item[0].id,
                type: 'GET',
                dataType: 'json',
                success: function (result) {
                    if (result.Status == "success") {
                        $('#txtFullName').val(result.Data.FullName);
                        $('#txtPatient').val(result.Data.FullName + " " + result.Data.Mobile);

                        if (result.Data.BirthDate) {
                            $('#txtBirthDate').datepicker("setDate", new Date(parseInt(result.Data.BirthDate.substr(6))));
                        }

                        $('#txtMobile').val(result.Data.Mobile);
                        $('#txtEmail').val(result.Data.Email);
                        $('#ddlDoctor').val(result.Data.DoctorId);
                        $('#ddlLocation').val(result.Data.LocationId);
                        $('#txtAppDate').datepicker("setDate", new Date(parseInt(result.Data.ApplicationOn.substr(6))));
                        $('#txtAppTime').val(DateToTime(new Date(parseInt(result.Data.ApplicationOn.substr(6)))));
                        $('#hdnAppinmentId').val(result.Data.AppointmentId);
                        ((result.Data.Gender||"male").toLowerCase().trim() == "male") ? $('#app_GenderMale').attr('checked', true) : $('#app_GenderFemale').attr('checked', true);

                        $('#hdnNewStatus').val($(ui.item[0]).parents('ul').attr('status'));
                        var oldDtatusId = parseInt(result.Data.Status);
                        var newStatusId = parseInt($('#hdnNewStatus').val());

                        // Order change
                        if (newStatusId == oldDtatusId) {
                            // TODO: Order will be changed
                        }
                        else {
                            if (newStatusId != 0) {
                                $('#modal-patientDetail').modal('toggle');
                            }
                            else
                            {
                                toastr.error("You can not move OPD to Appoinment");
                                sortableElement.sortable('cancel');
                            }
                        }
                    }
                    else {
                        toastr.error(result.Message);
                    }
                },
                error: function (result) {
                    console.log("err", result);
                }
            });

            //updateAppointmentStatus(ui.item[0].id, $(ui.item[0]).parents('ul').attr('status'), $(this));
            //$(this).sortable('cancel');
        }
    }).disableSelection();

    //$('#btn-delete').click(function () {
    //    $('#divPatientDetail').show('slow');
    //});

    $('#locationName').html(GetLocationName(getParameterByName("l")));
    $('#doctorName').html(GetDoctorName(getParameterByName("d")));

    var columns = [{
        minWidth: '100px',
        name: 'Name',
        valueField: 'FullName'
    }, {
        minWidth: '100px',
        name: 'Mobile',
        valueField: 'Mobile'
    }, {
        minWidth: '100px',
        name: 'Email',
        valueField: 'Email'
    }, {
        minWidth: '100px',
        name: 'Address',
        valueField: 'Address'
    }], ResultsLimit = 10;
    // colors = [['Red', '#f00', 'Address'], ['Green', '#0f0', 'Address'], ['Blue', '#00f', 'Address']];

    $('#txtPatient').mcautocomplete({
        autoFocus: true,
        showHeader: true,
        columns: columns,
        minLength: 3,
        maxResults: ResultsLimit,
        source: function (request, response) {
            console.log('request', request);
            $.ajax({
                url: '/Patient/SearchPatient',
                dataType: 'json', cache: false,
                data: {
                    keyword: request.term,
                    maxResults: ResultsLimit
                },
                // The success event handler will display "No match found" if no items are returned.
                success: function (data) {
                    var result;
                    if (!data || data.length === 0) {
                        $(".ui-autocomplete").hide();
                        return;
                    } else {
                        result = data;
                    }
                    response(result);
                }
            });
        },
        appendTo: '#patientContainer',
        select: function (event, ui) {
            this.value = ui.item.FullName;
            $('#hdnPatientId').val(ui.item.PatientId);
            return false;
        }
    });
});

$('#modal-patientDetail').on('hidden.bs.modal', function (e) {
    $('#divPatientDetail').hide();
    $('#divPatient').hide();
    $('#divIsNewCustumer').show();
    if (!clientDetailSubmited) {
        sortableElement.sortable('cancel');
    }
});

$('#btnSubmit').on('click', function () {
    var form = new FormData();
    form.append("FullName", $('#txtFullName').val());
    form.append("DoctorId", $('#ddlDoctor').val());
    form.append("LocationId", $('#ddlLocation').val());
    form.append("BirthDate", $('#txtBirthDate').val()); //dd/MM/yyyy
    form.append("PatientId", $('#hdnPatientId').val());
    form.append("AppointmentId", $("#hdnAppinmentId").val());
    form.append("Gender", $('input[name=Gender]:checked').val());
    form.append("Mobile", $('#txtMobile').val());
    form.append("Email", $('#txtEmail').val());
    form.append("ApplicationOn", $('#txtAppDate').val());
    form.append("Time", $('#txtAppTime').val());
    form.append("Status", $('#hdnNewStatus').val());

    var settings = {
        "async": false,
        "crossDomain": true,
        "url": "/Appointment/UpdateAppoimentStatusWithPatient",
        "method": "POST",
        "processData": false,
        "contentType": false,
        "mimeType": "multipart/form-data",
        "data": form
    }

    $.ajax(settings).done(function (response) {
        var res = JSON.parse(response);
        if (res.Status == 'error') {
            toastr.error(res.Message);
        } else {
            toastr.success("Submited Successfully");
            $('#modal-patientDetail').modal('toggle');
        }
        console.log(response);
    });
});

$('#btnAddNewPatient').on('click', function () {
    $('#divPatientDetail').show('slow');
    $('#divIsNewCustumer').hide('slow');
    $('#btnBack').show('slow');
    $('#hdnPatientId').val('')
    $('#txtAppDate').datepicker("setDate", new Date());
    $('#hdnNewStatus').val('1');
});

$('#btnSearchPatient').on('click', function () {
    $('#divPatient').show('slow');
    $('#divIsNewCustumer').hide('slow');
    $('#btnBack').show('slow', function () {
        $('#txtPatient').mcautocomplete('search');
    });

});

$('.back').on('click', function () {
    $('#divIsNewCustumer').show('slow');
    $('.divToggle').hide('slow');
    $(this).hide();
});

$('#AddNewOPD').on('click', function () {
    $('#modal-patientDetail').modal('toggle');
});


//$('#cbxIsNewPatient').on('change', function () {
//    if ($(this).is(':checked')) {
//        $('#divPatient').hide();
//    }
//    else {
//        $('#divPatient').show();
//    }
//});

function updateAppointmentStatus(appoimentid, statuscode, currenElement) {
    $.blockUI();
    $.ajax({
        type: 'POST',
        url: '/Appointment/UpdateAppoimentStatus',
        data: JSON.stringify({ appointmentid: appoimentid, statuscode: statuscode }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        success: function (response) {
            if (response.Status == "success") {
                toastr.success("Successfuly Updated!");
            }
            else {
                currenElement.sortable('cancel');
                toastr.error("Failed!");
            }

            console.log(response);
            $.unblockUI();
        },
        error: function (jqXHR, exception) {
            $.unblockUI();
            console.log(jqXHR);
            console.log(exception);
            toastr.error("Failed!");
            currenElement.sortable('cancel');
        }
    });
}

//function LoadAppoiments()
//{
//    $.ajax({
//        type: 'GET',
//        url: '/Appointment/GeAppoiments',
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        async: false,
//        success: function (response) {
//            $.each(response.Data, function (index, item) {
//                // Doctors.push({ key: item.Id, value: item.Name });
//                console.log("Appoiment", item);

//            });
//        },
//        error: function (jqXHR, exception) {
//            $.unblockUI();
//            console.log(jqXHR);
//            console.log(exception);
//        }
//    });
//}

var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http) {

    //$http.get("/Appointment/GeAppoiments").then(function (response) {
    //    console.log('response'.response)
    //$scope.firstName = response.Data;

    $http({
        method: "GET",
        url: "/Appointment/GeAppoiments"
    }).then(function mySucces(response) {
        console.log('response', response);
        $scope.records = response.data.Data;
        $scope.pendingRecords = [];
        $scope.currentRecords = [];

        $.each(response.data.Data, function (index, item) {
            // console.log(item);
            if (item.Status == 0) {
                $scope.pendingRecords.push(item);
            }
            else if (item.Status == 1) {
                $scope.currentRecords.push(item);
            }
            //  $scope.pendingRecords

        });

    }, function myError(response) {
        console.log('AppoimentError', response.data);
        //$scope.myWelcome = response.statusText;
    });

    $scope.getDateStringWithTime = function (date) {
        return DateToStringWithTime(new Date(parseInt(date.substr(6))));
    };

    $scope.getTime = function (date) {
        return DateToTime(new Date(parseInt(date.substr(6))));
    };

    $scope.getDateString = function (date) {
        return DateToString(new Date(parseInt(date.substr(6))));
    };
});