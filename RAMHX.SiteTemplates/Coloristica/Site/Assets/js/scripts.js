//

/*----------------------------------------------------*/
/* MOBILE DETECT FUNCTION
/*----------------------------------------------------*/
var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};



/////////////////////// ready
$(document).ready(function () {

    //Override form submit
    $("#sendEmail").on("click", function (event) {
        event.preventDefault();
        if ($("#inputName").val() == 'Full Name') {
            alert("Please enter valid name");
            $("#inputName").focus();
            return;
        }
        else if ($("#inputEmail").val() == 'E-mail address') {
            alert("Please enter valid email");
            $("#inputEmail").focus();
            return;
        }
        else if ($("#inputPhone").val() == 'Phone') {
            alert("Please enter valid phone");
            $("#inputPhone").focus();
            return;
        }
        else if ($("#inputMessage").val() == 'Message') {
            alert("Please enter message");
            $("#inputMessage").focus();
            return;
        }

        var formData = new FormData("frmEmail");
        formData.append("subject", "Contact Inquiry!");
        formData.append("toemail", "kidzee.motera@gmail.com");
        formData.append("fromemail", $("#inputEmail").val());
        var message = "Name: " + $("#inputName").val() + "<br/>";
        message += "Email: " + $("#inputEmail").val() + "<br/>";
        message += "Phone: " + $("#inputPhone").val() + "<br/>";
        message += "Message: " + $("#inputMessage").val() + "<br/>";
        formData.append("message", message);
        $.blockUI();
        $.ajax({
            url: "/admin/EmailManager/Send", // Get the action URL to send AJAX to
            type: "POST",
            dataType: 'json',
            cache: false,
            processData: false,
            contentType: false,
            data: formData, // get all form variables
            success: function (result) {
                $.unblockUI();
                if (result.status == "fail") {
                    alert(result.error);
                }
                else {
                    alert("Thank you email. We will get back to you soon!");
                }
            },
            error: function (jqXHR, exception) {
                $.unblockUI();
                console.log(jqXHR);
                console.log(exception);
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                alert(msg);
            }
        });
    });

    $("#btnLogin").on("click", function (event) {
        event.preventDefault();
        var formData = new FormData("frmLogin");
        formData.append("email", $("#Email").val());
        formData.append("password", $("#Password").val());
        $.blockUI();
        $.ajax({
            url: "/admin/Account/PublicLogin", // Get the action URL to send AJAX to
            type: "POST",
            dataType: 'json',
            cache: false,
            processData: false,
            contentType: false,
            data: formData, // get all form variables
            success: function (result) {
                $.unblockUI();
                if (result.status == "fail") {
                    alert(result.error);
                }
                else {
                    window.location = window.location;
                }
            },
            error: function (jqXHR, exception) {
                $.unblockUI();
                console.log(jqXHR);
                console.log(exception);
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                alert(msg);
            }
        });


    });

    // Front slider.
    $('.front #top').height($(window).height());
    if (isMobile.any() == null) {
        $('body').addClass("support-fixed");
    }

    // Carousels.
    var o = $('#mission .carousel.main ul');
    if (o.length > 0) {
        o.carouFredSel({
            auto: {
                timeoutDuration: 8000
            },
            responsive: true,
            prev: '.mission_prev',
            next: '.mission_next',
            width: '100%',
            scroll: {
                items: 1,
                duration: 1000,
                easing: "easeOutExpo"
            },
            items: {
                width: '2000',
                height: 'variable', //  optionally resize item-height
                visible: {
                    min: 1,
                    max: 1
                }
            },
            mousewheel: false,
            swipe: {
                onMouse: true,
                onTouch: true
            }
        });
    }

    var o = $('#parent .carousel.main ul');
    if (o.length > 0) {
        o.carouFredSel({
            auto: {
                timeoutDuration: 8000
            },
            responsive: true,
            prev: '.parent_prev',
            next: '.parent_next',
            // pagination  : ".tweets_pag",
            width: '100%',
            scroll: {
                items: 1,
                duration: 1000,
                easing: "easeOutExpo"
            },
            items: {
                width: '2000',
                height: 'variable', //  optionally resize item-height
                visible: {
                    min: 1,
                    max: 1
                }
            },
            mousewheel: false,
            swipe: {
                onMouse: true,
                onTouch: true
            }
        });
    }

    var o = $('#sl .carousel.main ul');
    if (o.length > 0) {
        o.carouFredSel({
            auto: {
                timeoutDuration: 8000
            },
            responsive: true,
            prev: '.sl_prev',
            next: '.sl_next',
            width: '100%',
            scroll: {
                items: 1,
                duration: 1000,
                easing: "easeOutExpo"
            },
            items: {
                width: '2000',
                height: 'variable', //  optionally resize item-height
                visible: {
                    min: 1,
                    max: 1
                }
            },
            mousewheel: false,
            swipe: {
                onMouse: true,
                onTouch: true
            }
        });
    }



    $(window).bind("resize", updateSizes_vat).bind("load", updateSizes_vat);
    function updateSizes_vat() {
        $('#mission .carousel.main ul').trigger("updateSizes");
        $('#parent .carousel.main ul').trigger("updateSizes");
        $('#sl .carousel.main ul').trigger("updateSizes");


    }
    updateSizes_vat();


    /*----------------------------------------------------*/
    // Sticky.
    /*----------------------------------------------------*/
    $("#top2").sticky({
        topSpacing: 0,
        getWidthFrom: 'body',
        responsiveWidth: true
    });

    /*----------------------------------------------------*/
    // PRELOADER CALLING
    /*----------------------------------------------------*/
    $("body.onepage").queryLoader2({
        //barColor: "#fff",
        //backgroundColor: "#000",
        percentage: true,
        barHeight: 3,
        completeAnimation: "fade",
        minimumTime: 200
    });



    /*----------------------------------------------------*/
    // PARALLAX CALLING
    /*----------------------------------------------------*/
    $(window).bind('load', function () {
        parallaxInit();
    });
    function parallaxInit() {
        testMobile = isMobile.any();

        if (testMobile == null) {
            // $('.parallax .bg1').addClass("bg-fixed").parallax("50%", 0.5);
        }
    }
    parallaxInit();




    /*----------------------------------------------------*/
    // prettyPhoto
    /*----------------------------------------------------*/
    $("a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'normal', theme: 'dark', social_tools: false, allow_resize: true, default_width: 500, default_height: 344 });


    /*----------------------------------------------------*/
    // MENU SMOOTH SCROLLING
    /*----------------------------------------------------*/
    $(".navbar_ .nav a, .menu_bot a, .scroll-to").bind('click', function (event) {

        //$(".navbar_ .nav a a").removeClass('active');
        //$(this).addClass('active');
        // var headerH = $('#top1').outerHeight();
        var headerH = $('#top2').outerHeight();

        if ($(this).attr("href") == "#home") {
            $("html, body").animate({
                scrollTop: 0 + 'px'
                // scrollTop: $($(this).attr("href")).offset().top + 'px'
            }, {
                duration: 1200,
                easing: "easeInOutExpo"
            });
        }
        else {
            $("html, body").animate({
                scrollTop: $($(this).attr("href")).offset().top - headerH + 'px'
                // scrollTop: $($(this).attr("href")).offset().top + 'px'
            }, {
                duration: 1200,
                easing: "easeInOutExpo"
            });
        }



        event.preventDefault();
    });

    /*----------------------------------------------------*/
    // Appear
    /*----------------------------------------------------*/
    $('.animated').appear(function () {
        // console.log("111111111111");
        var elem = $(this);
        var animation = elem.data('animation');
        if (!elem.hasClass('visible')) {
            var animationDelay = elem.data('animation-delay');
            if (animationDelay) {
                setTimeout(function () {
                    elem.addClass(animation + " visible");
                }, animationDelay);
            } else {
                elem.addClass(animation + " visible");
            }
        }
    });

    $('.yjsg-round-progress').appear(function () {
        var elem = $(this);
        var animationDelay = elem.data('animation-delay');
        if (animationDelay) {
            setTimeout(function () {
                $(elem).yjsgroundprogress();
            }, animationDelay);
        } else {
            $(elem).yjsgroundprogress();
        }
    });

    // Animate number
    $('.animated-number').appear(function () {
        var elem = $(this);
        var b = elem.text();
        var d = elem.data('duration');
        var animationDelay = elem.data('animation-delay');
        if (!animationDelay) { animationDelay = 0; }
        elem.text("0");

        setTimeout(function () {
            elem.animate({ num: b }, {
                duration: d,
                step: function (num) {
                    this.innerHTML = (num).toFixed(0)
                }
            });
        }, animationDelay);
    });


});

/////////////////////// load
$(window).load(function () {


    /*----------------------------------------------------*/
    // LOAD
    /*----------------------------------------------------*/
    //$('#load').fadeOut(2000).remove();
    $("#load").fadeOut(200, function () {
        $(this).remove();
    });

    /*----------------------------------------------------*/
    // IZOTOPE
    /*----------------------------------------------------*/
    var o = $('#container');
    if (o.length > 0) {

        var $container = $('#container');
        //Run to initialise column sizes
        updateSize();

        //Load fitRows when images all loaded
        $container.imagesLoaded(function () {

            $container.isotope({
                // options
                itemSelector: '.element',
                layoutMode: 'fitRows',
                transformsEnabled: true,
                columnWidth: function (containerWidth) {
                    containerWidth = $browserWidth;
                    return Math.floor(containerWidth / $cols);
                }
            });
        });

        // update columnWidth on window resize
        $(window).smartresize(function () {
            updateSize();
            $container.isotope('reLayout');
        });

        //Set item size
        function updateSize() {
            $browserWidth = $container.width();
            $cols = 4;

            if ($browserWidth >= 1170) {
                $cols = 4;
            }
            else if ($browserWidth >= 767 && $browserWidth < 1170) {
                $cols = 3;
            }
            else if ($browserWidth >= 480 && $browserWidth < 767) {
                $cols = 2;
            }
            else if ($browserWidth >= 0 && $browserWidth < 480) {
                $cols = 1;
            }
            //console.log("Browser width is:" + $browserWidth);
            //console.log("Cols is:" + $cols);

            // $gutterTotal = $cols * 20;
            $browserWidth = $browserWidth; // - $gutterTotal;
            $itemWidth = $browserWidth / $cols;
            $itemWidth = Math.floor($itemWidth);

            $(".element").each(function (index) {
                $(this).css({ "width": $itemWidth + "px" });
            });



            var $optionSets = $('#options .option-set'),
                $optionLinks = $optionSets.find('a');

            $optionLinks.click(function () {
                var $this = $(this);
                // don't proceed if already selected
                if ($this.hasClass('selected')) {
                    return false;
                }
                var $optionSet = $this.parents('.option-set');
                $optionSet.find('.selected').removeClass('selected');
                $this.addClass('selected');

                // make option object dynamically, i.e. { filter: '.my-filter-class' }
                var options = {},
                    key = $optionSet.attr('data-option-key'),
                    value = $this.attr('data-option-value');
                // parse 'false' as false boolean
                value = value === 'false' ? false : value;
                options[key] = value;
                if (key === 'layoutMode' && typeof changeLayoutMode === 'function') {
                    // changes in layout modes need extra logic
                    changeLayoutMode($this, options)
                } else {
                    // otherwise, apply new options
                    $container.isotope(options);
                }

                return false;
            });

        };

    }



});