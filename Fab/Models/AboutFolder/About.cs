namespace Fab.Models.AboutFolder
{
    public class About : BaseEntity
    {
        public string Image { get; set; }
        public List<AboutTranslate> Translates { get; set; }
    }
}
