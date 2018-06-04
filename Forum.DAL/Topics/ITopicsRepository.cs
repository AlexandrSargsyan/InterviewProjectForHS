using Forum.Common.Base;
using Forum.Common.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Topics
{
    public interface ITopicsRepository
    {
        PagingResult<TopicGridViewModel> GetTopicsGrid(int page);

        Guid Add(string name);

        void Remove(Guid id);
    }

}
