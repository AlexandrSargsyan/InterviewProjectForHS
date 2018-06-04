using Forum.Common.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Posts
{
    public interface IPostsService
    {
        ThreadPostsViewModel GetThreadPosts(Guid threadId, int page, bool latestFirst);

        Guid InsertOrEditPost(PostDTO post);

        PostDTO GetPostById(Guid id);

        void DeletePost(Guid id);

    }
}
