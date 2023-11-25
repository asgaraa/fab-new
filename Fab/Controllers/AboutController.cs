using Fab.Data;
using Fab.Models.AboutFolder;
using Fab.Models.AchievementFolder;
using Fab.Models.ActivitiyFolder;
using Fab.Models.BannersFolder;
using Fab.Models.VisionFolder;
using Fab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
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

            List<About> abouts = await _context.Abouts.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Achivement> achivements = await _context.Achivements.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Activity> activities = await _context.Activities.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();
            List<Vision> visions = await _context.Visions.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync();


            AboutPageVM aboutPage = new()
            {
                Abouts = abouts,
                Achivements = achivements,
                Activites = activities,
                Visions = visions,
                LangCode = lang,
            };

            return View(aboutPage);
        }
    }
}
