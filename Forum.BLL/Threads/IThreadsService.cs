using Forum.Common.Base;
using Forum.Common.Threads;
using System;

namespace Forum.BLL.Threads
{
    public interface IThreadsService
    {
        PagingResult<ThreadsGridViewModel> GetThreadsGrid(Guid id, int page);

        Guid InsertThread(ThreadDTO thread);
        void ChangeState(Guid id, bool state);
    }
}
