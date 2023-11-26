namespace Fab.ViewModels.Blog
{
    public class PressesVM
    {
        public string LangCode { get; set; }
        public List<Fab.Models.PressFolder.Press> Presses { get; set; }
        public Fab.Models.PressFolder.Press Press { get; set; }
    }
}
