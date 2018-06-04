using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Common.Users;

namespace Forum.DAL.Users
{
    public interface IUsersRepository
    {
        Guid Insert(UserDTO user);

        UserDTO Get(Guid userId);

        UserDTO Get(string nickname);

        UserDTO Get(string nickname, string password);

        IEnumerable<UserDTO> GetAll();

        bool Remove(Guid userId);

    }
}
