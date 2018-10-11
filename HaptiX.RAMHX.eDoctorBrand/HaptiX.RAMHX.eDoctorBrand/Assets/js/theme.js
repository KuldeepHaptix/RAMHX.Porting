/*
    Main Jquery File
*/

(function($) {
    "use strict";
    
    
    $(document).ready(function(){       
        
        /*----------------------------------------------------*/
        /*  Slider Preloader
        /*----------------------------------------------------*/
        $(".preloader").delay(700).fadeOut();
        
        /*----------------------------------------------------*/
        /*  Appointment Date
        /*----------------------------------------------------*/
        $('input[name="date"]').datepicker();
    
        /*----------------------------------------------------*/
        /*  Go Top
        /*----------------------------------------------------*/
        $('a[href="#appointment"]').on('click',function () {
            $('html, body').animate({ scrollTop: 350 }, 800);
            return false
        });  
        
        /*----------------------------------------------------*/
        /*  Appointment Date
        /*----------------------------------------------------*/
//        $('.appointment_home_form input').on('blur',function () {
//            if ($(this).val()) {
//                $(this).addClass('notEmpty')
//            }
//            else{
//                $(this).removeClass('notEmpty')
//            }
//        });
        
        /*----------------------------------------------------*/
        /*  Time Table Filter
        /*----------------------------------------------------*/
        var tableCell = $('.cell');
        $('.timeTableFilters li').on('click', function () {
            $('.active').removeClass('active');
            $(this).addClass('active');
            
            var filter_val = $(this).attr('data-filter');
            
            tableCell.addClass('bgf');            
            if(filter_val == 'all'){
                tableCell.removeClass('bgf')
            }
            else{
                tableCell.addClass('bgf');
                $('.timeTable td.'+ filter_val).removeClass('bgf')
            }            
        });


        /*----------------------------------------------------*/
        /*  Count Up
        /*----------------------------------------------------*/
        $('.counter').counterUp({
            delay: 15,
            time: 1500
        });

        /*----------------------------------------------------*/
        /*  Owl Carousels
        /*----------------------------------------------------*/        
        $('.offer_service_carousel').owlCarousel({
            loop:true,
            margin:0,
            nav:false,
            items:1
        });
        
        $('.clients_carousel').owlCarousel({
            loop:true,
            responsiveClass:true,
            dots:false,
            nav:true,
            navText:['<i class="fa fa-angle-right"></i>','<i class="fa fa-angle-left"></i>'],
            responsive:{
                0:{
                    items:2,
                    nav:true
                },
                500:{
                    items:3,
                    nav:true
                },
                992:{
                    items:4,
                    nav:false
                },
                1200:{
                    items:4,
                    nav:true,
                    loop:false
                }
            }
        })
        
    });
    
    
        
    /*----------------------------------------------------*/
    /*  Testimonial Slider
    /*----------------------------------------------------*/    
    $('.testimonial_slider').flexslider({
        animation: "fade",
        directionNav: false
    });
        
    /*----------------------------------------------------*/
    /*  Aflix
    /*----------------------------------------------------*/
    $(".navbar2,.navbar3").affix({
        offset: {
            top: $('.top_bar').height()
        }
    });
    
        
    /*----------------------------------------------------*/
    /*  Background Slider
    /*----------------------------------------------------*/    
    $('.background_slider').flexslider({
        animation: "fade",
        directionNav: false,
        controlNav: false
    })
    
})(jQuery)

