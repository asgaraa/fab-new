using Fab.Models.AboutFolder;

namespace Fab.Models.ActivitiyFolder
{
    public class Activity : BaseEntity
    {
        public string Image { get; set; }
        public List<ActivityTranslate> Translates { get; set; }
    }
}
