using Microsoft.EntityFrameworkCore;

namespace superweb.Models {
	public class TodoContext: DbContext {
		public TodoContext(DbContextOptions<TodoContext> options): base(options) {}
		/** construct a query to database */
		public DbSet<TodoItem> TodoItems { get; set; }
	}
}
