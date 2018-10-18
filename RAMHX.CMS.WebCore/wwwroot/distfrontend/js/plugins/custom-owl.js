 $(document).ready(function(){
  "use strict";
        $("#owl-partners").owlCarousel({
           
                autoPlay: true, //Set AutoPlay to 3 seconds
                items : 5,
                itemsDesktop      : [1199,5],
                itemsDesktopSmall     : [979,4],
                itemsTablet       : [768,3],
                itemsMobile       : [479,1],
                pagination:false,
                navigation:false,
            });
        var owlPartners = $("#owl-partners");
        // Custom Navigation Events

        $("body").on("click",".next-partners",function(event){
          owlPartners.trigger("owl.next");
        });
        $("body").on("click",".prev-partners",function(event){
          owlPartners.trigger("owl.prev");
        });
        // End Owl Partners
        $("#owl-partners2").owlCarousel({
           
                autoPlay: true, //Set AutoPlay to 3 seconds
                items : 5,
                itemsDesktop      : [1199,5],
                itemsDesktopSmall     : [979,5],
                itemsTablet       : [768,4],
                itemsMobile       : [479,1],
                pagination:true,
                navigation:false,
            });
        var owlPartners2 = $("#owl-partners2");
        // Custom Navigation Events

        $("body").on("click",".next-partners2",function(event){
          owlPartners2.trigger("owl.next");
        });
        $("body").on("click",".prev-partners2",function(event){
          owlPartners2.trigger("owl.prev");
        });
        // End Owl Partners2
         $("#owl-testimonials-2-columns").owlCarousel({
               
                    autoPlay: false, //Set AutoPlay to 3 seconds
                    items : 2,
                    itemsDesktop      : [1199,2],
                    itemsDesktopSmall     : [979,2],
                    itemsTablet       : [768,1],
                    itemsMobile       : [479,1],
                    pagination:true,
                    navigation:false,
               
                });
        var owlTestimonials2columns = $("#owl-testimonials-2-columns");
        // Custom Navigation Events

        $("body").on("click",".next-testimonials-2-columns",function(event){
          owlTestimonials2columns.trigger("owl.next");
        });
        $("body").on("click",".prev-testimonials-2-columns",function(event){
          owlTestimonials2columns.trigger("owl.prev");
        });
        // End Owl Testimonial 2 columns

         $("#owl-testimonials-1-columns").owlCarousel({
               
                    autoPlay: false, //Set AutoPlay to 3 seconds
                    items : 1,
                    itemsDesktop      : [1199,1],
                    itemsDesktopSmall     : [979,1],
                    itemsTablet       : [768,1],
                    itemsMobile       : [479,1],
                    pagination:false,
                    navigation:false,
                    transitionStyle : "fade",
                });
        var owlTestimonials1columns = $("#owl-testimonials-1-columns");
        // Custom Navigation Events

        $("body").on("click",".next-testimonials-1-columns",function(event){
          owlTestimonials1columns.trigger("owl.next");
        });
        $("body").on("click",".prev-testimonials-1-columns",function(event){
          owlTestimonials1columns.trigger("owl.prev");
        });
    // End Owl Testimonial 1 columns

    $("#owl-testimonials-1-columns-v2").owlCarousel({
               
                    autoPlay: false, //Set AutoPlay to 3 seconds
                    items : 1,
                    itemsDesktop      : [1199,1],
                    itemsDesktopSmall     : [979,1],
                    itemsTablet       : [768,1],
                    itemsMobile       : [479,1],
                    pagination:false,
                    navigation:false,
                    transitionStyle : "fade",
                });
        var owlTestimonials1columnsV2 = $("#owl-testimonials-1-columns-v2");
        // Custom Navigation Events

        $("body").on("click",".next-testimonials-1-columns-v2",function(event){
          owlTestimonials1columnsV2.trigger("owl.next");
        });
        $("body").on("click",".prev-testimonials-1-columns-v2",function(event){
          owlTestimonials1columnsV2.trigger("owl.prev");
        });
    // End Owl Testimonial 1 columns
        $("#owl-blog-1-column").owlCarousel({
               
                    autoPlay: false, //Set AutoPlay to 3 seconds
                    items : 1,
                    itemsDesktop      : [1199,1],
                    itemsDesktopSmall     : [979,1],
                    itemsTablet       : [768,1],
                    itemsMobile       : [479,1],
                    pagination:false,
                    navigation:false
                });
        var owlBlog1Column = $("#owl-blog-1-column");
        // Custom Navigation Events

        $("body").on("click",".next-blog-1-column",function(event){
          owlBlog1Column.trigger("owl.next");
        });
        $("body").on("click",".prev-blog-1-column",function(event){
          owlBlog1Column.trigger("owl.prev");
        });
    // End Owl Blog 1 column
    $("#owl-team-3-columns").owlCarousel({
               
                    autoPlay: false, //Set AutoPlay to 3 seconds
                    items : 3,
                    itemsDesktop      : [1199,3],
                    itemsDesktopSmall     : [979,2],
                    itemsTablet       : [768,2],
                    itemsMobile       : [479,1],
                    pagination:true,
                    navigation:false,
               
                });
        var owlTeam3Columns = $("#owl-team-3-columns");
        // Custom Navigation Events

        $("body").on("click",".next-team",function(event){
          owlTeam3Columns.trigger("owl.next");
        });
        $("body").on("click",".prev-team",function(event){
          owlTeam3Columns.trigger("owl.prev");
        });
    // End Owl Testimonial 3 columns

    $("#owl-feature").owlCarousel({
           
                autoPlay: true, //Set AutoPlay to 3 seconds
                items : 3,
                itemsDesktop      : [1199,3],
                itemsDesktopSmall     : [979,3],
                itemsTablet       : [768,2],
                itemsMobile       : [479,1],
                pagination:false,
                navigation:false,
            });
        var owlFeature = $("#owl-feature");
        // Custom Navigation Events

        $("body").on("click",".next-feature",function(event){
          owlFeature.trigger("owl.next");
        });
        $("body").on("click",".prev-feature",function(event){
          owlFeature.trigger("owl.prev");
        });
        // End Owl feature
        $("#owl-onsale").owlCarousel({
           
                autoPlay: true, //Set AutoPlay to 3 seconds
                items : 3,
                itemsDesktop      : [1199,3],
                itemsDesktopSmall     : [979,3],
                itemsTablet       : [768,2],
                itemsMobile       : [479,1],
                pagination:false,
                navigation:false,
            });
        var owlOnsale = $("#owl-onsale");
        // Custom Navigation Events

        $("body").on("click",".next-onsale",function(event){
          owlOnsale.trigger("owl.next");
        });
        $("body").on("click",".prev-onsale",function(event){
          owlOnsale.trigger("owl.prev");
        });
        // End Owl on Sale
        $("#owl-best-sale").owlCarousel({
           
                autoPlay: true, //Set AutoPlay to 3 seconds
                items : 3,
                itemsDesktop      : [1199,3],
                itemsDesktopSmall     : [979,3],
                itemsTablet       : [768,2],
                itemsMobile       : [479,1],
                pagination:false,
                navigation:false,
            });
        var owlBestSale = $("#owl-best-sale");
        // Custom Navigation Events

        $("body").on("click",".next-best-sale",function(event){
          owlBestSale.trigger("owl.next");
        });
        $("body").on("click",".prev-best-sale",function(event){
          owlBestSale.trigger("owl.prev");
        });
        // End Owl on Sale

        $("#owl-relate").owlCarousel({
           
                autoPlay: true, //Set AutoPlay to 3 seconds
                items : 3,
                itemsDesktop      : [1199,3],
                itemsDesktopSmall     : [979,3],
                itemsTablet       : [768,2],
                itemsMobile       : [479,1],
                pagination:false,
                navigation:false,
            });
        var owlRelate = $("#owl-relate");
        // Custom Navigation Events

        $("body").on("click",".next-relate",function(event){
          owlRelate.trigger("owl.next");
        });
        $("body").on("click",".prev-relate",function(event){
          owlRelate.trigger("owl.prev");
        });
        // End Owl feature

        var owlServicesDetail = $("#sync3");
        // Custom Navigation Events

         $("body").on("click",".next-detail-services",function(event){
          owlServicesDetail.trigger("owl.next");
        });
        $("body").on("click",".prev-detail-services",function(event){
          owlServicesDetail.trigger("owl.prev");
        });

         $(document).ready(function() {
          var sync1 = $("#sync3");
          var sync2 = $("#sync4");
          sync1.owlCarousel({
            singleItem : true,
            slideSpeed : 1000,
            navigation: false,
            pagination:false,
            transitionStyle : "fade",
            afterAction : syncPosition,
            responsiveRefreshRate : 200,
              afterInit : progressBar,
              afterMove : moved,
              startDragging : pauseOnDragging,
              
          });
          
          var time = 5; // time in seconds

          var $progressBar,
              $bar, 
              $elem, 
              isPause, 
              tick,
              percentTime;

         
            //Init progressBar where elem is $("#owl-demo")
            function progressBar(elem){
              $elem = elem;
              //build progress bar elements
              buildProgressBar();
              //start counting
              start();
            }
         
            //create div#progressBar and div#bar then prepend to $("#owl-demo")
            function buildProgressBar(){
              $progressBar = $("<div>",{
                id:"progressBar"
              });
              $bar = $("<div>",{
                id:"bar"
              });
              $progressBar.append($bar).prependTo($elem);
            }
         
            function start() {
              //reset timer
              percentTime = 0;
              isPause = false;
              //run interval every 0.01 second
              tick = setInterval(interval, 10);
            };
         
            function interval() {
              if(isPause === false){
                percentTime += 1 / time;
                $bar.css({
                   width: percentTime+"%"
                 });
                //if percentTime is equal or greater than 100
                if(percentTime >= 100){
                  //slide to next item 
                  $elem.trigger('owl.next')
                }
              }
            }
         
            //pause while dragging 
            function pauseOnDragging(){
              isPause = true;
            }
         
            //moved callback
            function moved(){
              //clear interval
              clearTimeout(tick);
              //start again
              start();
            }




          
          sync2.owlCarousel({
            items : 5,
            itemsDesktop      : [1199,5],
            itemsDesktopSmall     : [979,5],
            itemsTablet       : [768,5],
            itemsMobile       : [479,5],
            pagination:false,
            responsiveRefreshRate : 100,
            afterInit : function(el){
              el.find(".owl-item").eq(0).addClass("synced");
            }
          });
         
          function syncPosition(el){
            var current = this.currentItem;
            $("#sync4")
              .find(".owl-item")
              .removeClass("synced")
              .eq(current)
              .addClass("synced")
            if($("#sync4").data("owlCarousel") !== undefined){
              center(current)
            }
          }
         
          $("#sync4").on("click", ".owl-item", function(e){
            e.preventDefault();
            var number = $(this).data("owlItem");
            sync1.trigger("owl.goTo",number);
          });
         
          function center(number){
            var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
            var num = number;
            var found = false;
            for(var i in sync2visible){
              if(num === sync2visible[i]){
                var found = true;
              }
            }
         
            if(found===false){
              if(num>sync2visible[sync2visible.length-1]){
                sync2.trigger("owl.goTo", num - sync2visible.length+2)
              }else{
                if(num - 1 === -1){
                  num = 0;
                }
                sync2.trigger("owl.goTo", num);
              }
            } else if(num === sync2visible[sync2visible.length-1]){
              sync2.trigger("owl.goTo", sync2visible[1])
            } else if(num === sync2visible[0]){
              sync2.trigger("owl.goTo", num-1)
            }
            
          }
         
        }); 

        // End Owl Services Detail Sldie


        var owlProcess = $("#sync");
        // Custom Navigation Events

         $(document).ready(function() {
          var sync1 = $("#sync5");
          var sync2 = $("#sync6");
          sync1.owlCarousel({
            singleItem : true,
            slideSpeed : 1000,
            navigation: false,
            pagination:false,
            transitionStyle : "fade",
            afterAction : syncPosition,
            responsiveRefreshRate : 200,
              afterInit : progressBar,
              afterMove : moved,
              startDragging : pauseOnDragging,
              
          });
          
          var time = 5; // time in seconds

          var $progressBar,
              $bar, 
              $elem, 
              isPause, 
              tick,
              percentTime;

         
            //Init progressBar where elem is $("#owl-demo")
            function progressBar(elem){
              $elem = elem;
              //build progress bar elements
              buildProgressBar();
              //start counting
              start();
            }
         
            //create div#progressBar and div#bar then prepend to $("#owl-demo")
            function buildProgressBar(){
              $progressBar = $("<div>",{
                id:"progressBar"
              });
              $bar = $("<div>",{
                id:"bar"
              });
              $progressBar.append($bar).prependTo($elem);
            }
         
            function start() {
              //reset timer
              percentTime = 0;
              isPause = false;
              //run interval every 0.01 second
              tick = setInterval(interval, 10);
            };
         
            function interval() {
              if(isPause === false){
                percentTime += 1 / time;
                $bar.css({
                   width: percentTime+"%"
                 });
                //if percentTime is equal or greater than 100
                if(percentTime >= 100){
                  //slide to next item 
                  $elem.trigger('owl.next')
                }
              }
            }
         
            //pause while dragging 
            function pauseOnDragging(){
              isPause = true;
            }
         
            //moved callback
            function moved(){
              //clear interval
              clearTimeout(tick);
              //start again
              start();
            }




          
          sync2.owlCarousel({
            items : 4,
            itemsDesktop      : [1199,4],
            itemsDesktopSmall     : [979,4],
            itemsTablet       : [768,2],
            itemsMobile       : [479,1],
            pagination:false,
            responsiveRefreshRate : 100,
            afterInit : function(el){
              el.find(".owl-item").eq(0).addClass("synced");
            }
          });
         
          function syncPosition(el){
            var current = this.currentItem;
            $("#sync6")
              .find(".owl-item")
              .removeClass("synced")
              .eq(current)
              .addClass("synced")
            if($("#sync6").data("owlCarousel") !== undefined){
              center(current)
            }
          }
         
          $("#sync6").on("click", ".owl-item", function(e){
            e.preventDefault();
            var number = $(this).data("owlItem");
            sync1.trigger("owl.goTo",number);
          });
         
          function center(number){
            var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
            var num = number;
            var found = false;
            for(var i in sync2visible){
              if(num === sync2visible[i]){
                var found = true;
              }
            }
         
            if(found===false){
              if(num>sync2visible[sync2visible.length-1]){
                sync2.trigger("owl.goTo", num - sync2visible.length+2)
              }else{
                if(num - 1 === -1){
                  num = 0;
                }
                sync2.trigger("owl.goTo", num);
              }
            } else if(num === sync2visible[sync2visible.length-1]){
              sync2.trigger("owl.goTo", sync2visible[1])
            } else if(num === sync2visible[0]){
              sync2.trigger("owl.goTo", num-1)
            }
            
          }
         
        }); 

        // End Owl Process

        var owlTestimonialSync = $("#sync7");
        // Custom Navigation Events

         $("body").on("click",".next-testimonial-sync",function(event){
          owlTestimonialSync.trigger("owl.next");
        });
        $("body").on("click",".prev-testimonial-sync",function(event){
          owlTestimonialSync.trigger("owl.prev");
        });

         $(document).ready(function() {
          var sync1 = $("#sync7");
          var sync2 = $("#sync8");
          sync1.owlCarousel({
            singleItem : true,
            slideSpeed : 1000,
            navigation: false,
            pagination:false,
            transitionStyle : "fade",
            afterAction : syncPosition,
            responsiveRefreshRate : 200,
              afterInit : progressBar,
              afterMove : moved,
              startDragging : pauseOnDragging,
              
          });
          
          var time = 5; // time in seconds

          var $progressBar,
              $bar, 
              $elem, 
              isPause, 
              tick,
              percentTime;

         
            //Init progressBar where elem is $("#owl-demo")
            function progressBar(elem){
              $elem = elem;
              //build progress bar elements
              buildProgressBar();
              //start counting
              start();
            }
         
            //create div#progressBar and div#bar then prepend to $("#owl-demo")
            function buildProgressBar(){
              $progressBar = $("<div>",{
                id:"progressBar"
              });
              $bar = $("<div>",{
                id:"bar"
              });
              $progressBar.append($bar).prependTo($elem);
            }
         
            function start() {
              //reset timer
              percentTime = 0;
              isPause = false;
              //run interval every 0.01 second
              tick = setInterval(interval, 10);
            };
         
            function interval() {
              if(isPause === false){
                percentTime += 1 / time;
                $bar.css({
                   width: percentTime+"%"
                 });
                //if percentTime is equal or greater than 100
                if(percentTime >= 100){
                  //slide to next item 
                  $elem.trigger('owl.next')
                }
              }
            }
         
            //pause while dragging 
            function pauseOnDragging(){
              isPause = true;
            }
         
            //moved callback
            function moved(){
              //clear interval
              clearTimeout(tick);
              //start again
              start();
            }




          
          sync2.owlCarousel({
            items : 5,
            itemsDesktop      : [1199,5],
            itemsDesktopSmall     : [979,4],
            itemsTablet       : [768,3],
            itemsMobile       : [479,1],
            pagination:true,
            responsiveRefreshRate : 100,
            afterInit : function(el){
              el.find(".owl-item").eq(0).addClass("synced");
            }
          });
         
          function syncPosition(el){
            var current = this.currentItem;
            $("#sync8")
              .find(".owl-item")
              .removeClass("synced")
              .eq(current)
              .addClass("synced")
            if($("#sync8").data("owlCarousel") !== undefined){
              center(current)
            }
          }
         
          $("#sync8").on("click", ".owl-item", function(e){
            e.preventDefault();
            var number = $(this).data("owlItem");
            sync1.trigger("owl.goTo",number);
          });
         
          function center(number){
            var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
            var num = number;
            var found = false;
            for(var i in sync2visible){
              if(num === sync2visible[i]){
                var found = true;
              }
            }
         
            if(found===false){
              if(num>sync2visible[sync2visible.length-1]){
                sync2.trigger("owl.goTo", num - sync2visible.length+2)
              }else{
                if(num - 1 === -1){
                  num = 0;
                }
                sync2.trigger("owl.goTo", num);
              }
            } else if(num === sync2visible[sync2visible.length-1]){
              sync2.trigger("owl.goTo", sync2visible[1])
            } else if(num === sync2visible[0]){
              sync2.trigger("owl.goTo", num-1)
            }
            
          }
         
        }); 

        // End Owl Testimonial Sync
// =====================================================
 });