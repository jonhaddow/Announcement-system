function addAntiForgeryToken(data) {
    data.__RequestVerificationToken = $('#ajaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
    return data;
};

// These are the properties set for the loading spinner.
var $loading = $('#loadingSpinner').hide();
$(document)
  .ajaxStart(function () {
      $loading.show();
  })
  .ajaxStop(function () {
      $loading.hide();
  });

var id;

function getAnnouncement(button) {
    // The selected announcement changes colour
    $(".announcement-link").removeClass("selected");
    $(button).addClass("selected");

    // The id of the announcement is used to get the announcement data
    id = $(button).attr("id");
    $.ajax({
        url: "/Announcements/GetAnnouncement",
        type: "POST",
        data: addAntiForgeryToken({ announcementId: id }),
    }).done(function (result) {
        $("#displayAnnouncement").html(result);
    });

    // The id is also used to get the comments for the announcement.
    $.ajax({
        url: "/Comments/GetComments",
        type: "POST",
        data: addAntiForgeryToken({ announcementId: id }),
    }).done(function (result) {
        $("#displayComments").show();
        $("#comments").html(result);
    });
};

$(function () {
    // Add listener to check for form submission.
    $("#addCommentForm").submit(function (e) {
        $.ajax({
            url: "/Comments/AddComment",
            type: "POST",
            data: addAntiForgeryToken({
                announcementId: id,
                Content: $("#newCommentContent").val()
            }),
        }).done(function (result) {
            $("#comments").html(result);
            $("#newCommentContent").val("");
        });
        return false;
    });
});
