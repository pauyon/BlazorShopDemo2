using BlazorShopDemo2.Business.Repository;
using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.Domain.Models;
using BlazorShopDemo2.ServerApp.Services;
using BlazorShopDemo2.ServerApp.Services.IService;
using BlazorShopDemo2.ServerApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.ObjectModel;
using static MudBlazor.CategoryTypes;

namespace BlazorShopDemo2.ServerApp.Pages.Product
{
    public partial class ProductPrice
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductRepository ProductRepository { get; set; }

        [Inject]
        public IProductPriceRepository ProductPriceRepository { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ISnackbar SnackbarService { get; set; }

        private MudDataGrid<ProductPriceDto> _dataGrid = new();

        private bool _isLoading = true;
        private bool _canCancelEdit = true;

        private ProductDto _product { get; set; } = new();

        private ObservableCollection<ProductPriceDto> _productPrices { get; set; } = new();

        private List<string> _events = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadProductPrices();
            }
        }

        private async Task LoadProductPrices()
        {
            _isLoading = true;
            StateHasChanged();
            _product = await ProductRepository.Get(Id);
            var items = await ProductPriceRepository.GetAll(_product.Id);
            _productPrices = new();
            items.ToList().ForEach(x => _productPrices.Add(x));
            _isLoading = false;
            StateHasChanged();
        }

        private void StartedEditingItem(ProductPriceDto item)
        {
            _events.Insert(0, $"Event = StartedEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        private async Task CanceledEditingItem(ProductPriceDto item)
        {
            if (item.Id == 0)
            {
                _productPrices.Remove(item);
                await LoadProductPrices();
            }
        }

        private async Task CommittedItemChanges(ProductPriceDto item)
        {
            _events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");

            if (item.Id == 0)
            {
                await ProductPriceRepository.Create(item);
                SnackbarService.Add("Product price was added", Severity.Success);
            }
            else
            {
                await ProductPriceRepository.Update(item);
                SnackbarService.Add("Product price was updated", Severity.Success);
            }

            await LoadProductPrices();
        }

        private void AddItem()
        {
            _canCancelEdit = false;

            var newItem = new ProductPriceDto
            {
                Price = 0.0,
                Size = string.Empty,
                ProductId = _product.Id,
            };

            _productPrices.Add(newItem);
            _dataGrid.SetEditingItemAsync(newItem);

            _canCancelEdit = true;
        }

        private async Task OpenDeleteDialog(ProductPriceDto productPrice)
        {
            var parameters = new DialogParameters
        {
            { "ContentText", $"Do you really want to delete this price? This process cannot be undone." },
            { "ButtonText", "Yes" },
            { "Color", Color.Error },
        };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = await DialogService.ShowAsync<DeleteConfirmation>("Delete", parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await ProductPriceRepository.Delete(productPrice.Id);
                await LoadProductPrices();
            }
        }
    }
}