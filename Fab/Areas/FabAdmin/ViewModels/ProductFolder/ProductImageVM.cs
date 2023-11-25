namespace FabAdmin.ViewModels.ProductFolder
{
    public class ProductImageVM
    {
        public IFormFile Image { get; set; }
        public bool IsMain { get; set; }
        public ProductVM Product { get; set; }
    }
}
