using Fab.Data;
using Fab.Models.BlogsFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.Include(m => m.Translates).ToListAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchWord)
        {
                
            var blogs =await _context.Blogs
            .Where(m => m.Translates.Any(bt => bt.Desc.Contains(searchWord)))
            .Include(m => m.Translates)
            .ToListAsync();

            return View(blogs);
        }

    }
}
