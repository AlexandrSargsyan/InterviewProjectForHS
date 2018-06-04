using Forum.DAL.Base;
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
    public class TopicsRepositoryTests
    {
        private readonly ITopicsRepository _repository;
        private Guid topicId;

        public TopicsRepositoryTests()
        {
            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ForumDb;Integrated Security=True;AttachDbFilename=|DataDirectory|\App_Data\ForumDB.mdf";
            _repository = new TopicsRepository(new RepositoryConfigs(connectionString));
        }


        [TestMethod]
        public void Test_Insert()
        {
            topicId = _repository.Add("@_test");

            var topics = _repository.GetTopicsGrid(1);
            Assert.IsTrue(topics.TotalCount > 0);
        }

        [TestCleanup]
        public void Rollback()
        {
            _repository.Remove(topicId);
        }

    }
}
