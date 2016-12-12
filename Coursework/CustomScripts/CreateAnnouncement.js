$(function () {

    $("#arrowBack").click(function () {
        location.reload();
    });

    $("#createForm").submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: "/Announcements/Create",
                type: "POST",
                data: $(this).serialize()
            }).done(function (result) {
                location.reload();
            });
        }
        return false;
    });
});