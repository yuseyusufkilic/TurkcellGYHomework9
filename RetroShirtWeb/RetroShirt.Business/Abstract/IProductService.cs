using RetroShirtDtos.Requests;
using RetroShirtDtos.Responses;
using RetroShirtEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroShirt.Business.Abstract
{
    public interface IProductService
    {
        Task<IList<ProductListResponse>> GetProducts();
        Task<IList<Product>> GetProductsForDCache();
        Task<ProductListResponse> GetProductById(int id);
        Task<IList<ProductListResponse>> GetEntitiesWithActiveStatus();
        Task<int> AddProduct(ProductAddingRequest productAdding);
        Task<int> UpdateProduct(ProductUpdateRequest updateRequest);
        Task DeleteProduct(int id);
        Task RemoveProductCompletely(int id);
        Task<bool> ProductIsExist(Product product);
        Task<bool> ProductIsExistWithId(int id);
        Task<IList<ProductListResponse>> SearcyByName(string searchValue);

        
        
    }
}
