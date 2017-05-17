using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using superweb.Models.Postgres;

namespace superweb.Models
{
	public class TodoRepository : ITodoRepository
	{
		private readonly PostgresDatabaseContext _context;

		public TodoRepository(PostgresDatabaseContext context)
		{
			_context = context;
		}

		public async Task Add(TodoItem item)
		{
			await _context.TodoItems.AddAsync(item);
			await _context.SaveChangesAsync();
		}

		public async Task<TodoItem> Find(long key)
		{
			return await _context.TodoItems.FirstOrDefaultAsync(t => t.Key == key);
		}

		public async Task<IEnumerable<TodoItem>> GetAll()
		{
			return await _context.TodoItems.ToListAsync();
		}

		public async Task Remove(long key)
		{
			var entity = _context.TodoItems.First(item => item.Key == key);
			_context.TodoItems.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task Update(TodoItem item)
		{
			_context.TodoItems.Update(item);
			await _context.SaveChangesAsync();
		}
	}
}
