using System;

namespace Forum.Common.Threads
{
    public class ThreadsGridViewModel
    {
        public Guid ThreadId { get; set; }

        public string ThreadName { get; set; }

        public int TotalReplies { get; set; }

        public DateTime? LastPostDate { get; set; }

        public bool Closed { get; set; }

        public string LastPostedBy { get; set; }
    }
}
