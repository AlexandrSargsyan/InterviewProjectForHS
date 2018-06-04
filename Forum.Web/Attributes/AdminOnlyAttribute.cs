using Forum.Web.Secuirty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Attributes
{
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ForumAuthenticationManager.Data?.UserType == Common.Users.UserType.Admin;
        }
    }
}