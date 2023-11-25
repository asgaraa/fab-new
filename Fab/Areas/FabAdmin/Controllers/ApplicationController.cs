using Fab.Data;
using Fab.Models.SubcategoryFolder;
using Fab.Models.Tetbiq;
using Fab.Models.TetbiqSaheleriFolder;
using Fab.ViewModels.Tetbiq;
using FabAdmin.ViewModels.CategoriesFolder;
using FabAdmin.ViewModels.Subcategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class ApplicationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ApplicationController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var application = await _context.ApplicationFields.Include(m => m.Translates).ToListAsync();
            return View(application);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationVM applicaiton)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<ApplicationFieldTranslate> applicaitonTranslates = new();
                foreach (var translate in applicaiton.Translates)
                {
                    ApplicationFieldTranslate newapplicationTranslate = new()
                    {
                        Name = translate.Name,
                        LangCode = translate.LangCode
                    };
                    applicaitonTranslates.Add(newapplicationTranslate);
                }

                ApplicationField newApplication = new()
                {
                    CategoryId = applicaiton.CategoryId,
                    Translates = applicaitonTranslates,
                };

                await _context.ApplicationFieldTranslates.AddRangeAsync(applicaitonTranslates);
                await _context.ApplicationFields.AddAsync(newApplication);
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

            var application = await _context.ApplicationFields.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);

            ViewBag.Category = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisCategory = await _context.Categories.Where(m => m.Id == application.CategoryId).Select(he => new CategoryDTO
            {
                Id = application.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();



            return View(application);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationVM applicaiton, int Id)
        {
            var dbApplication = await _context.ApplicationFields.Include(s => s.Translates).FirstOrDefaultAsync(s => s.Id == Id);

            if (dbApplication == null) return View();

            dbApplication.Translates.Clear();
            foreach (var translate in applicaiton.Translates)
            {
                ApplicationFieldTranslate applicaitonTranslate = new()
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                };
                dbApplication.Translates.Add(applicaitonTranslate);
            }

            dbApplication.CategoryId = applicaiton.CategoryId;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            var applicaiton = await _context.ApplicationFields.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (applicaiton == null) return NotFound();


            _context.ApplicationFieldTranslates.RemoveRange(applicaiton.Translates);
            _context.ApplicationFields.Remove(applicaiton);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
