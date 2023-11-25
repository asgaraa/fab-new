using Fab.Models.CategoriesFolder;
using Fab.Models.Gornushler;
using Fab.Models.SubcategoryFolder;
using Fab.Models.TetbiqSaheleriFolder;
using Fab.Models.Xususiyyetler;

namespace Fab.Models.ProductFolder
{
    public class Product : BaseEntity
    {
        
        public string ProductCode { get; set; }
        public string Brand { get; set; }
        public string Properties { get; set; }
       
        public List<ProductTranslate> Translates { get; set; }
        public List<ProductImages> Images { get; set; }
        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
        public int? SubcategoryId { get; set; }
        public Characteristics? Characteristic { get; set; }
        public int? CharacteristicId { get; set; } 
        public ApplicationField? ApplicationField { get; set; }
        public int? ApplicationFieldId { get; set; }
        public AppearanceField? AppearanceField { get; set; }
        public int? AppearanceFieldId { get; set; }
    }
}
