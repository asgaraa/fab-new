using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.BannersFolder
{
    public class Banner : BaseEntity
    {
        public string Image { get; set; }
        public List<BannerTranslate> Translates { get; set; }
    }
}
