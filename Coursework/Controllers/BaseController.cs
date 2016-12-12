using Coursework.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Coursework.Controllers
{
    // This class is the base class for all the controllers I use. It holds any common fields and methods.
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        // This method gets the current User making the request.
        protected ApplicationUser getUser()
        {
            // Get current userId.
            string currentUserId = User.Identity.GetUserId();

            // Get user.
            return db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
        }
    }
}