// When the delete button is triggered.
function deleteComment(commentId) {
    // Send ajax post request to delete the comment using the commentId given.
    $.ajax({
        url: "/Comments/Delete",
        type: "POST",
        data: {
            __RequestVerificationToken: $('#addCommentForm input[name=__RequestVerificationToken]').val(), //anti-forgery token.
            id: commentId
        }
    }).done(function (result) {
        $("#comments").html(result);
    });
}

$(function () {

    // Show edit and delete icon on hover
    $(".comment-block").hover(function () {
        $(this).children(".modify-comment").show();
    }, function () {
        $(this).children(".modify-comment").hide();
    }
    );
});