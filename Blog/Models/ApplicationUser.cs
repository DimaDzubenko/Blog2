using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AppUserName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
