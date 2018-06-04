using Forum.Common.Base;
using Forum.Common.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Threads
{
    public interface IThreadsRepository
    {
       PagingResult<ThreadsGridViewModel> GetThreadsOfTopicGrid(Guid topicId, int page);

        Guid InsertThread(ThreadDTO thread);
        void ChangeState(Guid id, bool state);

        void Remove(Guid id);
    }
}
