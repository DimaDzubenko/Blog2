using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly workblogmvcdbContext _context;

        public Repository(workblogmvcdbContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts
                .ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.PostId == id);
        }

        public void RemovePost(int id)
        {
            _context.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }
    }
}
