using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using superweb.Models;

namespace superweb.Controllers
{
	[Route("api/[controller]")]
	public class TodoController
	{
		private readonly ITodoRepository _todoRepository;

		public TodoController(ITodoRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

		[HttpGet]
		public IEnumerable<TodoItem> GetAll()
		{
			return _todoRepository.GetAll();
		}

		[HttpGet("{id}", Name = "GetTodo")]
		public IActionResult GetById(long id)
		{
			var todoItem = _todoRepository.Find(id);
			if (todoItem == null) {
				return new NotFoundResult();
			}

			return new ObjectResult(todoItem);
		}

		[HttpPost]
		public IActionResult Create([FromBody] TodoItem item)
		{
			if (item == null)
			{
				return new BadRequestResult();
			}

			_todoRepository.Add(item);

			return new CreatedAtRouteResult("GetTodo", new { id = item.Key }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] TodoItem item)
		{
			if (item == null || item.Key != id)
			{
				return new BadRequestResult();
			}

			var todo = _todoRepository.Find(id);
			if (todo == null)
			{
				return new NotFoundResult();
			}

			todo.IsComplete = item.IsComplete;
			todo.Name = item.Name;
			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id) {
			var todo = _todoRepository.Find(id);
			if (todo == null)
			{
				return new NotFoundResult();
			}

			_todoRepository.Remove(id);
			return new NoContentResult();
		}
	}
}
