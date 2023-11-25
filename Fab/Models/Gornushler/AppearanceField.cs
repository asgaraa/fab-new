using Fab.Models.CategoriesFolder;
using Fab.Models.Xususiyyetler;

namespace Fab.Models.Gornushler
{
    public class AppearanceField : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<AppearanceFIeldTranslate> Translates { get; set; }
    }
}
