using Microsoft.EntityFrameworkCore;

namespace superweb.Models.Postgres
{
	public class PostgresDatabaseContext : DbContext
	{
		public PostgresDatabaseContext(DbContextOptions options): base(options)
		{

		}

		public DbSet<TodoItem> TodoItems { get; set; }
	}
}
