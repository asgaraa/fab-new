using Fab.Areas.FabAdmin.ViewModels.Xususiyyetler;
using Fab.Data;
using Fab.Models.SubcategoryFolder;
using Fab.Models.Xususiyyetler;
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
    public class CharacteristicsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CharacteristicsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var character = await _context.Characteristics.Include(m => m.Translates).ToListAsync();
            return View(character);
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
        public async Task<IActionResult> Create(CharacteristicsVM character)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<CharacteristicsTranslate> characterTranslates = new();
                foreach (var translate in character.Translates)
                {
                    CharacteristicsTranslate newCharacterTranslate = new()
                    {
                        Name = translate.Name,
                        LangCode = translate.LangCode
                    };
                    characterTranslates.Add(newCharacterTranslate);
                }

                Characteristics newCharacteristics = new()
                {
                    CategoryId = character.CategoryId,
                    Translates = characterTranslates,
                };

                await _context.CharacteristicsTranslates.AddRangeAsync(characterTranslates);
                await _context.Characteristics.AddAsync(newCharacteristics);
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
            var characteristics = await _context.Characteristics.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);

            ViewBag.Category = await _context.Categories.Select(he => new CategoryDTO
            {
                Id = he.Id,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();

            ViewBag.ThisCategory = await _context.Categories.Where(m => m.Id == characteristics.CategoryId).Select(he => new CategoryDTO
            {
                Id = characteristics.CategoryId,
                Name = he.Translates.Where(het => het.LangCode == "en").FirstOrDefault().Name
            }).ToListAsync();



            return View(characteristics);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(CharacteristicsVM characteristics, int Id)
        {
            var dbCharacter = await _context.Characteristics.Include(s => s.Translates).FirstOrDefaultAsync(s => s.Id == Id);

            if (dbCharacter == null) return View();

            dbCharacter.Translates.Clear();
            foreach (var translate in characteristics.Translates)
            {
                CharacteristicsTranslate characteristicsTranslate = new()
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                };
                dbCharacter.Translates.Add(characteristicsTranslate);
            }

            dbCharacter.CategoryId = characteristics.CategoryId;


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            var characteristics = await _context.Characteristics.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (characteristics == null) return NotFound();


            _context.CharacteristicsTranslates.RemoveRange(characteristics.Translates);
            _context.Characteristics.Remove(characteristics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
