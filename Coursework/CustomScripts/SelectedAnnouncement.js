$(function () {
    $("#editAnnouncementIcon").click(function () {
        $.ajax({
            url: "/Announcements/Edit",
            data: {
                announcementId: id
            }
        }).done(function (result) {
            $("#displayAnnouncement").html(result);
        });
    });
});