window.oncontextmenu = function () { return false; };
window.onselectstart = function () { return false; };

function setTag(id) {
    $('.gallery').load('/Images/Gallery/' + id);
}

function showImage(id) {
    currentImage = $('#image' + id);
    $('.image-info').load('/Images/Info/' + id);

    var windowWidth = $(window).width();
    var windowHeight = $(window).height();

    $('.lightbox').css('background-image', 'url("/Images/Get/' + id + '?x=' + windowWidth + '&y=' + windowHeight + '")');
}