using BlazorShopDemo2.Domain.Models;

namespace BlazorShopDemo2.ClientApp.Pages
{
    public partial class Index
    {
        private IEnumerable<ProductDto> _products = new List<ProductDto>();

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            _products = await ProductService.GetAll();
            IsLoading = false;
        }
    }
}