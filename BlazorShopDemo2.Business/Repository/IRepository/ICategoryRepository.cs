using BlazorShopDemo2.Models;

namespace BlazorShopDemo2.Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public CategoryDto Create(CategoryDto objDto);

        public CategoryDto Update(CategoryDto objDto);

        public int Delete(int id);

        public CategoryDto Get(int id);

        public IEnumerable<CategoryDto> GetAll();
    }
}