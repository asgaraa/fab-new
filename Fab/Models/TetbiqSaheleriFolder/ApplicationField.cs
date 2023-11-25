using Fab.Areas.FabAdmin.ViewModels.Gorunus;
using Fab.Models.CategoriesFolder;
using Fab.Models.Xususiyyetler;
using Fab.ViewModels.Tetbiq;

namespace Fab.Models.TetbiqSaheleriFolder
{
    public class ApplicationField:BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ApplicationFieldTranslate> Translates { get; set; }
    }
}
