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
        if ($(this).valid()) {
            $.ajax({
                url: "/Announcements/Edit",
                type: "POST",
                data: $(this).serialize()
            }).done(function (result) {
                $("#displayAnnouncement").html(result);
            });
        }
        return false;
    });

    $("#deleteAnnouncementButton").click(function () {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this announcement!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        }, function () {
            $.ajax({
                url: "Announcements/Delete",
                type: "POST",
                data: addAntiForgeryToken({
                    id: id
                })
            }).done(function () {
                swal({
                    title: "Deleted!",
                    timer: 1200,
                    type: "success",
                    showConfirmButton: false
                }, function () {
                    location.reload();
                });
            });
        });
    });
});