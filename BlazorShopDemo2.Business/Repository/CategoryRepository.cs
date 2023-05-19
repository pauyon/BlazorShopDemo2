using AutoMapper;
using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.DataAccess;
using BlazorShopDemo2.DataAccess.Data;
using BlazorShopDemo2.Models;

namespace BlazorShopDemo2.Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public CategoryDto Create(CategoryDto objDto)
        {
            var obj = _mapper.Map<CategoryDto, Category>(objDto);
            obj.CreatedDate = DateTime.Now;

            var addedObj = _db.Categories.Add(obj);
            _db.SaveChanges();

            return _mapper.Map<Category, CategoryDto>(addedObj.Entity);
        }

        public int Delete(int id)
        {
            var obj = _db.Categories.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                _db.Categories.Remove(obj);
                return _db.SaveChanges();
            }

            return 0;
        }

        public CategoryDto Get(int id)
        {
            var obj = _db.Categories.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                return _mapper.Map<Category, CategoryDto>(obj);
            }

            return new CategoryDto();
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_db.Categories);
        }

        public CategoryDto Update(CategoryDto objDto)
        {
            var objFromDb = _db.Categories.FirstOrDefault(x => x.Id == objDto.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = objDto.Name;
                _db.Categories.Update(objFromDb);
                _db.SaveChanges();

                return _mapper.Map<Category, CategoryDto>(objFromDb);
            }

            return objDto;
        }
    }
}