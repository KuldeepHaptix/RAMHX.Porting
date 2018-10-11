$(window).load(function(){
    "use strict";
            var $container = $('.projectContainer');
            $container.isotope({
                filter: '*',

                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false

                }
            });
         
            $('.projectFilter a').click(function(){
                $('.projectFilter .current').removeClass('current');
                $(this).addClass('current');
         
                var selector = $(this).attr('data-filter');
                $container.isotope({
                    filter: selector,
                    
                    animationOptions: {
                        duration: 750,
                        easing: 'linear',
                        queue: false
                    }
                 });
                 return false;
            }); 
            // End isotope Home 1

            var $containerPortfolioGrid1 = $('.portfolio-grid-1-container');
            $container.isotope({
                filter: '*',
                animationOptions: {
                    duration: 750,

                    easing: 'linear',
                    queue: false

                }
            });
         
         // Isotope Blog
         var $containerBlog = $('.blogContainer');
            $containerBlog.isotope({
                filter: '*',

                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false

                }
            });
         
            $('.blogFilter a').click(function(){
                $('.blogFilter .current').removeClass('current');
                $(this).addClass('current');
         
                var selector = $(this).attr('data-filter');
                $containerBlog.isotope({
                    filter: selector,
                    
                    animationOptions: {
                        duration: 750,
                        easing: 'linear',
                        queue: false
                    }
                 });
                 return false;
            }); 
           

});