using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Services
{
    public class TodoService
    {
        private List<TodoItem> _todos = new List<TodoItem>();
        private long _nextId = 1;

        public IEnumerable<TodoItem> GetAll() => _todos;

        public TodoItem GetById(long id) => _todos.Find(t => t.Id == id);

        public TodoItem Add(TodoItem item)
        {
            item.Id = _nextId++;
            _todos.Add(item);
            return item;
        }

        public bool Update(long id, TodoItem updatedItem)
        {
            var todo = GetById(id);
            if (todo == null) return false;

            todo.Name = updatedItem.Name;
            todo.IsComplete = updatedItem.IsComplete;
            return true;
        }

        public bool Delete(long id)
        {
            var todo = GetById(id);
            if (todo == null) return false;

            _todos.Remove(todo);
            return true;
        }
    }
}
