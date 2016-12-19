using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    // This model defines each comment a user makes under an announcement.
    public class Comment
    {
        public virtual int Id { get; set; }

        [Required (ErrorMessage = "A comment can't be empty.")]
        [StringLength(1000, ErrorMessage = "A comment can't be more than 1000 characters long.")]
        [DataType(DataType.MultilineText)]
        public virtual string Content { get; set; }

        public virtual Announcement Announcement { get; set; } // Link each comment to a single announcement

        public virtual ApplicationUser User { get; set; } // Link each comment to a User
    }
}