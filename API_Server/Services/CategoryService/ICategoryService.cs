using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task<int?> PutCategory(int id, CategoryDto updateCategory);
        Task<int?> PostCategory(CategoryDto newCategory);
        Task<int?> DeleteCategory(int id);
        Task<List<Product>> GetProductsByCategoryId(int id);
    }
}
