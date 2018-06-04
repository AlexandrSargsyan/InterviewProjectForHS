using Forum.Common.Security;
using Forum.Common.Users;
using Forum.DAL.Base;
using Forum.DAL.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Forum.Tests.RepositoryTests.UsersRepository
{
    [TestClass]
    public class UserRepositoryTests
    {
        #region locals
        private readonly IUsersRepository _repository;
        #endregion locasl

        #region ctor
        public UserRepositoryTests()
        {

            var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=ForumDb;Integrated Security=True;AttachDbFilename=|DataDirectory|\App_Data\ForumDB.mdf";
            _repository = new DAL.Users.UsersRepository(new RepositoryConfigs(connectionString));
        }
        #endregion ctor

        #region test methods

        [TestMethod]
        public void Insert_User()
        {
            var testUserId = Insert(GetTestUser());
            Assert.IsNotNull(Get(testUserId));
        }

        [TestMethod]
        public void Get_All()
        {
            var users = _repository.GetAll();

            Assert.IsNotNull(users);
        }


        [TestMethod]
        public void Get_User_By_Id()
        {
            var testUser = GetTestUser();
            testUser.Id = Insert(testUser);

            var userFromDb = _repository.Get(testUser.Id);

            Assert.IsNotNull(userFromDb);
        }

        [TestMethod]
        public void Get_User_By_NickName()
        {
            var testUser = GetTestUser();
            Insert(testUser);
            var userFromDb = _repository.Get(testUser.Nickname);
            Assert.IsNotNull(userFromDb);
        }

        [TestMethod]
        public void Get_User_By_Login()
        {
            var testUser = GetTestUser();
            Insert(testUser);

            var userFromDb = _repository.Get(testUser.Nickname, testUser.Password);

            Assert.IsNotNull(userFromDb);
        }

        [TestMethod]
        public void Must_Be_Unique_User_Id()
        {
            var testUser = GetTestUser();
            Insert(testUser);
            try
            {
                Insert(testUser);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();

        }

        #endregion test methods

        #region test cleanup
        [TestCleanup]
        public void Rollback()
        {
            var user = _repository.Get("_test");

            if (user != null)
            {
                _repository.Remove(user.Id);
            }

        }

        #endregion test cleanup

        #region private

        private UserDTO Get(Guid id)
        {
            return _repository.Get(id);
        }

        private UserDTO GetTestUser()
        {
            var userDTO = new UserDTO
            {
                Nickname = "_test",
                Password = HashingUtility.GetSHA256Hash("Pass123456."),
                City = "Yerevan",
                Country = "Armenia",
                Type = Common.Users.UserType.Default,
                RegistrationDate = DateTime.UtcNow

            };

            return userDTO;
        }

        private Guid Insert(UserDTO user)
        {
            return _repository.Insert(user);
        }

        #endregion private
    }
}
