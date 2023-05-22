using BlazorShopDemo2.Models;

namespace BlazorShopDemo2.Business.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDto> Create(ProductDto objDto);

        public Task<ProductDto> Update(ProductDto objDto);

        public Task<int> Delete(int id);

        public Task<ProductDto> Get(int id);

        public Task<IEnumerable<ProductDto>> GetAll();
    }
}