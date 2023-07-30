using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context) { 
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public async Task<int?> PostCategory(CategoryDto newCategory)
        {
            _context.Categories.Add(new Category(newCategory));

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int?> PutCategory(int id, CategoryDto updateCategory)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }
            else
            {
                category.Name = updateCategory.Name;
                category.Description = updateCategory.Description;
                category.ModifiedAt = DateTime.Now;

                var result = await _context.SaveChangesAsync();

                return result;
            }
        }

        public async Task<int?> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }

            _context.Categories.Remove(category);

            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
