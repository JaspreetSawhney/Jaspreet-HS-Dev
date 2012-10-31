window.fbAsyncInit = function () {
    FB.init({
        appId: '186693044757437',
        status: true,
        cookie: true,
        xfbml: true
    });
};
(function (d) {
    var js, id = 'facebook-jssdk'; if (d.getElementById(id)) { return; }
    js = d.createElement('script'); js.id = id; js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    d.getElementsByTagName('head')[0].appendChild(js);
} (document));

function log_user() {
    FB.login(function (response) {
        if (response.authResponse) {
            FB.api('/me', function (response) {
                window.location = "inter-fb.aspx?em=" + response.email;
                //FB.logout(function (response) {
                //    console.log('Logged out.');
                //});
            });
        } else {
            window.location = "Login.aspx?jd=e01";
        }
    }, { scope: 'email' });
}

function reg_user() {
    FB.login(function (response) {
        if (response.authResponse) {
            FB.api('/me', function (response) {
                document.getElementById('MainBody_txtName').value = response.name;
                document.getElementById('MainBody_txtEmail').value = response.email;
                document.getElementById('MainBody_txtPassword').value = "fb__99$";
                document.getElementById('MainBody_btnCreateAccount').click();
            });
        }
    }, { scope: 'email' });
}

function pingFacebook() {
    var body = 'Reading JS SDK documentation';
    document.getElementById('select-fb-photo').style.display = 'block';
    FB.api('/me/photos', function (response) {
        if (!response || response.error) {
            alert('Error occured');
        } else {
            var ul = document.getElementById('select-fb-photo');
            var tempHTML = "<ul id=\"side_bar_img_list\">";
            for (var i = 0, l = response.data.length; i < l; i++) {
                tempHTML = tempHTML + "<li><img src=\"" + response.data[i].picture + "\" onclick=\"SelectFBPhoto(this)\" class\"fb-choose\" alt=\"" + response.data[i].source + "\" \\>";
            }
            tempHTML = tempHTML + "<//ul>";
            ul.innerHTML = tempHTML;
        }
    });
}

function fb_login() {
    FB.login(function (response) {
        if (response.authResponse) {
            var body = 'Reading JS SDK documentation';
            document.getElementById('select-fb-photo').style.display = 'block';
            document.getElementById('social_cover').style.display = 'block';
            FB.api('/me/photos', function (response) {
                if (!response || response.error) {
                    alert('Error occured');
                } else {
                    var ul = document.getElementById('select-fb-photo');
                    var tempHTML = "<h2 class=\"facebook-do-title\">Select A Do From Your Facebook Albums</h2><ul id=\"fb_img_list\">";
                    for (var i = 0, l = response.data.length; i < l; i++) {
                        tempHTML = tempHTML + "<li><img src=\"" + response.data[i].picture + "\" onclick=\"SelectFBPhoto(this)\" class\"fb-choose\" alt=\"" + response.data[i].source + "\" \\>";
                    }
                    tempHTML = tempHTML + "<//ul>";
                    ul.innerHTML = tempHTML;
                }
            });
        }
    }, { scope: 'user_photos' });
}

function fb_login2() {
    FB.login(function (response) {
        if (response.authResponse) {
            var body = 'Reading JS SDK documentation';
            document.getElementById('select-fb-photo').style.display = 'block';
            document.getElementById('social_cover').style.display = 'block';
            FB.api('/me/photos', function (response) {
                if (!response || response.error) {
                    alert('Error occured');
                } else {
                    var ul = document.getElementById('select-fb-photo');
                    var tempHTML = "<h2 class=\"facebook-do-title\">Select A Do From Your Facebook Albums</h2><ul id=\"fb_img_list\">";
                    for (var i = 0, l = response.data.length; i < l; i++) {
                        tempHTML = tempHTML + "<li><img src=\"" + response.data[i].picture + "\" onclick=\"SelectFBPhoto2(this)\" class\"fb-choose\" alt=\"" + response.data[i].source + "\" \\>";
                    }
                    tempHTML = tempHTML + "<//ul>";
                    ul.innerHTML = tempHTML;
                }
            });
        }
    }, { scope: 'user_photos' });
}