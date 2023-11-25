using Fab.Models.CategoriesFolder;

namespace Fab.Models.CorporateFolder
{
    public class CorporateTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public int CorporateId { get; set; }
        public Corporate Corporate { get; set; }
    }
}
