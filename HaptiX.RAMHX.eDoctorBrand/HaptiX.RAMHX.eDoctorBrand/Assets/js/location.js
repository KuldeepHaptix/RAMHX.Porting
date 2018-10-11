
    function setLocationData(LocationId) {
        $.ajax({
            url: '/location/GetLocation?locationid=' + LocationId,
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                $('#hdnLocationId').val(result.Data.LocationId);
                console.log('result', result.Data);
                $('#Name').val(result.Data.Name);
                $('#Fulladd').val(result.Data.FullAddress);
                $('#City').val(result.Data.City);
                $('#Pin').val(result.Data.PinCode);
                $('#IsActive').prop('checked', result.Data.IsActive).change();
            },
            error: function (result) {
                console.log("err", result);
            }
        });
    }



    function LocationDataTable()
    {

        var dataSet = [];
        //$.blockUI();
        $.ajax({
            type: 'GET',
            url: '/location/GetLocations',
            //data: { 'doctorid': docId },
            async: false,
            success: function (response) {
                $.each(response, function (index, item) {
                    dataSet.push([item.LocationId, item.Name, item.FullAddress, item.City, item.PinCode, item.IsActive]);
                });


                //$.unblockUI();
            },
            error: function (jqXHR, exception) {
                alert('Error');
                //$.unblockUI();
                console.log(jqXHR);
                console.log(exception);
            }
        });

        console.log('dataSet', dataSet);

        var LocationTable = $('#location').DataTable({
            data: dataSet,
            destroy: true,
            columns: [
                {title: "Id"},
                {title: "Location Name"},
                {title: "Full Adddress"},
                {title: "City"},
                {title: "Pincode"},
                {title: "Active"},
                {title: "Actions"}

            ],
            columnDefs: [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": "<a class='btn btn-primary btn-xs btnEditLocation'>Edit</a> <a class='btn btn-danger btn-xs btnDeleteLocation' action='delete'>Delete</a>"
                },
                {
                    "targets": [0],
                    "visible": false
                }
            ]
        });

        console.log('LocationTable', LocationTable);

        $(document).on("click", ".btnEditLocation", function () {
            var LocationData = LocationTable.row($(this).parents('tr')).data();
            setLocationData(LocationData[0]);

        });
    }
    $(document).ready(function () {

        LocationDataTable();

        $("#btn_location").click(function () {

            if (!$('#Name').val()) {
                toastr.error("This field is required");

                $('#Name').focus();

            } else if (!$('#Pin').val()) {
                toastr.error("Pincod is required");
                $('#Pin').focus();
            } else {

                var form = new FormData();

                form.append("LocationId", $('#hdnLocationId').val());
                form.append("Name", $('#Name').val());
                form.append("FullAddress", $('#Fulladd').val());
                form.append("City", $('#City').val());
                form.append("PinCode", $('#Pin').val());
                form.append("IsActive", $('#IsActive').is(':checked'));

//            console.log(form);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": "/location/AddUpdate",
                    "method": "POST",

                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }


                $.ajax(settings).done(function (response) {
                    console.log(response);
                    if (response.Status == 'error') {
                        toastr.error(response.Message);
                    } else {
                        toastr.success("Submited Successfully");
                        LocationDataTable();

                    }
                });

            }

        });

    });
    
    function Clear() {
        $('#hdnLocationId').val('');
        $('#Name').val('');
        $('#Fulladd').val('');
        $('#City').val('');
        $('#Pin').val('');
        $('#IsActive').attr('checked', false).change();
        


    }
    $("#createNewLocation").click(function () {
        Clear();
    });
