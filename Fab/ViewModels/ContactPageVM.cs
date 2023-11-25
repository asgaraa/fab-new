using Fab.Models.ContactFolder;

namespace Fab.ViewModels
{
    public class ContactPageVM
    {
        public string LangCode { get; set; }
        public ContactInformations Informations { get; set; }
        public Contact Contact { get; set; }
    }
}
