using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Coursework.Models;
using Microsoft.AspNet.Identity;

namespace Coursework.Controllers
{
    public class AnnouncementsController : BaseController
    {
        public ActionResult Index()
        {
            // Send info on the current user claims.
            ViewBag.isLecturer = User.IsInRole("canModifyAnnouncements");

            // Send list of all the announcements
            return View(db.Announcements.ToList());
        }

        // Method that gets an announcements details.
        public ActionResult GetAnnouncement(int announcementId)
        {
            // Send info on the current user claims.
            bool isLecturer = User.IsInRole("canModifyAnnouncements");

            // Get selected announcement from id.
            Announcement announcement = db.Announcements.Find(announcementId);

            // If the user is a student
            if (!isLecturer)
            {
                // Check if User has seen the announcement
                IEnumerable<HasSeen> existingRows = db.HasSeens.ToList()
                    .Where(row => row.Announcement.Id == announcement.Id
                    && row.User.Id == User.Identity.GetUserId());

                // If no row exists
                if (existingRows.Count() == 0)
                {
                    // Add row to database
                    HasSeen prepareObj =  new HasSeen();
                    prepareObj.Announcement = announcement;
                    prepareObj.User = getUser();
                    db.HasSeens.Add(prepareObj);
                    db.SaveChanges();
                }
            }



            ViewBag.isLecturer = isLecturer;

            // Send back partial view showing selected announcement.
            return PartialView("_SelectedAnnouncement", announcement);
        }

        public string GetStudentsSeen(int announcement)
        {
            return "";
        }

        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Create()
        {
            return PartialView("_CreateAnnouncement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Create([Bind(Include = "Title,Content,Important")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                // Attach current user to the announcement.
                announcement.User = getUser();

                // Save to database.
                db.Announcements.Add(announcement);
                db.SaveChanges();

                return RedirectToAction("Index", "Announcements");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Edit(int? announcementId)
        {
            // Check that id is sent in URL
            if (announcementId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Check that id gives a valid announcement.
            Announcement announcement = db.Announcements.Find(announcementId);
            if (announcement == null)
            {
                return HttpNotFound();
            }

            return PartialView("_EditAnnouncement", announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Important")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                // Attach user to announcement.
                announcement.User = getUser();

                // Save new announcement to database
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();

                // Return back to announcement page.
               return  RedirectToAction("Index","Announcements");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canModifyAnnouncements")]
        public void Delete(int id)
        {
            // Check if announcement has any comments. If so, delete them.
            IEnumerable<Comment> comments = db.Comments.ToList().Where(c => c.Announcement.Id == id);
            db.Comments.RemoveRange(comments);

            // Check if announcement has any student views. If so, delete them.
            IEnumerable<HasSeen> hasSeens = db.HasSeens.ToList().Where(c => c.Announcement.Id == id);
            db.HasSeens.RemoveRange(hasSeens);

            // Delete announcement.
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
        }
    }
}
