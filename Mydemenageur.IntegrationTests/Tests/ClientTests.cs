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
using Newtonsoft.Json;
using Mydemenageur.API.Models.Clients;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class ClientTests : TestBase
    {
        public ClientTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Clients/93a8ac56bd2d9d2a13995f9b", "c02da7e40a2ec30b5e60dd89", Roles.Client)]
        public async Task Get_Client(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Clients/93a8ac56bd8579d2a13995f9b", "c02da7e40a2ec30b5e60dd89",Roles.Client)]
        public async Task Get_Client_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);


            Assert.True(response.StatusCode != HttpStatusCode.OK);
            
        }

        [Theory]
        [InlineData("/api/Clients/93a8ac56bd2d9d2a13995f9b/user", "c02da7e40a2ec30b5e60dd89", Roles.Client)]
        public async Task Get_ClientUser(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Clients/60c086c25582a5790af86b3f/user", "c02da7e40a2ec30b5e60dd89", Roles.Client)]
        public async Task Get_ClientUser_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Clients", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Post_Client(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var clientRegis = new ClientRegisterModel
            {
                UserId = "60b6064ff2f2711ff6e96e13"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(clientRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Clients", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Post_Client_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var clientRegis = new ClientRegisterModel
            {
                UserId = "61b6064ff2f2711ff6e96e13"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(clientRegis), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Clients/45d5ae0ad9221e701ceeba5b", "addc792a46a7f43619201a5b", Roles.Client)]
        public async Task Put_Client(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var clientUpd = new ClientUpdateModel
            {
                Address = "26 Rue les cerisiés",
                Town = "Rouen",
                Zipcode = "76000",
                Country = "France"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(clientUpd), Encoding.UTF8, "application/json"));

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Clients/85d5ae0ad9221e701ceeba5b", "addc792a46a7f43619201a5b", Roles.Client)]
        public async Task Put_Client_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var clientUpd = new ClientUpdateModel
            {
                Address = "26 Rue les cerisiés",
                Town = "Rouen",
                Zipcode = "76000",
                Country = "France"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(clientUpd), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }
    }
}
