using FabAdmin.ViewModels.Subcategory;

namespace Fab.Areas.FabAdmin.ViewModels.Xususiyyetler
{
    public class CharacteristicsVM
    {
        public int CategoryId { get; set; }
        public List<CharacteristicsTranslateVM> Translates { get; set; }
    }
}
