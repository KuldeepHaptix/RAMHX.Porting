$(document).ready(function () {
    $('#ddlLocations').change(function () {
        LoadAvaibility($('#docId').val(), $(this).val());
    });
    $('#ddlLocations').change();
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