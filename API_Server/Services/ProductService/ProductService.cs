using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetProduct(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<int> AddProduct(ProductCreateDto product)
        {
            Product newProduct = new Product(product);
            _context.Products.Add(newProduct);

            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int?> UpdateProduct(int id, ProductCreateDto newProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            product.Name = newProduct.Name;
            product.Images = newProduct.Images;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;

            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int?> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);

            var result = await _context.SaveChangesAsync();
            return result;
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                                 .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) || p.Description.ToLower().Contains(searchText.ToLower()))
                                 .ToListAsync();
        }

        public async Task<ProductSearchDto> SearchProducts(string searchText, int page)
        {
            ProductSearchDto result = new ProductSearchDto();

            result.CurrentPage = page;

            var pageResults = 2f;
            result.Pages = (int)Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);

            result.Products = await _context.Products
                                            .Where(p => p.Name.ToLower().Contains(searchText.ToLower())
                                                   || p.Description.ToLower().Contains(searchText.ToLower()))
                                            .Skip((page - 1) * (int)pageResults)
                                            .Take((int)pageResults)
                                            .ToListAsync();

            return result;
        }
    }
}
