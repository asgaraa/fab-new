using Fab.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class CVController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CVController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "CV";
            ViewBag.CurrentAction = "Index";

            var cv = await _context.CVs.Where(m => m.IsDeleted == false).ToListAsync();
            return View(cv);
        }


        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }   

            var cv = await _context.CVs.Where(m => m.IsDeleted == false).FirstOrDefaultAsync(m => m.Id == id)
                ;


            return View(cv);
        }
    }
}
