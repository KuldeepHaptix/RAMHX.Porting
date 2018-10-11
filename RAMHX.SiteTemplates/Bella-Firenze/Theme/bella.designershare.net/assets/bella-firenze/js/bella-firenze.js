// Required code to run the OWL CAROUSEL (Recommendations) Section 
//*******************
$(document).ready(function() {
      $("#owl-recommendation").owlCarousel({
	  items : 3,
      itemsDesktop : [1000,3], 
      itemsDesktopSmall : [900,2], 
      itemsTablet: [600,2], 
      itemsMobile : [480,1],
      navigation : true,
      slideSpeed : 600,
      pagination: true,
	  autoPlay: true,
	  
	  stopOnHover:true,
        navigationText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
      });

// Required code to run the OWL CAROUSEL (Testimonials) Section 
//*******************
      $("#owl-testimonials").owlCarousel({
      navigation : false,
      slideSpeed : 300,
      pagination: true,
      singleItem : true,
	  autoPlay: true,
	  stopOnHover:true,
	  autoHeight: true,
        navigationText: [
            "<i class='fa fa-angle-left accent'></i>",
            "<i class='fa fa-angle-right accent'></i>"
        ],
      });
	  
// Required code to run the OWL CAROUSEL (Menu) Section 
//*******************
      $("#owl-menu").owlCarousel({
	  items : 3,
      itemsDesktop : [1024,2], 
      itemsDesktopSmall : [900,2], 
      itemsTablet: [600,2], 
      itemsMobile : [480,1],
      navigation : true,
      slideSpeed : 600,
      pagination: true,
	  autoPlay: true,
	  stopOnHover:true,
        navigationText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
      });
    });	
	
// Required code to run WOW.js
//*******************
var wow = new WOW(
  {
    boxClass:     'wow',      // animated element css class (default is wow)
    animateClass: 'animated', // animation css class (default is animated)
    offset:       200,          // distance to the element when triggering the animation (default is 0)
    mobile:       false,       // trigger animations on mobile devices (default is true)
    live:         true,       // act on asynchronously loaded content (default is true)
    callback:     function(box) {
      // the callback is fired every time an animation is started
      // the argument that is passed in is the DOM node being animated
    }
  }
);
wow.init();

// Menu and menu Filter Styles 
//*******************
$(window).load(function(){
if ( $('#menu-masonry').length > 0 ) {
		// Call to Masonry plugin
		var menumasonry = $('#menu-masonry').isotope({
			itemSelector: '.single-item',
			layoutMode: 'fitRows',
			transitionDuration: '.6s',
			hiddenStyle: {
				opacity: 0,
				transform: "scale(.85)"
			},
			visibleStyle: {
				opacity: 1,
				transform: "scale(1)"
			}
		});

// Filtering the menu items
//*******************
$('#menu-masonry-sort a').on( 'click', function(e) {
			e.preventDefault();
			var $this = $(this);
			if ( $this.parent('li').hasClass('active') ) {
				return false;
			} else {
				$this.parent('li').addClass('active').siblings('li').removeClass('active');
			}
			var filterValue = $this.data('target');
			menumasonry.isotope({ filter: filterValue });
			return $this;
		});

	}
	
	  });


// Smooth Scroll
//*******************
//$('a[href*=#]').on('click', function(event){     
//   event.preventDefault();
//   $('html,body').animate({scrollTop:$(this.hash).offset().top}, 800);
//});