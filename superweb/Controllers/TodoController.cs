using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using superweb.Models;

namespace superweb.Controllers
{
	[Route("api/[controller]")]
	public class TodoController: Controller
	{
		private readonly ITodoRepository _todoRepository;

		public TodoController(ITodoRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(_todoRepository.GetAll());
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
			if(!ModelState.IsValid) {
				return BadRequest( ModelState );
			}
			if (item == null)
			{
				return BadRequest();
			}

			_todoRepository.Add(item);

			return new CreatedAtRouteResult("GetTodo", new { id = item.Key }, item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] TodoItem item)
		{
			if (item == null || item.Key != id)
			{
				return BadRequest();
			}

			var todo = _todoRepository.Find(id);
			if (todo == null)
			{
				return NotFound();
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
				return NotFound();
			}

			_todoRepository.Remove(id);
			return NoContent();
		}
	}
}
