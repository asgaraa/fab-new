using Fab.Areas.FabAdmin.Helpers;

namespace Fab.ViewModels.ProductVM
{
    public class GetAllVM
    {
        public GetallCategory Categories { get; set; }
        public PaginatedList<GetallProduct> Products { get; set; }
        public string? LangCode { get; set; }

    }
}
