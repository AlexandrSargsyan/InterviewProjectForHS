using Forum.Common.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Forum.Web.Secuirty
{
    public static class ForumAuthenticationManager
    {
        public static AuthenticationModel Data
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated) return null;

                var b64 = HttpContext.Current.User.Identity.Name;
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(b64));

                var authModel = JsonConvert.DeserializeObject<AuthenticationModel>(json);

                return authModel;
            }
        }
        public static void Login(UserDTO user, bool isPersistent)
        {
            var userAuthModel = new AuthenticationModel
            {
                Id = user.Id,
                Nickname = user.Nickname,
                UserType = user.Type
            };
            var authString = JsonConvert.SerializeObject(userAuthModel);
            var b64json = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));

            FormsAuthentication.SetAuthCookie(b64json, isPersistent);
        }


        public static void LogOut()
        {
            FormsAuthentication.SignOut();
        }
    }

}