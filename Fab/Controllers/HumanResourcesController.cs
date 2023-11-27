using Fab.Data;
using Fab.Models.CVFolder;
using Fab.Models.NewsFolder;
using Fab.ViewModels;
using Fab.ViewModels.VacancyVM;
using FabAdmin.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Controllers
{
    public class HumanResourcesController : Controller
    {
        private static DateTime lastSubmitTime = DateTime.MinValue;
        private static int submitCount = 0;

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public HumanResourcesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

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

            var vacancies = await _context.Vacancies.Take(4).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            var human = await _context.HumanResources.Take(3).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();


            HumanResourcesPageVM page = new()
            {
                LangCode = lang,
                Vacancies = vacancies,
                HumanResources = human,

            };
            return View(page);
        }

        public async Task<IActionResult> Vacancies()
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

            var vacancies = await _context.Vacancies.Take(4).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            var human = await _context.HumanResources.Take(3).Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();


            HumanResourcesPageVM page = new()
            {
                LangCode = lang,
                Vacancies = vacancies,
                HumanResources = human,

            };
            return View(page);
        }

        public async Task<IActionResult> VacancyDetail(int id)
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

            var vacancies = await _context.Vacancies.Include(m => m.Translates.Where(m => m.LangCode == lang && m.VacancyId==id)).FirstOrDefaultAsync();


            VacancyVM vacancyVM = new VacancyVM()
            {
                LangCode=lang,
                Vacancy=vacancies
            };
            return View(vacancyVM);
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
