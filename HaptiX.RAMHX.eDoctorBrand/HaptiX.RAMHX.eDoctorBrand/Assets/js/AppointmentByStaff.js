$(document).ready(function () {
    $('#btnSearchAvailibility').click(function () {
        LoadAvaibility($('#ddlDoctor').val(), $('#ddlLocation').val());
    });

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

    $('#txtSearchPatient').mcautocomplete({
        
        autoFocus: true,
        showHeader: true,
        columns: columns,
        minLength: 3,
        maxResults: ResultsLimit,
        source: function (request, response) {
            console.log('request', request);
            $('#txtFullName').val(request.term);
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
            $('#txtFullName').val(ui.item.FullName);
            $('#txtMobile').val(ui.item.Mobile);
            $('#txtEmail').val(ui.item.Email);
            $('#txtAddress').val(ui.item.Address);
            return false;
        }
    });
});

function LoadAvaibility(docId, locId) {
    $.blockUI();
    $.ajax({
        type: 'GET',
        url: '/Availability/GetAvailabilityView',
        data: { 'doctorid': docId, 'locationid': locId },
        success: function (response) {
            $('#slotViewer').html(response);
            $.unblockUI();
        },
        error: function (jqXHR, exception) {
            alert('Error');
            $.unblockUI();
            console.log(jqXHR);
            console.log(exception);
        }
    });
}

$(document).on('click', '.bookAppo', function () {
    $('#hdnPatientId').val('');
    $('#hdnAppDate').val($(this).attr('date'));
    $('#hdnAppTime').val($(this).html());
    $('#modal-bookappinment').modal();
});

$('#btnBookNow').on('click', function () {
    var form = new FormData();
    form.append("FullName", $('#txtFullName').val());
    form.append("DoctorId", $('#ddlDoctor').val());
    form.append("LocationId", $('#ddlLocation').val());
   // form.append("BirthDate", $('#txtBirthDate').val()); //dd/MM/yyyy
    form.append("PatientId", $('#hdnPatientId').val());
    //form.append("AppointmentId", $("#hdnAppinmentId").val());
    //form.append("Gender", $('input[name=Gender]:checked').val());
    form.append("Mobile", $('#txtMobile').val());
    form.append("Email", $('#txtEmail').val());
    form.append("ApplicationOn", $('#hdnAppDate').val());
    form.append("Time", $('#hdnAppTime').val());
   // form.append("Status", $('#hdnNewStatus').val());

    var settings = {
        "async": false,
        "crossDomain": true,
        "url": "/Appointment/BookAppointmentByStaff",
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
            $('#modal-bookappinment').modal('toggle');
        }
        console.log(response);
    });
});