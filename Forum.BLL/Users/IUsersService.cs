using Forum.Common.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Users
{
    public interface IUsersService
    {
        Guid InsertNewUser(UserDTO user);

        UserDTO GetUserById(Guid userId);

        UserDTO GetUserByNickName(string nickname);

        UserDTO GetUserByLoginPass(string nickname, string password);

        bool IsUserExists(string nickname);
    }
}
