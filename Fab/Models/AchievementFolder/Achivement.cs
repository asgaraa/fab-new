using Fab.Models.AboutFolder;

namespace Fab.Models.AchievementFolder
{
    public class Achivement : BaseEntity
    {
        public string Image { get; set; }
        public List<AchivementTranslate> Translates { get; set; }
    }
}
