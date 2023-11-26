using Fab.Areas.FabAdmin.ViewModels.ActivityFolder;
using Fab.Data;
using Fab.Models.ActivitiyFolder;
using Fab.Models.AdsFolder;
using FabAdmin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fab.Areas.FabAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("FabAdmin")]
    public class ActivityController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ActivityController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Activity";
            ViewBag.CurrentAction = "Index";
            var about = await _context.Activities.Where(m => m.IsDeleted == false).Include(m => m.Translates).ToListAsync();
            return View(about);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityVM activity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                List<ActivityTranslate> activityTranslates = new();
                foreach (var translate in activity.Translates)
                {

                    ActivityTranslate activityTranslate = new()
                    {
                        Header = translate.Header,
                        Desc = translate.Desc,
                        LangCode = translate.LangCode
                    };
                    activityTranslates.Add(activityTranslate);
                }
                string logoFileName = Guid.NewGuid().ToString() + "_" + activity.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ActivityImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, activity.ImageFile);
                Activity newActivity = new()
                {
                    //Image = "http://134.209.118.89/" + "ModelImages/ActivityImages/" + logoFileName,
                    Image = logoFileName,

                    Translates = activityTranslates,
                };
                await _context.Activities.AddAsync(newActivity);
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

            var activity = await _context.Activities.Include(x => x.Translates).FirstOrDefaultAsync(x => x.Id == Id);
            return View(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, ActivityVM activity)
        {
            var dbActivity = await _context.Activities.Include(m => m.Translates).FirstOrDefaultAsync(m => m.Id == Id);
            if (dbActivity == null)
            {
                return View();
            }
            if (activity.ImageFile != null)
            {
                string logoFileName = Guid.NewGuid().ToString() + "_" + activity.ImageFile.FileName;
                string logoPath = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ActivityImages/", logoFileName);
                await FileHelper.SaveFileAsync(logoPath, activity.ImageFile);

                //var pathbanner = dbActivity.Image.Replace("http://134.209.118.89/", "");
            


                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ActivityImages/",
                  dbActivity.Image);
                FileHelper.DeleteFile(filebanner);
                //dbActivity.Image = "http://134.209.118.89/" + "ModelImages/ActivityImages/" + logoFileName;
                dbActivity.Image = logoFileName;


            }
            dbActivity.Translates.Clear();
            foreach (var translate in activity.Translates)

            {
                ActivityTranslate activityTranslate = new()
                {

                    LangCode = translate.LangCode,
                    Header = translate.Header,
                    Desc = translate.Desc,
                };
                dbActivity.Translates.Add(activityTranslate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Activity");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var dbActivity = await _context.Activities
                .Include(e => e.Translates)
                .FirstOrDefaultAsync(e => e.Id == id);


            if (dbActivity.Image != null && dbActivity.Image != "")
            {
                //var pathbanner = dbActivity.Image.Replace("http://134.209.118.89/", "");

                var filebanner = FileHelper.GetFilePath(_env.WebRootPath, "ModelImages/ActivityImages/",
                 dbActivity.Image);
                FileHelper.DeleteFile(filebanner);
            }
            
            _context.ActivityTranslates.RemoveRange(dbActivity.Translates);
            _context.Activities.Remove(dbActivity);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
