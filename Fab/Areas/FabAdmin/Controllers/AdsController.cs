using Fab.Data;
using Fab.Models.AdsFolder;
using Fab.Models.NewsFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.AdsFolder;
using FabAdmin.ViewModels.NewsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class AdsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Ads";
            ViewBag.CurrentAction = "Index";
            var ads = await _context.Ads.Where(m => m.IsDeleted == false).ToListAsync();
            return View(ads);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(AdsVM ads)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }



                string logoFileName = Guid.NewGuid().ToString() + "_" + ads.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AdsImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, ads.ImageFile);
                Ads newAds = new()
                {
                    Link = ads.Link,
                    //Image = "http://134.209.118.89/" + "ModelImages/AdsImages/" + logoFileName
                    Image = logoFileName

                };

                await _context.Ads.AddAsync(newAds);
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

            var ads = await _context.Ads.FirstOrDefaultAsync(x => x.Id == Id);
            return View(ads);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, AdsVM ads)
        {
            var essn = await _context.Ads.FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (ads.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + ads.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AdsImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, ads.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AdsImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/AdsImages/" + logoFileName;
                essn.Image = logoFileName;


            }
            essn.Link = ads.Link;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ads");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbAds = await _context.Ads
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbAds.Image != null && dbAds.Image != "")
            {
                //var pathbanner = dbAds.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AdsImages/",
                 dbAds.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.Ads.Remove(dbAds);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
