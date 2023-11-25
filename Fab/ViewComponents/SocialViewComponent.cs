using Fab.Data;
using Fab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.ViewComponents
{
    public class SocialViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public SocialViewComponent(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
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

            ContactComponentVM contact = new ContactComponentVM()
            {
                LangCode = lang,
                Contact = await _context.ContactInformations.FirstOrDefaultAsync()
            };



            return View(contact);
        }
    }
}
