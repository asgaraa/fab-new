using Fab.Data;
using Fab.Models.BlogsFolder;
using Fab.Models.PressFolder;
using Fab.Models.ProductFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BlogsFolder;
using FabAdmin.ViewModels.PressFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class PressController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PressController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            var presses = await _context.Presses.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(presses);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(PressVM press)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<PressTranslate> pressTranslates = new();
                foreach (var translate in press.Translates)
                {

                    PressTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        Date = translate.Date,
                        LangCode = translate.LangCode
                    };
                    pressTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + press.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/PressImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, press.ImageFile);
                Press newPress = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/PressImages/" + logoFileName,
                    Image =logoFileName,

                    Translates = pressTranslates,
                };

                await _context.Presses.AddAsync(newPress);
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

            var presses = await _context.Presses.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(presses);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id, PressVM press)
        {
            var essn = await _context.Presses.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (press.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + press.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/PressImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, press.ImageFile);
                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/PressImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/PressImages/" + logoFileName;
                essn.Image = logoFileName;


            }
            essn.Translates.Clear();
            foreach (var translate in press.Translates)

            {
                PressTranslate pressTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                    Date = translate.Date
                };
                essn.Translates.Add(pressTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Press");

        }



        public async Task<IActionResult> Delete(int id)
        {
            var dbPress = await _context.Presses
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbPress.Image != null && dbPress.Image != "")
            {
                //var pathbanner = dbPress.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/PressImages/",
                 dbPress.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.PressTranslates.RemoveRange(dbPress.Translates);
            _context.Presses.Remove(dbPress);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
