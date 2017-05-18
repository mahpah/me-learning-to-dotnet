using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
				new ApiResource("superweb", "SuperwebApi")
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
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				AllowedScopes = { "superweb", "email.get" }
			};

			var roClient = new Client
			{
				ClientId = "ro.client",
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

				ClientSecrets = {
					new Secret("secret".Sha256())
				},
				AllowedScopes = { "superweb" }
			};

			return new List<Client>
			{
				client,
				roClient
			};
		}

		public static List<TestUser> GetUsers()
		{
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "1",
					Username = "alice",
					Password = "password"
				},
				new TestUser
				{
					SubjectId = "2",
					Username = "bob",
					Password = "password"
				}
			};
		}
	}
}
