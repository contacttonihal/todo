using System.Collections.Generic;
using todoapi.Models;

namespace todoapi.Repository
{
    public interface IToDoRepository
    {
        Task<IEnumerable<TbTodo>> GetAll();
        Task<TbTodo?> GetById(int? todoId);
        Task<int> Insert(TbTodo todo);
        Task<int> Update(int id, todoapi.Models.TbTodo todoData);
        Task<int> Delete(int todoID);
    }
}
