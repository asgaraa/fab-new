using Fab.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MessageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Message";
            ViewBag.CurrentAction = "Index";
            var messages = await _context.Contacts.Where(m => m.IsDeleted == false).ToListAsync();
            return View(messages);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var message = await _context.Contacts.Where(m => m.IsDeleted == false).FirstOrDefaultAsync(m => m.Id == id);


            return View(message);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            var message = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);

            if (message == null) return NotFound();


            _context.Contacts.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}