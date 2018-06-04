using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Common.Topics
{
    public class TopicGridViewModel
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }

        public DateTime? LastPostDate { get; set; }

        public int TotalReplies { get; set; }

        public string LastPostName { get; set; }
    }
}
