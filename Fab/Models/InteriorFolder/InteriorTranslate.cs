using Fab.Models.NewsFolder;

namespace Fab.Models.InteriorFolder
{
    public class InteriorTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string CoverText { get; set; }
        public string LangCode { get; set; }
        public int InteriorId { get; set; }
        public Interior Interior { get; set; }
    }
}
