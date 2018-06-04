using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Common.Posts
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string PostName { get; set; }

        public string PostText { get; set; }

        public string UserNickname { get; set; }

        public DateTime UserRegistrationDate { get; set; }

        public string UserCity { get; set; }

        public string UserCountry { get; set; }

        public int UserTotalPosts { get; set; }
          
    }

    
}
