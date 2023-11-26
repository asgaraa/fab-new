using Fab.Models.BlogsFolder;
using Fab.Models.NewsFolder;
using Fab.Models.PressFolder;

namespace Fab.ViewModels
{
    public class MediaPageVM
    {
        public string LangCode { get; set; }
        public List<Fab.Models.BlogsFolder.Blog> Blogs { get; set; }
        public List<Press> Presses { get; set; }
        public List<News> News { get; set; }
    }
}
