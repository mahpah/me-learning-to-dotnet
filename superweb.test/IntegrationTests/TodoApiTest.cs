using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using superweb.Models;
using Xunit;

namespace superweb.test.IntegrationTests
{
    public class TodoApiTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public TodoApiTest()
        {
            var ctR = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            _server = new TestServer(new WebHostBuilder()
                .UseContentRoot(ctR)
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetDefault_ShouldReturnListOfTodoItem()
        {
            var response = await _client.GetAsync("/api/todo");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject(responseString);
            Assert.IsType<JArray>(responseData);
        }

        [Fact]
        public async Task Post_ShouldSendError_WhenModelInvalid()
        {
            var data = new {};
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/todo", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldSendCreatedWithLocation_WhenModelValid()
        {
            var data = new TodoItem() {
                Name = "walk the dog"
            };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/todo", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.IsType<Uri>(response.Headers.Location);
        }
    }
}
