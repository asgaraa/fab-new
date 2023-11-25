using Fab.Models.CategoriesFolder;

namespace FabAdmin.ViewModels.CategoriesFolder
{
    public class CategoryVM
    {
        public string? Link { get; set; }
        public IFormFile IconFile { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<CategoryTranslateVM> Translates { get; set; }
    }
}
