using Microsoft.EntityFrameworkCore;
using todoapi.Models;

namespace todoapi.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TododbContext _context;

        public ToDoRepository(TododbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TbTodo>> GetAll()
        {
            return await _context.TbTodos.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<TbTodo?> GetById(int? todoId)
        {
            return await _context.TbTodos.FirstOrDefaultAsync(m => m.Id == todoId);
        }

        public async Task<int> Insert(TbTodo todo)
        {
            _context.TbTodos.Add(todo);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(int id, todoapi.Models.TbTodo todoData)
        {
            var todo = _context.TbTodos.Find(id);
            todo.Title = todoData.Title;
            todo.Description = todoData.Description;
            todo.IsActive = todoData.IsActive;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int todoID)
        {
            var todo = _context.TbTodos.Find(todoID);
            _context.TbTodos.Remove(todo);
            return await _context.SaveChangesAsync();
        }
    }
}
