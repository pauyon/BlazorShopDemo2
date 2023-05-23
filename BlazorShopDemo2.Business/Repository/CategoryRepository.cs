using AutoMapper;
using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.DataAccess.Data;
using BlazorShopDemo2.Domain.Entities;
using BlazorShopDemo2.Domain.Models;

namespace BlazorShopDemo2.Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Create(CategoryDto objDto)
        {
            var obj = _mapper.Map<CategoryDto, Category>(objDto);
            obj.CreatedDate = DateTime.Now;

            var addedObj = _context.Categories.Add(obj);
            _context.SaveChanges();

            return _mapper.Map<Category, CategoryDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                _context.Categories.Remove(obj);
                return _context.SaveChanges();
            }

            return 0;
        }

        public async Task<CategoryDto> Get(int id)
        {
            var obj = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                return _mapper.Map<Category, CategoryDto>(obj);
            }

            return new CategoryDto();
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_context.Categories);
        }

        public async Task<CategoryDto> Update(CategoryDto objDto)
        {
            var objFromDb = _context.Categories.FirstOrDefault(x => x.Id == objDto.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = objDto.Name;
                _context.Categories.Update(objFromDb);
                _context.SaveChanges();

                return _mapper.Map<Category, CategoryDto>(objFromDb);
            }

            return objDto;
        }
    }
}