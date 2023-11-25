using Fab.Models.ActivitiyFolder;

namespace Fab.Models.VisionFolder
{
    public class Vision : BaseEntity
    {
        public string Image { get; set; }
        public List<VisionTranslate> Translates { get; set; }
    }
}
