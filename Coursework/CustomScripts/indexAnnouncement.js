
$(function () {
    // When an announcement is selected.
    $(".announcement-link").click(function () {
        // The selected announcement changes colour
        $(".announcement-link").removeClass("selected");
        $(this).addClass("selected");
           });

    // When create new announcement link is clicked
    $(".create-announcement-link").click(function () {
        // Change colours of announcements back to default.
        $(".announcement-link").removeClass("selected");
    });
});