$(function () {
    
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
        swal({
            title: "Edit Comment",
            type: "input",
            showCancelButton: true,
            closeOnConfirm: false,
            inputValue: $("#" + commentId).children("div").text()
        }, function (inputValue) {
            if (inputValue === false) return false;
            if (inputValue === "") {
                swal.showInputError("You need to write something!");
                return false
            }
            $.ajax({
                url: "/Comments/Edit",
                type: "POST",
                data: addAntiForgeryToken({
                    announcementId: id,
                    Id: commentId,
                    Content: inputValue
                })
            }).done(function (result) {
                swal({
                    title: "Submitted!",
                    timer: 1200,
                    type: "success",
                    showConfirmButton: false
                });
                $("#displayComments").html(result);
            });
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