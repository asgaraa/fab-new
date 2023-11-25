using System.ComponentModel.DataAnnotations.Schema;

namespace Fab.Models.TwinFolder
{
    public class Twin : BaseEntity
    {

        public string WideImage { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile WideImageFile { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<TwinTranslate> Translates { get; set; }

    }
}
