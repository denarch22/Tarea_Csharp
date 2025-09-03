using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstApi.Data;
using MyFirstApi.Models;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TodosController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> GetAll(CancellationToken ct)
        {
            var items = await _db.Todos.AsNoTracking()
                .Select(t => new TodoReadDto { Id = t.Id, Title = t.Title, IsCompleted = t.IsCompleted })
                .ToListAsync(ct);

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoReadDto>> GetById(int id, CancellationToken ct)
        {
            var t = await _db.Todos.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new TodoReadDto { Id = x.Id, Title = x.Title, IsCompleted = x.IsCompleted })
                .FirstOrDefaultAsync(ct);

            return t is null ? NotFound() : Ok(t);
        }

        [HttpPost]
        public async Task<ActionResult<TodoReadDto>> Create([FromBody] TodoCreateDto input, CancellationToken ct)
        {
            var entity = new Todo { Title = input.Title, IsCompleted = false };

            _db.Todos.Add(entity);
            await _db.SaveChangesAsync(ct);

            var dto = new TodoReadDto { Id = entity.Id, Title = entity.Title, IsCompleted = entity.IsCompleted };

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto input, CancellationToken ct)
        {
            var entity = await _db.Todos.FindAsync([id], ct);
            if (entity is null) return NotFound();

            entity.Title = input.Title;
            entity.IsCompleted = input.IsCompleted;

            await _db.SaveChangesAsync(ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _db.Todos.FindAsync([id], ct);
            if (entity is null) return NotFound();

            _db.Todos.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return NoContent();
        }
    }
}
