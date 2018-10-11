
    function setServiceData(ServiceId) {
        $.ajax({
            url: '/Service/GetService?serviceid=' + ServiceId,
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                console.log('ser',result);
                $('#ServiceId').val(result.Data.ServiceId);
                console.log('result', result.Data);
                $('#Name').val(result.Data.Name);
                $('#IsActive').prop('checked', result.Data.IsActive).change();


            },
            error: function (result) {
                console.log("err", result);
            }
        });
    }



    function ServiceDataTable()
    {

        var dataSet = [];
        //$.blockUI();
        $.ajax({
            type: 'GET',
            url: '/Service/GetServices',
            //data: { 'doctorid': docId },
            async: false,
            success: function (response) {
                              console.log('virl',response);

                $.each(response.Data, function (index, item) {
                    dataSet.push([item.ServiceId, item.Name, item.IsActive]);
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

        var ServiceTable = $('#Service').DataTable({
            data: dataSet,
            destroy: true,
            columns: [
                {title: "Id"},
                {title: "Service Name"},
                {title: "Active"},
                {title: "Actions"}

            ],
            columnDefs: [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": "<a class='btn btn-primary btn-xs btnEditService'>Edit</a> <a class='btn btn-danger btn-xs btnDeleteService' action='delete'>Delete</a>"
                },
                {
                    "targets": [0],
                    "visible": false
                }
            ]
        });

        console.log('ServiceTable', ServiceTable);

        $(document).on("click", ".btnEditService", function () {
            var ServiceData = ServiceTable.row($(this).parents('tr')).data();
            setServiceData(ServiceData[0]);

        });
    }
    $(document).ready(function () {

        ServiceDataTable();

        $("#btn_service").click(function () {

            if (!$('#Name').val()) {
                toastr.error("This field is required");

                $('#Name').focus();

            } else {

                var form = new FormData();

                form.append("ServiceId", $('#ServiceId').val());
                form.append("Name", $('#Name').val());
                form.append("IsActive", $('#IsActive').is(':checked'));

//            console.log(form);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": "/Service/AddUpdate",
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
                        ServiceDataTable();

                    }
                });

            }

        });

    });

    function ClearServicedata() {
        $('#ServiceId').val('');
        $('#Name').val('');
        $('#IsActive').attr('checked', false).change();



    }
    $("#createNewService").click(function () {
        ClearServicedata();
    });
