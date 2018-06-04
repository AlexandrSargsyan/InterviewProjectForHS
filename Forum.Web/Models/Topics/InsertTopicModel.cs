using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Topics
{
    public class InsertTopicModel
    {
        [Required(ErrorMessage ="Topic name can not be empty")]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}