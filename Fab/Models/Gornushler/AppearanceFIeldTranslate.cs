using Fab.Models.TetbiqSaheleriFolder;

namespace Fab.Models.Gornushler
{
    public class AppearanceFIeldTranslate : BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public int AppearanceFieldId { get; set; }
        public AppearanceField AppearanceField { get; set; }
    }
}
