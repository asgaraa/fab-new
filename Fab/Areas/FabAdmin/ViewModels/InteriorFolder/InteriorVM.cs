using FabAdmin.ViewModels.NewsFolder;

namespace FabAdmin.ViewModels.InteriorFolder
{
    public class InteriorVM
    {
        public IFormFile ImageFile { get; set; }
        public List<InteriorTranslateVM> Translates { get; set; }
    }
}
