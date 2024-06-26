using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using to_do.Server.Models;

namespace to_do.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoItemsController(ToDoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItem()
        {
            return await _context.ToDoItems.OrderBy(item => item.Priority).ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(Guid id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        
        [HttpPut]
        public async Task<IActionResult> PutToDoItem(ToDoItem toDoItem)
        {
            if (!ToDoItemExists(toDoItem.Id))
            {
                return NotFound();
            }

            _context.Entry(toDoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem([FromBody] string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return BadRequest();
            }

            var newItem = new ToDoItem()
            {
                Id = Guid.NewGuid(),
                Item = item,
                Priority = await GetNextPriority(),

            };
            _context.ToDoItems.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostToDoItem", newItem);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(Guid id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();
            await CorrectToDoItemOrder();
            return NoContent();
        }

        private bool ToDoItemExists(Guid id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }

        private async Task<int> GetNextPriority()
        {
            int itemCount = await _context.ToDoItems.CountAsync();
            return itemCount + 1;
        }

        private async Task CorrectToDoItemOrder()
        {
            var items = await _context.ToDoItems.OrderBy(item => item.Priority).ToListAsync();

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Priority = i + 1;
                _context.Entry(items[i]).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
