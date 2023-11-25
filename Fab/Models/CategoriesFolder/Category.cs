using Fab.Models.Gornushler;
using Fab.Models.ProductFolder;
using Fab.Models.SubcategoryFolder;
using Fab.Models.TetbiqSaheleriFolder;
using Fab.Models.Xususiyyetler;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.CategoriesFolder
{
    public class Category : BaseEntity
    {
        public string Image { get; set; }
        public string Icon { get; set; }
        public string? Link { get; set; }
        public List<Product> Products  { get; set; }
        public List<CategoryTranslate> Translates { get; set; }
        public List<Subcategory> Subcategories { get; set; }
        public List<ApplicationField> ApplicationFields { get; set; }
        public List<AppearanceField> AppearanceFields { get; set; }
        public List<Characteristics> Characteristics { get; set; }
    }
}
