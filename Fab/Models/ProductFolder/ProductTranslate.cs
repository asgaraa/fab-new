namespace Fab.Models.ProductFolder
{
    public class ProductTranslate : BaseEntity
    {
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public string? ProducerCountry { get; set; }
        public string LangCode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string? Size { get; set; }
        public string? Length { get; set; }
        public string? Color { get; set; }
    }
}
