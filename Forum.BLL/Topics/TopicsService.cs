using System;
using Forum.Common.Base;
using Forum.Common.Topics;
using Forum.DAL.Topics;

namespace Forum.BLL.Topics
{
    public class TopicsService : ITopicsService
    {
        #region locals
        private ITopicsRepository _topicsRepository;

        #endregion locals

        #region ctor
        public TopicsService(ITopicsRepository topicsRepository)
        {
            this._topicsRepository = topicsRepository;
        }
        #endregion ctor

        #region methods
        public PagingResult<TopicGridViewModel> GetMainGrid(int page)
        {
            return _topicsRepository.GetTopicsGrid(page);
        }

        public Guid InsertNewTopic(string name)
        {
            return _topicsRepository.Add(name);
        }

        #endregion methods
    }
}
