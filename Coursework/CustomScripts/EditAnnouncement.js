$(function () {

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
                data: {
                    __RequestVerificationToken: $('#editAnnouncementForm input[name=__RequestVerificationToken]').val(),
                    id: $("#announcementId").val()
                }
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