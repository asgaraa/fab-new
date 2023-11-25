using Fab.Models.BannersFolder;

namespace Fab.Models.HumanResources
{
    public class HumanResourceTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public HumanResource HumanResource { get; set; }
        public int HumanResourceId { get; set; }
    }
}
