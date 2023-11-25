using FabAdmin.ViewModels.BlogsFolder;

namespace FabAdmin.ViewModels.PressFolder
{
    public class PressVM
    {
        public IFormFile ImageFile { get; set; }
        public List<PressTranslateVM> Translates { get; set; }
    }
}
