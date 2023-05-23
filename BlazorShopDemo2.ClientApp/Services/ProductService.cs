using BlazorShopDemo2.ClientApp.Services.IService;
using BlazorShopDemo2.Models;
using Newtonsoft.Json;

namespace BlazorShopDemo2.ClientApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string _baseServerUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ProductDto> Get(int id)
        {
            var response = await _httpClient.GetAsync($"api/product/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(content);
                product.ImageUrl = GenerateImageUrl(product.ImageUrl);

                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDto>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("api/product");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);

                foreach (var product in products)
                {
                    product.ImageUrl = GenerateImageUrl(product.ImageUrl);
                }

                return products;
            }

            return new List<ProductDto>();
        }

        private string GenerateImageUrl(string imageUrl)
        {
            var uriBuilder = new UriBuilder(_baseServerUrl);
            uriBuilder.Path = imageUrl;

            return uriBuilder.Uri.ToString();
        }
    }
}