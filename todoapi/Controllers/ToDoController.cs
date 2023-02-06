using Microsoft.AspNetCore.Mvc;
using todoapi.Models;
using Microsoft.EntityFrameworkCore;
using todoapi.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace todoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMemoryCache _memoryCache;


        public TodoController(IToDoRepository toDoRepository, IMemoryCache memoryCache)
        {
            _toDoRepository = toDoRepository;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IEnumerable<TbTodo>> Get()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.todos, out IEnumerable<TbTodo> TbTodos))
            {
                TbTodos = await _toDoRepository.GetAll(); 
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _memoryCache.Set(CacheKeys.todos, TbTodos, cacheEntryOptions);
            }
            return TbTodos;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();
            var todo = await _toDoRepository.GetById(id);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Post(todoapi.Models.TbTodo todo)
        {
            await _toDoRepository.Insert(todo);
            _memoryCache.Remove(CacheKeys.todos);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, todoapi.Models.TbTodo todoData)
        {
            if (todoData == null || id == 0)
                return BadRequest();

            await _toDoRepository.Update(id, todoData);
            _memoryCache.Remove(CacheKeys.todos);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            await _toDoRepository.Delete(id);
            _memoryCache.Remove(CacheKeys.todos);
            return Ok();
        }
    }
    public static class CacheKeys
    {
        public static string todos => "_todos";
    }
}
