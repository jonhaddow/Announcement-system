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

    $("#deleteAnnouncementButton").click(function () {
        bootbox.confirm({
            title: "Delete Announcement?",
            message: "Are you sure you want to delete this announcement?",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "Announcements/Delete",
                        type: "POST",
                        data: addAntiForgeryToken({
                            id: id
                        })
                    }).done(function () {
                        // Refresh page
                        location.reload();
                    });
                }

            }
        });
    });
});