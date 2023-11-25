using Fab.Models.ProductFolder;

namespace Fab.ViewModels.ProductVM
{
    public class DetailVM
    {
        public Product Product { get; set; }
        public List<Product> Others { get; set; }
        public string LangCode { get; set; }
    }
}
