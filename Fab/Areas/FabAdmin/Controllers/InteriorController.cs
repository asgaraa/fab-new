using Fab.Data;
using Fab.Models.InteriorFolder;
using Fab.Models.NewsFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.InteriorFolder;
using FabAdmin.ViewModels.NewsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class InteriorController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public InteriorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Interior";
            ViewBag.CurrentAction = "Index";
            var interior = await _context.Interiors.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(interior);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(InteriorVM interior)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<InteriorTranslate> interiorTranslates = new();
                foreach (var translate in interior.Translates)
                {

                    InteriorTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        CoverText = translate.CoverText,
                        LangCode = translate.LangCode
                    };
                    interiorTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + interior.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/InteriorImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, interior.ImageFile);
                Interior newInterior = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/InteriorImages/" + logoFileName,
                    Image =  logoFileName,

                    Translates = interiorTranslates,
                };

                await _context.Interiors.AddAsync(newInterior);
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

            var interior = await _context.Interiors.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(interior);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, InteriorVM interior)
        {
            var essn = await _context.Interiors.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (interior.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + interior.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/InteriorImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, interior.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/InteriorImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/InteriorImages/" + logoFileName;
                essn.Image =  logoFileName;


            }
            essn.Translates.Clear();
            foreach (var translate in interior.Translates)

            {
                InteriorTranslate interiorTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                    CoverText = translate.CoverText,
                };
                essn.Translates.Add(interiorTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Interior");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbInterior = await _context.Interiors
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbInterior.Image != null && dbInterior.Image != "")
            {
                //var pathbanner = dbInterior.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/InteriorImages/",
                 dbInterior.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.InteriorTranslates.RemoveRange(dbInterior.Translates);
            _context.Interiors.Remove(dbInterior);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
