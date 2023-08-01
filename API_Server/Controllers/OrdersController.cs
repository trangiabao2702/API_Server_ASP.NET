using API_Server.Data;
using API_Server.Dto;
using API_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Server.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            try
            {
                var listOrder = await _context.Orders
                                              .Include(o => o.OrderDetails)
                                              .ToListAsync();
                return Ok(listOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<OrderDetailDto>>> GetOrderDetail(int id)
        {
            try
            {
                var order = await _context.Orders
                                          .Include(o => o.OrderDetails)
                                          .Include(o => o.Payment)
                                          .FirstOrDefaultAsync(o => o.Id == id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> MakeAnOrder([FromBody] OrderDto orderDetail)
        {
            try
            {
                // Insert Table Order
                var newOder = new Order(orderDetail.UserId);
                _context.Orders.Add(newOder);
                var result1 = await _context.SaveChangesAsync();

                // Insert Table OderDetail
                var result2 = 0;
                if (result1 != 0)
                {
                    foreach (var product in orderDetail.OrderDetails)
                    {
                        var newDetail = new OrderDetail(newOder.Id, product.Product.Id, product.Quantity);

                        _context.OrderDetails.Add(newDetail);
                    }

                    result2 = await _context.SaveChangesAsync();
                }

                return Ok(result1 + result2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/updateStatus")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                {
                    return NotFound();
                }

                order.Status = status;
                var result = await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
