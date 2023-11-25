using Fab.Models.CategoriesFolder;
using Fab.Models.ProductFolder;

namespace FabAdmin.ViewModels.ProductFolder
{
    public class ProductVM
    {
        public string ProductCode { get; set; }
        public string Brand { get; set; }
        public List<ProductTranslateVM> Translates { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int? ApplicatId { get; set; }
        public int? AppearId { get; set; }
        public int? CharId { get; set; }

        public List<IFormFile> Images { get; set; }
        public IFormFile Properties { get; set; }
    }
}
