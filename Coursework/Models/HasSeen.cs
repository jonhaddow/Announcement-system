namespace Coursework.Models
{
    public class HasSeen
    {
        public virtual int Id { get; set; } 

        public virtual ApplicationUser User { get; set; }

        public virtual Announcement Announcement { get; set; }
    }
}