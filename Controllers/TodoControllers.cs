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

        // Injecting the TodoService singleton
        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAllTodos()
        {
            return Ok(_todoService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoById(long id)
        {
            var todo = _todoService.GetById(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            var createdTodo = _todoService.Add(todoItem);
            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(long id, TodoItem updatedTodo)
        {
            if (!_todoService.Update(id, updatedTodo)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            if (!_todoService.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
