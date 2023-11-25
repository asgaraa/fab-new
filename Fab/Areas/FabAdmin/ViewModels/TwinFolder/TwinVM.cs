using Fab.Models.TwinFolder;

namespace FabAdmin.ViewModels.TwinFolder
{
    public class TwinVM
    {
        public IFormFile WideImageFile { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<TwinTranslateVM> Translates { get; set; }
    }
}
