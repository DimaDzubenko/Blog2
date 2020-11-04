using Microsoft.AspNetCore.Http;
using System;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public string Body { get; set; } = "";
        public string CurrentImage { get; set; } = "";

        public IFormFile Image { get; set; } = null;
    }
}
