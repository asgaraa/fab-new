using Fab.Areas.FabAdmin.ViewModels.AboutFolder;
using Fab.Data;
using Fab.Models.AboutFolder;
using Fab.Models.BannersFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BannersFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var about = await _context.Abouts.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(about);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutVM about)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<AboutTranslate> aboutTranslates = new();
                foreach (var translate in about.Translates)
                {

                    AboutTranslate aboutTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    aboutTranslates.Add(aboutTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + about.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AboutImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, about.ImageFile);
                About newAbout = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/AboutImages/" + logoFileName,
                    Image =  logoFileName,

                    Translates = aboutTranslates,
                };

                await _context.Abouts.AddAsync(newAbout);
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

            var about = await _context.Abouts.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, AboutVM about)
        {
            var dbAbout = await _context.Abouts.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (dbAbout == null)
            {
                return View();
            }
            if (about.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + about.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AboutImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, about.ImageFile);
                //var pathbanner = dbAbout.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AboutImages/", dbAbout.Image);
                FileHelper.DeleteFile(filebanner);
                //dbAbout.Image = "http://134.209.118.89/" + "ModelImages/AboutImages/" + logoFileName;
                dbAbout.Image = logoFileName;


            }
            dbAbout.Translates.Clear();
            foreach (var translate in about.Translates)

            {
                AboutTranslate aboutTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                dbAbout.Translates.Add(aboutTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "About");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbAbout = await _context.Abouts
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbAbout.Image != null && dbAbout.Image != "")
            {

                //var pathbanner = dbAbout.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AboutImages/", dbAbout.Image);
                FileHelper.DeleteFile(filebanner);
                
            }
            _context.AboutTranslates.RemoveRange(dbAbout.Translates);
            _context.Abouts.Remove(dbAbout);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
