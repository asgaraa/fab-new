using Fab.Data;
using Fab.Models.ContactFolder;
using Fab.Models.SubcategoryFolder;
using Fab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ContactController(AppDbContext context, IWebHostEnvironment env)
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

            var information = await _context.ContactInformations.FirstOrDefaultAsync();

            ContactPageVM contactPage = new()
            {
                Informations = information,
                LangCode = lang
            };

            return View(contactPage);
        }

        public async Task<IActionResult> Location()
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

            var information = await _context.ContactInformations.FirstOrDefaultAsync();

            ContactPageVM contactPage = new()
            {
                Informations = information,
                LangCode = lang
            };

            return View(contactPage);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactPageVM contact)
        {
            try
            {
                if (contact.Fullname == "" || contact.Message == "" || contact.Email == "")
                {
                    return RedirectToAction("Index");
                }

                Contact newContact = new()
                {
                    Message = contact.Message,
                    Email = contact.Email,
                    Fullname = contact.Fullname,
                }

                ;

                await _context.Contacts.AddAsync(newContact);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
