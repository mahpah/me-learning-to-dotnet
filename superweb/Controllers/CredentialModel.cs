using System.ComponentModel.DataAnnotations;

namespace superweb.Controllers
{
	public class CredentialModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
