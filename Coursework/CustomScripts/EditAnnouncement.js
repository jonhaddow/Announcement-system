$(function () {

    $("#arrowBack").click(function () {
        $.ajax({
            url: "/Announcements/GetAnnouncement",
            data: { announcementId: id },
        }).done(function (result) {
            $("#displayAnnouncement").html(result);
        });
    });

    $("#editForm").submit(function () {

        $.ajax({
            url: "/Announcements/Edit",
            type: "POST",
            data: $(this).serialize()
        }).done(function (result) {
            $("#displayAnnouncement").html(result);
        });

        return false;
    });

    
});