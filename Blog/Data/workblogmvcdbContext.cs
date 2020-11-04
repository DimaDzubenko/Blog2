using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public partial class workblogmvcdbContext : IdentityDbContext<ApplicationUser>
    {        
        public workblogmvcdbContext(DbContextOptions<workblogmvcdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        
    }
}
