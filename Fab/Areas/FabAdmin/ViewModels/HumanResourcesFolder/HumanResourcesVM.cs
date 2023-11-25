using FabAdmin.ViewModels.BannersFolder;

namespace FabAdmin.ViewModels.HumanResourcesFolder
{
    public class HumanResourcesVM
    {
        public IFormFile ImageFile { get; set; }
        public List<HumanResourcesTranslateVM> Translates { get; set; }
    }
}
