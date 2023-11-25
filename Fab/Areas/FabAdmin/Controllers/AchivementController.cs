using Fab.Areas.FabAdmin.ViewModels.AboutFolder;
using Fab.Areas.FabAdmin.ViewModels.AchivementFolder;
using Fab.Data;
using Fab.Models.AboutFolder;
using Fab.Models.AchievementFolder;
using FabAdmin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class AchivementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AchivementController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var achivement = await _context.Achivements.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(achivement);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AchivementVM achivement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<AchivementTranslate> achviementTranslates = new();
                foreach (var translate in achivement.Translates)
                {

                    AchivementTranslate achviementTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    achviementTranslates.Add(achviementTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + achivement.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AchivementImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, achivement.ImageFile);
                Achivement newAchivement = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/AchivementImages/" + logoFileName,
                    Image =logoFileName,

                    Translates = achviementTranslates,
                };

                await _context.Achivements.AddAsync(newAchivement);
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

            var achivement = await _context.Achivements.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(achivement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, AchivementVM achivement)
        {
            var dbAchivement = await _context.Achivements.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (dbAchivement == null)
            {
                return View();
            }
            if (achivement.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + achivement.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AchivementImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, achivement.ImageFile);
                //var pathbanner = dbAchivement.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AchivementImages/", dbAchivement.Image);
                FileHelper.DeleteFile(filebanner);
                //dbAchivement.Image = "http://134.209.118.89/" + "ModelImages/AchivementImages/" + logoFileName;
                dbAchivement.Image = logoFileName;

            }
            dbAchivement.Translates.Clear();
            foreach (var translate in achivement.Translates)

            {
                AchivementTranslate achivementTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                dbAchivement.Translates.Add(achivementTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Achivement");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var dbAchivement = await _context.Achivements
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbAchivement.Image != null && dbAchivement.Image != "")
            {
                //var pathbanner = dbAchivement.Image.Replace("http://134.209.118.89/", "");
                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/AchivementImages/", dbAchivement.Image);
                FileHelper.DeleteFile(filebanner);
            }
            _context.AchivementTranslates.RemoveRange(dbAchivement.Translates);
            _context.Achivements.Remove(dbAchivement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
