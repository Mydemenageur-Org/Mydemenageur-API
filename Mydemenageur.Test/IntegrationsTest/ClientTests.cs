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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Mydemenageur.Test.Utils;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Mydemenageur.Test
{
    public class ClientTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public ClientTests(WebApplicationFactory<Startup> factory)
        {
            // Arrange
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            });
        }

        [Fact]
        public async Task GetClientTest()
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync("/api/Clients/93a8ac56bd2d9d2a13995f9b");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }
/*
        [Fact]
        public async Task GetUserFromClientTest()
        {
            // Act
            var response = await _client.GetAsync("/api/Clients/93a8ac56bd2d9d2a13995f9b/user");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task PostClientTest()
        {
            //Arrange



            // Act

            var response = await _client.GetAsync("/api/Clients/93a8ac56bd2d9d2a13995f9b/user");
            response.EnsureSuccessStatusCode();

            // Assert

            Assert.True(true);
        }

        [Fact]
        public async Task PutClientTest()
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
        }*/
    }
}
