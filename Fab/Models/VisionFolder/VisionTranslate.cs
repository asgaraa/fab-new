namespace Fab.Models.VisionFolder
{
    public class VisionTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public int VisionId { get; set; }
        public Vision Vision { get; set; }
    }
}
