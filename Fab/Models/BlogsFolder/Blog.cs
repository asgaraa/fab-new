using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.BlogsFolder
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<BlogTranslate> Translates { get; set; }
    }
}
