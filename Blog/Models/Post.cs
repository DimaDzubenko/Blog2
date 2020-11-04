using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
