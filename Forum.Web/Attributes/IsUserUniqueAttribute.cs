using Forum.BLL.Users;
using Forum.Web.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Forum.Web.Attributes
{
    public class IsUserUniqueAttribute : ValidationAttribute
    {
        private readonly IUsersService _usersService;

        public IsUserUniqueAttribute()
        {
            _usersService = UnityServiceLocator.Resolve<IUsersService>();
        }
        public override bool IsValid(object value)
        {
            var nick = (string)value;

            return _usersService.GetUserByNickName(nick) == null;
        }
    }
}