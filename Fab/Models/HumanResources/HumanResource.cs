using Fab.Models.BannersFolder;

namespace Fab.Models.HumanResources
{
    public class HumanResource : BaseEntity
    {
        public string Image { get; set; }
        public List<HumanResourceTranslate> Translates { get; set; }
    }
}
