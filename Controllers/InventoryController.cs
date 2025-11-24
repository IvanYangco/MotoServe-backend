using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    public class CreateInventoryDto
    {
        public string? Material { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalProfit { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly MotoServeContext _context;

        public InventoryController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: api/inventory
        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            var items = await _context.Inventories
                .Select(i => new
                {
                    id = i.Id,
                    material = i.Material,
                    quantity = i.Quantity,
                    price = i.Price,
                    total_profit = i.TotalProfit
                })
                .ToListAsync();

            return Ok(items);
        }

        // POST: api/inventory
        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var item = new Inventory
            {
                Material = dto.Material,
                Quantity = dto.Quantity,
                Price = dto.Price,
                TotalProfit = dto.TotalProfit
            };

            _context.Inventories.Add(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inventory item created successfully" });
        }

        // PUT: api/inventory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] CreateInventoryDto dto)
        {
            var item = await _context.Inventories.FindAsync(id);

            if (item == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Material)) item.Material = dto.Material;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;
            item.TotalProfit = dto.TotalProfit;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Inventory item updated successfully" });
        }

        // DELETE: api/inventory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var item = await _context.Inventories.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inventory item deleted successfully" });
        }
    }
}
