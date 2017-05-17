using System;
using System.Collections.Generic;
using System.Linq;
using superweb.Models.Postgres;

namespace superweb.Models
{
	public class TodoRepository : ITodoRepository
	{
		private readonly PostgresDatabaseContext _context;

		public TodoRepository(PostgresDatabaseContext context)
		{
			_context = context;

			if (_context.TodoItems.Count() == 0)
			{
				Add(new TodoItem { Name = "Item 1" });
			}
		}

		public void Add(TodoItem item)
		{
			_context.TodoItems.Add(item);
			_context.SaveChanges();
		}

		public TodoItem Find(long key)
		{
			return _context.TodoItems.FirstOrDefault(t => t.Key == key);
		}

		public IEnumerable<TodoItem> GetAll()
		{
			return _context.TodoItems.ToList();
		}

		public void Remove(long key)
		{
			var entity = _context.TodoItems.First(item => item.Key == key);
			_context.TodoItems.Remove(entity);
			_context.SaveChanges();
		}

		public void Update(TodoItem item)
		{
			_context.TodoItems.Update(item);
			_context.SaveChanges();
		}
	}
}
