using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Entity.MoreToMore
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Text { get; set; }

        public List<PostTag> postTags { get; set; }
    }
}
