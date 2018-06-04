using Forum.Common.Security;
using Forum.DAL.Base;
using Forum.DAL.Posts;
using Forum.DAL.Threads;
using Forum.DAL.Topics;
using Forum.DAL.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Forum.Tests.RepositoryTests.UsersRepository
{
    [TestClass]
    public class PostsRepositoryTests
    {

        #region locals
        private readonly IPostsRepository _repository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IThreadsRepository _threadsRepository;
        private readonly IUsersRepository _usersRepository;
        private Guid _topicId;
        private Guid _threadId;
        private Guid _postId;
        private Guid _userId;

        #endregion locals

        #region ctor
        public PostsRepositoryTests()
        {
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ForumDb;Integrated Security=True;AttachDbFilename=|DataDirectory|\App_Data\ForumDB.mdf";
            var configs = new RepositoryConfigs(connectionString);
            _repository = new PostsRepository(configs);
            _topicsRepository = new TopicsRepository(configs);
            _threadsRepository = new ThreadsRepository(configs);
            _usersRepository = new DAL.Users.UsersRepository(configs);

        }
        #endregion ctor

        #region test methods
        [TestMethod]
        public void Test_Insert()
        {
            _topicId = InsertTopic();
            _threadId = InsertThread();
            _userId = InsertUser();
            _postId = _repository.InsertOrEditPost(new Common.Posts.PostDTO
            {
                Text = "@_test_post",
                CreateDate = DateTime.UtcNow,
                ThreadId = _threadId,
                UserId = _userId
            });

            Assert.AreEqual(_repository.Get(_postId).Text, "@_test_post");

        }

        [TestMethod]
        public void Test_Edit()
        {
            _topicId = InsertTopic();
            _threadId = InsertThread();
            _userId = InsertUser();
            _postId = _repository.InsertOrEditPost(new Common.Posts.PostDTO
            {
                Text = "@_test_post",
                CreateDate = DateTime.UtcNow,
                ThreadId = _threadId,
                UserId = _userId
            });

            var post = _repository.Get(_postId);
            post.Text = "@_test_post_edit";
            _repository.InsertOrEditPost(post);

            Assert.AreEqual(_repository.Get(_postId).Text, "@_test_post_edit");
        }

        [TestMethod]
        public void Test_Get_Threads_Of_Posts()
        {
            _topicId = InsertTopic();
            _threadId = InsertThread();
            _userId = InsertUser();
            _postId = _repository.InsertOrEditPost(new Common.Posts.PostDTO
            {
                Text = "@_test_post",
                CreateDate = DateTime.UtcNow,
                ThreadId = _threadId,
                UserId = _userId
            });

            var posts = _repository.GetThreadPosts(_threadId, 1, true);

            Assert.AreEqual(posts.Posts.TotalCount, 1);
        }

        #endregion test methods

        #region test cleanup
        [TestCleanup]
        public void Rollback()
        {
            _repository.Remove(_postId);
            _threadsRepository.Remove(_threadId);
            _topicsRepository.Remove(_topicId);
            _usersRepository.Remove(_userId);
        }
        #endregion test cleanup


        #region private
        private Guid InsertTopic()
        {
            return _topicsRepository.Add("@_test_topic");
        }

        private Guid InsertThread()
        {
            return _threadsRepository.InsertThread(new Common.Threads.ThreadDTO
            { Name = "@_test_thread", Description = "test", TopicId = _topicId });

        }

        private Guid InsertUser()
        {
            return _usersRepository.Insert(new Common.Users.UserDTO
            {
                Nickname = "_test",
                City = "TestCity",
                Country = "Test Country",
                Password = HashingUtility.GetSHA256Hash("Pass123456."),
                RegistrationDate = DateTime.Now
            }); 
        }

        #endregion private
    }
}
