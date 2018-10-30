 /*** fixed nav ***/
 $(document).ready(function(){
	   $(window).on('scroll load', function() {
	   var navHeight = $( ".header" ).innerHeight();
			 if ($(window).scrollTop() > navHeight) {
				 $('.head-btm').addClass('fixed animated fadeInDown');
			 }
			 else {
				 $('.head-btm').removeClass('fixed fadeInDown');
			 }          
		});
	});
  
 /*** Accordian ***/   
function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);

/*** click to page scroll ***/
$(document).ready(function () {
// jQuery for page scrolling feature - requires jQuery Easing plugin
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 100)
        }, 3000, 'easeInOutExpo');
        event.preventDefault();
    });	
	
});

/*** Click to scrooll up ***/
$(document).ready(function () {
$(window).scroll(function(){
/***** click to scroll  ******/	
	 if ($(this).scrollTop() > 300) {
            $('.click-to-scroll').fadeIn();
        } else {
            $('.click-to-scroll').fadeOut();
        }
	
});

  $('.click-to-scroll').click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 600);
        return false;
    });

});

 
