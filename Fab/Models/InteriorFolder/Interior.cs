using Fab.Models.NewsFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.InteriorFolder
{
    public class Interior : BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<InteriorTranslate> Translates { get; set; }
    }
}
