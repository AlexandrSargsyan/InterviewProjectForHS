using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.DAL.Base
{
    public interface IRepositoryConfigs
    {
        string ConnectionString { get; }

        int PagingCount { get; }
    }
}
