
      function setHolidayData(HolidayId) {
        $.ajax({
            url: '/Holiday/GetHoliday?holidayid=' + HolidayId,
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                console.log('hol', result);

                $('#HolidayId').val(result.Data.HolidayId);
                console.log('resultholiday', result.Data);
                $('#Name').val(result.Data.Name);
                $('#HolidayDate').datepicker("setDate", new Date(parseInt(result.Data.HolidayDate.substr(6))));
            },
            error: function (result) {
                console.log("err", result);
            }
        });
    }



    function HolidayDataTable()
    {
        var dataSet = [];

        $.ajax({
            type: 'GET',
            url: '/Holiday/GetHolidays',
            async: false,
            success: function (response) {
                $.each(response.Data, function (index, item) {
                    dataSet.push([item.HolidayId, item.Name, DateToString(new Date(parseInt(item.HolidayDate.substr(6))))]);
                });

            },
            error: function (jqXHR, exception) {
                alert('Error');
                console.log(jqXHR);
                console.log(exception);
            }
        });

        console.log('dataSet', dataSet);

        var HolidayTable = $('#Holiday').DataTable({
            data: dataSet,
            destroy: true,
            columns: [
                {title: "Id"},
                {title: "Holiday Name"},
                {title: "Date"},
                {title: "Actions"}

            ],
            columnDefs: [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": "<a class='btn btn-primary btn-xs btnEditHoliday'>Edit</a> <a class='btn btn-danger btn-xs btnDeleteHoliday' action='delete'>Delete</a>"
                },
                {
                    "targets": [0],
                    "visible": false
                }
            ]
        });

//        console.log('ServiceTable', ServiceTable);

        $(document).on("click", ".btnEditHoliday", function () {
            var HolidayData = HolidayTable.row($(this).parents('tr')).data();
            setHolidayData(HolidayData[0]);

        });


    }
    $(document).ready(function () {
        
        HolidayDataTable();
         $('.datepicker').datepicker();
        
        $("#btn_holiday").click(function () {

            if (!$('#Name').val()) {
                toastr.error("This field is required");

                $('#Name').focus();

            } else {

                var form = new FormData();

                form.append("HolidayId", $('#HolidayId').val());
                form.append("Name", $('#Name').val());
                form.append("HolidayDate", $('#HolidayDate').val()); //dd/MM/yyyy

//            console.log(form);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": "/Holiday/AddUpdate",
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
                        HolidayDataTable();

                    }
                });

            }

        });

    });

    function ClearHolidaydata() {
        $('#HolidayId').val('');
        $('#Name').val('');
        $('#HolidayDate').val('');



    }
    $("#createNewholiday").click(function () {
        ClearHolidaydata();
    });
