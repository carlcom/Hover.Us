var animSpeed = 500;
var origX = 0;
var origY = 0;

window.oncontextmenu = function () { return false; };
window.onselectstart = function () { return false; };

function refreshGallery(tagIDs) {
    $('select option:selected').each(function (i, o) {
        if (o.value > 0) tagIDs = tagIDs + o.value + ',';
    });
    $('.gallery').load('/Images/Gallery/' + tagIDs);

    var tags = tagIDs.split(',');
    $('select option').filter(function () {
        return tags.indexOf($(this).val()) >= 0;
    }).prop('selected', true);
}

function showImage(id) {
    $('body').append('<div class="lightbox-background" onclick="javascript:hideImage();"></div>'
        + '<div class="lightbox" onclick="javascript:hideImage();">'
            + '<img class="lightbox-image" src="/Images/Get/' + id + '?x=100&y=100" />'
            + '<div class="image-info"></div>'
        + '</div>');
    $('.lightbox-background').animate({ opacity: 0.75 }, animSpeed, "easeInOutSine");

    var factor = 100;
    var maxX = parseInt($(window).width() / factor) * factor - factor;
    var maxY = parseInt($(window).height() / factor) * factor - factor;
    var aspect = $('#image' + id).width() / $('#image' + id).height();
    if (maxY * aspect < maxX) maxX = parseInt(maxY * aspect);
    else if (maxX / aspect < maxY) maxY = parseInt(maxX / aspect);

    origX = $('#image' + id).offset().left - window.pageXOffset;
    origY = $('#image' + id).offset().top - window.pageYOffset;
    $('.lightbox').animate({ left: origX, top: origY }, 0, "easeInOutSine", function () {
        $('.lightbox').animate({ opacity: 1, left: ($(window).width() - maxX) / 2, top: ($(window).height() - maxY - 50) / 2 }, animSpeed, "easeInOutSine");
        $('.lightbox-image').animate({ width: maxX, height: maxY }, animSpeed, "easeInOutSine", function () {
            $('.image-info').load('/Images/Info/' + id, function () {
                $('.image-info').animate({ opacity: 1 }, animSpeed, "easeInOutSine");
            });
        });
        $('.lightbox-image').prop('src', '/Images/Get/' + id + '?x=' + maxX + '&y=' + maxY);
    });
}

function hideImage() {
    $('.image-info').remove();
    $('.lightbox-image').animate({ width: 0, height: 0 }, animSpeed, "easeInOutSine");
    $('.lightbox').animate({ opacity: 0, left: origX, top: origY }, animSpeed, "easeInOutSine", function () { $('.lightbox').remove(); });
    $('.lightbox-background').animate({ opacity: 0 }, animSpeed, "easeInOutSine", function () { $('.lightbox-background').remove(); });
}