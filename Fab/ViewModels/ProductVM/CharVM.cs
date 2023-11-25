namespace Fab.ViewModels.ProductVM
{
    public class CharVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public List<int> TranslateIds { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}

