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

        [Required (ErrorMessage = "A comment can't be empty.")]
        [StringLength(1000, ErrorMessage = "A comment can't be more than 1000 characters long.")]
        [DataType(DataType.MultilineText)]
        public virtual string Content { get; set; }

        public virtual Announcement Announcement { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}