$(document).ready(function () {
    $('.fb-choose').click(function () {
        $('#MainBody_photoid').val($(this).attr("alt"));
        $('#select-fb-photo').hide();
    });

    $('#facebook-photos').click(function (event) {
        event.preventDefault();

        $.ajax({
            url: 'https://graph.facebook.com/me/photos?access_token=',
            data: { val: "Hello world" },
            dataType: 'json',
            success: function (json) {
                // Data processing code
                $('body').append('Response Value: ' + json.val);
            }
        });

        $('#select-fb-photo').show();
    });

    $('.non-select').click(function () {
        $('.selected').addClass("non-select");
        $('.selected').removeClass("selected");
        $(this).addClass("selected");
        $(this).removeClass("non-select");
        $('#SelectedDo').val($(this).attr("id").replace("pic", ""));
    });

    $('#DoSelectSubmit').click(function () {
        if ($('#SelectedDo').val() != "-1") {
            rpxSocial($('#SelectedDo').val());
        }
    });

    $('#home-pop').click(function (event) {
        event.preventDefault();
        $('#home-popup').show();
    });

    $('a.close-hme-popup').click(function (event) {
        event.preventDefault();
        $('#home-popup').hide();
    });

    $('#log_pop_button').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #login_popup').show();
        $('#log-intro').hide();

    });

    $('#rmd-close-x').click(function (event) {
        event.preventDefault();
        $('#submit-rmd').hide();
    });

    $('#join-ratemydo').click(function (event) {
        event.preventDefault();
        //$('#rmd-popup').show();
        $('#submit-rmd').show();
    });

    $('#rating_nonlogged').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });

    $('#rating_nonlogged').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });
    $('#rating_nonlogged2').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });
    $('#rating_nonlogged3').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });
    $('#rating_nonlogged4').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });
    $('#rating_nonlogged5').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });

    $('#MainBody_txtPassword').hide();

    $('#password-clear').focus(function () {
        $('#password-clear').hide();
        $('#MainBody_txtPassword').show();
        $('#MainBody_txtPassword').focus();
    });

    $('#MainBody_txtPassword').blur(function () {
        if ($('#MainBody_txtPassword').val() == '') {
            $('#password-clear').show();
            $('#MainBody_txtPassword').hide();
        }
    });


    $('#MainBody_NoLogCommentSubmit').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });

    $('#MainBody_SubmitCommentNonLogin').click(function (event) {
        event.preventDefault();
        $('#screen_cover, #log-intro').show();
    });

    $('a.refer-pop').click(function (event) {
        event.preventDefault();
        $('#refer-friend-pop').show();
    });

    $('a.close-pop').click(function (event) {
        event.preventDefault();
        $('#refer-friend-pop').hide();
    });

    $('a.press-button').click(function (event) {
        event.preventDefault();
        $('#press-pop').show();
    });

    $('a.close-press').click(function (event) {
        event.preventDefault();
        $('#press-pop').hide();
    });


    //this makes the default value of an input disappear when you focus on the input text field.
    //default values reappear when input is unfocused under the condition that value="".
    var smart_input = function (input_id, default_value) {
        $(input_id).focus(function () {
            if ($(input_id).val() === default_value) {
                $(this).val('');
                $(input_id).focusout(function () {
                    if ($(this).val() === '') {
                        $(this).val(default_value);
                    } //ends if statement
                }); //ends focusout
            }; //ends if statement
        }); //ends focus
    }; //ends the smart_input function

    smart_input('#looking_for', 'What are you looking for?');
    smart_input('#search_location', 'City, State or Zip');
    smart_input('#location_search_header', 'City, State or Zip');
    smart_input('#looking_for_header', 'What are you looking for?');

    smart_input('#MainBody_txtName', 'Full Name');
    smart_input('#MainBody_txtShopName', 'Business Name');
    smart_input('#MainBody_txtEmail', 'Email');
    smart_input('#MainBody_txtPassword', 'Password');
    smart_input('#MainBody_txtProviderName', 'Barber/Stylist Name');
    smart_input('#MainBody_txtProviderEmail', 'Barber/Stylist Email');

    //controls the login popup
    //when you click login the sreen cover and login box show
    //when you click the close button or screen cover the login is hidden
    $('#login').click(function () {
        $('#screen_cover').show();
        $('#login_popup').show();
    });

    $('#screen_cover, #close_button').click(function () {
        $('#screen_cover').hide();
        $('#login_popup').hide();
    });



    //controls the gallery popup
    var hideGallery = function () {

        $('#full_style_gallery').hide();
        $('#screen_cover').hide();
    }

    $('#view_more_button').click(function () {
        $('#screen_cover, #full_style_gallery').show();
    });

    $('#close_gallery, #screen_cover').click(function () {
        hideGallery();
    });

    //IRA ADDITION March 13, 2012
    //myfaves gallery
    var hidePics = function () {

        $('#fav_pic_gallery').hide();
        $('#screen_cover').hide();
    }

    $('#view_pics_button').click(function () {
        $('#screen_cover, #fav_pic_gallery').show();
    });

    $('#close_pics, #screen_cover').click(function () {
        hidePics();
    });

    //IRA ADDITION March 13, 2012
    //full services list
    var hideServices = function () {

        $('#full_services_list').hide();
        $('#screen_cover').hide();
    }

    $('#MainBody_view_full_services').click(function () {
        $('#screen_cover, #full_services_list').show();
    });

    $('#close_serv, #screen_cover').click(function () {
        hideServices();
    });

    //IRA ADDITION March 17, 2012
    //social sharing popup
    var hideSocial = function () {

        $('#share_pop').hide();
        $('#screen_cover').hide();
    }

    $('#view_social').click(function () {
        $('#screen_cover, #share_pop').show();
    });

    $('#screen_cover').click(function () {
        hideSocial();
    });


    var hideBio = function () {
        $('#full_bio').hide();
        $('#screen_cover').hide();
    }

    $('#view_bio_button').click(function () {
        $('#screen_cover, #full_bio').show();
    });

    $('#close_bio, #screen_cover').click(function () {
        hideBio();
    });


    var hidePayPal = function () {
        $('#paypal_full').hide();
        $('#screen_cover').hide();
    }

    $('#paypal_select').click(function () {
        $('#screen_cover, #paypal_full').show();
    });

    $('#close_paypal, #screen_cover').click(function () {
        hidePayPal();
    });


    var hideContact = function () {
        $('#customer-contact').hide();
        $('#screen_cover').hide();
    }

    $('#customer-contact-us').click(function () {
        $('#screen_cover, #customer-contact').show();
    });

    $('#customer-contact-close, #screen_cover').click(function () {
        hideContact();
    });




    var hideLogPrompt = function () {
        $('#log-intro').hide();
        $('#screen_cover').hide();
    }

    $('#MainBody_GetStyleNoLog, #MainBody_LikeStyleNoLog').click(function () {
        $('#screen_cover, #log-intro').show();
    });

    $('#log-intro-close, #screen_cover').click(function () {
        hideLogPrompt();
    });

    $('#MainBody_txtSchoolName').focus(function () {
        if ($(this).val() == "School Name") {
            $(this).val("");
        }
    });

    $('#MainBody_txtSchoolName').blur(function () {
        if ($(this).val() == "") {
            $(this).val("School Name");
        }
    });

    $('#social_cover').click(function () {
        $('#select-fb-photo').hide();
        $('#social_cover').hide();
    });

});
