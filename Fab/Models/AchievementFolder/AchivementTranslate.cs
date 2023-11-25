using Fab.Models.AboutFolder;

namespace Fab.Models.AchievementFolder
{
    public class AchivementTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public int AchivementId { get; set; }
        public Achivement Achivement { get; set; }
    }
}
