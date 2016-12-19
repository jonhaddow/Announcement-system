$(function () {
    // triggered when delete announcement button is clicked.
    $("#deleteAnnouncementButton").click(function () {
        // Set up SweetAlert
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this announcement!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        }, function () {
            // If user confirms, then send ajax request to delete announcement.
            $.ajax({
                url: "Announcements/Delete",
                type: "POST",
                data: {
                    __RequestVerificationToken: $('#editAnnouncementForm input[name=__RequestVerificationToken]').val(), // anti-forgery token.
                    id: $("#selectedAnnouncementId").val()
                }
            }).done(function () {
                // Show "Done!" alert
                swal({
                    title: "Deleted!",
                    timer: 1200,
                    type: "success",
                    showConfirmButton: false
                }, function () {
                    // then reload page.
                    location.reload();
                });
            });
        });
    });
});