using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.Domain.Models;
using BlazorShopDemo2.ServerApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.ObjectModel;

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

        private ProductDto _product { get; set; } = new();

        private ProductPriceDto _backupProductPrice { get; set; } = new();

        private ObservableCollection<ProductPriceDto> _productPrices { get; set; } = new();

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
            _backupProductPrice = new ProductPriceDto
            {
                Size = item.Size,
                Price = item.Price,
            };
        }

        private async Task CanceledEditingItem(ProductPriceDto item)
        {
            if (item.Id == 0)
            {
                _productPrices.Remove(item);
                await LoadProductPrices();
            }
            else
            {
                item.Size = _backupProductPrice.Size;
                item.Price = _backupProductPrice.Price;
                _backupProductPrice = new();
            }
        }

        private async Task CommittedItemChanges(ProductPriceDto item)
        {
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
            var newItem = new ProductPriceDto
            {
                Price = 0.0m,
                Size = string.Empty,
                ProductId = _product.Id,
            };

            _productPrices.Add(newItem);
            _dataGrid.SetEditingItemAsync(newItem);
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