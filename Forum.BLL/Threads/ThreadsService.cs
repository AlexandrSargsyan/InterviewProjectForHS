using Forum.Common.Base;
using Forum.Common.Threads;
using Forum.DAL.Threads;
using System;

namespace Forum.BLL.Threads
{
    public class ThreadsService : IThreadsService
    {
        #region locals
        private readonly IThreadsRepository _threadsRepository;
        #endregion locals

        #region ctor
        public ThreadsService(IThreadsRepository threadsRepository)
        {
            this._threadsRepository = threadsRepository;
        }
        #endregion ctor

        #region methods
        public void ChangeState(Guid id, bool state)
        {
            _threadsRepository.ChangeState(id, state);
        }

        public PagingResult<ThreadsGridViewModel> GetThreadsGrid(Guid id, int page)
        {
            return _threadsRepository.GetThreadsOfTopicGrid(id, page);
        }

        public Guid InsertThread(ThreadDTO thread)
        {
            return _threadsRepository.InsertThread(thread);
        } 
        #endregion methods
    }
}
