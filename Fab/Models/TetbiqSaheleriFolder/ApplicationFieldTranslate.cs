namespace Fab.Models.TetbiqSaheleriFolder
{
    public class ApplicationFieldTranslate:BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public int ApplicationFieldId { get; set; }
        public ApplicationField ApplicationField { get; set; }
    }
}
