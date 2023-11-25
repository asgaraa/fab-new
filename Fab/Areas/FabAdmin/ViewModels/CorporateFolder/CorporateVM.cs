using FabAdmin.ViewModels.NewsFolder;

namespace FabAdmin.ViewModels.CorporateFolder
{
    public class CorporateVM
    {
        public IFormFile ImageFile { get; set; }
        public IFormFile FrontImageFile { get; set; }
        public IFormFile BackgroundImageFile { get; set; }
        public List<CorporateTranslateVM> Translates { get; set; }
    }
}
