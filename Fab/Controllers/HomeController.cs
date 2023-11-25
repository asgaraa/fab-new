using Fab.Data;
using Fab.Models;
using Fab.Models.AdsFolder;
using Fab.Models.BannersFolder;
using Fab.Models.BlogsFolder;
using Fab.Models.CategoriesFolder;
using Fab.Models.CorporateFolder;
using Fab.Models.InteriorFolder;
using Fab.Models.NewsFolder;
using Fab.Models.PressFolder;
using Fab.Models.TwinFolder;
using Fab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Fab.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public HomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            var lang = Request.Cookies["selectedLanguage"];
            if (string.IsNullOrEmpty(lang))
            {
                lang = "az";
            }
            else
            {
                lang = lang.ToLower();
            }



            List<Banner> banners = await _context.Banners.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Category> categories = await _context.Categories.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<News> news = await _context.News.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Blog> blogs = await _context.Blogs.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Interior> interiors = await _context.Interiors.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Twin> twins = await _context.Twins.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Corporate> corporates = await _context.Corporates.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Press> presses = await _context.Presses.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Ads> ads = await _context.Ads.ToListAsync();

            HomeVM home = new()
            {
                Banners = banners,
                Categories = categories,
                News = news,
                Ads = ads,
                Presses = presses,
                Blogs = blogs,
                Interiors = interiors,
                Twins = twins,
                Corporates = corporates,
                LangCode = lang,
            };

            return View(home);
        }
             





        //public async Task<IActionResult> FilterBlogs(string langCode, string keyword)
        //{
        //    var filteredBlogs = _context.BlogTranslates
        //    .Where(t => t.LangCode == langCode && (t.Header.Contains(keyword) || t.Desc.Contains(keyword)))
        //    .Select(t => t.Blog)
        //    .Distinct()
        //    .ToList();

        //    return View("Index", filteredBlogs);
        //}














        #region privacy and error
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}