using System.Collections.Generic;

namespace Coursework.Models
{
    public class HasSeenViewModel
    {
        public IEnumerable<ApplicationUser> Seen { get; set; }

        public IEnumerable<ApplicationUser> NotSeen { get; set; }
    }
}