using Fab.Models.AdsFolder;
using Fab.Models.BannersFolder;
using Fab.Models.BlogsFolder;
using Fab.Models.CategoriesFolder;
using Fab.Models.CorporateFolder;
using Fab.Models.InteriorFolder;
using Fab.Models.NewsFolder;
using Fab.Models.PressFolder;
using Fab.Models.TwinFolder;

namespace Fab.ViewModels
{
    public class HomeVM
    {
        public List<Banner> Banners { get; set; }
        public List<Category> Categories { get; set; }
        public List<News> News { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Interior> Interiors { get; set; }
        public List<Twin> Twins { get; set; }
        public List<Corporate> Corporates { get; set; }
        public List<Ads> Ads { get; set; }
        public List<Press> Presses { get; set; }
        public string LangCode { get; set; }
    } 
}
