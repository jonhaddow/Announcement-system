// Called before and after each ajax request
function hideSelectedAnnouncement() {
    $("#selectedAnnouncement").hide();
}
function showSelectedAnnouncement() {
    $("#selectedAnnouncement").show();
}

$(function () {
    // When an announcement is selected.
    $(".announcement-button").click(function () {

        $("#noAnnouncementContainer").hide();

        // Change colours of announcements back to default.
        $(".announcement-button").removeClass("selected");
        // The selected announcement changes colour
        $(this).addClass("selected");
    });

    // When create new announcement link is clicked
    $(".create-announcement-link").click(function () {

        $("#noAnnouncementContainer").hide();

        // Change colours of announcements back to default.
        $(".announcement-button").removeClass("selected");
    });
});