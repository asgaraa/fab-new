namespace Fab.Models.TwinFolder
{
    public class TwinTranslate : BaseEntity 
    {
        public string WideDesc { get; set; }
        public string Desc { get; set; }
        public string LangCode { get; set; }
        public int TwinId { get; set; }
        public Twin Twin { get; set; }
    }
}
