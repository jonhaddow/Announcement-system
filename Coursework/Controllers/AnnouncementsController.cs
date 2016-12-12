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
        [HttpPost]
        public ActionResult GetAnnouncement(int announcementId)
        {
            // Send info on the current user claims.
            ViewBag.isLecturer = User.IsInRole("canModifyAnnouncements");

            // Get selected announcement from id.
            Announcement announcement = db.Announcements.Find(announcementId);

            // Send back partial view showing selected announcement.
            return PartialView("_SelectedAnnouncement", announcement);
        }
        
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Create()
        {
            return PartialView("_CreateAnnouncement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canModifyAnnouncements")]
        public void Create([Bind(Include = "Title,Content,Important")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                // Attach current user to the announcement.
                announcement.User = getUser();

                // Save to database.
                db.Announcements.Add(announcement);
                db.SaveChanges();
            }
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
                return GetAnnouncement(announcement.Id);
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

            // Delete announcement.
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
        }
    }
}
