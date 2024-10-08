using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController()
        {
            _todoService = new TodoService();
        }

        // GET: api/todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAllTodos()
        {
            return Ok(_todoService.GetAllTodos());
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoById(long id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public ActionResult<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            var createdTodo = _todoService.AddTodoItem(todoItem);
            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(long id, TodoItem updatedTodo)
        {
            if (!_todoService.UpdateTodoItem(id, updatedTodo))
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            if (!_todoService.DeleteTodoItem(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
