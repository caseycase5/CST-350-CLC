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

$(function () {
    console.log("Page is ready");

    $(document).on('click', '.btn-game', function() {
        event.preventDefault();

        var buttonNumber = $(this).val();
        console.log("Game button " + buttonNumber + " was clicked");
        doButtonUpdate(buttonNumber);
    });
});

function doButtonUpdate(buttonNumber) {
    console.log("In the 'doButtonUpdate' function.");
    $.ajax({
        url: '/Game/ShowUpdatedGrid',
        data: {
            "id": buttonNumber
        },
        success: function (data) {
            //console.log(data);
            $("#gameBoard").html(data);
        },
        error: function (data, thrownError) {
            console.log("Error: " + thrownError);
            console.log(data);
        }
    });
};