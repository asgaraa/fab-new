namespace Fab.Models.SubcategoryFolder
{
    public class SubcategoryTranslate : BaseEntity
    {
        public string Name { get; set; }
        public string LangCode { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
