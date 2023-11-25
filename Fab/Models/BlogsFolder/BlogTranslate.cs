namespace Fab.Models.BlogsFolder
{
    public class BlogTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string Date { get; set; }
        public string LangCode { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
