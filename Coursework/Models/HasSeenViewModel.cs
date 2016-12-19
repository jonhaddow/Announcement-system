using System.Collections.Generic;

namespace Coursework.Models
{
    // This model is passed down to the HasSeen view.
    public class HasSeenViewModel
    {
        public Announcement Announcement { get; set; } // The announcement

        public IEnumerable<ApplicationUser> Seen { get; set; } // The list of students that have seen the announcement.

        public IEnumerable<ApplicationUser> NotSeen { get; set; } // The list of students that haven't seen the announcement.
    }
}