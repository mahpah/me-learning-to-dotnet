using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace superweb.Models {
	public class TodoItem {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Key { get; set; }
		[Required]
		public string Name { get; set; }
		public bool IsComplete { get; set; }
	}
}
