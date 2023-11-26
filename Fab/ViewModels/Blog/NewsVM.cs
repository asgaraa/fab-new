namespace Fab.ViewModels.Blog
{
    public class NewsVM
    {
        public string LangCode { get; set; }
        public List<Fab.Models.NewsFolder.News> News { get; set; }
        public Fab.Models.NewsFolder.News  New { get; set; }
    }
}
