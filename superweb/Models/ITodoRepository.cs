using System.Collections.Generic;
using System.Threading.Tasks;

namespace superweb.Models {
	public interface ITodoRepository {
		Task Add(TodoItem item);
		Task<IEnumerable<TodoItem>> GetAll();
		Task<TodoItem> Find(long key);
		Task Remove(long key);
		Task Update(TodoItem item);
	}
}
