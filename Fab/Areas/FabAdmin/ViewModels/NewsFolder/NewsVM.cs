using Fab.Models.NewsFolder;

namespace FabAdmin.ViewModels.NewsFolder
{
    public class NewsVM
    {
        public IFormFile ImageFile { get; set; }
        public List<NewsTranslateVM> Translates { get; set; }
    }
}
