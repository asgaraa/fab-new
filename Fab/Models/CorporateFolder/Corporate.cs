using Fab.Models.BlogsFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.CorporateFolder
{
    public class Corporate : BaseEntity
    {
        public string Image { get; set; }
        public string FrontImage { get; set; }
        public string BackgroundImage { get; set; }
        public List<CorporateTranslate> Translates { get; set; }
    }
}
