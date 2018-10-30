$(document).ready(function(){
 quiz.OnBegin();     
});
var quiz = {
OnBegin: function () {
 quiz.fixedNavigation (); 
 quiz.pageScroll();
 quiz.clickToScroll ();
 quiz.showhide ();     
 },
/*** fixed nav ***/
fixedNavigation: function() {
  $(window).on('scroll load', function() {
  var navHeight = $( ".header" ).innerHeight();
   if ($(window).scrollTop() > navHeight) {
      $('.head-btm').addClass('fixed animated fadeInDown');
      }
    else {
      $('.head-btm').removeClass('fixed fadeInDown');
      }          
  });      
}, 
/*** Page Scroll ***/
pageScroll: function() {
 // jQuery for page scrolling feature - requires jQuery Easing plugin
 $('a.page-scroll').bind('click', function(event) {
  var $anchor = $(this);
  $('html, body').stop().animate({
    scrollTop: ($($anchor.attr('href')).offset().top - 100)
    }, 3000, 'easeInOutExpo');
  event.preventDefault();
  });	
},
/***** Show hide ******/	
showhide:function() {
 $(window).scroll(function(){
 if ($(this).scrollTop() > 300) {
    $('.click-to-scroll').fadeIn();
    } 
 else {
    $('.click-to-scroll').fadeOut();
    }
 });
},
/***** click to scroll  ******/	   
clickToScroll:function() {
$('.click-to-scroll').click(function () {
 $("html, body").animate({
 scrollTop: 0
 }, 600);
 return false;
 });
} 
    
    
}

/*** Accordian ***/   
function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);

//$('.selectpicker').selectpicker({
//    style: 'btn-info',
//    size: 4
//});

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        container: 'body'
    });
});

function userData() {
    debugger
    $.ajax
    ({
        url: '/Account/Profile',
        datatype:'json',
        type: 'get',
        data:{ },
        success: function(result){
            debugger
            $('#UName').val(result.Name)
            $('#Emid').empty();
            $('#Emid').append(result.EmailId);
            $('#PhnId').val('978-456-1287');
        },
        error: function (data) {
            debugger
            alert("Record not deleted")
        },
        
            
    });
}
function ProfChange() {
    debugger
    $('.alertMessage').show();
    $(".alertMessage").fadeTo(1500, 500).slideUp(500, function () {
        $(".alertMessage").hide();
    });
    $('#pwd').val("");
    $('#conPwd').val("");
}
function pwdChange() {
    debugger
    $('#pwd').val("");
    $('#conPwd').val("");
}



    



