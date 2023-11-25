using System.Reflection.PortableExecutable;

namespace Fab.Models.Xususiyyetler
{
    public class CharacteristicsTranslate:BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public int CharacteristicsId { get; set; }
        public Characteristics Characteristics { get; set; }
    }
}
