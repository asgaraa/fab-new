using Fab.Models.CategoriesFolder;

namespace Fab.Models.Xususiyyetler
{
    public class Characteristics:BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<CharacteristicsTranslate> Translates { get; set; }
    }
}
