using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly DBContext _context;

        public TasksController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.OrderBy(t => t.DueDate).ToListAsync();
            return Ok(tasks);
        }

        // POST: api/Tasks
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> CreateTask([FromBody] TaskManager.Models.Task newTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = newTask.TaskId }, newTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> UpdateTask(int id, [FromBody] TaskManager.Models.Task updatedTask)
        {
            if (id != updatedTask.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(updatedTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(t => t.TaskId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
