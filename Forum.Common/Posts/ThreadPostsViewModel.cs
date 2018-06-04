using Forum.Common.Base;
using System;

namespace Forum.Common.Posts
{
    public class ThreadPostsViewModel
    {
        public ThreadPostsViewModel()
        {
            this.Posts = new PagingResult<PostViewModel>();
        }
        public string ThreadName { get; set; }
        public string ThreadText { get; set; }
        
        public Guid ThreadId { get; set; }

        public bool Closed { get; set; }

        public PagingResult<PostViewModel> Posts { get; set; }
    }

    
}
