using Fab.Data;
using Fab.Models.BannersFolder;
using Fab.Models.CategoriesFolder;
using Fab.Models.CorporateFolder;
using Fab.Models.NewsFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BannersFolder;
using FabAdmin.ViewModels.CategoriesFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Category";
            ViewBag.CurrentAction = "Index";
            var category = await _context.Categories.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(category);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<CategoryTranslate> categoryTranslates = new();
                foreach (var translate in category.Translates)
                {

                    CategoryTranslate entertainmentTranslate = new()
                    {
                        Name = translate.Name,
                        LangCode = translate.LangCode
                    };
                    categoryTranslates.Add(entertainmentTranslate);
                }

                string logoFileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, category.ImageFile);


                string logoFileName2 = Guid.NewGuid().ToString() + "_" + category.IconFile.FileName;
                string logoPath2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/", logoFileName2);
                await FileHelper.SaveFileAsync(logoPath2, category.IconFile);
                Category newCategory = new()
                {
                    Link = category.Link,
                    //Icon = "http://134.209.118.89/" + "ModelImages/CategoryImages/" + logoFileName2,
                    //Image = "http://134.209.118.89/" + "ModelImages/CategoryImages/" + logoFileName,
                    Icon =  logoFileName2,
                    Image =logoFileName,
                    Translates = categoryTranslates,
                };

                await _context.Categories.AddAsync(newCategory);
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

            var category = await _context.Categories.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id, CategoryVM category)
        {
            var essn = await _context.Categories.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (category.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, category.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/CategoryImages/" + logoFileName;
                essn.Image = logoFileName;


            }
            if (category.IconFile != null)
            {
                string logoFileName2 = Guid.NewGuid().ToString() + "_" + category.IconFile.FileName;
                string logoPath2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/", logoFileName2);
                await FileHelper.SaveFileAsync(logoPath2, category.IconFile);

                //var pathbanner = essn.Icon.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Icon = "http://134.209.118.89/" + "ModelImages/CategoryImages/" + logoFileName2;
                essn.Icon = logoFileName2;


            }

            essn.Link = category.Link;

            essn.Translates.Clear();
            foreach (var translate in category.Translates)

            {
                CategoryTranslate categoryTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Name = translate.Name,
                    
                };
                essn.Translates.Add(categoryTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Category");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.Include(m =>m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();

            if (category.Image != null && category.Image != "")
            {
                //var pathbanner = category.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/",
                 category.Image);
                FileHelper.DeleteFile(filebanner);
            }
            

            if (category.Icon != null && category.Icon != "")
            {
                //var pathbanner = category.Icon.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CategoryImages/",
                 category.Icon);
                FileHelper.DeleteFile(filebanner);
            }


            _context.CategoryTranslates.RemoveRange(category.Translates);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
