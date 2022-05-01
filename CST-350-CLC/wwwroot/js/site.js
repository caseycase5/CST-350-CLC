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
                // Right mouse click. Image locations are "/img/imageName"
                // App checks to see if there is a flag already in that cell, if not, it will place a flag photo
                var buttonNumber = $(this).val();
                if (document.getElementById("cellImage " + buttonNumber).src == "https://localhost:5001/img/flag.png") {
                    document.getElementById("cellImage " + buttonNumber).src = "";
                    console.log("Cell already has an image");
                }
                else {
                    document.getElementById("cellImage " + buttonNumber).src = "/img/flag.png";
                    console.log("Cell was given an image");
                }
                
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
