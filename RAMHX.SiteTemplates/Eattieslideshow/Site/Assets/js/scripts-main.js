//Eattie v1.3 Main (Home) Page
//Copyright 2015 8Guild.com
//All scripts for Eatti Restaurant HTML Template

/*Preloading Animation*/
$(window).load(function () {
    $('#status').fadeOut();
    $('#preloader').delay(300).fadeOut('slow');
    $('body').delay(300).css({ 'overflow': 'visible' });
    setTimeout(function () { $('.intro-title').addClass('slideInLeft'); }, 100);
});

/*Checking if it's touch device we disable some functionality due to inconsistency*/
if (Modernizr.touch) {

    /////////////////////*Touch Devices*////////////////////////////////////

    $('.animation').css('opacity', '1');

    /*Scripts Executed on Page Load*/
    var wrapper = $('#wrapper');
    $(window).on('load', function () {

        /*Curtain Effect*/
        wrapper.css('margin-top', $(window).height() - 90);

        /*Stiky Header*/
        $(".stiky").waypoint('sticky');

    });

    /*Scripts Executed on Page Resize*/
    $(window).on('resize', function () {
        wrapper.css('margin-top', $(window).height() - 90);
    });

} else {

    /////////////////////*No-touch Devices*////////////////////////////////////

    /*Title onScroll Animation*/
    $(window).scroll(function () {
        var title = $('.intro-title');
        if ($(this).scrollTop() > 0) {
            title.stop().animate({ opacity: 1, marginTop: -165 }, 300);
        }
        if ($(this).scrollTop() > 200) {
            title.stop().animate({ opacity: 0, marginTop: -230 }, 300);
        }
    });

    /*Scripts Executed on Page Load*/
    var wrapper = $('#wrapper');
    $(window).on('load', function () {

        /*Curtain Effect*/
        if ($(window).width() > 976) {
            wrapper.css('margin-top', $(window).height() - 90);
        } else {
            wrapper.css('margin-top', $(window).height());
        }

        /*Stiky Header*/
        $(".stiky").waypoint('sticky');

        /*Content animations*/
        $('.animation').waypoint(function () {
            var animation = jQuery(this).attr("data-animation");
            $(this).addClass(animation);
            $(this).addClass('animated');
        }, { offset: '75%' });
    });

    /*Scripts Executed on Page Resize*/
    $(window).on('resize', function () {
        if ($(window).width() > 976) {
            wrapper.css('margin-top', $(window).height() - 90);
        } else {
            wrapper.css('margin-top', $(window).height());
        }
    });

}//Close Modernizr.touch




/*Document Ready*/
$(document).ready(function (e) {

    /*Tooltips*/
    $('.tooltipped').tooltip();

    /*Hide Dropdown on Scroll*/
    $(window).on('scroll', function () {
        $('.dropdown').removeClass('open');
    });

    /*Intro Fullscreen Slideshow Initializing*/
    if ($(".intro-slideshow ul li").length > 0) {
        slider.bgSlider($(".intro-slideshow ul li").length);
    }

    ////////////////////////////////////////////////////////////
    //INTERNAL ANCHOR LINKS SCROLLING (PAGINATION)
    $(".scroll").click(function (event) {
        event.preventDefault();
        $('html, body').animate({ scrollTop: $(this.hash).offset().top - 89 }, 800, 'easeOutCubic');
    });

    /*Scroll Up*/
    $('.scroll-up').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 800, 'easeOutCubic');
        return false;
    });

    //SCROLL-SPY
    // Cache selectors
    var lastId,
		topMenu = $(".navbar"),
		topMenuHeight = topMenu.outerHeight(),
		// All list items
		menuItems = topMenu.find("a"),
		// Anchors corresponding to menu items
		scrollItems = menuItems.map(function () {
		    //var item = $($(this).attr("href")); // ORG
		    var item = $($(this).attr("data-target"));
		    if (item.length) { return item; }
		});


    // Bind to scroll
    $(window).scroll(function () {
        // Get container scroll position
        var fromTop = $(this).scrollTop() + topMenuHeight + 150;

        // Get id of current scroll item
        var cur = scrollItems.map(function () {
            if ($(this).offset().top < fromTop)
                return this;
        });
        // Get the id of the current element
        cur = cur[cur.length - 1];
        var id = cur && cur.length ? cur[0].id : "";

        if (lastId !== id) {
            lastId = id;
            // Set/remove active class
            menuItems
              .parent().removeClass("active")
              .end().filter("[data-target=#" + id + "]").parent().addClass("active");
        }
    });
    ////////////////////////////////////////////////////////////////////

    /*About Slider*/
    $('.about-slider-inner, .info-slider').each(function (index) {
        $(this).cycle({
            fx: 'fade',
            speed: 800,
            timeout: 0,
            pager: '.pager',
            pagerAnchorBuilder: function (i) {
                if (index == 0)
                    // for first slideshow, return a new anchro
                    return '<a href="#">' + (i + 1) + '</a>';
                // for 2nd slideshow, select the anchor created previously
                return '.pager a:eq(' + i + ')';
            }
        });
    });


    //Enable swiping (News Slider)
    $(".carousel-inner").swipe({
        //Generic swipe handler for all directions
        swipeRight: function (event, direction, distance, duration, fingerCount) {
            $(this).parent().carousel('prev');
        },
        swipeLeft: function () {
            $(this).parent().carousel('next');
        },
        //Default is 75px, set to 0 for demo so any distance triggers swipe
        threshold: 0
    });


    /*FORMS*/
    /*Datepicker*/
    $('.datepicker').datepicker();

    /*Timepicker*/
    $('.timepicker').timepicker({
        'minTime': '09:00am',
        'maxTime': '11:00pm'
    });

    /*Input Hints*/
    $('input[placeholder]').inputHints();

    /*Validation*/
    $(".form-table").validate({
        rules: {
            phone: {
                required: true,
                number: true
            },
            email: {
                required: true,
                email: true
            }
        }
    });
    $(".form-banquet").validate({
        rules: {
            phone: {
                required: true,
                number: true
            },
            email: {
                required: true,
                email: true
            }
        }
    });
    $(".form-catering").validate({
        rules: {
            phone: {
                required: true,
                number: true
            },
            email: {
                required: true,
                email: true
            }
        }
    });
    $(".form-chef-at-home").validate({
        rules: {
            phone: {
                required: true,
                number: true
            },
            email: {
                required: true,
                email: true
            }
        }
    });
    $(".form-food-delivery").validate({
        rules: {
            phone: {
                required: true,
                number: true
            },
            email: {
                required: true,
                email: true
            }
        }
    });
    $(".subscribe-form").validate({
        rules: {
            email: {
                required: true,
                email: true
            }
        }
    });

    /*Custom Checkbox*/
    var chbx = $('.subscribe-chbx input[type=checkbox]');
    var cchbx = $('.subscribe-chbx span');
    cchbx.css('display', 'block');
    chbx.hide();
    cchbx.click(function () {
        var iteration = $(this).data('iteration') || 1
        switch (iteration) {
            case 1:
                $(this).addClass('checked');
                $(this).parent().find('input[type=checkbox]').attr('checked', true);
                break;

            case 2:
                $(this).removeClass('checked');
                $(this).parent().find('input[type=checkbox]').attr('checked', false);
                break;
        }
        iteration++;
        if (iteration > 2) iteration = 1
        $(this).data('iteration', iteration)
    });

    /* To scroll page */
    //var currentHref = window.location.href;
    //if (currentHref.indexOf("#") >= 0) {
    //    var id = currentHref.split("#")[currentHref.split("#").length - 1];
    //    $($("a[data-target=#" + id + "]")[0]).trigger("click");
    //}

});/*/Document ready*/

/*Intro Fullscreen Slideshow Function*/
var slider = {
    currentSlide: 0,
    currentTitle: 0,
    timeOut: 7000,
    pred2: true,
    offset: 0,
    selector: '.intro-slideshow ul',
    currentCls: "current",
    countItems: 0,
    titleSelector: '',
    titleActive: '',

    bgSlider: function (countItems) {
        var me = this;
        me.countItems = countItems || me.countItems;
        me._next();
        me._timeoutId = setTimeout(function () { me.bgSlider(); }, me.timeOut);
    },
    _next: function () {
        var me = this;
        if (me.currentSlide === me.countItems) {
            me.currentSlide = 1;
        } else {
            me.currentSlide++;
        }
        me.doSlide();
    },
    nextSlide: function () {
        var me = this;
        clearTimeout(me._timeoutId);
        me.bgSlider();
    },
    doSlide: function () {
        var me = this;
        // background
        $(me.selector + " li").removeClass(me.currentCls);
        $(me.selector + " li:nth-child(" + me.currentSlide + ")").addClass(me.currentCls);
        // title
        $(me.titleSelector).removeClass(me.titleActive);
        $(me.titleSelector + ":nth-child(" + me.currentSlide + ")").addClass(me.titleActive);
    }
};

// jQuery Input Hints plugin by Rob Volk
(function (n) {
    n.fn.inputHints = function () {
        function r(t) { jQuery(t).val() == "" && jQuery(t).val(n(t).attr("placeholder")).addClass("hint") } function i(t) { n(t).val() == n(t).attr("placeholder") && n(t).val("").removeClass("hint") } var t = n(this); return t.each(function () { r(this) }), t.closest("form").submit(function () { return t.each(function () { i(this) }), !0 })
        , t.focus(function () { i(this) }).blur(function () { r(this) })
    }
})(jQuery)


/*Google Maps API*/
var geocoder;
var map;
var query = '4120 Lenox Avenue, NY';
function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var map_canvas = document.getElementById('map_canvas');
    var map_options = {
        center: latlng,
        scrollwheel: false,
        zoom: 8,
        disableDefaultUI: true
    }
    map = new google.maps.Map(map_canvas, map_options);
    codeAddress();
}

function codeAddress() {
    var image = 'img/marker.png';
    var address = query;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location,
                icon: image,
                title: 'Eattie Restaurant'
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

google.maps.event.addDomListener(window, 'load', initialize);