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
    public class AnnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            ViewBag.isLecturer = User.IsInRole("canModifyAnnouncements");

            // Get selected announcement from id.
            Announcement announcement = db.Announcements.Find(announcementId);

            // Send back partial view showing selected announcement.
            return PartialView("_SelectedAnnouncement", announcement);
        }

        // GET: Announcements/Create
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = getUser();
                announcement.User = user;
                db.Announcements.Add(announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(announcement);
        }

        // GET: Announcements/Edit/5
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Edit(int? announcementId)
        {
            if (announcementId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(announcementId);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditAnnouncement", announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canModifyAnnouncements")]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = getUser();
                announcement.User = user;
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return GetAnnouncement(announcement.Id);
            }
            return View(announcement);
        }
        
        // POST: Announcements/Delete/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ApplicationUser getUser()
        {
            // Get current user Id
            string currentUserId = User.Identity.GetUserId();

            // Get user using user Id
            return db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
        }
    }
}
