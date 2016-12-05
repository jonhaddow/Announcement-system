using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Announcement
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Title { get; set; }

        public virtual string Content { get; set; }

    }
}