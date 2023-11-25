namespace Fab.Models.VacanciesFolder
{
    public class VacancyTranslate : BaseEntity
    {

        public string Desc { get; set; }
        public string Position { get; set; }
        public string LangCode { get; set; }
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }
    }
}
