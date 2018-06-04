using Forum.Common.Threads;
using Forum.DAL.Base;
using Forum.DAL.Threads;
using Forum.DAL.Topics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.RepositoryTests
{
    [TestClass]
    public class ThreadsRepositoryTests
    {
        #region locals

        private readonly IThreadsRepository _repository;
        private readonly ITopicsRepository _topicsRepository;
        private Guid _topicId;
        private Guid _threadId;

        #endregion locals

        #region ctor
        public ThreadsRepositoryTests()
        {
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ForumDb;Integrated Security=True;AttachDbFilename=|DataDirectory|\App_Data\ForumDB.mdf";
            var configs = new RepositoryConfigs(connectionString);
            _repository = new ThreadsRepository(configs);
            _topicsRepository = new TopicsRepository(configs);
        }

        #endregion ctor

        #region test methods
        [TestMethod]
        public void Test_Insert()
        {
            _topicId = InsertTopic();
            var thread = new ThreadDTO
            {
                Name = "@_test_thread",
                Description = "Using for test only",
                TopicId = _topicId
            };

          _threadId =  _repository.InsertThread(thread);
        }

        [TestMethod]
        public void Test_GetThreadsOfTopic()
        {
            _topicId = InsertTopic();
            var thread = new ThreadDTO
            {
                Name = "@_test_thread",
                Description = "Using for test only",
                TopicId = _topicId
            };

            _threadId = _repository.InsertThread(thread);
            var threads = _repository.GetThreadsOfTopicGrid(_topicId, 1);

            Assert.IsTrue(threads.TotalCount > 0);
        }

        #endregion test methods

        #region test cleanup
        [TestCleanup]
        public void Rollback()
        {
            _repository.Remove(_threadId);
            _topicsRepository.Remove(_topicId);
        }
        #endregion cleanup

        #region private

        private Guid InsertTopic()
        {
            return _topicsRepository.Add("@_test");
        }

        #endregion private
    }
}
