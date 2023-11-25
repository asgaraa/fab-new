using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.BannersFolder
{
    public class BannerTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public Banner Banner { get; set; }
        public int BannerId { get; set; }

    }
}
