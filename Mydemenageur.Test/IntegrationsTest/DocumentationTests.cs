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
using Microsoft.AspNetCore.Mvc.Testing;

namespace Mydemenageur.Test
{
    public class DocumentationTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public DocumentationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetDocumentationTest()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/documentation/index.html");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }
    }
}
