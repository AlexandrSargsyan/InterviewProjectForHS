using Forum.BLL.Posts;
using Forum.Common.Posts;
using Forum.Web.Models.Posts;
using Forum.Web.Secuirty;
using System;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {

        #region locals
        private readonly IPostsService _postsService;
        #endregion locals


        #region ctor
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }
        #endregion ctor

        #region actions

        [Route("Posts/{threadId:guid}/{threadName}/{id:guid?}")]
        public ActionResult Edit(Guid threadId, string threadName, Guid? id)
        {
            var model = id == null ? new PostDTO { ThreadId = threadId } : _postsService.GetPostById(id.Value);

            var insertEditModel = new InsertEditPostModel
            {
                Id = model.Id,
                CreateDate = model.CreateDate,
                PostText = model.Text,
                ThreadId = model.ThreadId,
                ThreadName = threadName

            };

            return View(insertEditModel);
        }

        [HttpPost]
        public ActionResult Edit(InsertEditPostModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new PostDTO
                {
                    Id = model.Id,
                    CreateDate = model.CreateDate,
                    Text = model.PostText,
                    ThreadId = model.ThreadId,
                    UserId = ForumAuthenticationManager.Data.Id
                };
                _postsService.InsertOrEditPost(dto);
                return RedirectToAction("Index", "Threads", new { id = model.ThreadId, name = model.ThreadName });
            }

            return View(model);
        }

        public ActionResult Remove(Guid id, Guid threadId, string threadName)
        {
            _postsService.DeletePost(id);

            return RedirectToAction("Index", "Threads", new { id = threadId, name = threadName });
        } 
        #endregion actions
    }
}