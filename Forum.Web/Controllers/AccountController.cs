using Forum.BLL.Users;
using Forum.Common.Users;
using Forum.Web.Models.Account;
using Forum.Web.Secuirty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class AccountController : Controller
    {
        #region locals
        private readonly IUsersService _usersService;
        #endregion locals

        #region ctor
        public AccountController(IUsersService usersService)
        {
            this._usersService = usersService;
        }
        #endregion ctor

        #region actions
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usersService.GetUserByLoginPass(model.Nickname, model.Password);
                if(user == null)
                {
                    ViewBag.error = "Invalid nickname or password";
                    return View();
                }
                else
                {
                    ForumAuthenticationManager.Login(user, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDTO
                {
                    Nickname = model.Nickname,
                    Password = model.Password,
                    City = model.City,
                    Country = model.Country,
                    Type = UserType.Default
                };
                user.Id  =_usersService.InsertNewUser(user);
                ForumAuthenticationManager.Login(user, false);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Route("Logout")]
        public ActionResult LogOut()
        {
            ForumAuthenticationManager.LogOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion actions
    }
}