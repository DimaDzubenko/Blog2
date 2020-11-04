using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.Components
{
    public class PostViewComponent : ViewComponent
    {
        private readonly workblogmvcdbContext _context;
        public PostViewComponent(workblogmvcdbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int PostId)
        {
            var items = await _context.Posts.Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.PostId == PostId);
            return View(items);
        }
    }
}
