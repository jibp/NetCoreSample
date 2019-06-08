using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Entity.MoreToMore
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }

        public List<PostTag> postTags { get; set; }
    }
}
