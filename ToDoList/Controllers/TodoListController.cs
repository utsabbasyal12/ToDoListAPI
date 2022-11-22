using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await _context.lists.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> InsertTasks(addTask add)
        {
            var newlist = new todoList()
            {
                Id = Guid.NewGuid(),
                Task = add.Task,
                Description = add.Description,
                IsActive = add.IsActive,
            };
            await _context.lists.AddAsync(newlist);
            await _context.SaveChangesAsync();

            return Ok("Data Inserted");

        }
        [HttpPut]
        public async Task<IActionResult> UpdateTasks(Guid id, editTask edit)
        {
          var task = await _context.lists.FindAsync(id);
            if(task != null)
            {
                task.Task = edit.Task;
                task.Description = edit.Description;
                task.IsActive = edit.IsActive;

                await _context.SaveChangesAsync();
                return Ok("Data Updated");
            }
            return NotFound();
           
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTasks(Guid id)
        {
            var list = await _context.lists.FindAsync(id);

            if (list != null)
            {
                _context.Remove(list);
                await _context.SaveChangesAsync();

                return Ok("Data Deleted");
            }
            return NotFound();
        }

    }
}
