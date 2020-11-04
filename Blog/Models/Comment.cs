using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }
        public int PostId { get; set; }
        public int AppUserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
