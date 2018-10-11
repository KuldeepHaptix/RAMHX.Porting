// --------------------------------------------------
// demo.js by ThemeModern 2016
// --------------------------------------------------

jQuery(document).ready(function($){
	
	
	jQuery(".color1").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/teal.css");
	});
	
	jQuery(".color2").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/blue.css");
	});
	
	jQuery(".color3").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/maroon.css");
	});
	
	jQuery(".color4").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/brown.css");
	});
	
	jQuery(".color5").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/green.css");
	});
	
	jQuery(".color6").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/dark_yellow.css");
	});
	
	jQuery(".color7").click(function(){
		jQuery("#colors").attr("href", "switcher/colors/yellow.css");
	});
	
	jQuery(".custom-show").hide();
	
	jQuery(".custom-close").click(function(){
		jQuery(this).hide();
		jQuery(".custom-show").show();
		jQuery('#switcher').animate({'margin-left': '0px'},'medium');
	});
  	
	jQuery(".custom-show").click(function(){
		jQuery(this).hide();
		jQuery(".custom-close").show();
		jQuery('#switcher').animate({'margin-left': '-300px'},'medium');
	});

	jQuery('#tm-layout-switch').on('change', function() {
		if($('#tm-layout-switch').val()=='wide'){
			$('.compact-boxed-bg').removeAttr('style');
			$('body').removeClass('compact-boxed-bg');			
		}else if($('#tm-layout-switch').val()=='boxed'){
			$('body').addClass('compact-boxed-bg');
			$('.compact-boxed-bg').css('background-image', 'url(switcher/images/patern01.png)');
		}
	});
	
	jQuery('#tm-boxed-bg li').click(function(){		
		if($('#tm-layout-switch').val()!="boxed"){
			alert('Please select boxed layout first.');
		}			
	});

	$('#switcher ul [class*="bg"]').click(function() {
		$('.compact-boxed-bg').css('background-image', 'url(switcher/images/patern' + $(this).attr('data-value') + '.png)');
	});

	jQuery('#de-menu').on('change', function() {
		$('.main-navi').removeClass('no-separator');
		$('.main-navi').removeClass('line-separator');
		$('.main-navi').removeClass('circle-separator');
		$('.main-navi').removeClass('square-separator');
		$('.main-navi').removeClass('plus-separator');
		$('.main-navi').removeClass('strip-separator');
		v = this.value
		if(v=='opt-1'){
			$('.main-navi').removeClass('no-separator');
			$('.main-navi').removeClass('line-separator');
		}else if(v=='opt-2'){
			$('.main-navi').addClass('line-separator');
		}else if(v=='opt-3'){
			$('.main-navi').addClass('circle-separator');
		}else if(v=='opt-4'){
			$('.main-navi').addClass('square-separator');
		}else if(v=='opt-5'){
			$('.main-navi').addClass('plus-separator');
		}else if(v=='opt-6'){
			$('.main-navi').addClass('strip-separator');
		}else if(v=='opt-0'){
			$('.main-navi').addClass('no-separator');
		}
	});

	jQuery('#de-menu-eff').on('change', function() {
		$('.main-navi').removeClass('hover-style-2');
		$('.main-navi').removeClass('hover-style-3');
		$('.main-navi').removeClass('hover-style-4');
		$('.main-navi').removeClass('hover-style-5');
		v = this.value
		if(v=='opt-1'){
			$('.main-navi').addClass('hover-style-2');
		}else if(v=='opt-2'){
			$('.main-navi').addClass('hover-style-3');
		}else if(v=='opt-3'){
			$('.main-navi').addClass('hover-style-4');
		}else if(v=='opt-4'){
			$('.main-navi').addClass('hover-style-5');
		}else if(v=='opt-0'){
			$('.main-navi').removeClass('hover-style-2');
			$('.main-navi').removeClass('hover-style-3');
			$('.main-navi').removeClass('hover-style-4');
			$('.main-navi').removeClass('hover-style-5');
		}
	});

	jQuery('#de-header-layout').on('change', function() {
		v = this.value
		if(v=='opt-1'){
			$('#info').addClass("hide-topbar");
		}else if(v=='opt-2'){
			$('#info').removeClass("hide-topbar");
		}
	});

	$('.demo-homepage > li ').hover(
		function(){ $('.popup-demo-homepage',this).addClass('animated fadeInUp') },
        function(){ $('.popup-demo-homepage',this).removeClass('animated fadeInUp') }
    )
});

