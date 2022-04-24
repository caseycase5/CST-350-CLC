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

    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
        console.log("Right click. Preventing context menu.");
    });

    $(document).on("mousedown", '.btn-game', function (event) {
        switch (event.which) {
            case 1:
                // Left mouse click
                event.preventDefault();
                var buttonNumber = $(this).val();
                console.log("Game button " + buttonNumber + " was clicked");
                doButtonUpdate(buttonNumber);
                break;
            case 2:
                // Middle mouse click
                break;
            case 3:
                // Right mouse click
                // add partial update that changes this cell's image to a flag.
                break;
            default:
                alert('Nothing pressed');
        }
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
