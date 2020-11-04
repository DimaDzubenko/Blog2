using Blog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Data.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Get post in Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Post GetPost(int id);
        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        List<Post> GetAllPosts();
        /// <summary>
        /// Add posts
        /// </summary>
        /// <param name="post">New post</param>
        void AddPost(Post post);
        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="post">Post</param>
        void UpdatePost(Post post);
        void RemovePost(int id);

        /// <summary>
        /// Add SubComent
        /// </summary>
        /// <param name="comment">New Subcoment</param>
        void AddComment(Comment comment);


        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
    }
}
