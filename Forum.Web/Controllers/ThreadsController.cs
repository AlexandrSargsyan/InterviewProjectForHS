using Forum.BLL.Posts;
using Forum.BLL.Threads;
using Forum.Common.Threads;
using Forum.Web.Attributes;
using Forum.Web.Models.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class ThreadsController : Controller
    {
        #region locals

        private readonly IPostsService _postsService;
        private readonly IThreadsService _threadsService;

        #endregion


        #region ctor
        public ThreadsController(IPostsService postsService, IThreadsService threadsService)
        {
            this._postsService = postsService;
            this._threadsService = threadsService;
        }

        #endregion

        #region actions
        [Route("Threads/{id:guid}/{name}/{page?}/{latestFirst?}")]
        public ActionResult Index(Guid id, string name, int? page, bool? latestFirst)
        {
            page = page ?? 1;
            latestFirst = latestFirst ?? true;
            var result = _postsService.GetThreadPosts(id, page.Value, latestFirst.Value);
            result.ThreadName = name;
            result.ThreadId = id;
            ViewBag.latestFirst = latestFirst;
            return View(result);
        }

        [Authorize]
        public ActionResult Add(Guid topicId)
        {
            return View(new InsertThreadModel { TopicId = topicId });
        }

        [Authorize, HttpPost]
        public ActionResult Add(InsertThreadModel model)
        {
            if (ModelState.IsValid)
            {
                var thread = new ThreadDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    TopicId = model.TopicId
                };

                var id = _threadsService.InsertThread(thread);

                return RedirectToAction("Index", new { id, name = model.Name });
            }

            return View(model);
        }

        [AdminOnly, HttpGet]
        public ActionResult SetState(Guid id, string name, bool state)
        {
            _threadsService.ChangeState(id, state);
            return RedirectToAction("Index", new { id, name });
        } 

        #endregion actions

    }
}