using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Server.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProduct(int id);
        Task<int> AddProduct(ProductCreateDto product);
        Task<int?> UpdateProduct(int id, ProductCreateDto newProduct);
        Task<int?> DeleteProduct(int id);
        Task<ProductSearchDto> SearchProducts(string searchText, int page);
    }
}
