 $(document).ready(function(){
  "use strict";
    // <!-- Intializing Slider-->
    jQuery("#slider-h1").revolution({
      sliderType:"standar",
      sliderLayout:"fullscreen",
      delay:6000,
      spinner:"off",
      navigation: {
        arrows:{enable:true},
        onHoverStop:"off"       
      },      
      parallax: {
                  type:"mouse",
                  origo:"slidercenter",
                  speed:2000,
                  levels:[2,3,4,5,6,7,12,16,10,50],
                },
      responsiveLevels:[1240,1024,778,480],
      gridwidth:[1240,1024,778,480],
      gridheight:[600,600,500,400],
      lazyType:"smart",

    });   
   // <!-- Intializing Slider-->
    jQuery("#slider-h5").revolution({
      sliderType:"standar",
      sliderLayout:"fullscreen",
      delay:6000,
      spinner:"off",
      disableProgressBar:"on",
      navigation: {
        arrows:{enable:true},  
        onHoverStop:"off"        
      },  
      lazyType:"smart",    
      responsiveLevels:[1240,1024,778,480],
      gridwidth:[1240,1024,778,480],
      gridheight:[600,600,500,400]  
    }); 
   // <!-- Intializing Slider-->
    jQuery("#slider-h2").revolution({
      sliderType:"standar",
      sliderLayout:"fullscreen",
      delay:6000,
      spinner:"off",
      navigation: {
        arrows:{enable:true},
        onHoverStop:"off"       
      },      
      responsiveLevels:[1240,1024,778,480],
      gridwidth:[1240,1024,778,480],
      gridheight:[600,600,500,400],
      lazyType:"smart",
    });   
    // <!-- Intializing Slider-->
    jQuery("#slider-h3").revolution({
      sliderType:"standar",
      sliderLayout:"fullscreen",
      disableProgressBar:"on",
      delay:6000,
      spinner:"on",
      navigation: {
        arrows:{enable:true},  
        onHoverStop:"off"        
      },      
      lazyType:"smart",
      responsiveLevels:[1240,1024,778,480],
      gridwidth:[1240,1024,778,480],
      gridheight:[600,600,500,300] 
    }); 
     // <!-- Intializing Slider-->
    jQuery("#slider-h4").revolution({
      sliderType:"standar",
      sliderLayout:"fullwidth",
     fullScreenOffsetContainer:".header-v4",
     disableProgressBar:"on",
      delay:6000,
      spinner:"on",
      navigation: {
        arrows:{enable:true},  
        onHoverStop:"off"        
      },    
      parallax: {
                  type:"mouse",
                  origo:"slidercenter",
                  speed:2000,
                  levels:[2,3,4,5,6,7,12,16,10,50],
                },
      responsiveLevels:[1170,1024,778,480],
      gridwidth:[1170,1024,778,480],
      gridheight:[588,588,500,400],
      lazyType:"smart"
    }); 
    // <!-- Intializing Slider-->
    jQuery("#slider-h6").revolution({
      sliderType:"standar",
      sliderLayout:"auto",
      disableProgressBar:"on",
      delay:6000,
      spinner:"on",
      navigation: {
        arrows:{enable:true},   
        onHoverStop:"off"       
      },      
      responsiveLevels:[1170,1024,778,480],
      gridwidth:[740,740,740,480],
      gridheight:[500,500,400,400],
      lazyType:"smart"
    }); 
// =====================================================
 });


