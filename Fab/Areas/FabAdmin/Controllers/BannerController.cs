using Fab.Data;
using Fab.Models.BannersFolder;
using Fab.Models.BlogsFolder;
using Fab.Models.CategoriesFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BannersFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BannerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Banner";
            ViewBag.CurrentAction = "Index";
            var banner = await _context.Banners.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(banner);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(BannerVM banner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<BannerTranslate> bannerTranslates = new();
                foreach (var translate in banner.Translates)
                {

                    BannerTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    bannerTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + banner.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BannerImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, banner.ImageFile);
                Banner newBanner = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/BannerImages/" + logoFileName,
                    Image =  logoFileName,

                    Translates = bannerTranslates,
                };

                await _context.Banners.AddAsync(newBanner);
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
           
            var banner = await _context.Banners.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(banner);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id,BannerVM banner)
        {
            var essn = await _context.Banners.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (banner.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + banner.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BannerImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, banner.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BannerImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/BannerImages/" + logoFileName;
                essn.Image = logoFileName;


            }
            essn.Translates.Clear();
            foreach (var translate in banner.Translates)

            {
                BannerTranslate bannerTranslate = new()
                {
                    
                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                essn.Translates.Add(bannerTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }




        public async Task<IActionResult> Delete(int id)
        {
            var dbBanner = await _context.Banners
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbBanner.Image != null && dbBanner.Image != "")
            {
                //var pathbanner = dbBanner.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/BannerImages/",
                 dbBanner.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.BannerTranslates.RemoveRange(dbBanner.Translates);
            _context.Banners.Remove(dbBanner);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        
    }
}
