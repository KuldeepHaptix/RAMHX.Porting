
    function setData(avaibilityId) {
        $.ajax({
            url: '/availability/GetAvailability?availabilityid=' + avaibilityId,
            type: 'GET',
            dataType: 'json',
            success: function (result) {
                $('#hdnAvailibilityId').val(result.Data.AvailabilityId);
                $('#StartDate').datepicker("setDate", new Date(parseInt(result.Data.CreatedDate.substr(6))));
                $('#EndDate').datepicker("setDate", new Date(parseInt(result.Data.EndDate.substr(6))));
                console.log('result', result.Data);
                $('#duration').val(result.Data.DurationInMinute);
                $('#ddlLocation').val(result.Data.LocationId);
                $('#Sunday').prop('checked', result.Data.OnSunday).change();
                $('#Monday').prop('checked', result.Data.OnMonday).change();
                $('#Tuesday').prop('checked', result.Data.OnTuesday).change();
                $('#Wednesday').prop('checked', result.Data.OnWedesday).change();
                $('#Thusday').prop('checked', result.Data.OnThusday).change();
                $('#Friday').prop('checked', result.Data.OnFriday).change();
                $('#Saturday').prop('checked', result.Data.OnSaturday).change();

                $('#SundayMorningStart').val(GetTowDigitInteger(result.Data.SundayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.SundayMorningStart.Minutes));
                $('#SundayMorningEnd').val(GetTowDigitInteger(result.Data.SundayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.SundayMorningEnd.Minutes));
                $('#SundayEveningStart').val(GetTowDigitInteger(result.Data.SundayEveningStart.Hours) + ':' + GetTowDigitInteger(result.Data.SundayEveningStart.Minutes));
                $('#SundayEveningEnd').val(GetTowDigitInteger(result.Data.SundayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.SundayEveningEnd.Minutes));

                $('#MondayMorningStart').val(GetTowDigitInteger(result.Data.MondayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.MondayMorningStart.Minutes));
                $('#MondayMorningEnd').val(GetTowDigitInteger(result.Data.MondayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.MondayMorningEnd.Minutes));
                $('#MondayEveningStart').val(GetTowDigitInteger(result.Data.MondayEveningStart.Hours) + ':' + GetTowDigitInteger(result.Data.MondayEveningStart.Minutes));
                $('#MondayEveningEnd').val(GetTowDigitInteger(result.Data.MondayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.MondayEveningEnd.Minutes));

                $('#TuesdayMorningStart').val(GetTowDigitInteger(result.Data.TuesdayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.TuesdayMorningStart.Minutes));
                $('#TuesdayMorningEnd').val(GetTowDigitInteger(result.Data.TuesdayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.TuesdayMorningEnd.Minutes));
                $('#TuesdayEveningStart').val(GetTowDigitInteger(result.Data.TuesdayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.TuesdayMorningEnd.Minutes));
                $('#TuesdayEveningEnd').val(GetTowDigitInteger(result.Data.TuesdayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.TuesdayEveningEnd.Minutes));

                $('#WednesdayMorningStart').val(GetTowDigitInteger(result.Data.WednesdayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.WednesdayMorningStart.Minutes));
                $('#WednesdayMorningEnd').val(GetTowDigitInteger(result.Data.WednesdayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.WednesdayMorningEnd.Minutes));
                $('#WednesdayEveningStart').val(GetTowDigitInteger(result.Data.WednesdayEveningStart.Hours) + ':' + GetTowDigitInteger(result.Data.WednesdayEveningStart.Minutes));
                $('#WednesdayEveningEnd').val(GetTowDigitInteger(result.Data.WednesdayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.WednesdayEveningEnd.Minutes));

                $('#ThursdayMorningStart').val(GetTowDigitInteger(result.Data.ThursdayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.ThursdayMorningStart.Minutes));
                $('#ThursdayMorningEnd').val(GetTowDigitInteger(result.Data.ThursdayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.ThursdayMorningEnd.Minutes));
                $('#ThursdayEveningStart').val(GetTowDigitInteger(result.Data.ThursdayEveningStart.Hours) + ':' + GetTowDigitInteger(result.Data.ThursdayEveningStart.Minutes));
                $('#ThursdayEveningEnd').val(GetTowDigitInteger(result.Data.ThursdayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.ThursdayEveningEnd.Minutes));

                $('#FridayMorningStart').val(GetTowDigitInteger(result.Data.FridayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.FridayMorningStart.Minutes));
                $('#FridayMorningEnd').val(GetTowDigitInteger(result.Data.FridayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.FridayMorningEnd.Minutes));
                $('#FridayEveningStart').val(GetTowDigitInteger(result.Data.FridayEveningStart.Hours) + ':' + GetTowDigitInteger(result.Data.FridayEveningStart.Minutes));
                $('#FridayEveningEnd').val(GetTowDigitInteger(result.Data.FridayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.FridayEveningEnd.Minutes));

                $('#SaturdayMorningStart').val(GetTowDigitInteger(result.Data.SaturdayMorningStart.Hours) + ':' + GetTowDigitInteger(result.Data.SaturdayMorningStart.Minutes));
                $('#SaturdayMorningEnd').val(GetTowDigitInteger(result.Data.SaturdayMorningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.SaturdayMorningEnd.Minutes));
                $('#SaturdayEveningStart').val(GetTowDigitInteger(result.Data.SaturdayEveningStart.Hours) + ':' + GetTowDigitInteger(result.Data.SaturdayEveningStart.Minutes));
                $('#SaturdayEveningEnd').val(GetTowDigitInteger(result.Data.SaturdayEveningEnd.Hours) + ':' + GetTowDigitInteger(result.Data.SaturdayEveningEnd.Minutes));
            },
            error: function (result) {
                console.log("err", result);
            }
        });
    }

    function ConvertToDate(stringDate)
    {

        var datearray = stringDate.split("/");

        var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        return new Date(newdate);
    }

    function ValidateEndDate() {
        var startDate = $("#StartDate").val();
        var endDate = $("#EndDate").val();
        if (startDate != '' && endDate != '') {
//            if (new Date(startDate) > new Date(endDate)) {
////                return new Date(value) >= new Date($(params).val());
//
//                $("#EndDate").val('');
//                toastr.error("Start date should not be greater than end date");
//
//            }
//            var date = $("#StartDate").val();
//            var datearray = date.split("/");
//
//            var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
//            var startDate = new Date(newdate);
//
//            var date1 = $("#EndDate").val();
//            var datearray1 = date.split("/");
//
//            var newdate1 = datearray1[1] + '/' + datearray1[0] + '/' + datearray1[2];
//            var endDate = new Date(newdate1);
//            var endDate = new Date($('#EndDate').val());

            if (ConvertToDate(startDate) > ConvertToDate(endDate)) {

                toastr.error("End Date must be greater than start date");
                return false;
            }
        }
        return true;
    }



    $(document).ready(function () {


        LoadLocation('ddlLocation');
        console.log('Avaibility.js Ready');

        $('.time').timepicker({
            showInputs: false,
            showMeridian: false

        });
        $('.datepicker').datepicker();


        LoadAvailabilityDataTable();

        //  $('#availabilityTable').DataTable();

        //        if (getParameterByName('aid') != '') {
        //            setData();
        //        }


        $('#Sunday').change(function () {
            $("#SundayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#SundayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#SundayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#SundayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });

        $('#Monday').change(function () {
            $("#MondayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#MondayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#MondayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#MondayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });

        $('#Tuesday').change(function () {
            $("#TuesdayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#TuesdayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#TuesdayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#TuesdayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });

        $('#Wednesday').change(function () {
            $("#WednesdayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#WednesdayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#WednesdayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#WednesdayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });

        $('#Thusday').change(function () {
            $("#ThursdayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#ThursdayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#ThursdayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#ThursdayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });

        $('#Friday').change(function () {
            $("#FridayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#FridayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#FridayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#FridayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });

        $('#Saturday').change(function () {
            $("#SaturdayMorningStart").prop("disabled", !$(this).is(':checked'));
            $("#SaturdayMorningEnd").prop("disabled", !$(this).is(':checked'));
            $("#SaturdayEveningStart").prop("disabled", !$(this).is(':checked'));
            $("#SaturdayEveningEnd").prop("disabled", !$(this).is(':checked'));
        });



        $("#btn_sub").click(function () {

            if (!$('#StartDate').val()) {
                toastr.error("This field is required");

                $('#StartDate').focus();

            } else if (!$('#EndDate').val()) {
                toastr.error("This field is required");
                $('#EndDate').focus();
            } else if (ValidateEndDate()) {
                var form = new FormData();
                form.append("AvailabilityId", $('#hdnAvailibilityId').val());
                form.append("DoctorId", "{EC2F4651-DE2A-4439-89FB-22E7769EBB07}");
                form.append("LocationId", $('#ddlLocation').val());
                form.append("StartDate", $('#StartDate').val()); //dd/MM/yyyy
                form.append("EndDate", $('#EndDate').val()); //dd/MM/yyyy
                form.append("DurationInMinute", $("#duration option:selected").text());
                form.append("SundayMorningStart", $('#SundayMorningStart').val());
                form.append("SundayMorningEnd", $('#SundayMorningEnd').val());
                form.append("SundayEveningStart", $('#SundayEveningStart').val());
                form.append("SundayEveningEnd", $('#SundayEveningEnd').val());
                form.append("MondayMorningStart", $('#MondayMorningStart').val());
                form.append("MondayMorningEnd", $('#MondayMorningEnd').val());
                form.append("MondayEveningStart", $('#MondayEveningStart').val());
                form.append("MondayEveningEnd", $('#MondayEveningEnd').val());
                form.append("TuesdayMorningStart", $('#TuesdayMorningStart').val());
                form.append("TuesdayMorningEnd", $('#TuesdayMorningEnd').val());
                form.append("TuesdayEveningStart", $('#TuesdayEveningStart').val());
                form.append("TuesdayEveningEnd", $('#TuesdayEveningEnd').val());
                form.append("WednesdayMorningStart", $('#WednesdayMorningStart').val());
                form.append("WednesdayMorningEnd", $('#WednesdayMorningEnd').val());
                form.append("WednesdayEveningStart", $('#WednesdayEveningStart').val());
                form.append("WednesdayEveningEnd", $('#WednesdayEveningEnd').val());
                form.append("ThursdayMorningStart", $('#ThursdayMorningStart').val());
                form.append("ThursdayMorningEnd", $('#ThursdayMorningEnd').val());
                form.append("ThursdayEveningStart", $('#ThursdayEveningStart').val());
                form.append("ThursdayEveningEnd", $('#ThursdayEveningEnd').val());
                form.append("FridayMorningStart", $('#FridayMorningStart').val());
                form.append("FridayMorningEnd", $('#FridayMorningEnd').val());
                form.append("FridayEveningStart", $('#FridayEveningStart').val());
                form.append("FridayEveningEnd", $('#FridayEveningEnd').val());
                form.append("SaturdayMorningStart", $('#SaturdayMorningStart').val());
                form.append("SaturdayMorningEnd", $('#SaturdayMorningEnd').val());
                form.append("SaturdayEveningStart", $('#SaturdayEveningStart').val());
                form.append("SaturdayEveningEnd", $('#SaturdayEveningEnd').val());
                form.append("OnSunday", $('#Sunday').is(':checked'));
                form.append("OnMonday", $('#Monday').is(':checked'));
                form.append("OnTuesday", $('#Tuesday').is(':checked'));
                form.append("OnWednesday", $('#Wednesday').is(':checked'));
                form.append("OnThursday", $('#Thusday').is(':checked'));
                form.append("OnFriday", $('#Friday').is(':checked'));
                form.append("OnSaturday", $('#Saturday').is(':checked'));

                console.log(form);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": "/Availability/AddUpdate",
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
                        LoadAvailabilityDataTable();
                    }

                    console.log(response);


                });

            }


        });

    });


    function LoadAvailabilityDataTable() {
        var availabilityData = [];
        //$.blockUI();
        $.ajax({
            type: 'GET',
            url: '/Availability/GetAvailabilities',
            //data: { 'doctorid': docId },
            async: false,
            success: function (response) {
                $.each(response.Data, function (index, item) {
                    availabilityData.push([item.AvailabilityId, GetLocationName(item.LocationId), DateToString(new Date(parseInt(item.StartDate.substr(6)))), DateToString(new Date(parseInt(item.EndDate.substr(6)))), item.DurationInMinute]);
                })

                //$.unblockUI();
            },
            error: function (jqXHR, exception) {
                alert('Error');
                //$.unblockUI();
                console.log(jqXHR);
                console.log(exception);
            }
        });

        var AvailabilityTable = $('#availabilityTable').DataTable({
            data: availabilityData,
            destroy: true,
            columns: [
                {title: "Id"},
                {title: "Location Name"},
                {title: "Start Date"},
                {title: "End Date"},
                {title: "Duration"},
                {title: "Actions"}
            ],
            columnDefs: [{
                    "targets": -1,
                    "data": null,
                    "defaultContent": "<a class='btn btn-primary btn-xs btnEditAvail'>Edit</a> <a class='btn btn-danger btn-xs btnDeleteAvail' action='delete'>Delete</a>"
                },
                {
                    "targets": [0],
                    "visible": false
                }
            ]
        });

        $(document).on("click", ".btnEditAvail", function () {
            var AvailData = AvailabilityTable.row($(this).parents('tr')).data();
            // alert(data[0] + "'s salary is: " + data[5]);
            setData(AvailData[0]);

        });
    }
    function ClearAvilibiitydata() {
        $('#hdnAvailibilityId').val('');
        $('#StartDate').val('');
        $('#EndDate').val('');
        $('#duration').val('0');
        $('#ddlLocation').val('');
        $('#Sunday').attr('checked', false).change();
        $('#Monday').attr('checked', false).change();
        $('#Tuesday').attr('checked', false).change();
        $('#Wednesday').attr('checked', false).change();
        $('#Thusday').attr('checked', false).change();
        $('#Friday').attr('checked', false).change();
        $('#Saturday').attr('checked', false).change();


    }
    $("#createNew").click(function () {
        ClearAvilibiitydata();
    });

