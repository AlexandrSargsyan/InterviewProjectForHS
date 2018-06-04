using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Posts
{
    public class InsertEditPostModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Post can't be empty")]
        [MaxLength(500)]
        public string PostText { get; set; }
        
        public DateTime? CreateDate { get; set; }
        
        public Guid ThreadId { get; set; }

        public string ThreadName { get; set; }
    }
}