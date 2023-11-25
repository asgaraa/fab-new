namespace Fab.ViewModels.ProductVM
{
    public class GetallProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int? ApplicationId { get; set; }
        public int? AppearanceId { get; set; }
        public int? CharacteristicsId { get; set; }
        public string ApplicationName { get; set; }
        public string AppearanceName { get; set; }
        public string CharacteristicsName { get; set; }
        public string Name { get; set; }
        public string? Size { get; set; }
        public string Image { get; set; }

        //public List<GetallCategory>? Categories { get; set; }
    }
}
