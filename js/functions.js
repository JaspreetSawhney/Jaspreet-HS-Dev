function clearTextBox(textBoxID) {
    document.getElementById(textBoxID).value = "";
}

function Del_ID(item) {
    document.getElementById("MainBody_DeleteShopID").value = item;
}

function clearSearchBox(textBoxID) {
    if (document.getElementById(textBoxID).value == "What are you looking for?" || document.getElementById(textBoxID).value == "City, State Or Zip") {
        document.getElementById(textBoxID).value = "";
    }
}

function addToFaves(doid) {
    document.getElementById("do_id").value = doid;
    document.getElementById("interactive-right").innerHTML = '<p>Saved</p><p><a href="do.aspx?id=' + doid + '">View</a></p>';
    document.getElementById("btnDoID").submit();
    //<p><a href="javascript:">Add</a></p><p><a href="do.aspx?id=6">View</a></p>
}

function toggleDiv(divid) {
    /*if (document.getElementById(divid).style.display == 'none') {
        document.getElementById(divid).style.display = 'block';
    } else {
        document.getElementById(divid).style.display = 'none';
    }*/

    //document.getElementById(divid).style.display = 'block';
    if (document.getElementById(divid).style.display != 'none') {
        document.getElementById(divid).style.display = 'none';
    } else {
        document.getElementById(divid).style.display = 'block';
    }
}


function delService(serviceID,shopid) {

    url = "DeleteService.aspx?id=" + serviceID + "&shop=" + shopid
    if (window.XMLHttpRequest) { // Non-IE browsers
        req = new XMLHttpRequest();
        req.onreadystatechange = targetDiv;
        try {
            req.open("GET", url, true);
        } catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = targetDiv;
            req.open("GET", url, true);
            req.send();

        }
    }
}

function targetDiv() {
    if (req.readyState == 4) { // Complete
        if (req.status == 200) { // OK response
            document.getElementById("MainBody_pnlYourServices").innerHTML = req.responseText;
        } else {
            alert("Problem: " + req.statusText);
        }
    }
}


function oajxp(idDo, idMember) {

    url = "AddFave.aspx?idDo=" + idDo + "&idMember=" + idMember;
    if (window.XMLHttpRequest) { // Non-IE browsers
        req = new XMLHttpRequest();
        req.onreadystatechange = targDiv;
        try {
            req.open("GET", url, true);
        } catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = targDiv;
            req.open("GET", url, true);
            req.send();

        }
    }
}

function targDiv() {
    if (req.readyState == 4) { // Complete
        if (req.status == 200) { // OK response
            document.getElementById("image_holder").innerHTML = req.responseText;
        } else {
            alert("Problem: " + req.statusText);
        }
    }
}


function addFave(idDo, idMember) {

    url = "AddFave.aspx?idDo=" + idDo + "&idMember=" + idMember;
    if (window.XMLHttpRequest) { // Non-IE browsers
        req = new XMLHttpRequest();
        req.onreadystatechange = targFaveDiv;
        try {
            req.open("GET", url, true);
        } catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = targFaveDiv;
            req.open("GET", url, true);
            req.send();

        }
    }
}

function targFaveDiv() {
    if (req.readyState == 4) { // Complete
        if (req.status == 200) { // OK response
            document.getElementById("fav_image_holder").innerHTML = req.responseText;
            document.getElementById("MainBody_LikeStyle").innerHTML = "Do Added!";
        } else {
            alert("Problem: " + req.statusText);
        }
    }
}

function rate_do(rating, idDo, idMember) {
    url = "RateDo.aspx?idDo=" + idDo + "&idMember=" + idMember + "&rating=" + rating;
    if (window.XMLHttpRequest) { // Non-IE browsers
        req = new XMLHttpRequest();
        req.onreadystatechange = completeRateMyDo;
        try {
            req.open("GET", url, true);
        } catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = completeRateMyDo;
            req.open("GET", url, true);
            req.send();

        }
    }
}

function rate_do2(rating, idDo, idMember) {
    url = "RateDo2.aspx?idDo=" + idDo + "&idMember=" + idMember + "&rating=" + rating;
    if (window.XMLHttpRequest) { // Non-IE browsers
        req = new XMLHttpRequest();
        req.onreadystatechange = completeRateMyDo;
        try {
            req.open("GET", url, true);
        } catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = completeRateMyDo;
            req.open("GET", url, true);
            req.send();

        }
    }
}

function completeRateMyDo() {
    if (req.readyState == 4) { // Complete
        if (req.status == 200) { // OK response
            document.getElementById("rating-radio").innerHTML = "<h5>Rating Submitted</h5>";
        } else {
            alert("Problem: " + req.statusText);
        }
    }
}


function rate_prov(rating, id, idMember) {
    url = "RateProv.aspx?id=" + id + "&idMember=" + idMember + "&rating=" + rating;
    if (window.XMLHttpRequest) { // Non-IE browsers
        req = new XMLHttpRequest();
        req.onreadystatechange = completeRateMyDo;
        try {
            req.open("GET", url, true);
        } catch (e) {
            alert(e);
        }
        req.send(null);
    } else if (window.ActiveXObject) { // IE
        req = new ActiveXObject("Microsoft.XMLHTTP");
        if (req) {
            req.onreadystatechange = completeRateProv;
            req.open("GET", url, true);
            req.send();

        }
    }
}

function completeRateProv() {
    if (req.readyState == 4) { // Complete
        if (req.status == 200) { // OK response
            document.getElementById("rating-radio").innerHTML = "<span class=&quot;rating-submitted&quot;>Rating Submitted</span>";
        } else {
            alert("Problem: " + req.statusText);
        }
    }
}