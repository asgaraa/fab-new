using Fab.Areas.FabAdmin.ViewModels.AchivementFolder;

namespace Fab.Areas.FabAdmin.ViewModels.ActivityFolder
{
    public class ActivityVM
    {
        public IFormFile ImageFile { get; set; }
        public List<ActivityTranslateVM> Translates { get; set; }
    }
}
