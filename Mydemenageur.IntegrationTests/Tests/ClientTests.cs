using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Mydemenageur.API;
using Mydemenageur.IntegrationTests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Mydemenageur.API.Entities;
using System.Dynamic;
using System.Net;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class ClientTests : TestBase
    {
        public ClientTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Clients/93a8ac56bd2d9d2a13995f9b")]
        public async Task Get_Client(string url)
        {
            dynamic data = new ExpandoObject();
            data.sub = Guid.NewGuid();
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }
    }
}
