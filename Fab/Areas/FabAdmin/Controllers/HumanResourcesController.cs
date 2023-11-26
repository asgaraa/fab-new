using Fab.Data;
using Fab.Models.BannersFolder;
using Fab.Models.HumanResources;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.BannersFolder;
using FabAdmin.ViewModels.HumanResourcesFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class HumanResourcesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public HumanResourcesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var human = await _context.HumanResources.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(human);
        }



        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(HumanResourcesVM human)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<HumanResourceTranslate> humanResourcesTranslates = new();
                foreach (var translate in human.Translates)
                {

                    HumanResourceTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    humanResourcesTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + human.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/HumanResourcesImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, human.ImageFile);
                HumanResource newHuman = new()
                {

                    //Image = "http://134.209.118.89/" + "ModelImages/HumanResourcesImages/" + logoFileName,
                    Image =  logoFileName,

                    Translates = humanResourcesTranslates,
                };

                await _context.HumanResources.AddAsync(newHuman);
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

            var human = await _context.HumanResources.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(human);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(int Id, HumanResourcesVM human)
        {
            var essn = await _context.HumanResources.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (human.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + human.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/HumanResourcesImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, human.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/HumanResourcesImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/HumanResourcesImages/" + logoFileName;
                essn.Image =  logoFileName;


            }
            essn.Translates.Clear();
            foreach (var translate in human.Translates)

            {
                HumanResourceTranslate humanTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                essn.Translates.Add(humanTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "HumanResources");

        }





        public async Task<IActionResult> Delete(int id)
        {
            var human = await _context.HumanResources.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == id);

            if (human == null) return NotFound();

            if (human.Image != null && human.Image != "")
            {
                //var pathbanner = human.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/HumanResourcesImages/",
                 human.Image);
                FileHelper.DeleteFile(filebanner);
            }


            


            _context.HumanResourceTranslates.RemoveRange(human.Translates);
            _context.HumanResources.Remove(human);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
