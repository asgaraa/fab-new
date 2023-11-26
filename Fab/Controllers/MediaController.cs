using Fab.Data;
using Fab.Models.BlogsFolder;
using Fab.ViewModels;
using Fab.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fab.Controllers
{
    public class MediaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MediaController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
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

            MediaPageVM media = new()
            {
                Blogs = await _context.Blogs.Take(6).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync(),
                News = await _context.News.Take(6).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync(),
                Presses = await _context.Presses.Take(6).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync(),
                LangCode = lang,
            };



            return View(media);
        }


        public async Task<IActionResult> BlogDetail(int id)
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

            BlogVM blogvm = new BlogVM()
            {
                Blogs = await _context.Blogs.Take(6).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync(),

                LangCode = lang,
                Blog= await _context.Blogs.Include(m=>m.Translates.Where(m=>m.LangCode== lang)).FirstOrDefaultAsync(m=>m.Id==id),
            };

            
           



            return View(blogvm);
        }
        //public async Task<List<SearchResultVM>> Search(string keyword)
        //{
        //    var lang = Request.Cookies["selectedLanguage"];
        //    if (string.IsNullOrEmpty(lang))
        //    {
        //        lang = "az";
        //    }
        //    else
        //    {
        //        lang = lang.ToLower();
        //    }

        //    // Blog model için arama
        //    var blogResults = _context.Blogs
        //        .Where(b => b.Translates.Any(t => t.Header.Contains(keyword) || t.Desc.Contains(keyword)))
        //        .Select(b => new SearchResultVM
        //        {
        //            Id = b.Id,
        //            Type = "Blog",
        //            Title = b.Translates.FirstOrDefault(t => t.LangCode == lang).Header,
        //            Description = b.Translates.FirstOrDefault(t => t.LangCode == lang).Desc
        //        })
        //        .ToList();

        //    // News model için arama
        //    var newsResults = _context.News
        //        .Where(n => n.Translates.Any(t => t.Header.Contains(keyword) || t.Desc.Contains(keyword)))
        //        .Select(n => new SearchResultVM
        //        {
        //            Id = n.Id,
        //            Type = "News",
        //            Title = n.Translates.FirstOrDefault(t => t.LangCode == lang).Header,
        //            Description = n.Translates.FirstOrDefault(t => t.LangCode == lang).Desc
        //        })
        //        .ToList();

        //    // Press model için arama
        //    var pressResults = _context.Presses
        //        .Where(p => p.Translates.Any(t => t.Header.Contains(keyword) || t.Desc.Contains(keyword)))
        //        .Select(p => new SearchResultVM
        //        {
        //            Id = p.Id,
        //            Type = "Press",
        //            Title = p.Translates.FirstOrDefault(t => t.LangCode == lang).Header,
        //            Description = p.Translates.FirstOrDefault(t => t.LangCode == lang).Desc
        //        })
        //        .ToList();

        //    // Tüm sonuçları birleştir
        //    var allResults = blogResults.Concat(newsResults).Concat(pressResults).ToList();

        //    return View(allResults);
        //}
    }
}
