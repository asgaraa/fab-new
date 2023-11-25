using Fab.Data;
using Fab.Models.CorporateFolder;
using Fab.Models.NewsFolder;
using FabAdmin.Helpers;
using FabAdmin.ViewModels.CorporateFolder;
using FabAdmin.ViewModels.NewsFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class CorporateController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CorporateController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var corporate = await _context.Corporates.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(corporate);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CorporateVM corporate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<CorporateTranslate> corporateTranslates = new();
                foreach (var translate in corporate.Translates)
                {

                    CorporateTranslate entertainmentTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    corporateTranslates.Add(entertainmentTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + corporate.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, corporate.ImageFile);

                string logoFileName2 = Guid.NewGuid().ToString() + "_" + corporate.FrontImageFile.FileName;
                string logoPath2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/", logoFileName2);
                await FileHelper.SaveFileAsync(logoPath2, corporate.FrontImageFile);

                string logoFileName3 = Guid.NewGuid().ToString() + "_" + corporate.BackgroundImageFile.FileName;
                string logoPath3 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/", logoFileName3);
                await FileHelper.SaveFileAsync(logoPath3, corporate.BackgroundImageFile);



                Corporate newCorporate = new()
                {
                    //Image = "http://134.209.118.89/" + "ModelImages/CorporateImages/" + logoFileName,
                    //FrontImage = "http://134.209.118.89/" + "ModelImages/CorporateImages/" + logoFileName2,
                    //BackgroundImage = "http://134.209.118.89/" + "ModelImages/CorporateImages/" + logoFileName3,
                    Image = logoFileName,
                    FrontImage = logoFileName2,
                    BackgroundImage = logoFileName3,
                    Translates = corporateTranslates,
                };

                await _context.Corporates.AddAsync(newCorporate);
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

            var corporate = await _context.Corporates.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(corporate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, CorporateVM corporate)
        {
            var essn = await _context.Corporates.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (essn == null)
            {
                return View();
            }
            if (corporate.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + corporate.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, corporate.ImageFile);

                //var pathbanner = essn.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/",
                  essn.Image);
                FileHelper.DeleteFile(filebanner);
                //essn.Image = "http://134.209.118.89/" + "ModelImages/CorporateImages/" + logoFileName;
                essn.Image =logoFileName;


            }


            if (corporate.FrontImageFile != null)
            {
                string logoFileName2 = Guid.NewGuid().ToString() + "_" + corporate.FrontImageFile.FileName;
                string logoPath2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/", logoFileName2);
                await FileHelper.SaveFileAsync(logoPath2, corporate.FrontImageFile);

                //var pathbanner = essn.FrontImage.Replace("http://134.209.118.89/", "");

                var filebanner2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/",
                  essn.FrontImage);
                FileHelper.DeleteFile(filebanner2);
                //essn.FrontImage = "http://134.209.118.89/" + "ModelImages/CorporateImages/" + logoFileName2;
                essn.FrontImage = logoFileName2;


            }


            if (corporate.BackgroundImageFile != null)
            {
                string logoFileName3 = Guid.NewGuid().ToString() + "_" + corporate.BackgroundImageFile.FileName;
                string logoPath3 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/", logoFileName3);
                await FileHelper.SaveFileAsync(logoPath3, corporate.BackgroundImageFile);

                //var pathbanner = essn.BackgroundImage.Replace("http://134.209.118.89/", "");

                var filebanner3 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/",
                  essn.BackgroundImage);
                FileHelper.DeleteFile(filebanner3);
                //essn.BackgroundImage = "http://134.209.118.89/" + "ModelImages/CorporateImages/" + logoFileName3;
                essn.BackgroundImage =  logoFileName3;


            }
            essn.Translates.Clear();
            foreach (var translate in corporate.Translates)

            {
                CorporateTranslate corporateTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                essn.Translates.Add(corporateTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Corporate");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dbCorporate = await _context.Corporates
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbCorporate.Image != null && dbCorporate.Image != "")
            {
                //var pathbanner = dbCorporate.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/",
                 dbCorporate.Image);
                FileHelper.DeleteFile(filebanner);
            }
            if (dbCorporate.FrontImage != null && dbCorporate.FrontImage != "")
            {
                //var pathbanner = dbCorporate.FrontImage.Replace("http://134.209.118.89/", "");

                var filebanner2 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/",
                 dbCorporate.FrontImage);
                FileHelper.DeleteFile(filebanner2);
            }
            if (dbCorporate.BackgroundImage != null && dbCorporate.BackgroundImage != "")
            {
                //var pathbanner = dbCorporate.BackgroundImage.Replace("http://134.209.118.89/", "");

                var filebanner3 = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/CorporateImages/",
                 dbCorporate.BackgroundImage);
                FileHelper.DeleteFile(filebanner3);
            }




            _context.CorporateTranslates.RemoveRange(dbCorporate.Translates);
            _context.Corporates.Remove(dbCorporate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }

}

