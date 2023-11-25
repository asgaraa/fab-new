using Fab.Models.BannersFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace FabAdmin.ViewModels.BannersFolder
{
    public class BannerVM
    {
        public IFormFile ImageFile { get; set; }
        public List<BannerTranslateVM> Translates { get; set; }
    }
}
