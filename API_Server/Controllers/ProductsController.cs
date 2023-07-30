using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using API_Server.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Server.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IProductService _productService;

        public ProductsController(DataContext dataContext, IProductService productService)
        {
            _context = dataContext;
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
        

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProduct(id);
                if (product == null)
                {
                    return NotFound("404 Not found - Product doesn't exist!");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto product)
        {
            try
            {
                var result = await _productService.AddProduct(product);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductCreateDto newProduct)
        {
            try
            {
                var product = await _productService.UpdateProduct(id, newProduct);
                if (product == null)
                {
                    return NotFound("404 Not found - Product doesn't exist!");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.DeleteProduct(id);
                if (product == null)
                {
                    return NotFound("404 Not found - Product doesn't exist!");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
