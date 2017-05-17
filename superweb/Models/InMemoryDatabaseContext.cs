using Microsoft.EntityFrameworkCore;

namespace superweb.Models.InMemoryDatabase {
	public class InMemoryDatabaseContext: DbContext {
		public InMemoryDatabaseContext(DbContextOptions<InMemoryDatabaseContext> options): base(options) {}
		/** construct a query to database */
		public DbSet<TodoItem> TodoItems { get; set; }

		// Configuration can be placed here
		// or in Startup, using dependency injection.
		// protected override void OnConfiguring(DbContextOptionsBuilder optiosBuilder)
		// {
		// 	optiosBuilder.UseInMemoryDatabase();
		// }
	}
}
