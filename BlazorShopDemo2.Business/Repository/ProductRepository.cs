using AutoMapper;
using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.DataAccess.Data;
using BlazorShopDemo2.Domain.Entities;
using BlazorShopDemo2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopDemo2.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(ProductDto objDto)
        {
            var obj = _mapper.Map<ProductDto, Product>(objDto);

            var addedObj = _context.Products.Add(obj);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException;
            }

            return _mapper.Map<Product, ProductDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = _context.Products.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                _context.Products.Remove(obj);
                return _context.SaveChanges();
            }

            return 0;
        }

        public async Task<ProductDto> Get(int id)
        {
            var obj = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                return _mapper.Map<Product, ProductDto>(obj);
            }

            return new ProductDto();
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_context.Products.Include(x => x.Category));
        }

        public async Task<ProductDto> Update(ProductDto objDto)
        {
            var objFromDb = _context.Products.FirstOrDefault(x => x.Id == objDto.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = objDto.Name;
                objFromDb.Description = objDto.Description;
                objFromDb.ImageUrl = objDto.ImageUrl;
                objFromDb.CategoryId = objDto.CategoryId;
                objFromDb.Color = objDto.Color;
                objFromDb.ShopFavorites = objDto.ShopFavorites;
                objFromDb.CustomerFavorites = objDto.CustomerFavorites;

                _context.Products.Update(objFromDb);
                _context.SaveChanges();

                return _mapper.Map<Product, ProductDto>(objFromDb);
            }

            return objDto;
        }
    }
}