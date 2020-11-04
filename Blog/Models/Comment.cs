using System;

namespace Blog.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }
        public int PostId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
