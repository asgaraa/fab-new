namespace Fab.ViewModels.ProductVM
{
    public class GetCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Getsubcategory> Subcategories { get; set; }
    }
}
