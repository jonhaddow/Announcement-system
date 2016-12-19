namespace Coursework.Models
{
    // This model makes a link between the user and announcement when a user (student) views an announcement.
    public class HasSeen
    {
        public virtual int Id { get; set; } 

        public virtual ApplicationUser User { get; set; }

        public virtual Announcement Announcement { get; set; }
    }
}