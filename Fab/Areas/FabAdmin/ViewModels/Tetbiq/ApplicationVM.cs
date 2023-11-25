using Fab.ViewModels.Tetbiq;
using FabAdmin.ViewModels.Subcategory;

namespace Fab.Models.Tetbiq
{
    public class ApplicationVM
    {
        public int CategoryId { get; set; }
        public List<ApplicationTranslateVM> Translates { get; set; }
    }
}
