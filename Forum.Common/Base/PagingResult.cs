using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Common.Base
{
    public class PagingResult<T> : IPager
    {
        public PagingResult()
        {
            this.Items = new List<T>();
        }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }

        public int ShowPerPage { get; set; }
        public int Count { get => this.Items.Count; }
    }
}
