using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.NewsFolder
{
    public class News : BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<NewsTranslate> Translates { get; set; }
    }
}
