$(document).ready(function(){
  $('.featured_style').hover(
							
		function(){
		  $('.view_info', this).show();
			$('.style_info', this).show();
			},
							 
			function(){
			$('.view_info', this).hide();
			$('.style_info', this).hide();
			}
		);

//smart input that works across browsers
//this makes the default value of an input disappear when you focus on the input text field.
//default values reappear when input is unfocused under the condition that value="".
  var smart_input = function( input_id, default_value){
    $(input_id).focus(function(){
      if($(input_id).val() === default_value){
        $(this).val('');
        
        $(input_id).focusout(function(){
          if($(this).val() === ''){
            $(this).val(default_value);
          }//ends if statement
        });//ends focusout
      };//ends if statement
    });//ends focus
  };//ends the smart_input function
 
  smart_input('#txtSearch', 'What are you looking for?');
  smart_input('#txtWhere', 'City, State or Zip');



var hideGallery = function(){
  
  $('#full_style_gallery').fadeOut('fast');
  $('#transparent_background_cover').fadeOut('slow');
}

$('#view_more_button').click(function(){
  $('#transparent_background_cover').fadeIn('fast');
  $('#full_style_gallery').fadeIn('slow');
  
  
});

$('#close_gallery').click(function(){
  hideGallery();
});

$('#transparent_background_cover').click(function(){
   hideGallery();  
});


});
						
					 
