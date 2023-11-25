using Fab.Models.AboutFolder;
using Fab.Models.AchievementFolder;
using Fab.Models.ActivitiyFolder;
using Fab.Models.VisionFolder;

namespace Fab.ViewModels
{
    public class AboutPageVM
    {
        public string LangCode { get; set; }
        public List<Activity> Activites { get; set; }
        public List<Achivement> Achivements { get; set; }
        public List<About> Abouts { get; set; }
        public List<Vision> Visions { get; set; }

    }
}
