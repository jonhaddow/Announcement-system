$(function () {
    // Add listener to check for form submission.
    $("#addCommentForm").submit(function (e) {
        $.ajax({
            url: "/Comments/Create",
            type: "POST",
            data: addAntiForgeryToken({
                announcementId: id,
                Content: $("#newCommentContent").val()
            }),
        }).done(function (result) {
            $("#displayComments").html(result);
            $("#newCommentContent").val("");
        });
        return false;
    });

    // Show delete icon on hover
    $(".comment-block").hover(
        function () {
            $(this).children(".modify-comment").show();
        }, function () {
            $(this).children(".modify-comment").hide();
        }
    );

    //Edit comment on icon click
    $(".edit-comment-icon").click(function () {
        var commentId = $(this).parent().parent().closest("div").children(".comment-content").attr("id");
        bootbox.prompt({
            size: "small",
            title: "Edit comment:",
            value: $("#" + commentId).children("div").text(),
            callback: function (result) {
                if (result != null && result != "") {
                    $.ajax({
                        url: "/Comments/Edit",
                        type: "POST",
                        data: addAntiForgeryToken({
                            announcementId: id,
                            Id: commentId,
                            Content: result
                        })
                    }).done(function (result) {
                        $("#displayComments").html(result);
                    });
                }
            }
        });
    });

    //Delete comment on icon click
    $(".delete-comment-icon").click(function () {
        var commentId = $(this).parent().parent().closest("div").children(".comment-content").attr("id");
        $.ajax({
            url: "/Comments/Delete",
            type: "POST",
            data: addAntiForgeryToken({
                id: commentId
            })
        }).done(function (result) {
            $("#displayComments").html(result);
        });
    });
});