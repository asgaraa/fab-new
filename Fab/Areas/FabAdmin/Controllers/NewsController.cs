using Fab.Data;
using Fab.Models.BannersFolder;
using Fab.Models.NewsFolder;
using Fab.Models.PressFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BannersFolder;
using FabAdmin.ViewModels.NewsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public NewsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "News";
            ViewBag.CurrentAction = "Index";
            var news = await _context.News.Where(m => m.IsDeleted == false)
                .Include(m => m.Translates)
                .ToListAsync();
            return View(news);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(NewsVM news)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<NewsTranslate> newsTranslates = new();
                foreach (var translate in news.Translates)
                {

                    NewsTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        Date = translate.Date,
                        LangCode = translate.LangCode
                    };
                    newsTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + news.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/NewsImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, news.ImageFile);
                News newNews = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/NewsImages/" + logoFileName,
                    Image =  logoFileName,

                    Translates = newsTranslates,
                };

                await _context.News.AddAsync(newNews);
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

            var news = await _context.News.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(news);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id, NewsVM news)
        {
            var essn = await _context.News.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (news.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + news.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/NewsImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, news.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/NewsImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/NewsImages/" + logoFileName;
                essn.Image =  logoFileName;


            }
            essn.Translates.Clear();
            foreach (var translate in news.Translates)

            {
                NewsTranslate newsTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                    Date = translate.Date
                };
                essn.Translates.Add(newsTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "News");

        }
         
        public async Task<IActionResult> Delete(int id)
        {
            var dbNews = await _context.News
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if(dbNews.Image != null && dbNews.Image != "")
            {
                //var pathbanner = dbNews.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/NewsImages/",
                 dbNews.Image);
                FileHelper.DeleteFile(filebanner);
            }

            _context.NewsTranslates.RemoveRange(dbNews.Translates);
            _context.News.Remove(dbNews);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
