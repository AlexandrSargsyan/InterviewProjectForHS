using Forum.Common.Posts;
using Forum.DAL.Posts;
using System;

namespace Forum.BLL.Posts
{
    public class PostsService : IPostsService
    {
        #region locals
        private readonly IPostsRepository _postsRepository;
        #endregion locals

        #region ctor
        public PostsService(IPostsRepository postsRepository)
        {
            this._postsRepository = postsRepository;
        }
        #endregion ctor

        #region methods
        public void DeletePost(Guid id)
        {
            this._postsRepository.Remove(id);
        }

        public PostDTO GetPostById(Guid id)
        {
            return _postsRepository.Get(id);
        }

        public ThreadPostsViewModel GetThreadPosts(Guid threadId, int page, bool latestFirst)
        {
            return _postsRepository.GetThreadPosts(threadId, page, latestFirst);
        }

        public Guid InsertOrEditPost(PostDTO post)
        {
            if (post.Id == null)
            {
                post.CreateDate = DateTime.UtcNow;
            }
            else
            {
                post.UpdateDate = DateTime.UtcNow;
            }

            return _postsRepository.InsertOrEditPost(post);
        } 
        #endregion methods
    }
}
