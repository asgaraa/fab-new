using Fab.Models.BlogsFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.PressFolder
{
    public class Press : BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<PressTranslate> Translates { get; set; }
    }
}
