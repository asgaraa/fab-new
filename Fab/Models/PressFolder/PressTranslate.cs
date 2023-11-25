using Fab.Models.BlogsFolder;

namespace Fab.Models.PressFolder
{
    public class PressTranslate : BaseEntity
    {
        public string Header { get; set; }
        public string Desc { get; set; }
        public string Date { get; set; }
        public string LangCode { get; set; }
        public int PressId { get; set; }
        public Press Press { get; set; }
    }
}
