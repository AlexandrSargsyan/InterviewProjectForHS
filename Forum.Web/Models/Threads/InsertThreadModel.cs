using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Threads
{
    public class InsertThreadModel
    { 
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(150,ErrorMessage = "Must be less than 150 characters")]
        public  string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Must be less than 500 characters")]
        public string Description { get; set; }

        [Required]
        public Guid TopicId { get; set; }
    }
}