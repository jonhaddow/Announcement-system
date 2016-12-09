using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Comment
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Content { get; set; }

        public virtual Announcement Announcement { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}