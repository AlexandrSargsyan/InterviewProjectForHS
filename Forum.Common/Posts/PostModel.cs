using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Common.Posts
{
    public class PostDTO
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid ThreadId { get; set; }

        public Guid UserId { get; set; }
    }
}
