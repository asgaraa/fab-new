using Fab.Data;
using Fab.Models.NewsFolder;
using Fab.Models.VacanciesFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.NewsFolder;
using FabAdmin.ViewModels.VacancyFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class VacancyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public VacancyController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var vacancy = await _context.Vacancies.Where(m => m.IsDeleted == false)
                .Include(m => m.Translates)
                .ToListAsync();
            return View(vacancy);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VacancyVM vacancy)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<VacancyTranslate> vacancyTranslates = new();
                foreach (var translate in vacancy.Translates)
                {

                    VacancyTranslate entertainmentTranslate = new()
                    {
                        Position = translate.Position,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    vacancyTranslates.Add(entertainmentTranslate);
                }
                
                Vacancy newVacancy = new()
                {
                    Translates = vacancyTranslates,
                };

                await _context.Vacancies.AddAsync(newVacancy);
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

            var vacanies = await _context.Vacancies.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(vacanies);
        }





        [HttpPost]
        public async Task<IActionResult> Edit(int Id, VacancyVM vacancy)
        {
            var essn = await _context.Vacancies.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            
            essn.Translates.Clear();
            foreach (var translate in vacancy.Translates)

            {
                VacancyTranslate vacancyTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Position = translate.Position,
                    Desc = translate.Desc,
                };
                essn.Translates.Add(vacancyTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Vacancy");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbVacancies = await _context.Vacancies
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            _context.VacancyTranslates.RemoveRange(dbVacancies.Translates);
            _context.Vacancies.Remove(dbVacancies);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}
