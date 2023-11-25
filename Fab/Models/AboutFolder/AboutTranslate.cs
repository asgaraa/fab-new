namespace Fab.Models.AboutFolder
{
    public class AboutTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }

    }
}
