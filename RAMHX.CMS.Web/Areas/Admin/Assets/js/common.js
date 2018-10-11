var jqxTheme = 'bootstrap';

function SetPageUrlWithParams(id, pageurl) {

    var url = window.location.href.split('?');

    $(id).attr("href", pageurl + "?" + url[1]);

};

function GetQSValue(name) {

    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");

    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),

        results = regex.exec(location.search);

    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));

}

function AppendQueryStringToLinks() {
    var href = window.location.href;
    if (href.indexOf('?') > -1) {
        $('a').each(function () {
            var aHref = $(this).attr('href');
            if (aHref) {
                var newahref = aHref;
                if (aHref.indexOf('dialog=1') === -1) {
                    if (aHref.indexOf('?') > -1) {
                        newahref = newahref + "&" + href.split('?')[1];
                    }
                    else {
                        newahref = newahref + "?" + href.split('?')[1];
                    }
                    $(this).attr('href', newahref);
                }
            }
        });
    }
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    url = url.toLowerCase();
    name = name.replace(/[\[\]]/g, "\\$&");
    name = name.toLowerCase();
    if (url.indexOf(name) == -1) {
        return "";
    }
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
