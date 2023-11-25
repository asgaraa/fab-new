using Fab.Models.BannersFolder;

namespace Fab.Models.CategoriesFolder
{
    public class CategoryTranslate : BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
