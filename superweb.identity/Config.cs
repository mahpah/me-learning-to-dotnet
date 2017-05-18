using System.Collections.Generic;
using IdentityServer4.Models;

// TODO: refactor: Read from data source

namespace superweb.identity
{
	public class Config
	{
		public static IEnumerable<ApiResource> GetResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("profile.get", "GetProfile"),
				new ApiResource("profile.update", "UpdateProfile"),
				new ApiResource("email.get", "GetEmail"),
				new ApiResource("unit.add", "CreateUnit")
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			var client = new Client
			{
				ClientId = "Superweb",
				ClientSecrets = {
					new Secret("supreweb".Sha256())
				},
				AllowedScopes = { "profile.get", "email.get" }
			};

			return new List<Client>
			{
				client
			};
		}
	}
}
