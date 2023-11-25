using Fab.Models.AchievementFolder;

namespace Fab.Models.ActivitiyFolder
{
    public class ActivityTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
