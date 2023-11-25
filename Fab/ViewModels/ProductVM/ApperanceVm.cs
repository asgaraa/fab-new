namespace Fab.ViewModels.ProductVM
{
    public class ApperanceVm
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public List<int> TranslateIds { get; set; }
        public string Name { get; set; }
    }
}
