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
            return await _context.ToDoItems.ToListAsync();
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
                Item = item
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

            return NoContent();
        }

        private bool ToDoItemExists(Guid id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
