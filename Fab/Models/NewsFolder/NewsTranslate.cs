namespace Fab.Models.NewsFolder
{
    public class NewsTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string Date { get; set; }
        public string LangCode { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
