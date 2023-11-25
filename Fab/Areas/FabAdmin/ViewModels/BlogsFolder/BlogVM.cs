using FabAdmin.ViewModels.NewsFolder;

namespace FabAdmin.ViewModels.BlogsFolder
{
    public class BlogVM
    {
        public IFormFile ImageFile { get; set; }
        public List<BlogTranslateVM> Translates { get; set; }
    }
}
