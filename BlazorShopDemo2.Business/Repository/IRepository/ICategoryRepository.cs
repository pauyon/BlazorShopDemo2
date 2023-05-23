using BlazorShopDemo2.Domain.Models;

namespace BlazorShopDemo2.Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDto> Create(CategoryDto objDto);

        public Task<CategoryDto> Update(CategoryDto objDto);

        public Task<int> Delete(int id);

        public Task<CategoryDto> Get(int id);

        public Task<IEnumerable<CategoryDto>> GetAll();
    }
}