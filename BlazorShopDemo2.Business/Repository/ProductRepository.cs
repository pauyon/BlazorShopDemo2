using AutoMapper;
using BlazorShopDemo2.Business.Repository.IRepository;
using BlazorShopDemo2.DataAccess.Data;
using BlazorShopDemo2.DataAccess;
using BlazorShopDemo2.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopDemo2.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(ProductDto objDto)
        {
            var obj = _mapper.Map<ProductDto, Product>(objDto);

            var addedObj = _db.Products.Add(obj);
            try
            {
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException;
            }

            return _mapper.Map<Product, ProductDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = _db.Products.FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                _db.Products.Remove(obj);
                return _db.SaveChanges();
            }

            return 0;
        }

        public async Task<ProductDto> Get(int id)
        {
            var obj = _db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

            if (obj != null)
            {
                return _mapper.Map<Product, ProductDto>(obj);
            }

            return new ProductDto();
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_db.Products.Include(x => x.Category));
        }

        public async Task<ProductDto> Update(ProductDto objDto)
        {
            var objFromDb = _db.Products.FirstOrDefault(x => x.Id == objDto.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = objDto.Name;
                objFromDb.Description = objDto.Description;
                objFromDb.ImageUrl = objDto.ImageUrl;
                objFromDb.CategoryId = objDto.CategoryId;
                objFromDb.Color = objDto.Color;
                objFromDb.ShopFavorites = objDto.ShopFavorites;
                objFromDb.CustomerFavorites = objDto.CustomerFavorites;

                _db.Products.Update(objFromDb);
                _db.SaveChanges();

                return _mapper.Map<Product, ProductDto>(objFromDb);
            }

            return objDto;
        }
    }
}