var cityAPIUrl = "/Registration/GetCities";
var jqxTheme = "darkblue"
function blockUI() {
    $.blockUI();
    $('.blockMsg').css("z-index", "99999");
    $('.blockOverlay').css("z-index", "99999");
}


$(document).ready(function () {
    $('head').append('<link href="/jqwidgets/styles/jqx.' + jqxTheme + '.css" rel="stylesheet" />');

    var currUrl = window.location.href.toLowerCase();

    if (currUrl.indexOf('/appadmin/gallery/category') > -1) {
        $($(".sidebar a[href*='/appadmin/gallery/category']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/gallery/category']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/gallery/album') > -1) {
        $($(".sidebar a[href*='/appadmin/gallery/album']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/gallery/album']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/gallery/albumitems') > -1) {
        $($(".sidebar a[href*='/appadmin/gallery/albumitems']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/gallery/albumitems']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/productcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/productcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/productcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/productmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/productmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/productmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/packagecategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/packagecategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/packagecategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/packagemaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/packagemaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/packagemaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/projectcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/projectcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/projectcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/projectmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/projectmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/projectmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/jobcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/jobcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/jobcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/jobmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/jobmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/jobmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/testimonialcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/testimonialcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/testimonialcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/testimonialmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/testimonialmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/testimonialmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/faqcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/faqcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/faqcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/faqmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/faqmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/faqmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/slidermaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/slidermaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/slidermaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/slidercategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/slidercategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/slidercategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/newscategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/newscategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/newscategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/newsmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/newsmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/newsmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/eventcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/eventcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/eventcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/eventmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/eventmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/eventmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/blogcategory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/blogcategory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/blogcategory']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/blogmaster') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/blogmaster']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/blogmaster']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/sendquicksms') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/sendquicksms']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/sendquicksms']").parent()).parent()).parent()).addClass("active");
    }
    else if (currUrl.indexOf('/appadmin/masters/smshistory') > -1) {
        $($(".sidebar a[href*='/appadmin/masters/smshistory']").parent()).addClass("active");
        $($($($(".sidebar a[href*='/appadmin/masters/smshistory']").parent()).parent()).parent()).addClass("active");
    }

    ApplyNumericBox();
    //$('#txtFirstName').capitalize();
    //$('#txtMiddleName').capitalize();
    //$('#txtLastName').capitalize();
    //$('#txtName').capitalize();


});

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    url = url.toLowerCase();
    name = name.replace(/[\[\]]/g, "\\$&");
    name = name.toLowerCase();
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function loadAll(callback, apiUrl) {
    $.ajax({
        url: apiUrl,
        type: "GET",
        cache: false,
        success: function (response) {
            callback(response);
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR);
            console.log(exception);
            $.unblockUI();
        }
    });
}

function applyRequiredValidation() {
    $("[isRequired='1']").each(function (ind, item) {
        $(this).change(function () {
            validateReqField($(this));
        });
        $(this).blur(function () {
            validateReqField($(this));
        })
    });
}

function LoadTooltipsy() {

    $('.tooltipsy').tooltipsy({
        offset: [-10, 0],
        css: {
            'padding': '10px',
            'max-width': '200px',
            'color': '#303030',
            'background-color': '#f5f5b5',
            'border': '1px solid #deca7e',
            '-moz-box-shadow': '0 0 10px rgba(0, 0, 0, .5)',
            '-webkit-box-shadow': '0 0 10px rgba(0, 0, 0, .5)',
            'box-shadow': '0 0 10px rgba(0, 0, 0, .5)',
            'text-shadow': 'none'
        }
    });
}
function ApplyNumericBox() {
    $(".numberbox").on("keypress", function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {

            return false;
        }

        if (e.which == 46) {
            if ($(this).val().indexOf('.') > -1) {
                return false;
            }
        }
    });
}
function validateRequiredFields() {
    $('.error').each(function (index, itm) {
        if (!$(itm).hasClass('hide')) {
            $(itm).addClass('hide');
        }
    });
    $('.has-error').each(function (index, itm) {
        if ($(this).hasClass('has-error')) {
            $(this).removeClass('has-error');
        }
    });

    $("[isRequired='1']").each(function (ind, item) {
        validateReqField($(this));
    });


    if ($('.has-error').length > 0) {
        return false;
    }
    return true;
}

function validateRequiredFieldsByGroup(groupId) {
    $('[id="' + groupId + '"] .error').each(function (index, itm) {
        if (!$(itm).hasClass('hide')) {
            $(itm).addClass('hide');
        }
    });
    $('[id="' + groupId + '"] .has-error').each(function (index, itm) {
        if ($(this).hasClass('has-error')) {
            $(this).removeClass('has-error');
        }
    });

    $("[id='" + groupId + "'] [isRequired='1']").each(function (ind, item) {
        validateReqField($(this));
    });


    if ($('[id="' + groupId + '"] .has-error').length > 0) {
        return false;
    }
    return true;
}


function validateReqField(obj) {
    if ($(obj).val() == $(obj).attr("defaultvalue")) {
        $("#" + $(obj).attr("errorspan")).removeClass('hide');
        $("#" + $(obj).attr("divcontainer")).addClass('has-error');
    }
    else {
        $("#" + $(obj).attr("errorspan")).addClass('hide');
        $("#" + $(obj).attr("divcontainer")).removeClass('has-error');
    }
}

function setFocusOnNavigation() {
    var url = window.location.href;
    var activePage = url;
    $('.nav a').each(function () {
        var linkPage = this.href;

        if (activePage == linkPage) {
            $(this).closest("li").addClass("active");
        }
    });
}

function ApplyDateRangePicker(dptId, dptHdnId) {
    $('#' + dptId).daterangepicker(
        {
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            startDate: moment().startOf('month'),
            endDate: moment().endOf('month')
        },
        function (start, end) {
            $("#" + dptHdnId).val("&start=" + start.format('DD/MMM/YYYY') + '&end=' + end.format('DD/MMM/YYYY'))
            $('#' + dptId + ' span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
    );
    $("#" + dptHdnId).val("&start=" + moment().startOf('month').format('DD/MMM/YYYY') + '&end=' + moment().endOf('month').format('DD/MMM/YYYY'))
    $('#' + dptId + ' span').html(moment().startOf('month').format('MMMM D, YYYY') + ' - ' + moment().endOf('month').format('MMMM D, YYYY'));
}

function studentApplyDateRangePicker(dptId, dptHdnId) {
    $('#' + dptId).daterangepicker(
        {
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            startDate: moment().startOf('month'),
            endDate: moment().endOf('month')
        },
        function (start, end) {
            $("#" + dptHdnId).val("&start=" + start.format('DD/MMM/YYYY') + '&end=' + end.format('DD/MMM/YYYY'))
            $('#' + dptId + ' span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
    );
    $("#" + dptHdnId).val("&start=" + moment().subtract(180, 'days').startOf('month').format('DD/MMM/YYYY') + '&end=' + moment().endOf('month').format('DD/MMM/YYYY'))
    $('#' + dptId + ' span').html(moment().subtract(180, 'days').startOf('month').format('MMMM D, YYYY') + ' - ' + moment().endOf('month').format('MMMM D, YYYY'));
}


function unhandledError(jqXHR, exception) {
    alert("Unhandle exception occured. Please review console log or contact admistrator");
    console.log(jqXHR);
    console.log(exception);
    $.unblockUI();
}


function getCurrentDate() {
    var date = new Date();
    var dateCurrent = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var dtParts = dateCurrent.split('/');
    var monthName = "";
    switch (dtParts[1]) {
        case "02":
        case "2":
            monthName = "FEB";
            break;
        case "03":
        case "3":
            monthName = "MAR";
            break;
        case "04":
        case "4":
            monthName = "APR";
            break;
        case "05":
        case "5":
            monthName = "MAY";
            break;
        case "06":
        case "6":
            monthName = "JUN";
            break;
        case "07":
        case "7":
            monthName = "JUL";
            break;
        case "08":
        case "8":
            monthName = "AUG";
            break;
        case "09":
        case "9":
            monthName = "SEP";
            break;
        case "10":
            monthName = "OCT";
            break;
        case "11":
            monthName = "NOV";
            break;
        case "12":
            monthName = "DEC";
            break;
        default:
            monthName = "JAN";
            break;
    }

    return dtParts[0] + "/" + monthName + "/" + dtParts[2] + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
}

function getOnlyCurrentDate() {
    var date = new Date();
    var dateCurrent = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var dtParts = dateCurrent.split('/');
    var monthName = "";
    switch (dtParts[1]) {
        case "02":
        case "2":
            monthName = "FEB";
            break;
        case "03":
        case "3":
            monthName = "MAR";
            break;
        case "04":
        case "4":
            monthName = "APR";
            break;
        case "05":
        case "5":
            monthName = "MAY";
            break;
        case "06":
        case "6":
            monthName = "JUN";
            break;
        case "07":
        case "7":
            monthName = "JUL";
            break;
        case "08":
        case "8":
            monthName = "AUG";
            break;
        case "09":
        case "9":
            monthName = "SEP";
            break;
        case "10":
            monthName = "OCT";
            break;
        case "11":
            monthName = "NOV";
            break;
        case "12":
            monthName = "DEC";
            break;
        default:
            monthName = "JAN";
            break;
    }

    return dtParts[0] + "/" + monthName + "/" + dtParts[2];
}

function getDateFromFields(fieldId) {
    var dtParts = $("#" + fieldId).val().split('/');
    var monthName = "";
    switch (dtParts[1]) {
        case "02":
            monthName = "FEB";
            break;
        case "03":
            monthName = "MAR";
            break;
        case "04":
            monthName = "APR";
            break;
        case "05":
            monthName = "MAY";
            break;
        case "06":
            monthName = "JUN";
            break;
        case "07":
            monthName = "JUL";
            break;
        case "08":
            monthName = "AUG";
            break;
        case "09":
            monthName = "SEP";
            break;
        case "10":
            monthName = "OCT";
            break;
        case "11":
            monthName = "NOV";
            break;
        case "12":
            monthName = "DEC";
            break;
        default:
            monthName = "JAN";
            break;
    }

    return dtParts[0] + "/" + monthName + "/" + dtParts[2];
}

function getFormatedDate(date) {
    //var dateCurrent = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var dateCurrent = ("0" + date.getDate()).slice(-2) + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var dtParts = dateCurrent.split('/');
    var monthName = "";
    switch (dtParts[1]) {
        case "02":
        case "2":
            monthName = "FEB";
            break;
        case "03":
        case "3":
            monthName = "MAR";
            break;
        case "04":
        case "4":
            monthName = "APR";
            break;
        case "05":
        case "5":
            monthName = "MAY";
            break;
        case "06":
        case "6":
            monthName = "JUN";
            break;
        case "07":
        case "7":
            monthName = "JUL";
            break;
        case "08":
        case "8":
            monthName = "AUG";
            break;
        case "09":
        case "9":
            monthName = "SEP";
            break;
        case "10":
            monthName = "OCT";
            break;
        case "11":
            monthName = "NOV";
            break;
        case "12":
            monthName = "DEC";
            break;
        default:
            monthName = "JAN";
            break;
    }

    return dtParts[0] + "/" + monthName + "/" + dtParts[2];
}

function getFormatedDateTime(date) {
    //var dateCurrent = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var dateCurrent = ("0" + date.getDate()).slice(-2) + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
    var dtParts = dateCurrent.split('/');
    var monthName = "";
    switch (dtParts[1]) {
        case "02":
        case "2":
            monthName = "FEB";
            break;
        case "03":
        case "3":
            monthName = "MAR";
            break;
        case "04":
        case "4":
            monthName = "APR";
            break;
        case "05":
        case "5":
            monthName = "MAY";
            break;
        case "06":
        case "6":
            monthName = "JUN";
            break;
        case "07":
        case "7":
            monthName = "JUL";
            break;
        case "08":
        case "8":
            monthName = "AUG";
            break;
        case "09":
        case "9":
            monthName = "SEP";
            break;
        case "10":
            monthName = "OCT";
            break;
        case "11":
            monthName = "NOV";
            break;
        case "12":
            monthName = "DEC";
            break;
        default:
            monthName = "JAN";
            break;
    }

    return dtParts[0] + "/" + monthName + "/" + dtParts[2] + " " + date.getHours() + ":" + date.getMinutes();
}

function uploadFile(dir, fileId) {

    var formData = new FormData("frmUpload");
    formData.append('file', $('[id=' + fileId + ']')[0].files[0]);

    $.ajax({
        url: '/appgeneral/UploadFile?dir=' + dir,
        data: formData,
        type: 'POST',
        async: false,
        contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
        processData: false, // NEEDED, DON'T OMIT THIS
        success: function (result) {
            return result.path;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR);
            console.log(exception);
            return "";
        }
    });
}