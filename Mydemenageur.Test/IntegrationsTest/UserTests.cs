using Xunit;
using Mydemenageur.API.Services;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Mydemenageur.API;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http.Headers;

namespace Mydemenageur.Test
{
    public class UserTests
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly AuthenticationTestService _fakeAuth;

        public UserTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetUserTest()
        {
            // Act
            var response = await _client.GetAsync("/api/Users/c02da7e40a2ec30b5e60dd89");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task PutUserTest()
        {
            //Arrange
            var stringContent = new StringContent(
                    "{ \"profilePicture\": \"string\",\"firstName\": \"Victor\",\"lastName\": \"DENIS\",\"email\": \"admin@feldrise.com\",\"phone\": \" + 33652809335\",\"username\": \"Feldrise\",\"about\": \"string\"}"
                );

            // Act
            var response = await _client.PutAsync("/api/Users/c02da7e40a2ec30b5e60dd89", stringContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }
    }
}
