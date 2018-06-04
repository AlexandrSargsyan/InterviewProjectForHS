using Forum.Common.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Posts
{
    public interface IPostsRepository
    {
        ThreadPostsViewModel GetThreadPosts(Guid threadId, int page, bool latestFirst);

        PostDTO Get(Guid id);

        Guid InsertOrEditPost(PostDTO post);
        void Remove(Guid id);
    }
}
