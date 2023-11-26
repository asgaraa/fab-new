using Fab.Data;
using Fab.Models.InteriorFolder;
using Fab.Models.ProductFolder;
using Fab.Models.TwinFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.InteriorFolder;
using FabAdmin.ViewModels.TwinFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class TwinController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TwinController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Twin";
            ViewBag.CurrentAction = "Index";
            var twin = await _context.Twins.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(twin);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TwinVM twin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<TwinTranslate> twinTranslates = new();
                foreach (var translate in twin.Translates)
                {
                    TwinTranslate entertainmentTranslate = new()
                    {
                        WideDesc = translate.WideDesc,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    twinTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + twin.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, twin.ImageFile);

                string logoFileName2 = Guid.NewGuid().ToString() + "_" + twin.WideImageFile.FileName;
                string logoPath2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/", logoFileName2);
                await FileHelper.SaveFileAsync(logoPath2, twin.WideImageFile);

                Twin newTwin = new()
                {
                    //WideImage = "http://134.209.118.89/" + "ModelImages/TwinImages/" + logoFileName2,
                    //Image = "http://134.209.118.89/" + "ModelImages/TwinImages/" + logoFileName,
                    WideImage = logoFileName2,
                    Image =logoFileName,
                    Translates = twinTranslates,
                };

                await _context.Twins.AddAsync(newTwin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var twin = await _context.Twins.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(twin);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id, TwinVM twin)
        {
            var essn = await _context.Twins.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (twin.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + twin.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, twin.ImageFile);
                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/TwinImages/" + logoFileName;
                essn.Image =  logoFileName;


            }

            if (twin.WideImageFile != null)
            {
                string logoFileName2 = Guid.NewGuid().ToString() + "_" + twin.WideImageFile.FileName;
                string logoPath2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/", logoFileName2);
                await FileHelper.SaveFileAsync(logoPath2, twin.WideImageFile);
                //var pathbanner = essn.WideImage.Replace("http://134.209.118.89/", "");

                var filebanner2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/",
                  essn.WideImage);
                FileHelper.DeleteFile(filebanner2);

                //essn.WideImage = "http://134.209.118.89/" + "ModelImages/TwinImages/" + logoFileName2;
                essn.WideImage = logoFileName2;

            }

            essn.Translates.Clear();
            foreach (var translate in twin.Translates)

            {
                TwinTranslate twinTranslate = new()
                {

                    LangCode = translate.LangCode,
                    WideDesc = translate.WideDesc,
                    Desc = translate.Desc,
                };
                essn.Translates.Add(twinTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Twin");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbTwin = await _context.Twins
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbTwin.Image != null && dbTwin.Image != "")
            {
                //var pathbanner = dbTwin.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/",
                 dbTwin.Image);
                FileHelper.DeleteFile(filebanner);
            }

            if (dbTwin.WideImage != null && dbTwin.WideImage != "")
            {
                //var pathbanner = dbTwin.WideImage.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/TwinImages/",
                 dbTwin.WideImage);
                FileHelper.DeleteFile(filebanner);
            }

            _context.TwinTranslates.RemoveRange(dbTwin.Translates);
            _context.Twins.Remove(dbTwin);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
