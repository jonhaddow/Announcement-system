// These are the properties set for the loading spinner.
var $loading = $('#loadingSpinner').hide();
$(document)
  .ajaxStart(function () {
      $loading.show();
  })
  .ajaxStop(function () {
      $loading.hide();
  });

$(function () {

    // This method is called when any announcement from the side bar list is selected.
    $(".announcement-link").click(function () {

        // The selected announcement changes colour
        $(".announcement-link").removeClass("selected");
        $(this).addClass("selected");

        // The id of the announcement is used to get the announcement data
        var id = $(this).attr("id");
        $.ajax({
            url: "/Announcements/GetAnnouncement",
            type: "POST",
            data: {
                announcementId: id
            }
        }).done(function (result) {
            $("#displayAnnouncement").html(result);
        });

        // The id is also used to get the comments for the announcement.
        $.ajax({
            url: "/Comments/GetComments",
            type: "POST",
            data: {
                announcementId: id
            }
        }).done(function (result) {
            $("#displayComments").show().html(result);
        });
    });
});
