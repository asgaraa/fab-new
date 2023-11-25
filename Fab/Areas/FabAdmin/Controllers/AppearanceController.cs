using Fab.Areas.FabAdmin.ViewModels.Gorunus;
using Fab.Data;
using Fab.Models.Gornushler;
using Fab.Models.SubcategoryFolder;
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
    public class AppearanceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AppearanceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var appear = await _context.AppearanceFields.Include(m => m.Translates).ToListAsync();
            return View(appear);
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
        public async Task<IActionResult> Create(AppearenceVM appear)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<AppearanceFIeldTranslate> appearTranslates = new();
                foreach (var translate in appear.Translates)
                {
                    AppearanceFIeldTranslate newAppearTranslate = new()
                    {
                        Name = translate.Name,
                        LangCode = translate.LangCode
                    };
                    appearTranslates.Add(newAppearTranslate);
                }

                AppearanceField newAppear = new()
                {
                    CategoryId = appear.CategoryId,
                    Translates = appearTranslates
                };

                await _context.AppearanceFIeldTranslates.AddRangeAsync(appearTranslates);
                await _context.AppearanceFields.AddAsync(newAppear);
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

            var appear = await _context.AppearanceFields.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);

            ViewBag.Category = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisCategory = await _context.Categories.Where(m => m.Id == appear.CategoryId).Select(he => new CategoryDTO
            {
                Id = appear.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();



            return View(appear);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AppearenceVM appear, int Id)
        {
            var dbappear = await _context.AppearanceFields.Include(s => s.Translates).FirstOrDefaultAsync(s => s.Id == Id);

            if (dbappear == null) return View();

            dbappear.Translates.Clear();
            foreach (var translate in appear.Translates)
            {
                AppearanceFIeldTranslate appearTranslate = new()
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                };
                dbappear.Translates.Add(appearTranslate);
            }

            dbappear.CategoryId = appear.CategoryId;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            var appear = await _context.AppearanceFields.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (appear == null) return NotFound();


            _context.AppearanceFIeldTranslates.RemoveRange(appear.Translates);
            _context.AppearanceFields.Remove(appear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
