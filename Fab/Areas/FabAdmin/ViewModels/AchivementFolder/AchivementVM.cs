using Fab.Areas.FabAdmin.ViewModels.AboutFolder;

namespace Fab.Areas.FabAdmin.ViewModels.AchivementFolder
{
    public class AchivementVM
    {
        public IFormFile ImageFile { get; set; }
        public List<AchivementTranslateVM> Translates { get; set; }
    }
}
