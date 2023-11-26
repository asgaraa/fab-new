using Fab.Data;
using Fab.Models.BlogsFolder;
using Fab.Models.NewsFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BlogsFolder;
using FabAdmin.ViewModels.NewsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Blog";
            ViewBag.CurrentAction = "Index";
            var blogs = await _context.Blogs.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(blogs);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(BlogVM blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<BlogTranslate> blogTranslates = new();
                foreach (var translate in blog.Translates)
                {

                    BlogTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        Date = translate.Date,
                        LangCode = translate.LangCode
                    };
                    blogTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + blog.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BlogImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, blog.ImageFile);
                Blog newBlog = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/BlogImages/" + logoFileName,
                    Image =  logoFileName,

                    Translates = blogTranslates,
                };

                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {

            var blogs = await _context.Blogs.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(blogs);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, BlogVM blog)
        {
            var essn = await _context.Blogs.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (blog.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + blog.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BlogImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, blog.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BlogImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/BlogImages/" + logoFileName;
                essn.Image = logoFileName;


            }
            essn.Translates.Clear();
            foreach (var translate in blog.Translates)

            {
                BlogTranslate blogTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                    Date = translate.Date,
                };
                essn.Translates.Add(blogTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Blog");

        }



        public async Task<IActionResult> Delete(int id)
        {
            var dbBlog = await _context.Blogs
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbBlog.Image != null && dbBlog.Image != "")
            {
                //var pathbanner = dbBlog.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BlogImages/",
                 dbBlog.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.BlogTranslates.RemoveRange(dbBlog.Translates);
            _context.Blogs.Remove(dbBlog);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
