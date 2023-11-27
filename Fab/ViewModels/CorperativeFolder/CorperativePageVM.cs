namespace Fab.ViewModels.CorperativeFolder
{
    public class CorperativePageVM
    {
        public string LangCode { get; set; }
        public List<Fab.Models.CorporateFolder.Corporate> Corporates { get; set; }
        public string MyProperty { get; set; }
        public string Fullname { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public IFormFile File { get; set; }
    }
}
