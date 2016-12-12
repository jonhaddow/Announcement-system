$(function () {

    $('<input />').attr('type', 'hidden')
          .attr('name', "announcementId")
          .attr('value', id)
          .appendTo('#addCommentForm');

    //// Add listener to check for form submission.
    //$("#addCommentForm").submit(function (e) {
    //    alert(id);
    //    var obj = $.extend((this).serialize(), { announcementid: id });
    //    alert(obj.announcementid + obj.Content);
    //    //$.ajax({
    ////        url: "/Comments/Create",
    ////        type: "POST",
    ////        data: obj
    ////    }).done(function (result) {
    ////        $("#displayComments").html(result);
    ////        $("#newCommentContent").val("");
    ////    });

    ////    return false;
    //});
});