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

                if (aHref.indexOf('?') > -1) {
                    newahref = newahref + "&" + href.split('?')[1];
                }
                else {
                    newahref = newahref + "?" + href.split('?')[1];
                }
                $(this).attr('href', newahref);
            }
        });
    }
}