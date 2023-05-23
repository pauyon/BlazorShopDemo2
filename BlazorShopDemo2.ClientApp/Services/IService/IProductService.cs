﻿using BlazorShopDemo2.Models;

namespace BlazorShopDemo2.ClientApp.Services.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAll();

        public Task<ProductDto> Get(int productId);
    }
}