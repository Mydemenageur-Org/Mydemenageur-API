using System;
using Xunit;
using Mydemenageur.API.Controllers;
using Mydemenageur.API.Services;
using Mydemenageur.API.Services.Interfaces;
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
    public class SocietyTests
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SocietyTests()
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
        public async Task GetSocietyTest()
        {
            // Act
            var response = await _client.GetAsync("/api/Clients/93a8ac56bd2d9d2a13995f9b");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task PostSocietyTest()
        {
            //Arrange



            // Act

            var response = await _client.GetAsync("/api/Clients/93a8ac56bd2d9d2a13995f9b/user");
            response.EnsureSuccessStatusCode();

            // Assert

            Assert.True(true);
        }

        [Fact]
        public async Task PutSocietyTest()
        {
            //Arrange

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "99917ff8839ee8e47c4834e226c653611711e7a32ad9519bd69b42982d10b980");

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
