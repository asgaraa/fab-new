using Fab.Models.CVFolder;
using Fab.Models.HumanResources;
using Fab.Models.VacanciesFolder;

namespace Fab.ViewModels
{
    public class HumanResourcesPageVM
    {
        public string LangCode { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public List<HumanResource> HumanResources { get; set; }
        public string Fullname { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public IFormFile File { get; set; }

    }
}
