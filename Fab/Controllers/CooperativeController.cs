using Fab.Data;
using Fab.Models.AboutFolder;
using Fab.Models.AchievementFolder;
using Fab.Models.CVFolder;
using Fab.Models.VisionFolder;
using Fab.ViewModels;
using Fab.ViewModels.CorperativeFolder;
using FabAdmin.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Controllers
{
    public class CooperativeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CooperativeController(AppDbContext context, IWebHostEnvironment env)
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

            CorperativePageVM pageVM = new CorperativePageVM()
            {
                LangCode = lang,
                Corporates = await _context.Corporates.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync(),
            };

            return View(pageVM);
        }

        [HttpPost]
        public async Task<IActionResult> SendCV(HumanResourcesPageVM cv)
        {


            if (cv.Tel == null || cv.Fullname == null || cv.File == null || cv.File.FileName == "" || cv.Email == null)
            {
                return BadRequest();
            }

            string logoFileName = Guid.NewGuid().ToString() + "_" + cv.File.FileName;
            //string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/Files/UserCVs/", logoFileName);
            string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/UserCv", logoFileName);
            await FileHelper.SaveFileAsync(logoPath, cv.File);

            CV newCv = new()
            {
                Tel = cv.Tel,
                Fullname = cv.Fullname,
                Email = cv.Email,
                FileName = logoFileName,
            };

            await _context.CVs.AddAsync(newCv);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
