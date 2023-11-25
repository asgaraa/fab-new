using FabAdmin.ViewModels.Subcategory;

namespace Fab.Areas.FabAdmin.ViewModels.Gorunus
{
    public class AppearenceVM
    {
        public int CategoryId { get; set; }
        public List<AppearenceTranslateVM> Translates { get; set; }
    }
}
