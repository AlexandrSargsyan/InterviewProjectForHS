using Forum.BLL.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        #region locals
        private readonly ITopicsService _topicsService;
        #endregion locals

        #region ctor
        public HomeController(ITopicsService topicsService)
        {
            this._topicsService = topicsService;
        }

        #endregion ctor


        #region actions

        [Route("{page:int?}")]
        public ActionResult Index(int? page)
        {
            page = page ?? 1;
            var grid = _topicsService.GetMainGrid(page.Value);

            return View(grid);
        }

        [Route("Error/{message}")]
        public ActionResult Error(string message)
        {
            return View("~/Views/Shared/Error.cshtml",model:message);
        }

        #endregion actions

    }
}