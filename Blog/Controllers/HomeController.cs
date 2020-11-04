using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.Models;
using Blog.Data.Repositories;
using Blog.Data.FileManager;
using Blog.Data;
using GoogleReCaptcha.V3.Interface;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly IFileManager _fileManager;
        private readonly workblogmvcdbContext _context;
        private readonly ICaptchaValidator _captchaValidator;

        private readonly UserManager<ApplicationUser> _userManager;

        public int PageSize = 9;

        public HomeController(  ILogger<HomeController> logger,
                                IRepository repository,
                                IFileManager fileManager,
                                workblogmvcdbContext context,
                                ICaptchaValidator captchaValidator,
                                UserManager<ApplicationUser> userManager
                              )
        {
            _logger = logger;
            _repository = repository;
            _fileManager = fileManager;
            _context = context;
            _captchaValidator = captchaValidator;
            _userManager = userManager;
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image) =>
            new FileStreamResult(
                _fileManager.ImageStream(image),
                $"image/{image.Substring(image.LastIndexOf('.') + 1)}"
                );

        public IActionResult Index(int page = 1)
        {
            var posts = _repository.GetAllPosts();
            return View(new PostListViewModel
            {
                Posts = posts
                    .OrderBy(c => c.PostId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageViewModel = new PageViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = posts.Count()
                }
            });
        }

        [HttpGet("Home/Post/{PostId}")]
        public async Task<IActionResult> Post(int? PostId)
        {
            //var user = await _userManager.GetUserAsync(User);
            //var id = _userManager.GetUserId(User);
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;
            ApplicationUser usr = await GetCurrentUserAsync();
            var comment = new Comment { PostId = PostId.Value };
            return View(comment);
        }

        [HttpPost("Home/Post/{PostId}")]
        public async Task<IActionResult> CreateComment(int PostId, Comment comment, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("captcha", "Captcha validation failed");
            }
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();

                ViewData["Message"] = "Success";

                return View("Post", new Comment { PostId = comment.PostId });
            }
            return RedirectToAction("Post", new { id = comment.PostId });
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
