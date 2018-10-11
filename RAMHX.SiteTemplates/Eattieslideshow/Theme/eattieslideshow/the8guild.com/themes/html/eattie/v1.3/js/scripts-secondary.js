//Eattie v1.3 Secondary Pages
//Copyright 2015 8Guild.com
//All scripts for Eatti Restaurant HTML Template

/*Preloading Animation*/
$(window).load(function() {
	$('#status').fadeOut();
	$('#preloader').delay(300).fadeOut('slow');
	$('body').delay(300).css({'overflow':'visible'});
});


	/*Scripts Executed on Page Load*/
	var wrapper = $('#wrapper');
	$(window).on('load', function(){
		if(wrapper.length > 0){
			wrapper.css('margin-top', $(window).height()-90);
		}
	});
		
	/*Scripts Executed on Page Resize*/	
	$(window).on('resize', function(){
		if(wrapper.length > 0){
			wrapper.css('margin-top', $(window).height()-90);
		}
	});
	
	/*Scripts Executed on Page Load*/
	var recipe = $('.recipes-single');
	$(window).on('load', function(){
		if(recipe.length > 0){
			recipe.css('margin-top', $(window).height()-150);
		}
		
		/*Header reveal*/
		var recipePos = $('.recipes-single .inner');
		if(recipePos.length > 0){
			recipePos.waypoint(function(direction) {
					if(direction == 'down'){
						 $('.reveal').addClass('visible');
					} else {
						 $('.reveal').removeClass('visible');
					}
			}, { offset: '60px' });
		}
		
	});
		
	/*Scripts Executed on Page Resize*/	
	$(window).on('resize', function(){
		if(recipe.length > 0){
			recipe.css('margin-top', $(window).height()-150);
		}
	});

/*Document Ready*/
$(document).ready(function(e) {
	
	
	
	 /*Hide Dropdown on Scroll*/
	 $(window).on('scroll', function () {
			$('.dropdown').removeClass('open');	 
	 });
	 
	/*Change Background on hover (Recipes)*/
	if($('.recipes a').length > 0) {
		$('.recipes a').on('mouseenter', function(){
			var myLink = $(this).attr('data-recipe');
			$('.fsbg div').removeClass('visible');
			$('#' + myLink).addClass('visible');
		});
	}
	
	/*Recipe Animated Scroll Up*/
	if($(".recipe-scroll").length > 0){
		
		$(".recipe-scroll").click(function(event){		
			event.preventDefault();
			$('html, body').animate({scrollTop:$(this.hash).offset().top}, 700, 'easeOutCubic');
		});
	}
	
	/*Change Background on hover (Menu)*/
	if($('.menu1 a').length > 0) {
		$('.menu1 a').on('mouseenter', function(){
			var myLink = $(this).attr('data-menu');
			$('.fsbg div').removeClass('visible');
			$('#' + myLink).addClass('visible');
		});
	}
	
	/*Rating*/
	var score = $('.score');
	if(score.length > 0){
		score.raty({
			path: 'img/rating/',
			starOff : 'off.png',
			starOn  : 'on.png'
		});
		score.find('img').addClass('tooltipped');
	}

 /*Tooltips*/
 $('.tooltipped').tooltip();
	
	/*Validation*/
	var reviewForm = $(".review-form");
	if(reviewForm.length > 0){
		reviewForm.validate({
			 rules: {
				email: {
					required: true,
					email: true
				}
			}
		});
	}
	
	$(".subscribe-form").validate({
		 rules: {
			email: {
				required: true,
				email: true
			}
		}
	});
	
});/*/Document ready*/

// jQuery Input Hints plugin by Rob Volk
(function (n) { n.fn.inputHints = function () { function r(t) { jQuery(t).val() == "" && jQuery(t).val(n(t).attr("placeholder")).addClass("hint") } function i(t) { n(t).val() == n(t).attr("placeholder") && n(t).val("").removeClass("hint") } var t = n(this); return t.each(function () { r(this) }), t.closest("form").submit(function () { return t.each(function () { i(this) }), !0 })
, t.focus(function () { i(this) }).blur(function () { r(this) }) } })(jQuery)