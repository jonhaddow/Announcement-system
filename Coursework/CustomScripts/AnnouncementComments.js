function deleteComment(commentId) {
    $.ajax({
        url: "/Comments/Delete",
        type: "POST",
        data: {
            __RequestVerificationToken: $('#addCommentForm input[name=__RequestVerificationToken]').val(),
            id: commentId
        }
    }).done(function (result) {
        $("#comments").html(result);
    });
}

$(function () {
    
    // Show delete icon on hover
    $(".comment-block").hover(
        function () {
            $(this).children(".modify-comment").show();
        }, function () {
            $(this).children(".modify-comment").hide();
        }
    );
});