using System;
using Forum.Common.Security;
using Forum.Common.Users;
using Forum.DAL.Users;

namespace Forum.BLL.Users
{
    public class UsersService : IUsersService
    {
        #region locals
        private readonly IUsersRepository _usersRepository;
        #endregion locals

        #region ctor
        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }
        #endregion ctor

        #region methods
        public UserDTO GetUserById(Guid userId)
        {
            return _usersRepository.Get(userId);
        }

        public UserDTO GetUserByLoginPass(string nickname, string password)
        {
            password = HashingUtility.GetSHA256Hash(password);

            return _usersRepository.Get(nickname, password);
        }

        public UserDTO GetUserByNickName(string nickname)
        {
            return _usersRepository.Get(nickname);
        }

        public Guid InsertNewUser(UserDTO user)
        {
            user.Password = HashingUtility.GetSHA256Hash(user.Password);
            user.RegistrationDate = DateTime.UtcNow;

            return _usersRepository.Insert(user);
        }

        public bool IsUserExists(string nickname)
        {
            return GetUserByNickName(nickname) != null;
        }

        #endregion methods
    }
}
