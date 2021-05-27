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

namespace Mydemenageur.Test
{
    public class DocumentationTests
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public DocumentationTests()
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
        public async Task GetDocumentationTest()
        {
            // Act
            var response = await _client.GetAsync("/documentation/index.html");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }
    }
}
