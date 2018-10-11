
$(document).ready(function () {
    blockUI();
    ApplyDateRangePicker('ViewSMSHistorydaterange-btn', 'ViewSMSHistoryhdnDates');

    $("#btnSMSHistory").click(function () {
        filterSMSRecords();
    });

    filterSMSRecords();

});

function filterSMSRecords() {
    blockUI();
    var fltUrl = '/CoreModule_SMS/GetAllSMSHistory' + "?" + $('#ViewSMSHistoryhdnDates').val();
    loadAll(BindData, fltUrl);
}

function BindData(response) {
    var source =
        {
            datatype: "array",
            datafields: [
                { name: 'Id', type: 'int' },
                { name: 'SMSNumber', type: 'string' },
                { name: 'SMSText', type: 'string' },
                { name: 'IsSent', type: 'bool' },
                { name: 'CreatedDate', type: 'date' },
                { name: 'SentDate', type: 'date' },
                { name: 'SenderName', type: 'string' },

            ],
        };

    source.localdata = response;
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#smsJqxgrid").jqxGrid(
        {
            theme: jqxTheme,
            width: '100%',
            source: dataAdapter,
            columnsresize: false,
            altrows: true,
            columns: [
                { text: 'Id', datafield: 'Id', width: "75px" },
                { text: 'Mobile', datafield: 'SMSNumber', width: "130px" },
                { text: 'Message', datafield: 'SMSText', width: "350px" },
                { text: 'CreatedOn', datafield: 'CreatedDate', width: "200px", cellsformat: "dd/MM/yyyy" },
                { text: 'Sender', datafield: 'SenderName', width: "150px" },
                {
                    text: 'Is Sent', datafield: 'IsSent', width: "100px", cellsrenderer: function (row, columnfield, value, defaulthtml, columnproperties) {
                        var dataRecord = $("#smsJqxgrid").jqxGrid('getrowdata', row);
                        console.log(dataRecord.IsSent);
                        if (dataRecord.IsSent == true) {
                            return '<div style="text-align: center;">' + "Yes" + '</div>';

                        }
                        else {
                            return '<div style="text-align: center;">' + "No" + '</div>';
                        }
                    }
                },
            ],
            filterable: true,
            sortable: true,
            pageable: true,
            pagermode: 'simple',
            selectionmode: 'singlerow'
        });
    $.unblockUI();
}
