using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    public class Announcement
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "A Title is required")]
        [DisplayName("Title")]
        [StringLength(200, ErrorMessage = "Title should be less than 200 Characters.")]
        public virtual string Title { get; set; }

        [DisplayName("Message")]
        [StringLength(5000, ErrorMessage = "Message should be less than 5000 characters.")]
        [DataType(DataType.MultilineText)]
        public virtual string Content { get; set; }

        [Required]
        [DisplayName("Mark as Important")]
        public virtual bool Important { get; set; } // True if lecturer wants to flag announcement as important

        public virtual ApplicationUser User { get; set; }
    }
}