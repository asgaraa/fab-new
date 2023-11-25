using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.AdsFolder
{
    public class Ads : BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Link { get; set; }
    }
}
