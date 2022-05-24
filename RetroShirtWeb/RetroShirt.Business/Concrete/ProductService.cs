using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using RetroShirt.Business.Abstract;
using RetroShirtDAL.Repositories;
using RetroShirtDtos.Requests;
using RetroShirtDtos.Responses;
using RetroShirtEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroShirt.Business.Concrete
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepostiry;
        private IMapper mapper;
        private IMemoryCache cache;

        public ProductService(IProductRepository productRepository,IMapper mapper,IMemoryCache cache)
        {
            this.productRepostiry = productRepository;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<int> AddProduct(ProductAddingRequest productAdding)
        {
            var addProduct= mapper.Map<Product>(productAdding);
            return await productRepostiry.Add(addProduct);

        }

        public async Task DeleteProduct(int id)
        {
            await productRepostiry.Delete(id);

        }

        public async Task<IList<ProductListResponse>> GetEntitiesWithActiveStatus()
        {
            var productWithoutMapping = await productRepostiry.GetProductsWithActiveStatus();
            var mapProduct= mapper.Map<List<ProductListResponse>>(productWithoutMapping);
            return mapProduct;
        }

        public async Task<ProductListResponse> GetProductById(int id)
        {
            var productWithoutMapping = await productRepostiry.GetEntityById(id);
            var mappedProduct = mapper.Map<ProductListResponse>(productWithoutMapping);

            return mappedProduct;
        }

        public async Task<IList<ProductListResponse>> GetProducts()
        {
            object cachedItems;
            if (!cache.TryGetValue("ProductsCache",out cachedItems))
            {
                var products = await productRepostiry.GetAllEntities();
                cachedItems = mapper.Map<List<ProductListResponse>>(products);
                var cacheOptions = new MemoryCacheEntryOptions()
                                   .SetSlidingExpiration(TimeSpan.FromSeconds(15)); //istek geldikçe süre refreshlenir.
                cache.Set("ProductsCache", cachedItems, cacheOptions);
            }                          
            return (IList<ProductListResponse>)cachedItems;
        }

        public async Task<IList<Product>> GetProductsForDCache()
        {
            var productsForCache = await productRepostiry.GetAllEntities();
            return productsForCache;
        }

        public async Task<bool> ProductIsExist(Product product)
        {           

            return await productRepostiry.IsExist(product);
        }

        public async Task<bool> ProductIsExistWithId(int id)
        {
            return await productRepostiry.ProductIsExist(id);
        }

        public async Task RemoveProductCompletely(int id)
        {
            await productRepostiry.RemoveProductCompletely(id);
        }

        public async Task<IList<ProductListResponse>> SearcyByName(string searchValue)
        {
            var products = await productRepostiry.SearchProducts(searchValue);
            var convertProducts = mapper.Map<List<ProductListResponse>>(products);
            return convertProducts;
        }

        public async Task<int> UpdateProduct(ProductUpdateRequest updateRequest)
        {
            var convertProduct = mapper.Map<Product>(updateRequest);
            var result= await productRepostiry.Update(convertProduct);
            return result;

        }

        
    }
}
