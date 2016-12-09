using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coursework.Models;
using Microsoft.AspNet.Identity;

namespace Coursework.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.AnnouncementId = id;
            Announcement a = db.Announcements.Find(id);
            ViewBag.AnnouncementTitle = a.Title;
            return View(db.Comments.ToList().Where(x => x.Announcement.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetComments(int announcementId)
        {
            // Get list of comments associated with the given announcement id
            IEnumerable<Comment> listOfComments = db.Comments.ToList().Where(x => x.Announcement.Id == announcementId);

            return PartialView("_AnnouncementComments", listOfComments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int announcementId, [Bind(Include = "Content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.User = getUser();
                comment.Announcement = db.Announcements.Find(announcementId);
                db.Comments.Add(comment);
                db.SaveChanges();
                return GetComments(announcementId);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnnouncementId = comment.Announcement.Id;
            return View(comment);
        }

        // GET: Comments/Create/5
        public ActionResult Create(int? id)
        {
            ViewBag.AnnouncementId = id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int aId, [Bind(Include = "Id,Content")] Comment comment)
        {

            if (ModelState.IsValid)
            {
                var user = getUser();
                comment.User = user;
                comment.Announcement = db.Announcements.Find(aId);
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = aId });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnnouncementId = comment.Announcement.Id;
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int aId, [Bind(Include = "Id,Content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var user = getUser();
                comment.User = user;
                comment.Announcement = db.Announcements.Find(aId);
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = aId });
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnnouncementId = comment.Announcement.Id;
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int aId, int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = aId });
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
