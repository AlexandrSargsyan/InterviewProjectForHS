using Forum.Common.Base;
using Forum.Common.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Topics
{
    public interface ITopicsService
    {
        PagingResult<TopicGridViewModel> GetMainGrid(int page);

        Guid InsertNewTopic(string name);
    }
}
