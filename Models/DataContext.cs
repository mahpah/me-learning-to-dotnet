using Microsoft.EntityFrameworkCore;

namespace superweb.Models {
	public class DataContext: DbContext {
		public DataContext(DbContextOptions<DataContext> options): base(options) {}
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
