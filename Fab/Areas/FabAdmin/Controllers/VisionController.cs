using Fab.Areas.FabAdmin.ViewModels.AboutFolder;
using Fab.Areas.FabAdmin.ViewModels.VisionFolder;
using Fab.Data;
using Fab.Models.AboutFolder;
using Fab.Models.TwinFolder;
using Fab.Models.VisionFolder;
using FabAdmin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class VisionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public VisionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var vision = await _context.Visions.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(vision);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VisionVM vision)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<VisionTranslate> visionTranslates = new();
                foreach (var translate in vision.Translates)
                {

                    VisionTranslate visionTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    visionTranslates.Add(visionTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + vision.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/VisionImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, vision.ImageFile);
                Vision newVision = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/TwinImages/" + logoFileName,
                    Image = logoFileName,

                    Translates = visionTranslates,
                };

                await _context.Visions.AddAsync(newVision);
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

            var vision = await _context.Visions.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(vision);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, VisionVM vision)
        {
            var dbVision = await _context.Visions.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (dbVision == null)
            {
                return View();
            }
            if (vision.ImageFile != null)
            {

                string logoFileName = Guid.NewGuid().ToString() + "_" + vision.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/VisionImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, vision.ImageFile);

                //var pathbanner = dbVision.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/VisionImages/",
                  dbVision.Image);
                FileHelper.DeleteFile(filebanner);
                //dbVision.Image = "http://134.209.118.89/" + "ModelImages/TwinImages/" + logoFileName;
                dbVision.Image = logoFileName;


            }
            dbVision.Translates.Clear();
            foreach (var translate in vision.Translates)

            {
                VisionTranslate visionTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                dbVision.Translates.Add(visionTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Vision");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var dbVision = await _context.Visions
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbVision.Image != null && dbVision.Image != "")
            {
                //var pathbanner = dbVision.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/VisionImages/",
                 dbVision.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.VisionTranslates.RemoveRange(dbVision.Translates);
            _context.Visions.Remove(dbVision);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
