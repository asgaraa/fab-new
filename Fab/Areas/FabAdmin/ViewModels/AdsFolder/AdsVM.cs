using System.ComponentModel.DataAnnotations.Schema;

namespace FabAdmin.ViewModels.AdsFolder
{
    public class AdsVM
    {
        public IFormFile ImageFile { get; set; }
        public string Link { get; set; }
    }
}
