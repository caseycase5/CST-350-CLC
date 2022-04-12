$(function () {
    blinkeffect('#txtblnk');
})
function blinkeffect(selector) {
    $(selector).fadeOut('slow', function () {
        $(this).fadeIn('slow', function () {
            blinkeffect(this);
        });
    });
}