namespace Fab.Models.ProductFolder
{
    public class ProductImages : BaseEntity
    {
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
