using Fab.Data;
using Fab.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fab.ViewComponents
{
    public class MyViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public MyViewComponent(AppDbContext context, IWebHostEnvironment env)
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



            FooterCategoryVM footerCategoryVM = new()
            {
                Categories = await _context.Categories.Include(m => m.Translates.Where(m => m.LangCode == lang)).ToListAsync(),
                LangCode = lang
            };

            return View(footerCategoryVM);
        }
    }
}
