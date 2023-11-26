

namespace Fab.ViewModels.Blog
{
    public class BlogVM
    {
        public string LangCode { get; set; }
        public List<Fab.Models.BlogsFolder.Blog> Blogs { get; set; }
        public Fab.Models.BlogsFolder.Blog Blog { get; set; }
    }
}
