function toggleSelectedAnnouncement() {
    $("#selectedAnnouncement").toggle();
}

$(function () {
    // When an announcement is selected.
    $(".announcement-button").click(function () {

        $("#noAnnouncementContainer").hide();
        $("#selectedAnnouncement").show();

        // The selected announcement changes colour
        $(".announcement-button").removeClass("selected");
        $(this).addClass("selected");
    });

    // When create new announcement link is clicked
    $(".create-announcement-link").click(function () {

        $("#noAnnouncementContainer").hide();
        $("#selectedAnnouncement").show();

        // Change colours of announcements back to default.
        $(".announcement-button").removeClass("selected");
    });
});