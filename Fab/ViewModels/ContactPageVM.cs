using Fab.Models.ContactFolder;

namespace Fab.ViewModels
{
    public class ContactPageVM
    {
        public string LangCode { get; set; }
        public ContactInformations Informations { get; set; }
        public string Fullname { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }
}
