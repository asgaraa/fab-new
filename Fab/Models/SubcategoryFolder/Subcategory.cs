using Fab.Models.CategoriesFolder;

namespace Fab.Models.SubcategoryFolder
{
    public class Subcategory : BaseEntity
    {
        //public string File { get; set; }  niye var bu ? 
        public List<SubcategoryTranslate> Translates { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
