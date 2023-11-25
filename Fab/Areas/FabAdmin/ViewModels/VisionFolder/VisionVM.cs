using FabAdmin.ViewModels.BannersFolder;

namespace Fab.Areas.FabAdmin.ViewModels.VisionFolder
{
    public class VisionVM
    {
        public IFormFile ImageFile { get; set; }
        public List<VisionTranslateVM> Translates { get; set; }
    }
}
