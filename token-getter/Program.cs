using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace token_getter
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "";
            try {
                url = args[0];
            } catch (IndexOutOfRangeException e) {
                url = "http://localhost:5000/api/todo";
            }
            Program.CallResourceOwner(url).Wait();
        }

        static async Task CallResourceOwner(string url) {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5500");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("bob", "password", "superweb");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            var token = tokenResponse.AccessToken;
            await Program.CallApi(token, url);
        }

        static async Task CallClientCred(string url) {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5500");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "Superweb", "supreweb");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("superweb");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            var token = tokenResponse.AccessToken;
            await Program.CallApi(token, url);
        }

        static async Task CallApi(string bearer, string url) {
            var client = new HttpClient();
            client.SetBearerToken(bearer);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine(response.StatusCode);
                return;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
        }
    }
}
