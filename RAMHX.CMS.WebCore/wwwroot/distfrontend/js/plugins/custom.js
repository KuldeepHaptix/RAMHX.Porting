$(document).ready(function(){
     "use strict";
    // <!-- Intializing Navigation Effect-->
     $('ul.navi-level-1 li').on('mouseenter', function(){
        $(this).children('ul.navi-level-2').addClass("open-navi-2 animated fadeInUp");
    });
    $('ul.navi-level-1 li').on('mouseleave', function(){
       $(this).children('ul.navi-level-2').removeClass("open-navi-2 animated fadeInUp");
    });


       

     
     // <!-- Intializing Navi Menu-->
      $("#mobile-menu").mobileMenu({
                MenuWidth: 250,
                SlideSpeed : 400,
                WindowsMaxWidth : 991,
                PagePush : false,
                FromLeft : false,
                Overlay : true,
                CollapseMenu : true,
                ClassName : "mobile-menu"
            });
    //Mobile Menu Scroll Enabel
    
    // --------------------------------------------------
    // Back To Top
    // --------------------------------------------------
    var offset = 450;
    var duration = 500;
    
            
   $(window).on('scroll', function(){
         if ($(this).scrollTop() > offset) {
                $('#to-the-top').fadeIn(duration);
            } else {
                $('#to-the-top').fadeOut(duration);
            }
    });

    $('#to-the-top').on('click', function(event){
        event.preventDefault();
        $('html, body').animate({scrollTop: 0}, duration);
        return false;
    });
    //Mobile Menu Scroll Enabel
        $(window).load(function(){
            $(".mCustomScrollbar").mCustomScrollbar(
                {
                    scrollInertia:400
                });

        });

    // wow jquery
    new WOW().init();
    // Skroll animate header
    var s = skrollr.init(
        {
            forceHeight: false,
            mobileCheck: function() {
            //hack - forces mobile version to be off
            return false;
            }

        }
        
    );
// =====================================================
 });