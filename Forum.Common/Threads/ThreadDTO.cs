using System;

namespace Forum.Common.Threads
{
    public class ThreadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TopicId { get; set; }

        public bool Closed { get; set; }
    }
}
