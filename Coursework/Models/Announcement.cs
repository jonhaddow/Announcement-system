using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class Announcement
    {
        public  int Id { get; set; }

        [Required]
        public  string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public  string Content { get; set; }
        
        public  ApplicationUser User { get; set; }
    }
}