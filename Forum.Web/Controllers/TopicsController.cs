using Forum.BLL.Threads;
using Forum.BLL.Topics;
using Forum.Web.Attributes;
using Forum.Web.Models.Topics;
using System;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    [RoutePrefix("Topics")]
    public class TopicsController : Controller
    {

        #region locals
        private readonly IThreadsService _threadsService;
        private readonly ITopicsService _topicsService;
        #endregion locals

        #region ctor
        public TopicsController(IThreadsService threadsService, ITopicsService topicsService)
        {
            this._threadsService = threadsService;
            this._topicsService = topicsService;
        }
        #endregion

        #region actions
        [Route("{id:guid}/{name}/{page?}")]
        public ActionResult Index(Guid id, string name, int? page)
        {
            ViewBag.name = name;
            ViewBag.id = id;
            page = page ?? 1;
            var result = _threadsService.GetThreadsGrid(id, page.Value);
            return View(result);
        }


        [AdminOnly]
        public ActionResult Add()
        {
            return View();
        }

        [AdminOnly, HttpPost]
        public ActionResult Add(InsertTopicModel model)
        {
            if (ModelState.IsValid)
            {
                var id = _topicsService.InsertNewTopic(model.Name);

                return RedirectToAction("Index", new { id, name = model.Name });
            }
            return View(model);
        } 
        #endregion actions
    }
}