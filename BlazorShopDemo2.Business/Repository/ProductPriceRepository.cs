using AutoMapper;
using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.DataAccess.Data;
using BlazorShopDemo2.Domain.Entities;
using BlazorShopDemo2.Domain.Models;

namespace BlazorShopDemo2.Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductPriceRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductPriceDto> Create(ProductPriceDto objDto)
        {
            var obj = _mapper.Map<ProductPriceDto, ProductPrice>(objDto);

            var addedObj = _context.ProductPrices.Add(obj);
            _context.SaveChanges();

            return _mapper.Map<ProductPrice, ProductPriceDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = _context.ProductPrices.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                _context.ProductPrices.Remove(obj);
                return _context.SaveChanges();
            }

            return 0;
        }

        public async Task<ProductPriceDto> Get(int id)
        {
            var obj = _context.ProductPrices.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDto>(obj);
            }

            return new ProductPriceDto();
        }

        public async Task<IEnumerable<ProductPriceDto>> GetAll(int? id)
        {
            if (id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDto>>
                    (_context.ProductPrices.Where(x => x.ProductId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDto>>(_context.ProductPrices);
            }
        }

        public async Task<ProductPriceDto> Update(ProductPriceDto objDto)
        {
            var objFromDb = _context.ProductPrices.FirstOrDefault(x => x.Id == objDto.Id);

            if (objFromDb != null)
            {
                objFromDb.Price = objDto.Price;
                objFromDb.Size = objDto.Size;
                _context.ProductPrices.Update(objFromDb);
                _context.SaveChanges();

                return _mapper.Map<ProductPrice, ProductPriceDto>(objFromDb);
            }

            return objDto;
        }
    }
}