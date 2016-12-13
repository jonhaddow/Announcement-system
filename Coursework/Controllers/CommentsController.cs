using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Coursework.Models;

namespace Coursework.Controllers
{
    public class CommentsController : BaseController
    {
        // This method gets a lists of comments attached to an announcement id.
        public ActionResult GetComments(int announcementId)
        {
            // Get list of comments associated with the given announcement id
            IEnumerable<Comment> listOfComments =
                db.Comments.ToList().Where(x => x.Announcement.Id == announcementId);

            return PartialView("_AnnouncementComments", listOfComments);
        }

        public ActionResult CreateView(int announcementId)
        {
            ViewBag.announcementId = announcementId;
            return PartialView("_CreateComment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int announcementId, [Bind(Include = "Content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                // Attach current user and selected announcement to the comment.
                comment.User = getUser();
                comment.Announcement = db.Announcements.Find(announcementId);

                // Add to database.
                db.Comments.Add(comment);
                db.SaveChanges();

                // Return partial view of a list of comments.
                return GetComments(announcementId);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditView(int? commentId)
        {
            // Check that id is sent in URL
            if (commentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Check that id gives a valid announcement.
            Comment comment = db.Comments.Find(commentId);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditComment", comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int announcementId, [Bind(Include = "Id,Content")] Comment inputComment)
        {
            if (ModelState.IsValid)
            {
                // Get current comment from database using the comment Id.
                Comment comment = db.Comments.Find(inputComment.Id);

                // Check that comment belongs to the user.
                ApplicationUser currentUser = getUser();
                if (comment.User.Id == currentUser.Id)
                {
                    // Change only the content of the comment.
                    comment.Content = inputComment.Content;
                    db.SaveChanges();

                    // Return partial view of the list of comments.
                    return GetComments(announcementId);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // Get the comment to be deleted.
            Comment comment = db.Comments.Find(id);

            // Take the announcement Id attached to the comment.
            var announcementId = comment.Announcement.Id;

            // Delete the comment.
            db.Comments.Remove(comment);
            db.SaveChanges();

            // Return a partial view of all remaining the comments attached to this announcement.
            return GetComments(announcementId);
        }
    }
}
