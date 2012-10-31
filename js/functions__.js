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


 