using Fab.Data;
using Fab.Models.SubcategoryFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.CategoriesFolder;
using FabAdmin.ViewModels.Subcategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class SubcategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SubcategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var subctg = await _context.Subcategories.Include(m => m.Translates).ToListAsync();
            return View(subctg);
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
        public async Task<IActionResult> Create(SubcategoryVM subcategory)
        {
           
            try
            {
                if (!ModelState.IsValid)
                {
                        return View();
                }

                List<SubcategoryTranslate> subcategoryTranslates = new();
                foreach (var translate in subcategory.Translates)
                {
                    SubcategoryTranslate newsubcategoryTranslate = new()
                    {
                        Name = translate.Name,
                        LangCode = translate.LangCode
                    };
                    subcategoryTranslates.Add(newsubcategoryTranslate);
                }

                Subcategory newNewSubcategory = new()
                {
                    CategoryId = subcategory.CategoryId,
                    Translates = subcategoryTranslates,
                };

                await _context.SubcategoryTranslates.AddRangeAsync(subcategoryTranslates);
                await _context.Subcategories.AddAsync(newNewSubcategory);
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

            var subctg = await _context.Subcategories.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);

            ViewBag.Category = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisCategory = await _context.Categories.Where(m => m.Id == subctg.CategoryId).Select(he => new CategoryDTO
            {
                Id = subctg.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            

            return View(subctg);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SubcategoryVM subcategory, int Id)
        {
            var dbSubctg = await _context.Subcategories.Include(s => s.Translates).FirstOrDefaultAsync(s => s.Id == Id);

            if (dbSubctg == null) return View();

            dbSubctg.Translates.Clear();
            foreach (var translate in subcategory.Translates)
            {
                SubcategoryTranslate subcategoryTranslate = new()
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                };
                dbSubctg.Translates.Add(subcategoryTranslate);
            }

            dbSubctg.CategoryId = subcategory.CategoryId;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            var subCategory = await _context.Subcategories.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (subCategory == null) return NotFound();


            _context.SubcategoryTranslates.RemoveRange(subCategory.Translates);
            _context.Subcategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
