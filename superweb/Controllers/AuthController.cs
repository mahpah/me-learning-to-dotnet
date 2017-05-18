using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace superweb.Controllers
{
	// Grant resource owner
    [Route("auth")]
	public class AuthController: Controller
	{
		[HttpPost]
		public async Task<IActionResult> PostAuth([FromBody] CredentialModel cred)
		{
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var tokenResponse = await CallResourceOwner(cred);

            if (tokenResponse.IsError)
            {
                return Unauthorized();
            }
            return Ok(tokenResponse.Json);
		}

		private async Task<TokenResponse> CallResourceOwner(CredentialModel cred)
		{
			var disco = await DiscoveryClient.GetAsync("http://localhost:5500");
			var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
			var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(cred.Username, cred.Password, "superweb");
			return tokenResponse;
		}
	}

}
